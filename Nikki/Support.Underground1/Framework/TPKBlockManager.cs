using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Underground1.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="TPKBlock"/> collections.
	/// </summary>
	public class TPKBlockManager : Manager<TPKBlock>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground1.ToString();

		/// <summary>
		/// Name of this <see cref="TPKBlockManager"/>.
		/// </summary>
		public override string Name => "TPKBlocks";

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		public override bool AllowsNoSerialization => true;

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="TPKBlockManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="TPKBlockManager"/>.
		/// </summary>
		public override Type CollectionType => typeof(TPKBlock);

		/// <summary>
		/// Initializes new instance of <see cref="TPKBlockManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public TPKBlockManager(Datamap db) : base(db)
		{
			this.Extender = 1;
			this.Alignment = new Alignment(0x80, Alignment.AlignmentType.Modular);
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal override void Assemble(BinaryWriter bw, string mark)
		{
			if (this.Count == 0) return;

			foreach (var collection in this)
			{
				bw.GeneratePadding(mark, this.Alignment);

				if (collection.TexturePageCount > 0)
				{

					collection.WriteTexturePages(bw);
					bw.GeneratePadding(mark, this.Alignment);

				}

				collection.Watermark = mark;
				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="TPKBlockManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != BinBlockID.TPKBlocks) return;

			this.Capacity = block.Offsets.Count;
			long settings = -1;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				var offset = block.Offsets[loop];
				br.BaseStream.Position = offset;
				var id = br.ReadEnum<BinBlockID>();

				if (id == BinBlockID.EmitterTexturePage)
				{

					settings = block.Offsets[loop];
					continue;

				}
				else if (id == BinBlockID.TPKBlocks)
				{

					br.BaseStream.Position = offset;

					var collection = new TPKBlock(br, this);

					if (settings != -1)
					{

						br.BaseStream.Position = settings;
						collection.ReadTexturePages(br);
						settings = -1;

					}

					try { this.Add(collection); }
					catch { } // skip if exists

				}

			}
		}

		/// <summary>
		/// Checks whether CollectionName provided allows creation of a new collection.
		/// </summary>
		/// <param name="cname">CollectionName to check.</param>
		internal override void CreationCheck(string cname)
		{
			if (String.IsNullOrWhiteSpace(cname))
			{

				throw new ArgumentNullException("CollectionName cannot be null, empty or whitespace");

			}

			if (cname.Contains(" "))
			{

				throw new ArgumentException("CollectionName cannot contain whitespace");

			}

			if (cname.Length > 0x40)
			{

				throw new ArgumentLengthException(0x40);

			}

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}
		}

		/// <summary>
		/// Exports collection with CollectionName specified to a filename provided.
		/// </summary>
		/// <param name="cname">CollectionName of a collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		public override void Export(string cname, BinaryWriter bw, bool serialized = true)
		{
			var index = this.IndexOf(cname);

			if (index == -1)
			{

				throw new Exception($"Collection named {cname} does not exist");

			}
			else
			{

				if (serialized)
				{

					this[index].Serialize(bw);

				}
				else
				{

					var collection = this[index];

					if (collection.TexturePageCount > 0)
					{

						collection.WriteTexturePages(bw);
						bw.GeneratePadding(String.Empty, this.Alignment);

					}

					collection.Assemble(bw);

				}

			}
		}

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="Manager{T}"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(SerializeType type, BinaryReader br)
		{
			var position = br.BaseStream.Position;
			var header = new SerializationHeader();
			header.Read(br);

			var collection = new TPKBlock();

			if (header.ID != BinBlockID.Nikki)
			{

				br.BaseStream.Position = position;

				while (br.BaseStream.Position < br.BaseStream.Length)
				{

					var offset = br.BaseStream.Position;
					var id = br.ReadEnum<BinBlockID>();
					var size = br.ReadInt32();

					br.BaseStream.Position = offset;

					if (id == BinBlockID.EmitterTexturePage)
					{

						collection.ReadTexturePages(br);

					}

					if (id == BinBlockID.TPKBlocks)
					{

						collection.Disassemble(br);
						break;

					}

					br.BaseStream.Position = offset + size + 8;

				}

			}
			else
			{

				if (header.Game != this.GameINT)
				{

					throw new Exception($"Stated game inside collection is {header.Game}, while should be {this.GameINT}");

				}

				if (header.Name != this.Name)
				{

					throw new Exception($"Imported collection is not a collection of type {this.Name}");

				}

				collection.Deserialize(br);

			}

			var index = this.IndexOf(collection);

			if (index == -1)
			{

				collection.Manager = this;
				this.Add(collection);

			}
			else
			{

				switch (type)
				{
					case SerializeType.Negate:
						break;

					case SerializeType.Override:
						collection.Manager = this;
						this.Replace(collection, index);
						break;

					case SerializeType.Synchronize:
						this[index].Synchronize(collection);
						break;

					default:
						break;
				}

			}
		}
	}
}
