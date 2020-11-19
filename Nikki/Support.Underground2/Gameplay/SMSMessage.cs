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
	/// <see cref="SMSMessage"/> is a collection of settings related to world messages.
	/// </summary>
	public class SMSMessage : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Enums

		/// <summary>
		/// Unlock type of an <see cref="SMSMessage"/>.
		/// </summary>
		public enum UnlockType : byte
		{
			/// <summary>
			/// Invalid unlock type.
			/// </summary>
			Invalid = 0x0,
			
			/// <summary>
			/// Requires specific race won.
			/// </summary>
			SpecificRaceWon = 0x1,

			/// <summary>
			/// Requires shop being found.
			/// </summary>
			ShopFound = 0x2,

			/// <summary>
			/// Requires specific game time elapsed.
			/// </summary>
			TimeElapsed = 0x3,

			/// <summary>
			/// Requires outrun being engaged.
			/// </summary>
			OutrunEngaged = 0x4,

			/// <summary>
			/// Requires victory in an outrun race.
			/// </summary>
			OutrunVictory = 0x5,

			/// <summary>
			/// Requires defeat in an outrun race.
			/// </summary>
			OutrunDefeat = 0x6,

			/// <summary>
			/// Requires specific number of sponsor races won.
			/// </summary>
			ReqSponRacesWon = 0x9,

			/// <summary>
			/// Requires specific number of URL races won.
			/// </summary>
			ReqURLRacesWon = 0xA,

			/// <summary>
			/// Requires specific number of regular races won.
			/// </summary>
			ReqRegRacesWon = 0xC,

			/// <summary>
			/// Requires a magazine being unlocked.
			/// </summary>
			MagazineUnlock = 0xD,

			/// <summary>
			/// Requires a DVD being unlocked.
			/// </summary>
			DVDUnlock = 0xE,

			/// <summary>
			/// Requires being found in the freeroam.
			/// </summary>
			FreeroamFind = 0xF,
		}

		/// <summary>
		/// Mail type of an <see cref="SMSMessage"/>.
		/// </summary>
		public enum MailType : byte
		{
			/// <summary>
			/// Invalid mail type.
			/// </summary>
			Invalid = 0,

			/// <summary>
			/// Message is an inbox mail.
			/// </summary>
			Inbox = 1,

			/// <summary>
			/// Message is a game tip.
			/// </summary>
			GameTips = 2,

			/// <summary>
			/// Message is about a showcase.
			/// </summary>
			Showcase = 3,

			/// <summary>
			/// Message is from Rachel.
			/// </summary>
			Rachel = 4,

			/// <summary>
			/// Message is about an unlock.
			/// </summary>
			Unlock = 5,
		}

		/// <summary>
		/// Icon type of an <see cref="SMSMessage"/>.
		/// </summary>
		public enum IconType : byte
		{
			/// <summary>
			/// Invalid icon type.
			/// </summary>
			Invalid = 0,

			/// <summary>
			/// Rachel/Yellow icon type.
			/// </summary>
			Rachel = 1,

			/// <summary>
			/// Opponent/Orange icon type.
			/// </summary>
			Opponent = 2,

			/// <summary>
			/// Showcase/Star icon type.
			/// </summary>
			Showcase = 3,

			/// <summary>
			/// Outrun/Red icon type.
			/// </summary>
			Outrun = 4,
			
			/// <summary>
			/// Shop/Magenta icon type.
			/// </summary>
			Shop = 5,

			/// <summary>
			/// Tips/Blue icon type.
			/// </summary>
			Tips = 6,

			/// <summary>
			/// Race/Purple icon type.
			/// </summary>
			Race = 7,

			/// <summary>
			/// Mail/Green icon type.
			/// </summary>
			Mail = 8,

			/// <summary>
			/// Freeroam/Green icon type.
			/// </summary>
			Freeroam = 9,
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
				if (this.Career.GetCollection(value, nameof(this.Career.SMSMessages)) != null)
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
		/// Unlock requirement for this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public UnlockType UnlockRequirement { get; set; }

		/// <summary>
		/// Storage type of this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public MailType StorageType { get; set; } 

		/// <summary>
		/// True if message should be automatically opened; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean AutoOpen { get; set; }

		/// <summary>
		/// Icon style of this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public IconType IconStyle { get; set; }

		/// <summary>
		/// Career identifier for this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short CareerIdentifier { get; set; }

		/// <summary>
		/// Required specific race won to unlock this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string RequiredSpecRaceWon { get; set; } = String.Empty;

		/// <summary>
		/// Required shop found to unlock this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string RequiredShopFound { get; set; } = String.Empty;

		/// <summary>
		/// Required time elapsed to unlock this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int RequiredTimeElapsed { get; set; }

		/// <summary>
		/// Required number of races won to unlock this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short RequiredRacesWon { get; set; }

		/// <summary>
		/// Required DVD number unlocked to unlock this message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int RequiredDVDUnlocked { get; set; }

		/// <summary>
		/// Freeroam engage trigger of the message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string FreeroamTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Stage to which this message belongs to.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short BelongsToStage { get; set; }

		/// <summary>
		/// Cash value player gets when receiving the message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int CashValue { get; set; }

		/// <summary>
		/// String label of the sender of the message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string MessageSender { get; set; } = String.Empty;

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
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public SMSMessage(string CName, GCareer career)
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
		public SMSMessage(BinaryReader br, BinaryReader strr, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br, strr);
		}

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

			bw.WriteEnum(this.UnlockRequirement);
			bw.WriteEnum(this.StorageType);
			bw.WriteEnum(this.AutoOpen);
			bw.WriteEnum(this.IconStyle);
			bw.Write(this.CareerIdentifier);
			
			switch (this.UnlockRequirement)
			{
				case UnlockType.SpecificRaceWon:
					bw.Write(this.RequiredSpecRaceWon.BinHash());
					break;

				case UnlockType.ShopFound:
					bw.Write(this.RequiredShopFound.BinHash());
					break;

				case UnlockType.TimeElapsed:
					bw.Write(this.RequiredTimeElapsed);
					break;

				case UnlockType.ReqSponRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqRegRacesWon:
					bw.Write(this.RequiredRacesWon);
					bw.Write(this.BelongsToStage);
					break;

				case UnlockType.DVDUnlock:
					bw.Write(this.RequiredDVDUnlocked);
					break;

				case UnlockType.FreeroamFind:
					bw.Write(this.FreeroamTrigger.BinHash());
					break;

				default:
					bw.Write((int)0);
					break;

			}

			bw.Write(this.CashValue);
			bw.Write(this.MessageSender.BinHash());
		}

		/// <summary>
		/// Disassembles array into <see cref="SMSMessage"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="SMSMessage"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			var position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			this.UnlockRequirement = br.ReadEnum<UnlockType>();
			this.StorageType = br.ReadEnum<MailType>();
			this.AutoOpen = br.ReadEnum<eBoolean>();
			this.IconStyle = br.ReadEnum<IconType>();
			this.CareerIdentifier = br.ReadInt16();

			switch (this.UnlockRequirement)
			{
				case UnlockType.SpecificRaceWon:
					this.RequiredSpecRaceWon = br.ReadUInt32().BinString(LookupReturn.EMPTY);
					break;

				case UnlockType.ShopFound:
					this.RequiredShopFound = br.ReadUInt32().BinString(LookupReturn.EMPTY);
					break;

				case UnlockType.TimeElapsed:
					this.RequiredTimeElapsed = br.ReadInt32();
					break;

				case UnlockType.ReqSponRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqRegRacesWon:
					this.RequiredRacesWon = br.ReadInt16();
					this.BelongsToStage = br.ReadInt16();
					break;

				case UnlockType.DVDUnlock:
					this.RequiredDVDUnlocked = br.ReadInt32();
					break;

				case UnlockType.FreeroamFind:
					this.FreeroamTrigger = br.ReadUInt32().BinString(LookupReturn.EMPTY);
					break;

				default:
					br.BaseStream.Position += 4;
					break;

			}

			this.CashValue = br.ReadInt32();
			this.MessageSender = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new SMSMessage(CName, this.Career);
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

			bw.WriteEnum(this.UnlockRequirement);
			bw.WriteEnum(this.StorageType);
			bw.WriteEnum(this.AutoOpen);
			bw.WriteEnum(this.IconStyle);
			bw.Write(this.CareerIdentifier);

			switch (this.UnlockRequirement)
			{
				case UnlockType.SpecificRaceWon:
					bw.WriteNullTermUTF8(this.RequiredSpecRaceWon);
					break;

				case UnlockType.ShopFound:
					bw.WriteNullTermUTF8(this.RequiredShopFound);
					break;

				case UnlockType.TimeElapsed:
					bw.Write(this.RequiredTimeElapsed);
					break;

				case UnlockType.ReqSponRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqRegRacesWon:
					bw.Write(this.RequiredRacesWon);
					bw.Write(this.BelongsToStage);
					break;

				case UnlockType.DVDUnlock:
					bw.Write(this.RequiredDVDUnlocked);
					break;

				case UnlockType.FreeroamFind:
					bw.WriteNullTermUTF8(this.FreeroamTrigger);
					break;

				default:
					break;

			}

			bw.Write(this.CashValue);
			bw.WriteNullTermUTF8(this.MessageSender);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();

			this.UnlockRequirement = br.ReadEnum<UnlockType>();
			this.StorageType = br.ReadEnum<MailType>();
			this.AutoOpen = br.ReadEnum<eBoolean>();
			this.IconStyle = br.ReadEnum<IconType>();
			this.CareerIdentifier = br.ReadInt16();

			switch (this.UnlockRequirement)
			{
				case UnlockType.SpecificRaceWon:
					this.RequiredSpecRaceWon = br.ReadNullTermUTF8();
					break;

				case UnlockType.ShopFound:
					this.RequiredShopFound = br.ReadNullTermUTF8();
					break;

				case UnlockType.TimeElapsed:
					this.RequiredTimeElapsed = br.ReadInt32();
					break;

				case UnlockType.ReqSponRacesWon:
				case UnlockType.ReqURLRacesWon:
				case UnlockType.ReqRegRacesWon:
					this.RequiredRacesWon = br.ReadInt16();
					this.BelongsToStage = br.ReadInt16();
					break;

				case UnlockType.DVDUnlock:
					this.RequiredDVDUnlocked = br.ReadInt32();
					break;

				case UnlockType.FreeroamFind:
					this.FreeroamTrigger = br.ReadNullTermUTF8();
					break;

				default:
					break;

			}

			this.CashValue = br.ReadInt32();
			this.MessageSender = br.ReadNullTermUTF8();
		}

		#endregion
	}
}