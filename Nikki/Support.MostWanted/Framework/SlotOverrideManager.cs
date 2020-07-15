using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Enum.SlotID;
using Nikki.Support.MostWanted.Class;



namespace Nikki.Support.MostWanted.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="SlotOverride"/> collections.
	/// </summary>
	public class SlotOverrideManager : Manager<SlotOverride>
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
		/// Name of this <see cref="SlotOverrideManager"/>.
		/// </summary>
		public override string Name => "SlotOverrides";

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		public override bool AllowsNoSerialization => false;

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="SlotOverrideManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="SlotOverrideManager"/>.
		/// </summary>
		public override Type CollectionType => typeof(SlotOverride);

		/// <summary>
		/// Initializes new instance of <see cref="SlotOverrideManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public SlotOverrideManager(Datamap db) : base(db)
		{
			this.Extender = 5;
			this.Alignment = Alignment.Default;
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

				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="SlotOverrideManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != BinBlockID.SlotTypes) return;

			const int maxslotsize = 0x458;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();

				if (size <= maxslotsize) continue;
				else br.BaseStream.Position += maxslotsize;

				int count = (size - maxslotsize) / SlotOverride.BaseClassSize;
				this.Capacity += count;
				
				for (int i = 0; i < count; ++i)
				{

					var collection = new SlotOverride(br, this);

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

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}

			var keys = cname.Split("_PART_", 2, StringSplitOptions.None);

			if (keys == null || keys.Length != 2 || String.IsNullOrEmpty(keys[0]))
			{

				throw new ArgumentException($"CollectionName passed is of invalid format. Valid " +
					$"format is \"CARNAME_PART_SLOTTYPE\"");

			}
			else if (!Enum.TryParse(keys[1], out SlotMostWanted _))
			{

				throw new ArgumentException($"CollectionName passed is of invalid format. Valid " +
					$"format is \"CARNAME_PART_SLOTTYPE\"");

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

				if (serialized) this[index].Serialize(bw);
				else throw new NotSupportedException("Collection supports only serialization and no plain export");

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

			var collection = new SlotOverride();

			if (header.ID != BinBlockID.Nikki)
			{

				throw new Exception($"Missing serialized header in the imported collection");

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

					case SerializeType.Synchronize:
					case SerializeType.Override:
						collection.Manager = this;
						this.Replace(collection, index);
						break;

					default:
						break;
				}

			}
		}
	}
}
