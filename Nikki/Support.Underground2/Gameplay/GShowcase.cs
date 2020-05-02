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
	/// <see cref="GShowcase"/> is a collection of settings related to showcase events.
	/// </summary>
	public class GShowcase : ACollectable
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
				if (value.Length > 0x1F)
					throw new ArgumentLengthException(0x1F);
				if (this.Database.GShowcases.FindCollection(value) != null)
					throw new CollectionExistenceException();
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
		/// Description string for the showcase message.
		/// </summary>
		[AccessModifiable()]
		public string DescStringLabel { get; set; } = String.Empty;

		/// <summary>
		/// Destination trigger of the showcase.
		/// </summary>
		[AccessModifiable()]
		public string DestinationPoint { get; set; } = String.Empty;

		/// <summary>
		/// Descriptive attribute of the showcase.
		/// </summary>
		[AccessModifiable()]
		public string DescAttrib { get; set; } = String.Empty;

		/// <summary>
		/// Method of taking photo.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public eTakePhotoMethod TakePhotoMethod { get; set; }

		/// <summary>
		/// Stage to which this <see cref="GShowcase"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Cash value player gets from completing the showcase.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public short CashValue { get; set; }

		/// <summary>
		/// Required visual rating of a car to unlock this <see cref="GShowcase"/>.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float RequiredVisualRating { get; set; }

		/// <summary>
		/// Unknown value at offset 0x34.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown0x34 { get; set; }

		/// <summary>
		/// Unknown value at offset 0x35.
		/// </summary>
		[AccessModifiable()]
		public byte Unknown0x35 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GShowcase"/>.
		/// </summary>
		public GShowcase() { }

		/// <summary>
		/// Initializes new instance of <see cref="GShowcase"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public GShowcase(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GShowcase"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public unsafe GShowcase(BinaryReader br, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~GShowcase() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GShowcase"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GShowcase"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			// Write CollectionName
			strw.WriteNullTermUTF8(this._collection_name);
			bw.WriteNullTermUTF8(this._collection_name, 0x20);

			// Write settings
			bw.Write(this.BinKey);
			bw.WriteEnum(this.TakePhotoMethod);
			bw.Write(this.BelongsToStage);
			bw.Write(this.CashValue);
			bw.Write(this.DescStringLabel.BinHash());
			bw.Write(this.DestinationPoint.BinHash());
			bw.Write((int)0);
			bw.Write(this.Unknown0x34);
			bw.Write(this.Unknown0x35);
			bw.Write((short)0);
			bw.Write(this.DescAttrib.BinHash());
			bw.Write(this.RequiredVisualRating);
		}

		/// <summary>
		/// Disassembles array into <see cref="GShowcase"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GShowcase"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			// Collection Name
			this._collection_name = br.ReadNullTermUTF8(0x20);

			// Take Photo Settings
			this.TakePhotoMethod = br.ReadEnum<eTakePhotoMethod>();
			this.BelongsToStage = br.ReadByte();
			this.CashValue = br.ReadInt16();
			this.DescStringLabel = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DestinationPoint = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.Unknown0x34 = br.ReadByte();
			this.Unknown0x35 = br.ReadByte();
			br.BaseStream.Position += 2;
			this.DescAttrib = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RequiredVisualRating = br.ReadSingle();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new GShowcase(CName, this.Database)
			{
				DescAttrib = this.DescAttrib,
				DescStringLabel = this.DescStringLabel,
				DestinationPoint = this.DestinationPoint,
				TakePhotoMethod = this.TakePhotoMethod,
				BelongsToStage = this.BelongsToStage,
				CashValue = this.CashValue,
				RequiredVisualRating = this.RequiredVisualRating,
				Unknown0x34 = this.Unknown0x34,
				Unknown0x35 = this.Unknown0x35
			};

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerBrand"/> 
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