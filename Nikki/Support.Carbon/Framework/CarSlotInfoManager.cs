using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using Nikki.Reflection.Enum.SlotID;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="CarSlotInfo"/> collections.
	/// </summary>
	public class CarSlotInfoManager : Manager<CarSlotInfo>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Carbon.ToString();

		/// <summary>
		/// Name of this <see cref="CarSlotInfoManager"/>.
		/// </summary>
		public override string Name => "CarSlotInfos";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="CarSlotInfoManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="CarSlotInfoManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public CarSlotInfoManager(Datamap db)
		{
			this.Database = db;
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
		/// Disassembles data into separate collections in this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.SlotTypes) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();

				if (size <= 0xB98) continue;
				else br.BaseStream.Position += 0xB98;

				int count = (size - 0xB98) / CarSlotInfo.BaseClassSize;
				this.Capacity += count;
				
				for (int i = 0; i < count; ++i)
				{

					var collection = new CarSlotInfo(br, this);
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

			var keys = cname.Split("_PART_", 2, StringSplitOptions.None);

			if (keys == null || keys.Length != 2 || String.IsNullOrEmpty(keys[0]))
			{

				throw new ArgumentException($"CollectionName passed is of invalid format. Valid " +
					$"format is \"CARNAME_PART_SLOTTYPE\"");

			}
			else if (!Enum.TryParse(keys[1], out eSlotCarbon _))
			{

				throw new ArgumentException($"CollectionName passed is of invalid format. Valid " +
					$"format is \"CARNAME_PART_SLOTTYPE\"");

			}
		}
	}
}
