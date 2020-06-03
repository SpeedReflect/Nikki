using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection;
using Nikki.Reflection.ID;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.STRParts;
using CoreExtensions.IO;
using CoreExtensions.Text;



namespace Nikki.Support.Underground1.Class
{
	/// <summary>
	/// <see cref="STRBlock"/> is a collection of language strings, hashes and labels.
	/// </summary>
	public class STRBlock : Shared.Class.STRBlock
	{
		#region Fields

		private string _collection_name;
		private byte[] _unk_data;
		private List<StringRecord> _stringinfo = new List<StringRecord>();

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
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground1.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Underground1 Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (value.Length > MaxCNameLength)
					throw new ArgumentLengthException(MaxCNameLength);
				if (this.Database.STRBlocks.FindCollection(value) != null)
					throw new CollectionExistenceException(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Length of the string information array.
		/// </summary>
		public override int InfoLength => this._stringinfo.Count;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		public STRBlock() { }

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public STRBlock(string CName, Database.Underground1 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="STRBlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read text data with.</param>
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public STRBlock(BinaryReader br, Database.Underground1 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~STRBlock() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="STRBlock"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="STRBlock"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			var udat_offset = 0x30;
			var hash_offset = udat_offset + this._unk_data.Length;
			var text_offset = hash_offset + this.InfoLength * 8;

			// Sort records by keys
			this._stringinfo.Sort((a, b) => a.Key.CompareTo(b.Key));

			// Write ID and temporary size
			bw.Write(Global.STRBlocks);
			bw.Write(-1);

			// Save position
			var position = bw.BaseStream.Position;

			// Write offsets
			bw.Write(udat_offset);
			bw.Write(this.InfoLength);
			bw.Write(hash_offset);
			bw.Write(text_offset);
			bw.WriteNullTermUTF8(this.Watermark, 0x20);
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
			this._collection_name = "GLOBAL"; // since there exists only one at a time

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
				info.Label = info.Key.BinString(eLookupReturn.EMPTY);
				this._stringinfo.Add(info);

			}

			// Set position to end
			br.BaseStream.Position = broffset + BlockSize;
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
				: label.IsHexString()
					? Convert.ToUInt32(label, 16)
					: 0;

			if (hash == 0)
			{

				throw new ArgumentException("Unable to convert string to a hexadecimal key or it equals 0");

			}

			if (this.GetRecord(hash) != null)
			{

				throw new InfoAccessException($"String record with key 0x{hash:X8} already exists");

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

				throw new InfoAccessException($"String record with key 0x{key:X8} does not exist");

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
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}