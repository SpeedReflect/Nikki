using System;
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
			bw.Write((int)0x1C);
			bw.Write((long)0);
			bw.Write(this.NumberOfPaths);
			bw.Write((int)0);
			bw.Write(this.BinKey);
			bw.Write(this.CenterX);
			bw.Write(this.CenterY);

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
							this.CenterX = br.ReadUInt32();
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
