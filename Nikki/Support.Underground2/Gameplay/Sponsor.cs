using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="Sponsor"/> is a collection of settings related to sponsors and contracts.
	/// </summary>
	public class Sponsor : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="Sponsor"/> race types.
		/// </summary>
		public enum SponsorRaceType : byte
		{
			/// <summary>
			/// No race type.
			/// </summary>
			None = 0,

			/// <summary>
			/// Circuit race type.
			/// </summary>
			Circuit = 1,

			/// <summary>
			/// Drag race type.
			/// </summary>
			Drift = 2,

			/// <summary>
			/// Drag race type.
			/// </summary>
			Drag = 3,

			/// <summary>
			/// Sprint race type.
			/// </summary>
			Sprint = 4,

			/// <summary>
			/// StreetX race type.
			/// </summary>
			StreetX = 5,
		}

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
				if (this.Career.GetCollection(value, nameof(this.Career.Sponsors)) != null)
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
		/// First required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public SponsorRaceType ReqSponsorRace1 { get; set; }

		/// <summary>
		/// Second required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public SponsorRaceType ReqSponsorRace2 { get; set; }

		/// <summary>
		/// Third required sponsor race to win.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public SponsorRaceType ReqSponsorRace3 { get; set; }

		/// <summary>
		/// Cash value player gets per winning in a sponsor race.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short CashValuePerWin { get; set; }

		/// <summary>
		/// Cash value player gets when signing contract with this <see cref="Sponsor"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short SignCashBonus { get; set; }

		/// <summary>
		/// Potential cash value player can get when signing with this <see cref="Sponsor"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
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
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public Sponsor(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="SMSMessage"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public Sponsor(BinaryReader br, BinaryReader strr, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br, strr);
		}

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
			strw.WriteNullTermUTF8(this._collection_name);

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
			this.ReqSponsorRace1 = br.ReadEnum<SponsorRaceType>();
			this.ReqSponsorRace2 = br.ReadEnum<SponsorRaceType>();
			this.ReqSponsorRace3 = br.ReadEnum<SponsorRaceType>();

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
		public override Collectable MemoryCast(string CName)
		{
			var result = new Sponsor(CName, this.Career);
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
			bw.WriteNullTermUTF8(this._collection_name);
			bw.Write(this.CashValuePerWin);
			bw.WriteEnum(this.ReqSponsorRace1);
			bw.WriteEnum(this.ReqSponsorRace2);
			bw.WriteEnum(this.ReqSponsorRace3);
			bw.Write(this.SignCashBonus);
			bw.Write(this.PotentialCashBonus);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.CashValuePerWin = br.ReadInt16();
			this.ReqSponsorRace1 = br.ReadEnum<SponsorRaceType>();
			this.ReqSponsorRace2 = br.ReadEnum<SponsorRaceType>();
			this.ReqSponsorRace3 = br.ReadEnum<SponsorRaceType>();
			this.SignCashBonus = br.ReadInt16();
			this.PotentialCashBonus = br.ReadInt16();
		}

		#endregion
	}
}