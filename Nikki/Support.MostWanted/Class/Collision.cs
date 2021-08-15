using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Framework;
using Nikki.Support.Shared.Parts.BoundParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Class
{
	/// <summary>
	/// <see cref="Collision"/> is a collection of settings related to a car's bounds.
	/// </summary>
	public class Collision : Shared.Class.Collision
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.MostWanted;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.MostWanted.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public CollisionManager Manager { get; set; }

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
		/// List of collision bounds.
		/// </summary>
		[Category("Secondary")]
		public List<CollisionBound> CollisionBounds { get; }
		
		/// <summary>
		/// List of collision clouds.
		/// </summary>
		[Category("Secondary")]
		public List<CollisionCloud> CollisionClouds { get; }

		/// <summary>
		/// Number of collision bounds.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public override int NumberOfBounds
		{
			get => this.CollisionBounds.Count;
			set => this.CollisionBounds.Resize(value);
		}

		/// <summary>
		/// Number of collision clouds.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public override int NumberOfClouds
		{
			get => this.CollisionClouds.Count;
			set => this.CollisionClouds.Resize(value);
		}

		/// <summary>
		/// True if this <see cref="Collision"/> is resolved; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public override eBoolean IsResolved { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		public Collision()
		{
			this.CollisionBounds = new List<CollisionBound>();
			this.CollisionClouds = new List<CollisionCloud>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="CollisionManager"/> to which this instance belongs to.</param>
		public Collision(string CName, CollisionManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="CollisionManager"/> to which this instance belongs to.</param>
		public Collision(BinaryReader br, CollisionManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Collision"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Collision"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			// Precalculate size
			int size = 0x28 + this.NumberOfBounds * 0x30; // 0x28 = alignment (8) + headers

			for (int loop = 0; loop < this.NumberOfClouds; ++loop)
			{

				size += 0x10 + this.CollisionClouds[loop].NumberOfVertices * 0x10;

			}

			// Write data
			bw.WriteEnum(BinBlockID.Collision);
			bw.Write(size);
			bw.Write(0x11111111);
			bw.Write(0x11111111);
			bw.Write(this.VltKey);
			bw.Write(this.NumberOfBounds);
			bw.Write(this.IsResolved == eBoolean.False ? (int)0 : (int)1);
			bw.Write((int)0);

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				this.CollisionBounds[loop].Write(bw);

			}

			bw.Write(this.NumberOfClouds);
			bw.Write((int)0);
			bw.Write((long)0);

			for (int loop = 0; loop < this.NumberOfClouds; ++loop)
			{

				this.CollisionClouds[loop].Write(bw);

			}
		}

		/// <summary>
		/// Disassembles array into <see cref="Collision"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Collision"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			br.BaseStream.Position += 0x10; // skip ID, size and alignment
			this._collection_name = br.ReadUInt32().VltString(LookupReturn.EMPTY);
			this.NumberOfBounds = br.ReadInt32();
			this.IsResolved = br.ReadInt32() == 0 ? eBoolean.False : eBoolean.True;
			br.BaseStream.Position += 4;

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				this.CollisionBounds[loop].Read(br);

			}

			this.NumberOfClouds = br.ReadInt32();
			br.BaseStream.Position += 12;

			for (int loop = 0; loop < this.NumberOfClouds; ++loop)
			{

				this.CollisionClouds[loop].Read(br);

			}
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new Collision(CName, this.Manager)
			{
				NumberOfBounds = this.NumberOfBounds,
				NumberOfClouds = this.NumberOfClouds,
				IsResolved = this.IsResolved
			};

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				result.CollisionBounds[loop] = (CollisionBound)this.CollisionBounds[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfClouds; ++loop)
			{

				result.CollisionClouds[loop] = (CollisionCloud)this.CollisionClouds[loop].PlainCopy();

			}

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="Collision"/> 
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
			using (var ms = new MemoryStream(0x300))
			using (var writer = new BinaryWriter(ms))
			{

				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this.NumberOfBounds);
				writer.Write(this.NumberOfClouds);

				for (int loop = 0; loop < this.NumberOfBounds; ++loop)
				{

					this.CollisionBounds[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfClouds; ++loop)
				{

					writer.Write(this.CollisionClouds[loop].NumberOfVertices);

					for (int i = 0; i < this.CollisionClouds[loop].NumberOfVertices; ++i)
					{

						this.CollisionClouds[loop].Vertices[i].Write(writer);

					}

				}

				writer.WriteEnum(this.IsResolved);

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

			this._collection_name = reader.ReadNullTermUTF8();
			this.NumberOfBounds = reader.ReadInt32();
			this.NumberOfClouds = reader.ReadInt32();

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				this.CollisionBounds[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfClouds; ++loop)
			{

				this.CollisionClouds[loop].NumberOfVertices = reader.ReadInt32();

				for (int i = 0; i < this.CollisionClouds[loop].NumberOfVertices; ++i)
				{

					this.CollisionClouds[loop].Vertices[i].Read(reader);

				}

			}

			this.IsResolved = reader.ReadEnum<eBoolean>();
		}

		#endregion
	}
}
