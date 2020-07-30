using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Nikki.Utils.EA
{
	public class SVGReader : IDisposable
	{
		private class ObservableElement
		{
			public string ID { get; }
			public string FillColor { get; set; }
			public string FillOpacity { get; set; }
			public string StrokeColor { get; set; }
			public string StrokeOpacity { get; set; }
			public string Thickness { get; set; }
			public List<PathPointSet> PointDatas { get; }

			public ObservableElement(string id) { this.ID = id; this.PointDatas = new List<PathPointSet>(); }
		}

		private class PathPointSet
		{
			public List<XYPoint> Points { get; }
			public XYPoint First => this.Points.Count == 0 ? null : this.Points[0];
			public XYPoint Last => this.Points.Count == 0 ? null : this.Points[^1];
			public string LastCurveType { get; set; } = String.Empty;

			public PathPointSet() => this.Points = new List<XYPoint>();
			public PathPointSet(int capacity) => this.Points = new List<XYPoint>(capacity);
		}

		private struct RGBColor
		{
			public byte Red { get; set; }
			public byte Green { get; set; }
			public byte Blue { get; set; }
			
			public RGBColor(byte r, byte g, byte b)
			{
				this.Red = r;
				this.Green = g;
				this.Blue = b;
			}
		}

		private class XYPoint
		{
			public float X { get; set; }
			public float Y { get; set; }
			
			public XYPoint(string x, string y)
			{
				this.X = Single.Parse(x);
				this.Y = Single.Parse(y);
			}
			public XYPoint(string x, float y)
			{
				this.X = Single.Parse(x);
				this.Y = y;
			}
			public XYPoint(float x, string y)
			{
				this.X = x;
				this.Y = Single.Parse(y);
			}
			public XYPoint(float x, float y)
			{
				this.X = x;
				this.Y = y;
			}
			public XYPoint(XYPoint other)
			{
				this.X = other is null ? 0 : other.X;
				this.Y = other is null ? 0 : other.Y;
			}
		}

		private Dictionary<string, ObservableElement> _map;
		private XmlReader _reader;
		private ushort _width;
		private ushort _height;

		public SVGReader(string filename)
		{
			if (!File.Exists(filename))
			{

				throw new FileNotFoundException($"File with path {filename} does not exist");

			}

			var settings = new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse };
			this._reader = XmlReader.Create(filename, settings);
			this._map = new Dictionary<string, ObservableElement>();
		}

		public void ReadAllContents()
		{
			while (this._reader.Read())
			{

				switch (this._reader.NodeType)
				{
					case XmlNodeType.DocumentType:
						this.ValidateSVGorHTML();
						break;

					case XmlNodeType.Element:
						this.ParseSingleElement();
						break;

					default:
						break;

				}

			}

		}

		private void ValidateSVGorHTML()
		{
			if (String.Compare("svg", this._reader.Name, StringComparison.OrdinalIgnoreCase) != 0 &&
				String.Compare("html", this._reader.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{

				throw new Exception("Document type is not supported; supported types are: SVG, HTML");

			}
		}

		private void ParseSingleElement()
		{
			if (String.Compare("path", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this.ParsePathSet();

			}
			else if (String.Compare("svg", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this._width = UInt16.Parse(this._reader.GetAttribute("width"));
				this._height = UInt16.Parse(this._reader.GetAttribute("height"));

			}
			else if (String.Compare("use", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this.ParseUsageSet();

			}
		}

		private void ParsePathSet()
		{
			var id = this._reader.GetAttribute("id");
			var path = this._reader.GetAttribute("d");
			if (id == null || path == null) return;

			if (!this._map.TryGetValue(id, out var element))
			{

				element = new ObservableElement(id);
				this._map.Add(id, element);

			}

			var sets = path.Split(new char[] { 'm', 'M' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var set in sets)
			{


				var points = this.EnsureDelimWhitespace(set).Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (points.Length < 2) continue; // if less than 1 point then curve is invalid
				var list = new PathPointSet(points.Length >> 1);

				// Considering after M/m there should be at least one point
				list.Points.Add(new XYPoint(points[0], points[1]));
				int index = 2;

				while (index < points.Length)
				{

					var obj = points[index++];

					index += obj switch
					{
						"C" => this.ReadCubicCurve(points, index, list),
						"S" => this.ReadSmoothCurve(points, index, list),
						"Q" => this.ReadQuadraticCurve(points, index, list),
						"T" => this.ReadTupledCurve(points, index, list),
						"L" => this.ReadLinearCurve(points, index, list),
						"H" => this.ReadHorizontalCurve(points, index, list),
						"V" => this.ReadVerticalCurve(points, index, list),
						"Z" => this.ReadEnclosion(list),
						_ => this.ReadLinearCurve(points, index, list),
					};

					list.LastCurveType = obj;

				}

				element.PointDatas.Add(list);

			}

			element.FillColor = this._reader.GetAttribute("fill");
			element.StrokeColor = this._reader.GetAttribute("stroke");
			element.FillOpacity = this._reader.GetAttribute("opacity");
			element.StrokeOpacity = this._reader.GetAttribute("stroke-opacity");
			element.Thickness = this._reader.GetAttribute("stroke-width");
		}

		private void ParseUsageSet()
		{
			var id = this._reader.GetAttribute("xlink:href")[1..];
			if (!this._map.TryGetValue(id, out var element)) return;

			element.FillColor = this._reader.GetAttribute("fill");
			element.StrokeColor = this._reader.GetAttribute("stroke");
			element.FillOpacity = this._reader.GetAttribute("opacity");
			element.StrokeOpacity = this._reader.GetAttribute("stroke-opacity");
			element.Thickness = this._reader.GetAttribute("stroke-width");
		}

		private string EnsureDelimWhitespace(string str)
		{
			var builder = new StringBuilder(str.Length + 100);

			foreach (var c in str)
			{
				string n = c switch
				{
					'c' => " C ",
					'C' => " C ",
					's' => " S ",
					'S' => " S ",
					'h' => " H ",
					'H' => " H ",
					'v' => " V ",
					'V' => " V ",
					'l' => " L ",
					'L' => " L ",
					'q' => " Q ",
					'Q' => " Q ",
					't' => " T ",
					'T' => " T ",
					'z' => " Z ",
					'Z' => " Z ",
					_ => c.ToString(),
				};

				builder.Append(n);

			}

			return builder.ToString();
		}

		private int ReadCubicCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 6 > points.Length)
			{

				throw new Exception("Invalid path: when reading cubic curve expected 6 coordinates available");

			}

			set.Points.Add(new XYPoint(points[index], points[index + 1]));
			set.Points.Add(new XYPoint(points[index + 2], points[index + 3]));
			set.Points.Add(new XYPoint(points[index + 4], points[index + 5]));
			return 6;
		}

		private int ReadSmoothCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 4 > points.Length)
			{

				throw new Exception("Invalid path: when reading smooth curve expected 4 coordinates available");

			}

			if (set.LastCurveType == "C" || set.LastCurveType == "S")
			{

				var prev = set.Points[^2];
				set.Points.Add(new XYPoint(set.Last.X * 2 - prev.X, set.Last.Y * 2 - prev.Y));
				set.Points.Add(new XYPoint(points[index], points[index + 1]));
				set.Points.Add(new XYPoint(points[index + 2], points[index + 3]));
				return 4;

			}
			else
			{

				return this.ReadQuadraticCurve(points, index, set);

			}
		}

		private int ReadQuadraticCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 4 > points.Length)
			{

				throw new Exception("Invalid path: when reading quadratic curve expected 4 coordinates available");

			}

			var mid = new XYPoint(points[index], points[index + 1]);
			var end = new XYPoint(points[index + 2], points[index + 3]);
			var c1x = set.Last.X + (mid.X - set.Last.X) * 2 / 3;
			var c1y = set.Last.Y + (mid.Y - set.Last.Y) * 2 / 3;
			var c2x = end.X + (mid.X - end.X) * 2 / 3;
			var c2y = end.Y + (mid.X - end.Y) * 2 / 3;

			set.Points.Add(new XYPoint(c1x, c1y));
			set.Points.Add(new XYPoint(c2x, c2y));
			set.Points.Add(end);
			return 4;
		}

		private int ReadTupledCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 2 > points.Length)
			{

				throw new Exception("Invalid path: when reading tupled curve expected 2 coordinates available");

			}

			if (set.LastCurveType == "Q" || set.LastCurveType == "T")
			{

				var slope1 = set.Points[^3];
				var slope2 = set.Points[^2];
				var c1x = set.Last.X * 2 - slope2.X;
				var c1y = set.Last.Y * 2 - slope2.Y;
				var c2x = set.Last.X * 2 - slope1.X;
				var c2y = set.Last.Y * 2 - slope1.Y;
				set.Points.Add(new XYPoint(c1x, c1y));
				set.Points.Add(new XYPoint(c2x, c2y));
				set.Points.Add(new XYPoint(points[index], points[index + 1]));
				return 2;

			}
			else
			{

				return this.ReadLinearCurve(points, index, set);

			}
		}

		private int ReadLinearCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 2 > points.Length)
			{

				throw new Exception("Invalid path: when reading linear curve expected 2 coordinates available");

			}

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(new XYPoint(points[index], points[index + 1]));
			set.Points.Add(new XYPoint(set.Last));
			return 2;
		}

		private int ReadHorizontalCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 1 > points.Length)
			{

				throw new Exception("Invalid path: when reading horizontal curve expected 1 coordinate available");

			}

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(new XYPoint(points[index], set.Last.Y));
			set.Points.Add(new XYPoint(set.Last));
			return 1;
		}

		private int ReadVerticalCurve(string[] points, int index, PathPointSet set)
		{
			if (index + 1 > points.Length)
			{

				throw new Exception("Invalid path: when reading vertical curve expected 1 coordinate available");

			}

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(new XYPoint(set.Last.X, points[index]));
			set.Points.Add(new XYPoint(set.Last));
			return 1;
		}

		private int ReadEnclosion(PathPointSet set)
		{
			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(new XYPoint(set.First));
			set.Points.Add(new XYPoint(set.First));
			return 0;
		}

		public List<Support.Carbon.Parts.VinylParts.PathSet> GetPathSets()
		{
			var list = new List<Support.Carbon.Parts.VinylParts.PathSet>(this._map.Count);

			foreach (var element in this._map.Values)
			{

				var set = new Support.Carbon.Parts.VinylParts.PathSet();
				

			}

			return list;
		}

		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="SVGReader"/>.
		/// </summary>
		public void Dispose() => this.Dispose(true);

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="SVGReader"/>.
		/// </summary>
		/// <param name="disposing">True if release both managed and unmanaged resources; false 
		/// if release unmanaged only.</param>
		protected void Dispose(bool disposing)
		{
			if (disposing) this._reader.Close();

			var reader = this._reader;
			this._reader = null;
			reader.Dispose();
		}
	}
}
