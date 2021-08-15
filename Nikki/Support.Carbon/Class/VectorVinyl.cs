using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Carbon.Framework;
using Nikki.Support.Carbon.Parts.VinylParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
{
	/// <summary>
	/// <see cref="VectorVinyl"/> is a collection of vectors that form a vinyl.
	/// </summary>
	public class VectorVinyl : Shared.Class.VectorVinyl
	{
		#region Fields

		private string _collection_name;
		private readonly List<PathSet> _pathsets;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Carbon.ToString();

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
		public override int NumberOfPaths
		{
			get => this._pathsets.Count;
			set => this._pathsets.Resize(value);
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

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="VectorVinyl"/>.
		/// </summary>
		public VectorVinyl()
		{
			this._pathsets = new List<PathSet>();
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
			bw.Write((int)0x1C);
			bw.Write((long)0);
			bw.Write(this.NumberOfPaths);
			bw.Write((int)0);
			bw.Write(this.BinKey);
			bw.Write(this.CenterX);
			bw.Write(this.CenterY);

			bw.WriteEnum(BinBlockID.Vinyl_PointerTable);
			bw.Write(this.NumberOfPaths << 2);
			bw.WriteBytes(0, this.NumberOfPaths << 2);

			foreach (var set in this._pathsets) set.Write(bw);

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
				this._pathsets[i].Read(br);

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
			};

			foreach (var set in this._pathsets) result._pathsets.Add((PathSet)set.PlainCopy());

			return result;
		}

		/// <summary>
		/// Gets <see cref="PathSet"/> in this <see cref="VectorVinyl"/> at index specified.
		/// </summary>
		/// <param name="index">Index of the <see cref="PathSet"/> to get.</param>
		/// <returns><see cref="PathSet"/> at index specified.</returns>
		public override Shared.Parts.VinylParts.PathSet GetPathSet(int index)
		{
			return index < 0 || index >= this.NumberOfPaths ? null : this._pathsets[index];
		}

		/// <summary>
		/// Adds <see cref="PathSet"/> to the end.
		/// </summary>
		public override void AddPathSet()
		{
			this._pathsets.Add(new PathSet());
		}

		/// <summary>
		/// Removes <see cref="PathSet"/> at index specified.
		/// </summary>
		/// <param name="index">Index of <see cref="PathSet"/> to remove.</param>
		public override void RemovePathSet(int index)
		{
			if (index < 0 || index >= this.NumberOfPaths) return;
			else this._pathsets.RemoveAt(index);
		}

		/// <summary>
		/// Removes all <see cref="PathSet"/> from the vinyl.
		/// </summary>
		public override void ClearPaths() => this._pathsets.Clear();

		/// <summary>
		/// Swaps two <see cref="PathSet"/> with indexes provided.
		/// </summary>
		/// <param name="index1">Index of the first <see cref="PathSet"/> to switch.</param>
		/// <param name="index2">Index of the second <see cref="PathSet"/> to switch.</param>
		public override void SwitchPaths(int index1, int index2)
		{
			if (index1 < 0 || index1 >= this.NumberOfPaths) return;
			else if (index2 < 0 || index2 >= this.NumberOfPaths) return;

			var temp = this._pathsets[index1];
			this._pathsets[index1] = this._pathsets[index2];
			this._pathsets[index2] = temp;
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

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(0x8000))
			using (var writer = new BinaryWriter(ms))
			{

				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this.CenterX);
				writer.Write(this.CenterY);
				writer.Write(this.NumberOfPaths);

				foreach (var set in this._pathsets)
				{

					writer.Write(set.NumPathDatas);
					writer.Write(set.NumPathPoints);

					foreach (var data in set.PathDatas) data.Write(writer);
					foreach (var point in set.PathPoints) point.Write(writer);

					writer.WriteEnum(set.FillEffectExists);
					writer.WriteEnum(set.StrokeEffectExists);
					writer.WriteEnum(set.DropShadowEffectExists);
					writer.WriteEnum(set.InnerGlowEffectExists);
					writer.WriteEnum(set.InnerShadowEffectExists);
					writer.WriteEnum(set.GradientEffectExists);

					if (set.FillEffectExists == eBoolean.True) set.FillEffect.Write(writer);
					if (set.StrokeEffectExists == eBoolean.True) set.StrokeEffect.Write(writer);
					if (set.DropShadowEffectExists == eBoolean.True) set.DropShadowEffect.Write(writer);
					if (set.InnerGlowEffectExists == eBoolean.True) set.InnerGlowEffect.Write(writer);
					if (set.InnerShadowEffectExists == eBoolean.True) set.InnerShadowEffect.Write(writer);
					if (set.GradientEffectExists == eBoolean.True) set.GradientEffect.Write(writer);

				}

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.RAWW);

			var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
			header.Write(bw);
			bw.Write(array.Length);
			bw.Write(array);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Deserialize(BinaryReader br)
		{
			int size = br.ReadInt32();
			var array = br.ReadBytes(size);

			array = Interop.Decompress(array);

			using var ms = new MemoryStream(array);
			using var reader = new BinaryReader(ms);

			// Read all directories and locations
			this._collection_name = reader.ReadNullTermUTF8();
			this.CenterX = reader.ReadSingle();
			this.CenterY = reader.ReadSingle();
			this.NumberOfPaths = reader.ReadInt32();

			foreach (var set in this._pathsets)
			{

				set.NumPathDatas = reader.ReadInt32();
				set.NumPathPoints = reader.ReadInt32();

				foreach (var data in set.PathDatas) data.Read(reader);
				foreach (var point in set.PathPoints) point.Read(reader);

				set.FillEffectExists = reader.ReadEnum<eBoolean>();
				set.StrokeEffectExists = reader.ReadEnum<eBoolean>();
				set.DropShadowEffectExists = reader.ReadEnum<eBoolean>();
				set.InnerGlowEffectExists = reader.ReadEnum<eBoolean>();
				set.InnerShadowEffectExists = reader.ReadEnum<eBoolean>();
				set.GradientEffectExists = reader.ReadEnum<eBoolean>();

				if (set.FillEffectExists == eBoolean.True) set.FillEffect.Read(reader);
				if (set.StrokeEffectExists == eBoolean.True) set.StrokeEffect.Read(reader);
				if (set.DropShadowEffectExists == eBoolean.True) set.DropShadowEffect.Read(reader);
				if (set.InnerGlowEffectExists == eBoolean.True) set.InnerGlowEffect.Read(reader);
				if (set.InnerShadowEffectExists == eBoolean.True) set.InnerShadowEffect.Read(reader);
				if (set.GradientEffectExists == eBoolean.True) set.GradientEffect.Read(reader);

			}
		}

		#endregion
	}
}
