using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="SMSMessage"/> is a collection of settings related to world messages.
	/// </summary>
	public class SMSMessage : ACollectable
	{
		#region Fields

		private string _collection_name;

		[MemoryCastable()]
		private byte b0x02;

		[MemoryCastable()]
		private byte b0x03;

		[MemoryCastable()]
		private byte b0x04;

		[MemoryCastable()]
		private byte b0x05;

		[MemoryCastable()]
		private byte b0x06;

		[MemoryCastable()]
		private byte b0x07;

		[MemoryCastable()]
		private byte b0x08;

		[MemoryCastable()]
		private byte b0x09;

		[MemoryCastable()]
		private byte b0x0A;

		[MemoryCastable()]
		private byte b0x0B;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Underground2 Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (this.Database.SMSMessages.FindCollection(value) != null)
					throw new CollectionExistenceException(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Cash value player gets when receiving the message.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public int CashValue { get; set; }

		/// <summary>
		/// String label of the sender of the message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string MessageSenderLabel { get; set; } = String.Empty;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		public SMSMessage() { }

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public SMSMessage(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public unsafe SMSMessage(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br, strr);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~SMSMessage() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="SMSMessage"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SMSMessage"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			bw.Write((ushort)strw.BaseStream.Position);
			strw.WriteNullTermUTF8(this._collection_name);

			bw.Write(this.b0x02);
			bw.Write(this.b0x03);
			bw.Write(this.b0x04);
			bw.Write(this.b0x05);
			bw.Write(this.b0x06);
			bw.Write(this.b0x07);
			bw.Write(this.b0x08);
			bw.Write(this.b0x09);
			bw.Write(this.b0x0A);
			bw.Write(this.b0x0B);

			bw.Write(this.CashValue);
			bw.Write(this.MessageSenderLabel.BinHash());
		}

		/// <summary>
		/// Disassembles array into <see cref="SMSMessage"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="SMSMessage"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			// CollectionName
			var position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			// Unknown Yet Values
			this.b0x02 = br.ReadByte();
			this.b0x03 = br.ReadByte();
			this.b0x04 = br.ReadByte();
			this.b0x05 = br.ReadByte();
			this.b0x06 = br.ReadByte();
			this.b0x07 = br.ReadByte();
			this.b0x08 = br.ReadByte();
			this.b0x09 = br.ReadByte();
			this.b0x0A = br.ReadByte();
			this.b0x0B = br.ReadByte();

			// Cash and Sender
			this.CashValue = br.ReadInt32();
			this.MessageSenderLabel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new SMSMessage(CName, this.Database);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerRace"/> 
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