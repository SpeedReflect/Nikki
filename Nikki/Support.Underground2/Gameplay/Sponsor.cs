using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="Sponsor"/> is a collection of settings related to sponsors and contracts.
	/// </summary>
	public class Sponsor : ACollectable
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
				if (this.Database.Sponsors.FindCollection(value) != null)
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
		/// First required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eSponsorRaceType ReqSponsorRace1 { get; set; }

		/// <summary>
		/// Second required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eSponsorRaceType ReqSponsorRace2 { get; set; }

		/// <summary>
		/// Third required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eSponsorRaceType ReqSponsorRace3 { get; set; }

		/// <summary>
		/// Cash value player gets per winning in a sponsor race.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short CashValuePerWin { get; set; }

		/// <summary>
		/// Cash value player gets when signing contract with this <see cref="Sponsor"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short SignCashBonus { get; set; }

		/// <summary>
		/// Potential cash value player can get when signing with this <see cref="Sponsor"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short PotentialCashBonus { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		public Sponsor() { }

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public Sponsor(string CName, Database.Underground2 db)
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
		public Sponsor(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br, strr);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~Sponsor() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Sponsor"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Sponsor"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			bw.Write((ushort)strw.BaseStream.Position);
			strw.Write(this._collection_name);

			bw.Write(this.CashValuePerWin);
			bw.WriteEnum(this.ReqSponsorRace1);
			bw.WriteEnum(this.ReqSponsorRace2);
			bw.WriteEnum(this.ReqSponsorRace3);
			bw.Write((byte)0);
			bw.Write(this.BinKey);
			bw.Write(this.SignCashBonus);
			bw.Write(this.PotentialCashBonus);
		}

		/// <summary>
		/// Disassembles array into <see cref="Sponsor"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Sponsor"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			// CollectionName
			var position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			// Cash Value
			this.CashValuePerWin = br.ReadInt16();
			
			// Required Sponsor Races
			this.ReqSponsorRace1 = br.ReadEnum<eSponsorRaceType>();
			this.ReqSponsorRace2 = br.ReadEnum<eSponsorRaceType>();
			this.ReqSponsorRace3 = br.ReadEnum<eSponsorRaceType>();

			// Signing values
			br.BaseStream.Position += 5;
			this.SignCashBonus = br.ReadInt16();
			this.PotentialCashBonus = br.ReadInt16();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new Sponsor(CName, this.Database);
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