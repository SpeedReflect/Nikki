using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Nikki.Reflection.Enum;
using Nikki.Support.Shared.Class;



namespace Nikki.Utils.EA
{
	/// <summary>
	/// A class for reading SVG path data and converting to <see cref="VectorVinyl"/> data.
	/// </summary>
	public class SVGReader : IDisposable
	{
		[DebuggerDisplay("ID: [{ID}] PointDatas: [{PointDatas.Count}]")]
		private class ObservableElement
		{
			public string ID { get; }
			public string FillColor { get; set; }
			public string FillOpacity { get; set; } = "1";
			public string StrokeColor { get; set; }
			public string StrokeOpacity { get; set; } = "1";
			public string Thickness { get; set; }
			public List<PathPointSet> PointDatas { get; }
			public float FillSingleOpacity
			{
				get
				{
					try { return Single.Parse(this.FillOpacity); }
					catch { return 1; }
				}
			}
			public float StrokeSingleOpacity
			{
				get
				{
					try { return Single.Parse(this.StrokeOpacity); }
					catch { return 1; }
				}
			}
			public float ThinknessSingle
			{
				get
				{
					try { return Convert.ToSingle(this.Thickness); }
					catch { return 1; }
				}
			}

			public ObservableElement(string id) { this.ID = id; this.PointDatas = new List<PathPointSet>(); }
		}

		[DebuggerDisplay("F ({First.X}, {First.Y}) C [{Points.Count}] L ({Last.X}, {Last.Y})")]
		private class PathPointSet
		{
			public List<XYPoint> Points { get; }
			public XYPoint First => this.Points.Count == 0 ? null : this.Points[0];
			public XYPoint Last => this.Points.Count == 0 ? null : this.Points[^1];
			public string LastCurveType { get; set; } = String.Empty;

			public PathPointSet() => this.Points = new List<XYPoint>();
			public PathPointSet(int capacity) => this.Points = new List<XYPoint>(capacity);
		}

		[DebuggerDisplay("({X}, {Y})")]
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

		[DebuggerDisplay("Relative: {StartRelative} | Path: {Path}")]
		private class PathD
		{
			public string Path { get; set; }
			public bool StartRelative { get; set; }
		}

		private Dictionary<string, ObservableElement> _map;
		private XmlReader _reader;
		private float _width;
		private float _height;
		private Stack<ObservableElement> _groups;

		/// <summary>
		/// Initializes new instance of <see cref="SVGReader"/> using file path provided.
		/// </summary>
		/// <param name="filename">SVG file to read.</param>
		public SVGReader(string filename)
		{
			if (!File.Exists(filename))
			{

				throw new FileNotFoundException($"File with path {filename} does not exist");

			}

			var settings = new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse };
			this._reader = XmlReader.Create(filename, settings);
			this._map = new Dictionary<string, ObservableElement>();
			this._groups = new Stack<ObservableElement>();
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

		/// <summary>
		/// Reads all contents of a file passed during initialization.
		/// </summary>
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

					case XmlNodeType.EndElement:
						this.ParseIsEndGroup();
						break;

