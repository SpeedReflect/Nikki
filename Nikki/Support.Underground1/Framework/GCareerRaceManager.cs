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
	/// A <see cref="Manager{T}"/> for <see cref="GCareerRace"/> collections.
	/// </summary>
	public class GCareerRaceManager : Manager<GCareerRace>
	{
		private byte[] _upgrade_info;

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground1.ToString();

		/// <summary>
		/// Name of this <see cref="GCareerRaceManager"/>.
		/// </summary>
		public override string Name => "GCareerRaces";

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		public override bool AllowsNoSerialization => true;

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="GCareerRaceManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="GCareerRaceManager"/>.
		/// </summary>
		public override Type CollectionType => typeof(GCareerRace);

		/// <summary>
		/// Initializes new instance of <see cref="GCareerRaceManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public GCareerRaceManager(Datamap db) : base(db)
		{
			this.Extender = 5;
			this.Alignment = Alignment.Default;
			this._upgrade_info = new byte[0];
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

			var size = 8 + this._upgrade_info.Length; // add length of upgrade info
			size += 8 + this.Count * GCareerRace.BaseClassSize; // add length of races

			bw.WriteEnum(BinBlockID.GCareer_Old);
			bw.Write(size);

			bw.WriteEnum(BinBlockID.GCareer_Races_Old);
			bw.Write(this.Count * GCareerRace.BaseClassSize);

			foreach (var collection in this)
			{

				collection.Assemble(bw);

			}

			bw.WriteEnum(BinBlockID.GCareer_Upgrade);
			bw.Write(this._upgrade_info.Length);
			if (this._upgrade_info.Length > 0) bw.Write(this._upgrade_info);
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="GCareerRaceManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != BinBlockID.GCareer_Old) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var offset = br.ReadInt32();
				offset += (int)br.BaseStream.Position; // last position in the block
				
				while (br.BaseStream.Position < offset)
				{

					var id = br.ReadEnum<BinBlockID>();
					var size = br.ReadInt32();

					switch (id)
					{

						case BinBlockID.GCareer_Upgrade:
							this._upgrade_info = br.ReadBytes(size);
							break;

						case BinBlockID.GCareer_Races_Old:
							int count = size / GCareerRace.BaseClassSize;
							this.Capacity += count;

							for (int i = 0; i < count; ++i)
							{

								var collection = new GCareerRace(br, this);

								try { this.Add(collection); }
								catch { } // skip if exists

							}

							break;

						default:
							br.BaseStream.Position += size;
							break;

					}

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

			if (!Int32.TryParse(cname, out int _))
			{

				throw new Exception("CollectionName of a GCareerRace should be an integer");

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

				if (serialized) this[index].Serialize(bw);
				else this[index].Assemble(bw);

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

			var collection = new GCareerRace();

			if (header.ID != BinBlockID.Nikki)
			{

				br.BaseStream.Position = position;
				collection.Disassemble(br);

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
