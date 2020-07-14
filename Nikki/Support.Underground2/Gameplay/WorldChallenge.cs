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
	/// <see cref="Sponsor"/> is a collection of settings related to world challenge events.
	/// </summary>
	public class WorldChallenge : ACollectable
	{
		#region Fields

		private string _collection_name;

		[MemoryCastable()]
		private byte _padding0;

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
				if (this.Database.WorldChallenges.FindCollection(value) != null)
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
		/// Event trigger of this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string WorldChallengeTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Stage to which this challenge belongs to.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// True if challenge requires specific number of outruns won in order to unlock; 
		/// false otherwise.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eBoolean UseOutrunsAsReqRaces { get; set; }

		/// <summary>
		/// Required races won to unlock this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte RequiredRacesWon { get; set; }

		/// <summary>
		/// Label of the SMS sent when challenge is unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string ChallengeSMSLabel { get; set; } = String.Empty;

		/// <summary>
		/// Parent, or destination in this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string ChallengeParent { get; set; } = String.Empty;
		
		/// <summary>
		/// Time limit to complete this challenge.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public int TimeLimit { get; set; }

		/// <summary>
		/// Type of the challenge.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public eWorldChallengeType WorldChallengeType { get; set; }

		/// <summary>
		/// Index of the first unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte UnlockablePart1_Index { get; set; }

		/// <summary>
		/// Index of the second unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte UnlockablePart2_Index { get; set; }

		/// <summary>
		/// Index of the third unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public byte UnlockablePart3_Index { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		public WorldChallenge() { }

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public WorldChallenge(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public WorldChallenge(BinaryReader br, BinaryReader strr, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br, strr);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~WorldChallenge() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Sponsor"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Sponsor"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			// CollectionName
			bw.Write((ushort)strw.BaseStream.Position);
			strw.WriteNullTermUTF8(this._collection_name);

			// World Trigger
			bw.Write((ushort)strw.BaseStream.Position);
			strw.WriteNullTermUTF8(this.WorldChallengeTrigger);

			// All settings
			bw.Write(this.BelongsToStage);
			bw.Write(this._padding0);
			bw.Write((byte)((byte)this.UseOutrunsAsReqRaces * 2));
			bw.Write(this.RequiredRacesWon);
			bw.Write(this.ChallengeSMSLabel.BinHash());
			bw.Write(this.ChallengeParent.BinHash());
			bw.Write(this.TimeLimit);
			bw.WriteEnum(this.WorldChallengeType);
			bw.Write(this.UnlockablePart1_Index);
			bw.Write(this.UnlockablePart2_Index);
			bw.Write(this.UnlockablePart3_Index);
		}

		/// <summary>
		/// Disassembles array into <see cref="Sponsor"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Sponsor"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			ushort position = 0;

			// Collection Name
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			// Challenge Trigger
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.WorldChallengeTrigger = strr.ReadNullTermUTF8();

			// Stage and Unlock settings
			this.BelongsToStage = br.ReadByte();
			this._padding0 = br.ReadByte();
			this.UseOutrunsAsReqRaces = (br.ReadByte() == 2) ? eBoolean.True : eBoolean.False;
			this.RequiredRacesWon = br.ReadByte();

			// Hashes
			this.ChallengeSMSLabel = br.ReadUInt32().BinString(eLookupReturn.EMPTY); // unlock sms
			this.ChallengeParent = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			// Time Limit
			this.TimeLimit = br.ReadInt32();

			// Type and Unlockables
			this.WorldChallengeType = br.ReadEnum<eWorldChallengeType>();
			this.UnlockablePart1_Index = br.ReadByte();
			this.UnlockablePart2_Index = br.ReadByte();
			this.UnlockablePart3_Index = br.ReadByte();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new WorldChallenge(CName, this.Database);
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