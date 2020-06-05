using System;
using System.IO;
using Nikki.Utils;
using Nikki.Database;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="PresetRide"/> collections.
	/// </summary>
	public class PresetRideManager : Manager<PresetRide>
	{
		/// <summary>
		/// Name of this <see cref="PresetRideManager"/>.
		/// </summary>
		public override string Name => "PresetRides";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="CollisionManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="PresetRideManager"/>.
		/// </summary>
		/// <param name="db"><see cref="FileBase"/> to which this manager belongs to.</param>
		public PresetRideManager(FileBase db)
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
		internal void Assemble(BinaryWriter bw, string mark)
		{
			bw.GeneratePadding(mark, this.Alignment);

			bw.WriteEnum(eBlockID.PresetRides);
			bw.Write(this.Count * PresetRide.BaseClassSize);

			foreach (var collection in this)
			{

				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="PresetRideManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.PresetRides) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();

				int count = size / PresetRide.BaseClassSize;
				this.Capacity += count;

				for (int i = 0; i < count; ++i)
				{

					var collection = new PresetRide(br, this);
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

			if (cname.Length > PresetRide.MaxCNameLength)
			{

				throw new ArgumentLengthException(PresetRide.MaxCNameLength);

			}

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}
		}
	}
}
