using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Undercover.Framework;
using Nikki.Support.Shared.Parts.SunParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Undercover.Class
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
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Undercover;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Undercover.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public SunInfoManager Manager { get; set; }

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
				this.Manager?.CreationCheck(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Sun layer 1.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER1 { get; }

		/// <summary>
		/// Sun layer 2.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER2 { get; }

		/// <summary>
		/// Sun layer 3.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER3 { get; }

		/// <summary>
		/// Sun layer 4.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER4 { get; }

		/// <summary>
		/// Sun layer 5.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER5 { get; }

		/// <summary>
		/// Sun layer 6.
		/// </summary>
		[Expandable("SunLayers")]
		[Browsable(false)]
		public SunLayer SUNLAYER6 { get; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		public SunInfo()
		{
			this.SUNLAYER1 = new SunLayer();
			this.SUNLAYER2 = new SunLayer();
			this.SUNLAYER3 = new SunLayer();
			this.SUNLAYER4 = new SunLayer();
			this.SUNLAYER5 = new SunLayer();
			this.SUNLAYER6 = new SunLayer();
		}

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="SunInfoManager"/> to which this instance belongs to.</param>
		public SunInfo(string CName, SunInfoManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="SunInfo"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="SunInfoManager"/> to which this instance belongs to.</param>
		public SunInfo(BinaryReader br, SunInfoManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

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
		public override Collectable MemoryCast(string CName)
		{
			var result = new SunInfo(CName, this.Manager);
			base.MemoryCast(this, result);
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
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(0xF5 + this._collection_name.Length))
			using (var writer = new BinaryWriter(ms))
			{

				// Write CollectionName and Version
				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this.Version);

				// Write Positions
				writer.Write(this.PositionX);
				writer.Write(this.PositionY);
				writer.Write(this.PositionZ);
				writer.Write(this.CarShadowPositionX);
				writer.Write(this.CarShadowPositionY);
				writer.Write(this.CarShadowPositionZ);

				// Write Layers
				this.SUNLAYER1.Write(writer);
				this.SUNLAYER2.Write(writer);
				this.SUNLAYER3.Write(writer);
				this.SUNLAYER4.Write(writer);
				this.SUNLAYER5.Write(writer);
				this.SUNLAYER6.Write(writer);

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.RAWW);

			var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
			header.Write(bw);
			bw.Write(array.Length);
			bw.Write(array);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Deserialize(BinaryReader br)
		{
			int size = br.ReadInt32();
			var array = br.ReadBytes(size);

			array = Interop.Decompress(array);

			using var ms = new MemoryStream(array);
			using var reader = new BinaryReader(ms);

			// Read CollectionName and Version
			this._collection_name = reader.ReadNullTermUTF8();
			this.Version = reader.ReadInt32();

			// Read Positions
			this.PositionX = reader.ReadSingle();
			this.PositionY = reader.ReadSingle();
			this.PositionZ = reader.ReadSingle();
			this.CarShadowPositionX = reader.ReadSingle();
			this.CarShadowPositionY = reader.ReadSingle();
			this.CarShadowPositionZ = reader.ReadSingle();

			// Read Layers
			this.SUNLAYER1.Read(reader);
			this.SUNLAYER2.Read(reader);
			this.SUNLAYER3.Read(reader);
			this.SUNLAYER4.Read(reader);
			this.SUNLAYER5.Read(reader);
			this.SUNLAYER6.Read(reader);
		}

		#endregion
	}
}