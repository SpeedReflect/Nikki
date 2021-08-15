using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Framework;
using Nikki.Support.Shared.Parts.STRParts;
using CoreExtensions.IO;
using CoreExtensions.Text;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Class
{
	/// <summary>
	/// <see cref="STRBlock"/> is a collection of language strings, hashes and labels.
	/// </summary>
	public class STRBlock : Shared.Class.STRBlock
	{
		#region Fields

		private string _collection_name;
		private byte[] _unk_data;
		private List<StringRecord> _stringinfo;

		/// <summary>
		/// Maximum length of the CollectionName.
		/// </summary>
		public const int MaxCNameLength = 0xF;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x14;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = -1;

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
		public STRBlockManager Manager { get; set; }

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
		/// Length of the string information array.
		/// </summary>
		[Category("Primary")]
		public override int StringRecordCount => this._stringinfo.Count;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		public STRBlock() => this._stringinfo = new List<StringRecord>();

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="STRBlockManager"/> to which this instance belongs to.</param>
		public STRBlock(string CName, STRBlockManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read text data with.</param>
		/// <param name="manager"><see cref="STRBlockManager"/> to which this instance belongs to.</param>
		public STRBlock(BinaryReader br, STRBlockManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="STRBlock"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="STRBlock"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			var udat_offset = 0x20;
			var hash_offset = udat_offset + this._unk_data.Length;
			var text_offset = hash_offset + this.StringRecordCount * 8;

			// Sort records by keys
			this.SortRecordsByKey();

			// Write ID and temporary size
			bw.WriteEnum(BinBlockID.STRBlocks);
			bw.Write(-1);

			// Save position
			var position = bw.BaseStream.Position;

			// Write offsets
			bw.Write(udat_offset);
			bw.Write(this.StringRecordCount);
			bw.Write(hash_offset);
			bw.Write(text_offset);
			bw.WriteNullTermUTF8(this._collection_name, 0x10);
			bw.Write(this._unk_data);

			int length = 0;
			
			foreach (var info in this._stringinfo)
			{
			
				bw.Write(info.Key);
				bw.Write(length);
				length += info.NulledTextLength;
			
			}

			foreach (var info in this._stringinfo)
			{

				bw.WriteNullTermUTF8(info.Text);

			}

			bw.FillBuffer(0x10);
			bw.BaseStream.Position = position - 4;
			bw.Write((int)(bw.BaseStream.Length - position));
			bw.BaseStream.Position = bw.BaseStream.Length;
		}

		/// <summary>
		/// Disassembles array into <see cref="STRBlock"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="STRBlock"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			uint ReaderID = br.ReadUInt32();
			int BlockSize = br.ReadInt32();
			var broffset = br.BaseStream.Position;

			int udatoffset = br.ReadInt32();
			int numentries = br.ReadInt32();
			int hashoffset = br.ReadInt32();
			int textoffset = br.ReadInt32();

			// Read CollectionName
			// Since CollectionNames do not exist in vanilla files, but they do exist
			// in saved files using this library, they are supposed to be located at 
			// offset 0x10, while unknown data at offset 0x20
			string count = this.Manager is null
				? String.Empty
				: this.Manager.Count == 0 ? String.Empty : this.Manager.Count.ToString();
			
			// Since there exists only one at a time
			this._collection_name = $"GLOBAL{count}";

			if (udatoffset > 0x10 && hashoffset > 0x10 && textoffset > 0x10)
			{

				br.BaseStream.Position = broffset + 0x10;
				var cname = br.ReadNullTermUTF8();
				if (!String.IsNullOrEmpty(cname)) this._collection_name = cname;

			}

			// Read unknown data
			var unksize = hashoffset - udatoffset;
			br.BaseStream.Position = broffset + udatoffset;
			this._unk_data = br.ReadBytes(unksize);

			// Begin reading through string records
			for (int loop = 0; loop < numentries; ++loop)
			{
				
				br.BaseStream.Position = broffset + hashoffset + loop * 8;
				
				var info = new StringRecord(this)
				{
					Key = br.ReadUInt32()
				};
				
				br.BaseStream.Position = broffset + textoffset + br.ReadInt32();
				info.Text = br.ReadNullTermUTF8();
				info.Label = info.Key.BinString(LookupReturn.EMPTY);
				this._stringinfo.Add(info);
			
			}

			// Set position to end
			br.BaseStream.Position = broffset + BlockSize;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new STRBlock(CName, this.Manager);

			foreach (var record in this._stringinfo)
			{

				result._stringinfo.Add(record.PlainCopy() as StringRecord);

			}

			return result;
		}

		/// <summary>
		/// Gets the <see cref="StringRecord"/> from the internal list.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to find.</param>
		/// <returns>StringRecord is it exists; otherwise null;</returns>
		public override StringRecord GetRecord(uint key) =>
			this._stringinfo.Find(_ => _.Key == key);

		/// <summary>
		/// Gets all <see cref="StringRecord"/> stored in <see cref="STRBlock"/>.
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"/> of <see cref="StringRecord"/>.</returns>
		public override IEnumerable<StringRecord> GetRecords() => this._stringinfo;

		/// <summary>
		/// Gets text from the binary key of a label provided.
		/// </summary>
		/// <param name="key">Key of the string label.</param>
		/// <returns>Text of the label as a string.</returns>
		public override string GetText(uint key) => base.GetText(key);

		/// <summary>
		/// Adds <see cref="StringRecord"/> in the <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="key">Key of the new <see cref="StringRecord"/></param>
		/// <param name="label">Label of the new <see cref="StringRecord"/></param>
		/// <param name="text">Text of the new <see cref="StringRecord"/></param>
		public override void AddRecord(string key, string label, string text)
		{
			uint hash = key == BaseArguments.AUTO
				? label.BinHash()
				: key.IsHexString()
					? Convert.ToUInt32(key, 16)
					: 0;

			if (hash == 0)
			{

				throw new ArgumentException("Unable to convert string to a hexadecimal key or it equals 0");

			}

			if (this.GetRecord(hash) != null)
			{

				throw new ArgumentException($"String record with key 0x{hash:X8} already exists");

			}

			this._stringinfo.Add(new StringRecord(this)
			{
				Key = hash,
				Label = label,
				Text = text
			});
		}

		/// <summary>
		/// Removes <see cref="StringRecord"/> at the index specified.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		public override void RemoveRecord(uint key)
		{
			var record = this.GetRecord(key);

			if (record == null)
			{

				throw new InfoAccessException($"0x{key:X8}");

			}
			else
			{

				this._stringinfo.Remove(record);

			}
		}

		/// <summary>
		/// Attempts to remove <see cref="StringRecord"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="StringRecord"/> to be removed.</param>
		/// <returns>True if removing was successful; false otherwise.</returns>
		public override void RemoveRecord(string key)
		{
			if (!key.IsHexString())
			{

				throw new ArgumentException($"Value {key} is not a hexadecimal key");

			}
			else
			{

				this.RemoveRecord(Convert.ToUInt32(key, 16));

			}
		}

		/// <summary>
		/// Retrieves all <see cref="StringRecord"/> that have their texts containing text provided.
		/// </summary>
		/// <param name="text">Text that other <see cref="StringRecord"/> should match.</param>
		/// <returns>Enumerable of records containing text provided.</returns>
		public override IEnumerable<StringRecord> FindWithText(string text)
		{
			foreach (var record in this._stringinfo)
			{
			
				if (record.Text?.Contains(text) ?? false)
				{

					yield return record;

				}
			
			}
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="STRBlock"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their BinKey value.
		/// </summary>
		public override void SortRecordsByKey() => this._stringinfo.Sort((x, y) => x.Key.CompareTo(y.Key));

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their Label value.
		/// </summary>
		public override void SortRecordsByLabel() => this._stringinfo.Sort((x, y) => x.Label.CompareTo(y.Label));

		/// <summary>
		/// Sorts all <see cref="StringRecord"/> by their Text value.
		/// </summary>
		public override void SortRecordsByText() => this._stringinfo.Sort((x, y) => x.Text.CompareTo(y.Text));

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(this.StringRecordCount << 5 + this._unk_data.Length))
			using (var writer = new BinaryWriter(ms))
			{

				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this._unk_data.Length);
				writer.Write(this.StringRecordCount);

				writer.Write(this._unk_data);

				for (int loop = 0; loop < this.StringRecordCount; ++loop)
				{

					writer.WriteNullTermUTF8(this._stringinfo[loop].Label);
					writer.WriteNullTermUTF8(this._stringinfo[loop].Text);

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

			this._collection_name = reader.ReadNullTermUTF8();
			var length = reader.ReadInt32();
			var count = reader.ReadInt32();
			this._stringinfo.Capacity = count;

			this._unk_data = reader.ReadBytes(length);

			for (int loop = 0; loop < count; ++loop)
			{

				var info = new StringRecord(this)
				{
					Label = reader.ReadNullTermUTF8(),
					Text = reader.ReadNullTermUTF8()
				};

				info.Key = info.Label.BinHash();
				this._stringinfo.Add(info);

			}
		}

		/// <summary>
		/// Synchronizes all parts of this instance with another instance passed.
		/// </summary>
		/// <param name="other"><see cref="STRBlock"/> to synchronize with.</param>
		internal void Synchronize(STRBlock other)
		{
			var records = new List<StringRecord>(other._stringinfo);

			for (int i = 0; i < this.StringRecordCount; ++i)
			{

				bool found = false;

				for (int j = 0; j < other.StringRecordCount; ++j)
				{

					if (other._stringinfo[j].Key == this._stringinfo[i].Key)
					{

						found = true;
						break;

					}

				}

				if (!found) records.Add(this._stringinfo[i]);

			}

			this._stringinfo = records;

			// Replace unknown data, if any
			if (other._unk_data != null) this._unk_data = other._unk_data;
		}

		#endregion
	}
}