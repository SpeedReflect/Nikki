using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="FNGroup"/> collections.
	/// </summary>
	public class FNGroupManager : Manager<FNGroup>
	{
		private bool _is_read_only = true;

		/// <summary>
		/// Name of this <see cref="FNGroupManager"/>.
		/// </summary>
		public override string Name => "FNGroups";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => this._is_read_only;

		/// <summary>
		/// Indicates required alighment when this <see cref="FNGroupManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="FNGroupManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public FNGroupManager(Datamap db)
		{
			this.Database = db;
			this.Extender = 0;
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

			bw.GeneratePadding(mark, this.Alignment);

			foreach (var collection in this)
			{

				bw.GeneratePadding(mark, this.Alignment);
				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="FNGroupManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.FEngFiles) return;

			this._is_read_only = false;
			this.Capacity = block.Offsets.Count;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop];
				var collection = new FNGroup(br, this);
				this.Add(collection);

			}

			this._is_read_only = true;
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
	}
}
