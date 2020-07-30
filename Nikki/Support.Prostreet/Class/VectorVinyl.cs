using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Prostreet.Framework;
using Nikki.Support.Prostreet.Parts.VinylParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Prostreet.Class
{
	/// <summary>
	/// <see cref="VectorVinyl"/> is a collection of vectors that form a vinyl.
	/// </summary>
	public class VectorVinyl : Shared.Class.VectorVinyl
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Prostreet;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Prostreet.ToString();

		/// <summary>
		/// 
		/// </summary>
		[Browsable(false)]
		public VectorVinylManager Manager { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				this.Manager?.CreationCheck(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Number of <see cref="PathSet"/> in this <see cref="VectorVinyl"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int NumberOfPaths
		{
			get => this.PathSets.Count;
			set => this.PathSets.Resize(value);
		}

		/// <summary>
		/// X position of the center of this <see cref="VectorVinyl"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float CenterX { get; set; }

		/// <summary>
		/// Y position of the center of this <see cref="VectorVinyl"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float CenterY { get; set; }

		/// <summary>
		/// Aspect ratio of this <see cref="VectorVinyl"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float AspectRatio { get; set; }

		/// <summary>
		/// List of <see cref="PathSet"/> in this <see cref="VectorVinyl"/>.
		/// </summary>
		[Browsable(false)]
		public List<PathSet> PathSets { get; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="VectorVinyl"/>.
		/// </summary>
		public VectorVinyl()
		{
			this.PathSets = new List<PathSet>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="CName"></param>
		/// <param name="manager"></param>
		public VectorVinyl(string CName, VectorVinylManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="br"></param>
		/// <param name="manager"></param>
		public VectorVinyl(BinaryReader br, VectorVinylManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~VectorVinyl() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="VectorVinyl"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="VectorVinyl"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			bw.WriteEnum(BinBlockID.VinylSystem);
			bw.Write(-1);
			var start = bw.BaseStream.Position;

			bw.WriteEnum(BinBlockID.Vinyl_Header);
			bw.Write((int)0x20);
			bw.Write((long)0);
			bw.Write(this.NumberOfPaths);
			bw.Write((int)0);
			bw.Write(this.BinKey);
			bw.Write(this.CenterX);
			bw.Write(this.CenterY);
			bw.Write(this.AspectRatio);

			bw.WriteEnum(BinBlockID.Vinyl_PointerTable);
			bw.Write(this.NumberOfPaths << 2);
			bw.WriteBytes(this.NumberOfPaths << 2);

			foreach (var set in this.PathSets) set.Write(bw);

			var end = bw.BaseStream.Position;
			bw.BaseStream.Position = start - 4;
			bw.Write((int)(end - start));
			bw.BaseStream.Position = end;
		}

		/// <summary>
		/// Disassembles array into <see cref="VectorVinyl"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="VectorVinyl"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			br.BaseStream.Position += 4;
			var size = br.ReadInt32();
			var off = br.BaseStream.Position;
			var end = off + size;
			var list = new List<long>();

			while (br.BaseStream.Position < end)
			{

				var id = br.ReadEnum<BinBlockID>();
				var len = br.ReadInt32();
				var cur = br.BaseStream.Position;

				switch (id)
				{
					case BinBlockID.Vinyl_Header:
						{

							br.BaseStream.Position += 8;
							this.NumberOfPaths = br.ReadInt32();
							br.BaseStream.Position += 4;
							this._collection_name = br.ReadUInt32().BinString(LookupReturn.EMPTY);
							this.CenterX = br.ReadSingle();
							this.CenterY = br.ReadSingle();
							this.AspectRatio = br.ReadSingle();

						}
						goto default;

					case BinBlockID.Vinyl_PointerTable:
						goto default; // do not process pointers

					case BinBlockID.Vinyl_PathSet:
						list.Add(br.BaseStream.Position - 8);
						goto default;

					default:
						br.BaseStream.Position = cur + len;
						break;

				}

			}

			for (int i = 0; i < list.Count && i < this.NumberOfPaths; ++i)
			{

				br.BaseStream.Position = list[i];
				this.PathSets[i].Read(br);

			}

			br.BaseStream.Position = end;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new VectorVinyl(CName, this.Manager)
			{
				CenterX = this.CenterX,
				CenterY = this.CenterY,
				AspectRatio = this.AspectRatio,
			};

			foreach (var set in this.PathSets) result.PathSets.Add((PathSet)set.PlainCopy());

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="VectorVinyl"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion

		#region Functional

		/// <summary>
		/// Gets data of this <see cref="VectorVinyl"/> as an SVG-formatted string.
		/// </summary>
		/// <param name="resolution">Resolution of the SVG image.</param>
		/// <returns>Data as an SVG-formatted string.</returns>
		public string GetSVGString(int resolution)
		{
			if (resolution > 0x10000) resolution = 0x10000;
			var difference = 0x10000 / resolution;
			var bitshift = (byte)Math.Log2(difference);

			var builder = new StringBuilder(0x1000);

			var defs = new List<string>();
			var gs = new List<string>();

			for (int setnum = 0; setnum < this.NumberOfPaths; ++setnum)
			{

				var set = this.PathSets[setnum];
				var id = $"set{setnum}";
				builder.Append($"<path id=\"{id}\" d=\"" + Environment.NewLine);

				for (int datnum = 0; datnum < set.NumPathDatas; ++datnum)
				{

					var data = set.PathDatas[datnum];
					builder.Append("M ");

					for (int i = 0, index = data.StartIndex; i < data.NumCurves; ++i)
					{

						var px = set.PathPoints[index].X >> bitshift;
						var py = set.PathPoints[index++].Y >> bitshift;
						var cx = set.PathPoints[index].X >> bitshift;
						var cy = set.PathPoints[index++].Y >> bitshift;
						var mx = set.PathPoints[index].X >> bitshift;
						var my = set.PathPoints[index++].Y >> bitshift;

						var str = $"{px} {py} C {cx} {cy} {mx} {my} ";
						builder.Append(str);

					}

					var last = data.StartIndex + data.NumCurves * 3;
					var lx = set.PathPoints[last].X >> bitshift;
					var ly = set.PathPoints[last].Y >> bitshift;

					builder.Append($"{lx} {ly} Z " + Environment.NewLine);


				}

				builder.Append("\" />" + Environment.NewLine);
				defs.Add(builder.ToString());
				builder.Clear();
				gs.Add(this.GetFormattedSetG(set, id, resolution));

			}

			builder.Clear();

			builder.Append(this.GetSVGHeaderString(resolution));
			builder.Append("<defs>" + Environment.NewLine);
			foreach (var def in defs) builder.Append(def);
			builder.Append("</defs>" + Environment.NewLine);
			builder.Append("<g>" + Environment.NewLine);
			foreach (var g in gs) builder.Append(g);
			builder.Append("</g>" + Environment.NewLine);
			builder.Append("</svg>" + Environment.NewLine);
			return builder.ToString();
		}

		public void ReadFromFile(string file)
		{
			using var svgreader = new SVGReader(file);
			svgreader.ReadAllContents();

			int oof = 0;
		}

		private string GetSVGHeaderString(int resolution)
		{
			var builder = new StringBuilder(0x200);
			builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + Environment.NewLine);
			builder.Append("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" + Environment.NewLine);
			builder.Append("<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"" + Environment.NewLine);
			builder.Append("xmlns:xlink=\"http://www.w3.org/1999/xlink\"" + Environment.NewLine);
			builder.Append("preserveAspectRatio=\"xMidYMid meet\"" + Environment.NewLine);
			builder.Append($"viewBox=\"0 0 {resolution} {resolution}\"" + Environment.NewLine);
			builder.Append($"width=\"{resolution}\" height=\"{resolution}\">" + Environment.NewLine);
			return builder.ToString();
		}

		private string GetFormattedSetG(PathSet set, string id, int resolution)
		{
			var result = $"<use xlink:href=\"#{id}\" fill-rule=\"evenodd\" ";

			var fill = set.FillEffect.GetHTMLColor();
			var stroke = set.StrokeEffect.GetHTMLColor();
			var thick = set.StrokeEffect.Thickness * resolution;
			if (thick == 0) thick = resolution >> 11;

			result += $"fill=\"{fill}\" stroke=\"{stroke}\" stroke-width=\"{thick:0.00}\" />";
			return result + Environment.NewLine;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Serialize(BinaryWriter bw)
		{

		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Deserialize(BinaryReader br)
		{

		}

		#endregion
	}
}
