using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Reflection.Exception;



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
				if (this.Database.BankTriggers.FindCollection(value) != null)
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
		/// Cash value that player gets when collecting the trigger.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public ushort CashValue { get; set; }

		/// <summary>
		/// True if initially unlocked; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eBoolean InitiallyUnlocked { get; set; }

		/// <summary>
		/// Index of the trigger.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte BankIndex { get; set; }

		/// <summary>
		/// Requires stages completed in order to be unlocked.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
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
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public BankTrigger(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="BankTrigger"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public BankTrigger(BinaryReader br, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~BankTrigger() { }

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
			this._collection_name = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new BankTrigger(CName, this.Database)
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
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}