					default:
						break;

				}

			}

		}

		/// <summary>
		/// Converts all data read from SVG file that was passed on initialization.
		/// </summary>
		/// <param name="vinyl"><see cref="VectorVinyl"/> to copy data to.</param>
		public void ToVectorVinyl(VectorVinyl vinyl)
		{
			var ratioX = 0x10000 / this._width;
			var ratioY = 0x10000 / this._height;
			var maxres = Math.Max(this._width, this._height);

			foreach (var element in this._map.Values)
			{

				if (element.PointDatas.Count == 0) continue;
				var num = vinyl.NumberOfPaths;
				vinyl.AddPathSet();
				var set = vinyl.GetPathSet(num);
				ushort start = 0;

				foreach (var data in element.PointDatas)
				{

					set.PathDatas.Add(new Support.Shared.Parts.VinylParts.PathData()
					{
						StartIndex = start,
						NumCurves = (ushort)((data.Points.Count - 1) / 3),
					});

					foreach (var point in data.Points)
					{

						var x = point.X * ratioX;
						var y = point.Y * ratioY;

						set.PathPoints.Add(new Support.Shared.Parts.VinylParts.PathPoint()
						{
							X = x > UInt16.MaxValue ? UInt16.MaxValue : (ushort)x,
							Y = y > UInt16.MaxValue ? UInt16.MaxValue : (ushort)y,
						});

					}

					start += (ushort)data.Points.Count;

				}

				var fill = this.FromHTMLColor(element.FillColor);
				var stroke = this.FromHTMLColor(element.StrokeColor);

				set.FillEffect.Red = fill.Item1;
				set.FillEffect.Green = fill.Item2;
				set.FillEffect.Blue = fill.Item3;
				set.StrokeEffect.Red = stroke.Item1;
				set.StrokeEffect.Green = stroke.Item2;
				set.StrokeEffect.Blue = stroke.Item3;

				if (String.Compare("none", element.FillColor, StringComparison.OrdinalIgnoreCase) != 0)
				{
					
					set.FillEffectExists = eBoolean.True;
					set.FillEffect.Alpha = (byte)(element.FillSingleOpacity * 255.0f);

				}

				if (String.Compare("none", element.StrokeColor, StringComparison.OrdinalIgnoreCase) != 0)
				{

					set.StrokeEffectExists = eBoolean.True;
					set.StrokeEffect.Alpha = (byte)(element.StrokeSingleOpacity * 255.0f);
					set.StrokeEffect.Thickness = (float)(element.ThinknessSingle / maxres);

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
			else if (String.Compare("g", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this.ParseGroupSet();

			}
			else if (String.Compare("svg", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				var viewbox = this._reader.GetAttribute("viewBox");
				if (viewbox == null)
				{

					throw new Exception("SVG without ViewBox properties are not supported");

				}

				var splits = viewbox.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var w_shift = Single.Parse(splits[0]);
				var h_shift = Single.Parse(splits[1]);
				this._width = Single.Parse(splits[2]);
				this._height = Single.Parse(splits[3]);
				
				if (w_shift != 0 || h_shift != 0)
				{

					throw new Exception("SVG Image ViewBox shifts are not supported. Please ensure they are set to 0");

				}

			}
			else if (String.Compare("use", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this.ParseUsageSet();

			}
			else if (String.Compare("image", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				this.ParseImageSet();

			}
		}

		private void ParsePathSet()
		{
			var id = this._reader.GetAttribute("id");
			var path = this._reader.GetAttribute("d");
			if (path == null) return;

			if (id == null)
			{

				if (this._groups.Count == 0) // if current group stack is empty, means on its own
				{

					var random = new Random();
					id = random.Next(0, Int32.MaxValue).ToString("X8");

				}
				else // else belongs to the last found group, so id is the same
				{

					id = this._groups.Peek().ID;

				}

			}

			if (!this._map.TryGetValue(id, out var element))
			{

				element = new ObservableElement(id);
				this._map.Add(id, element);

			}

			var sets = this.SplitPathInMoves(path);
			XYPoint lastknown = null;

			foreach (var set in sets)
			{

				var points = this.EnsureDelimWhitespace(set.Path).Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (points.Length < 2) continue; // if less than 1 point then curve is invalid
				var list = new PathPointSet(points.Length >> 1);

				// Considering after M/m there should be at least one point
				if (lastknown != null && set.StartRelative)
				{

					// If 'm' use last known point to build new one
					var p = new XYPoint(points[0], points[1]);
					p.X += lastknown.X; p.Y += lastknown.Y;
					list.Points.Add(p);

				}
				else
				{

					// Else if M, or first path, means absolute coordinate
					list.Points.Add(new XYPoint(points[0], points[1]));

				}

				list.LastCurveType = set.StartRelative ? "m" : "M";
				int index = 2;

				while (index < points.Length)
				{

					var obj = points[index++];

					index += obj switch
					{
						"c" => this.ReadCubicCurve(points, index, list, true),
						"C" => this.ReadCubicCurve(points, index, list, false),
						"s" => this.ReadSmoothCurve(points, index, list, true),
						"S" => this.ReadSmoothCurve(points, index, list, false),
						"q" => this.ReadQuadraticCurve(points, index, list, true),
						"Q" => this.ReadQuadraticCurve(points, index, list, false),
						"t" => this.ReadTupledCurve(points, index, list, true),
						"T" => this.ReadTupledCurve(points, index, list, false),
						"l" => this.ReadLinearCurve(points, index, list, true),
						"L" => this.ReadLinearCurve(points, index, list, false),
						"h" => this.ReadHorizontalCurve(points, index, list, true),
						"H" => this.ReadHorizontalCurve(points, index, list, false),
						"v" => this.ReadVerticalCurve(points, index, list, true),
						"V" => this.ReadVerticalCurve(points, index, list, false),
						"z" => this.ReadEnclosion(list),
						"Z" => this.ReadEnclosion(list),
						_ => this.ReadRepetitiveCurve(points, index, list),
					};

					obj = this.ReturnCurveType(obj);
					if (obj != null) list.LastCurveType = obj;

				}

				element.PointDatas.Add(list);
				lastknown = list.Last;

			}

			this.ReadColorSet(element);
			this.ReadStyleSet(element);
		}

		private void ParseUsageSet()
		{
			var id = this._reader.GetAttribute("xlink:href");
			if (id == null) return;
			if (!this._map.TryGetValue(id[1..], out var element)) return;
			else this.ReadColorSet(element);
		}

		private void ParseGroupSet()
		{
			var id = this._reader.GetAttribute("id");
			if (id == null)
			{

				// Create random id
				var random = new Random();
				id = random.Next(0, Int32.MaxValue).ToString("X8");

			}

			// If no group with the same id is to be found
			if (!this._map.ContainsKey(id))
			{

				var element = new ObservableElement(id);
				this._groups.Push(element);
				this._map.Add(id, element);
				this.ReadColorSet(element);

			}
		}

		private void ParseImageSet()
		{
			var data = this._reader.GetAttribute("xlink:href");
			if (data == null) return;
			if (data.Contains("base64", StringComparison.OrdinalIgnoreCase))
			{

				throw new Exception("Base64 SVG formats are not supported");

			}
		}

		private void ParseIsEndGroup()
		{
			if (String.Compare("g", this._reader.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{

				if (this._groups.Count != 0) this._groups.Pop();

			}
		}

		private PathD[] SplitPathInMoves(string str)
		{
			var splits = str.Trim().Split(new char[] { 'm', 'M' }, StringSplitOptions.RemoveEmptyEntries);
			var paths = new PathD[splits.Length];
			
			for (int i = 0, k = 0; i < str.Length; ++i)
			{

				switch (str[i])
				{

					case 'M':
						var pathM = new PathD() { Path = splits[k], StartRelative = false };
						paths[k++] = pathM;
						continue;

					case 'm':
						var pathm = new PathD() { Path = splits[k], StartRelative = true };
						paths[k++] = pathm;
						continue;

					default:
						continue;

				}

			}

			return paths;
		}

		private string EnsureDelimWhitespace(string str)
		{
			var builder = new StringBuilder(str.Length + 100);

			foreach (var c in str)
			{
				string n = c switch
				{
					'c' => " c ", // cubic relative
					'C' => " C ", // cubic absolute
					's' => " s ", // smooth relative
					'S' => " S ", // smooth absolute
					'h' => " h ", // horizontal relative
					'H' => " H ", // horizontal absolute
					'v' => " v ", // vertical relative
					'V' => " V ", // vertical absolute
					'l' => " l ", // linear relative
					'L' => " L ", // linear absolute
					'q' => " q ", // quadratic relative
					'Q' => " Q ", // quadratic absolute
					't' => " t ", // tupled relative
					'T' => " T ", // tupled absolute
					'z' => " z ", // enclosion
					'Z' => " Z ", // enclosion
					_ => c.ToString(),
				};

				builder.Append(n);

			}

			return builder.ToString();
		}

		private int ReadCubicCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 6 > points.Length)
			{

				throw new Exception("Invalid path: when reading cubic curve expected 6 coordinates available");

			}

			var ps = new XYPoint[3];
			ps[0] = new XYPoint(points[index], points[index + 1]);
			ps[1] = new XYPoint(points[index + 2], points[index + 3]);
			ps[2] = new XYPoint(points[index + 4], points[index + 5]);

			if (relative)
			{

				ps[0].X += set.Last.X; ps[0].Y += set.Last.Y;
				ps[1].X += set.Last.X; ps[1].Y += set.Last.Y;
				ps[2].X += set.Last.X; ps[2].Y += set.Last.Y;

			}

			set.Points.AddRange(ps);
			return 6;
		}

		private int ReadSmoothCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 4 > points.Length)
			{

				throw new Exception("Invalid path: when reading smooth curve expected 4 coordinates available");

			}

			if (set.LastCurveType == "C" || set.LastCurveType == "S")
			{

				var prev = set.Points[^2];
				var ps = new XYPoint[3];
				ps[0] = new XYPoint(set.Last.X * 2 - prev.X, set.Last.Y * 2 - prev.Y);
				ps[1] = new XYPoint(points[index], points[index + 1]);
				ps[2] = new XYPoint(points[index + 2], points[index + 3]);
				
				if (relative)
				{

					ps[1].X += set.Last.X; ps[1].Y += set.Last.Y;
					ps[2].X += set.Last.X; ps[2].Y += set.Last.Y;

				}

				set.Points.AddRange(ps);
				return 4;

			}
			else
			{

				return this.ReadQuadraticCurve(points, index, set, relative);

			}
		}

		private int ReadQuadraticCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 4 > points.Length)
			{

				throw new Exception("Invalid path: when reading quadratic curve expected 4 coordinates available");

			}

			var ps = new XYPoint[3];
			var mid = new XYPoint(points[index], points[index + 1]);
			ps[2] = new XYPoint(points[index + 2], points[index + 3]);

			if (relative)
			{

				mid.X += set.Last.X; mid.Y += set.Last.Y;
				ps[2].X += set.Last.X; ps[2].Y += set.Last.Y;

			}

			var c1x = set.Last.X + (mid.X - set.Last.X) * 2 / 3;
			var c1y = set.Last.Y + (mid.Y - set.Last.Y) * 2 / 3;
			var c2x = ps[2].X + (mid.X - ps[2].X) * 2 / 3;
			var c2y = ps[2].Y + (mid.X - ps[2].Y) * 2 / 3;

			ps[0] = new XYPoint(c1x, c1y);
			ps[1] = new XYPoint(c2x, c2y);		
			set.Points.AddRange(ps);
			return 4;
		}

		private int ReadTupledCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 2 > points.Length)
			{

				throw new Exception("Invalid path: when reading tupled curve expected 2 coordinates available");

			}

			if (set.LastCurveType == "Q" || set.LastCurveType == "T")
			{

				var ps = new XYPoint[3];
				var slope1 = set.Points[^3];
				var slope2 = set.Points[^2];
				var c1x = set.Last.X * 2 - slope2.X;
				var c1y = set.Last.Y * 2 - slope2.Y;
				var c2x = set.Last.X * 2 - slope1.X;
				var c2y = set.Last.Y * 2 - slope1.Y;
				ps[0] = new XYPoint(c1x, c1y);
				ps[1] = new XYPoint(c2x, c2y);
				ps[2] = new XYPoint(points[index], points[index + 1]);

				if (relative) { ps[2].X += set.Last.X; ps[2].Y += set.Last.Y; }
				set.Points.AddRange(ps);
				return 2;

			}
			else
			{

				return this.ReadLinearCurve(points, index, set, relative);

			}
		}

		private int ReadLinearCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 2 > points.Length)
			{

				throw new Exception("Invalid path: when reading linear curve expected 2 coordinates available");

			}

			var p = new XYPoint(points[index], points[index + 1]);
			if (relative) { p.X += set.Last.X; p.Y += set.Last.Y; }

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(p);
			set.Points.Add(new XYPoint(set.Last));
			return 2;
		}

		private int ReadHorizontalCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 1 > points.Length)
			{

				throw new Exception("Invalid path: when reading horizontal curve expected 1 coordinate available");

			}

			var p = new XYPoint(points[index], set.Last.Y);
			if (relative) p.X += set.Last.X;

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(p);
			set.Points.Add(new XYPoint(set.Last));
			return 1;
		}

		private int ReadVerticalCurve(string[] points, int index, PathPointSet set, bool relative)
		{
			if (index + 1 > points.Length)
			{

				throw new Exception("Invalid path: when reading vertical curve expected 1 coordinate available");

			}

			var p = new XYPoint(set.Last.X, points[index]);
			if (relative) p.Y += set.Last.Y;

			set.Points.Add(new XYPoint(set.Last));
			set.Points.Add(p);
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

		private int ReadRepetitiveCurve(string[] points, int index, PathPointSet set)
		{
			--index;

			var result = set.LastCurveType switch
			{
				"m" => this.ReadLinearCurve(points, index, set, true),
				"M" => this.ReadLinearCurve(points, index, set, false),
				"c" => this.ReadCubicCurve(points, index, set, true),
				"C" => this.ReadCubicCurve(points, index, set, false),
				"s" => this.ReadSmoothCurve(points, index, set, true),
				"S" => this.ReadSmoothCurve(points, index, set, false),
				"q" => this.ReadQuadraticCurve(points, index, set, true),
				"Q" => this.ReadQuadraticCurve(points, index, set, false),
				"t" => this.ReadTupledCurve(points, index, set, true),
				"T" => this.ReadTupledCurve(points, index, set, false),
				"l" => this.ReadLinearCurve(points, index, set, true),
				"L" => this.ReadLinearCurve(points, index, set, false),
				"h" => this.ReadHorizontalCurve(points, index, set, true),
				"H" => this.ReadHorizontalCurve(points, index, set, false),
				"v" => this.ReadVerticalCurve(points, index, set, true),
				"V" => this.ReadVerticalCurve(points, index, set, false),
				"z" => this.ReadEnclosion(set),
				"Z" => this.ReadEnclosion(set),
				_ => 1,
			};

			return result - 1;
		}

		private string ReturnCurveType(string str)
		{
			switch (str)
			{
				case "c": case "C": case "s": case "S":
				case "q": case "Q": case "t": case "T":
				case "l": case "L": case "z": case "Z":
				case "h": case "H": case "v": case "V":
					return str;

				default:
					return null;
			}
		}

		private void ReadColorSet(ObservableElement element)
		{
			string attrib = null;
			attrib = this._reader.GetAttribute("fill");
			if (attrib != null) element.FillColor = attrib;
			attrib = this._reader.GetAttribute("stroke");
			if (attrib != null) element.StrokeColor = attrib;
			attrib = this._reader.GetAttribute("opacity");
			if (attrib != null) element.FillOpacity = attrib;
			attrib = this._reader.GetAttribute("stroke-opacity");
			if (attrib != null) element.StrokeOpacity = attrib;
			attrib = this._reader.GetAttribute("stroke-width");
			if (attrib != null) element.Thickness = attrib;
		}

		private void ReadStyleSet(ObservableElement element)
		{
			var style = this._reader.GetAttribute("style");
			if (style == null) return;

			var attribs = style.Replace(" ", "")
				.Split(';', StringSplitOptions.RemoveEmptyEntries)
				.Select(str => str.Split(':'))
				.Select(str => Tuple.Create(str[0], str[1]))
				.ToArray();

			foreach (var attrib in attribs)
			{

				switch (attrib.Item1)
				{

					case "fill":
						element.FillColor = attrib.Item2;
						break;

					case "stroke":
						element.StrokeColor = attrib.Item2;
						break;

					case "opacity":
						element.FillOpacity = attrib.Item2;
						break;

					case "stroke-opacity":
						element.StrokeOpacity = attrib.Item2;
						break;

					case "stroke-width":
						element.Thickness = attrib.Item2;
						break;

					default:
						break;

				}

			}

		}

		private (byte, byte, byte) FromHTMLColor(string color)
		{
			if (String.IsNullOrWhiteSpace(color) || !color.StartsWith('#') || color.Length != 7)
			{

				return (0, 0, 0);

			}
			else
			{

				var red = Convert.ToByte(color.Substring(1, 2), 16);
				var green = Convert.ToByte(color.Substring(3, 2), 16);
				var blue = Convert.ToByte(color.Substring(5, 2), 16);
				return (red, green, blue);

			}
		}
	}
}
