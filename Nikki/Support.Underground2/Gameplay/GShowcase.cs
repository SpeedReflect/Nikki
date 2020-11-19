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
	/// <see cref="GShowcase"/> is a collection of settings related to showcase events.
	/// </summary>
	public class GShowcase : Collectable
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of taking photo methods in <see cref="GShowcase"/>.
		/// </summary>
		public enum TakePhotoMethod : byte
		{
			/// <summary>
			/// Take photo by yourself for magazine.
			/// </summary>
			MagazineYourself = 1,

			/// <summary>
			/// Take photo by yourself for dvd.
			/// </summary>
			DVDYourself = 2,

			/// <summary>
			/// Take photo in-place for magazine.
			/// </summary>
			MagazineAuto = 3,

			/// <summary>
			/// Take photo in-place for dvd.
			/// </summary>
			DVDAuto = 4,
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
				if (this.Career.GetCollection(value, nameof(this.Career.GShowcases)) != null)
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
		/// Description string for the showcase message.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string DescStringLabel { get; set; } = String.Empty;

		/// <summary>
		/// Destination trigger of the showcase.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string DestinationPoint { get; set; } = String.Empty;

		/// <summary>
		/// Descriptive attribute of the showcase.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string DescAttrib { get; set; } = String.Empty;

		/// <summary>
		/// Method of taking photo.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public TakePhotoMethod TakePhotoType { get; set; }

		/// <summary>
		/// Stage to which this <see cref="GShowcase"/> belongs to.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// Cash value player gets from completing the showcase.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public short CashValue { get; set; }

		/// <summary>
		/// Required visual rating of a car to unlock this <see cref="GShowcase"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public float RequiredVisualRating { get; set; }

		/// <summary>
		/// Unknown value at offset 0x34.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte Unknown0x34 { get; set; }

		/// <summary>
		/// True if this <see cref="GShowcase"/> is required to complete; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean RequiredToComplete { get; set; }

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
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GShowcase(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GShowcase"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GShowcase(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
		}

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
			bw.WriteEnum(this.TakePhotoType);
			bw.Write(this.BelongsToStage);
			bw.Write(this.CashValue);
			bw.Write(this.DescStringLabel.BinHash());
			bw.Write(this.DestinationPoint.BinHash());
			bw.Write((int)0);
			bw.Write(this.Unknown0x34);
			bw.WriteEnum(this.RequiredToComplete);
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
			br.BaseStream.Position += 4;
			this.TakePhotoType = br.ReadEnum<TakePhotoMethod>();
			this.BelongsToStage = br.ReadByte();
			this.CashValue = br.ReadInt16();
			this.DescStringLabel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DestinationPoint = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			br.BaseStream.Position += 4;
			this.Unknown0x34 = br.ReadByte();
			this.RequiredToComplete = br.ReadEnum<eBoolean>();
			br.BaseStream.Position += 2;
			this.DescAttrib = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RequiredVisualRating = br.ReadSingle();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new GShowcase(CName, this.Career);
			base.MemoryCast(this, result);
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
			bw.WriteEnum(this.TakePhotoType);
			bw.Write(this.BelongsToStage);
			bw.Write(this.CashValue);
			bw.WriteNullTermUTF8(this.DescStringLabel);
			bw.WriteNullTermUTF8(this.DestinationPoint);
			bw.Write(this.Unknown0x34);
			bw.WriteEnum(this.RequiredToComplete);
			bw.WriteNullTermUTF8(this.DescAttrib);
			bw.Write(this.RequiredVisualRating);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.TakePhotoType = br.ReadEnum<TakePhotoMethod>();
			this.BelongsToStage = br.ReadByte();
			this.CashValue = br.ReadInt16();
			this.DescStringLabel = br.ReadNullTermUTF8();
			this.DestinationPoint = br.ReadNullTermUTF8();
			this.Unknown0x34 = br.ReadByte();
			this.RequiredToComplete = br.ReadEnum<eBoolean>();
			this.DescAttrib = br.ReadNullTermUTF8();
			this.RequiredVisualRating = br.ReadSingle();
		}

		#endregion
	}
}