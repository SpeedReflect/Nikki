using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.SunParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Class
{
	/// <summary>
	/// <see cref="SunInfo"/> is a collection of sun and daylight settings.
	/// </summary>
	public class SunInfo : Shared.Class.SunInfo
	{
		#region Fields

		private string _collection_name;

		/// <summary>
		/// Maximum length of the CollectionName.
		/// </summary>
		public const int MaxCNameLength = 0x17;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x8;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = 0x110;

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
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (value.Length > MaxCNameLength)
					throw new ArgumentLengthException(MaxCNameLength);
				if (this.Database.SunInfos.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Sun layer 1.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER1 { get; set; }

		/// <summary>
		/// Sun layer 2.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER2 { get; set; }

		/// <summary>
		/// Sun layer 3.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER3 { get; set; }

		/// <summary>
		/// Sun layer 4.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER4 { get; set; }

		/// <summary>
		/// Sun layer 5.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER5 { get; set; }

		/// <summary>
		/// Sun layer 6.
		/// </summary>
		[Expandable("SunLayers")]
		public SunLayer SUNLAYER6 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		public SunInfo() { }

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public SunInfo(string CName, Database.Underground1 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.SUNLAYER1 = new SunLayer();
			this.SUNLAYER2 = new SunLayer();
			this.SUNLAYER3 = new SunLayer();
			this.SUNLAYER4 = new SunLayer();
			this.SUNLAYER5 = new SunLayer();
			this.SUNLAYER6 = new SunLayer();
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground1"/> to which this instance belongs to.</param>
		public SunInfo(BinaryReader br, Database.Underground1 db)
		{
			this.Database = db;
			this.SUNLAYER1 = new SunLayer();
			this.SUNLAYER2 = new SunLayer();
			this.SUNLAYER3 = new SunLayer();
			this.SUNLAYER4 = new SunLayer();
			this.SUNLAYER5 = new SunLayer();
			this.SUNLAYER6 = new SunLayer();
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~SunInfo() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="SunInfo"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SunInfo"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			// Write Version, BinKey and CollectionName
			bw.Write(this.Version);
			bw.Write(this.BinKey);
			bw.WriteNullTermUTF8(this._collection_name, 0x18);

			// Write Positions
			bw.Write(this.PositionX);
			bw.Write(this.PositionY);
			bw.Write(this.PositionZ);
			bw.Write(this.CarShadowPositionX);
			bw.Write(this.CarShadowPositionY);
			bw.Write(this.CarShadowPositionZ);

			// Write Layers
			this.SUNLAYER1.Write(bw);
			this.SUNLAYER2.Write(bw);
			this.SUNLAYER3.Write(bw);
			this.SUNLAYER4.Write(bw);
			this.SUNLAYER5.Write(bw);
			this.SUNLAYER6.Write(bw);
		}

		/// <summary>
		/// Disassembles array into <see cref="SunInfo"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="SunInfo"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			// Read Version and CollectionName
			this.Version = br.ReadInt32();
			br.BaseStream.Position += 4;
			this._collection_name = br.ReadNullTermUTF8(0x18);

			// Read Positions
			this.PositionX = br.ReadSingle();
			this.PositionY = br.ReadSingle();
			this.PositionZ = br.ReadSingle();
			this.CarShadowPositionX = br.ReadSingle();
			this.CarShadowPositionY = br.ReadSingle();
			this.CarShadowPositionZ = br.ReadSingle();

			// Read Layers
			this.SUNLAYER1.Read(br);
			this.SUNLAYER2.Read(br);
			this.SUNLAYER3.Read(br);
			this.SUNLAYER4.Read(br);
			this.SUNLAYER5.Read(br);
			this.SUNLAYER6.Read(br);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new SunInfo(CName, this.Database)
			{
				Version = this.Version,
				PositionX = this.PositionX,
				PositionY = this.PositionY,
				PositionZ = this.PositionZ,
				CarShadowPositionX = this.CarShadowPositionX,
				CarShadowPositionY = this.CarShadowPositionY,
				CarShadowPositionZ = this.CarShadowPositionZ,
				SUNLAYER1 = this.SUNLAYER1.PlainCopy(),
				SUNLAYER2 = this.SUNLAYER2.PlainCopy(),
				SUNLAYER3 = this.SUNLAYER3.PlainCopy(),
				SUNLAYER4 = this.SUNLAYER4.PlainCopy(),
				SUNLAYER5 = this.SUNLAYER5.PlainCopy(),
				SUNLAYER6 = this.SUNLAYER6.PlainCopy()
			};

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="SunInfo"/> 
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