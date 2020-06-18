using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.MostWanted.Class;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="Collision"/> collections.
	/// </summary>
	public class CollisionManager : Manager<Collision>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.MostWanted;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.MostWanted.ToString();

		/// <summary>
		/// Name of this <see cref="CollisionManager"/>.
		/// </summary>
		public override string Name => "Collisions";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="CollisionManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="CollisionManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public CollisionManager(Datamap db)
		{
			this.Database = db;
			this.Extender = 5;
			this.Alignment = new Alignment(0x8, Alignment.eAlignType.Actual);
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal override void Assemble(BinaryWriter bw, string mark)
		{
			if (this.Count == 0) return;

			bw.GeneratePadding(mark, this.Alignment);

			bw.WriteEnum(eBlockID.DBCarBounds);
			bw.Write(-1);
			var start = bw.BaseStream.Position;

			foreach (var collection in this)
			{

				collection.Assemble(bw);

			}

			var end = bw.BaseStream.Position;
			bw.BaseStream.Position = start - 4;
			bw.Write((int)(end - start));
			bw.BaseStream.Position = end;
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="CollisionManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.DBCarBounds) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();
				var offset = br.BaseStream.Position;
				this.Capacity = size / 0x320;

				while (br.BaseStream.Position < offset + size)
				{

					var collection = new Collision(br, this);
					this.Add(collection);

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
		public override void Export(string cname, BinaryWriter bw)
		{

		}

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="Manager{T}"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(eSerializeType type, BinaryReader br)
		{

		}
	}
}
