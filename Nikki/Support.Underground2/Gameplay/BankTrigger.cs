using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="BankTrigger"/> is a collection of settings related to cash zone triggers.
	/// </summary>
	public class BankTrigger : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// GCareer to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public GCareer Career { get; set; }

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
				if (String.IsNullOrWhiteSpace(value))
				{

					throw new ArgumentNullException("This value cannot be left empty.");

				}
				if (value.Contains(' '))
				{

					throw new Exception("CollectionName cannot contain whitespace.");

				}
				if (this.Career.GetCollection(value, nameof(this.Career.BankTriggers)) != null)
				{

					throw new CollectionExistenceException(value);

				}

				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Cash value that player gets when collecting the trigger.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public ushort CashValue { get; set; }

		/// <summary>
		/// True if initially unlocked; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public eBoolean InitiallyUnlocked { get; set; }

		/// <summary>
		/// Index of the trigger.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BankIndex { get; set; }

		/// <summary>
		/// Requires stages completed in order to be unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public int RequiredStagesCompleted { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="BankTrigger"/>.
		/// </summary>
		public BankTrigger() { }

		/// <summary>
		/// Initializes new instance of <see cref="BankTrigger"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public BankTrigger(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="BankTrigger"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public BankTrigger(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="BankTrigger"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="BankTrigger"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(this.CashValue);
			bw.Write(this.InitiallyUnlocked == eBoolean.True ? (byte)0 : (byte)1);
			bw.Write(this.BankIndex);
			bw.Write(this.RequiredStagesCompleted);
			bw.Write(this.BinKey);
		}

		/// <summary>
		/// Disassembles array into <see cref="BankTrigger"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="BankTrigger"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			this.CashValue = br.ReadUInt16();
			this.InitiallyUnlocked = br.ReadByte() == 0 ? eBoolean.True : eBoolean.False;
			this.BankIndex = br.ReadByte();
			this.RequiredStagesCompleted = br.ReadInt32();
			this._collection_name = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new BankTrigger(CName, this.Career)
			{
				BankIndex = this.BankIndex,
				InitiallyUnlocked = this.InitiallyUnlocked,
				CashValue = this.CashValue,
				RequiredStagesCompleted = this.RequiredStagesCompleted
			};

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="BankTrigger"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Serialize(BinaryWriter bw)
		{
			bw.Write(this.CashValue);
			bw.WriteEnum(this.InitiallyUnlocked);
			bw.Write(this.BankIndex);
			bw.Write(this.RequiredStagesCompleted);
			bw.WriteNullTermUTF8(this._collection_name);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this.CashValue = br.ReadUInt16();
			this.InitiallyUnlocked = br.ReadEnum<eBoolean>();
			this.BankIndex = br.ReadByte();
			this.RequiredStagesCompleted = br.ReadInt32();
			this._collection_name = br.ReadNullTermUTF8();
		}

		#endregion
	}
}