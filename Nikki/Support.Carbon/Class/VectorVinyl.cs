using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;


namespace Nikki.Support.Carbon.Class
{
	/// <summary>
	/// <see cref="VectorVinyl"/> is a collection of vectors that form a vinyl.
	/// </summary>
	public class VectorVinyl : Shared.Class.VectorVinyl
	{
		#region Fields

		private string _collection_name;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Carbon.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public VectorVinylManager Manager { get; set; }

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



		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="VectorVinyl"/>.
		/// </summary>
		public VectorVinyl()
		{

		}

		/// <summary>
		/// Initializes new instance of <see cref="VectorVinyl"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="SunInfoManager"/> to which this instance belongs to.</param>
		public VectorVinyl(string CName, VectorVinylManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="VectorVinyl"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="VectorVinylManager"/> to which this instance belongs to.</param>
		public VectorVinyl(BinaryReader br, VectorVinylManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~VectorVinyl() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="SunInfo"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SunInfo"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{

		}

		/// <summary>
		/// Disassembles array into <see cref="SunInfo"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="SunInfo"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{

		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new VectorVinyl(CName, this.Manager);
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

		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Deserialize(BinaryReader br)
		{

		}

		#endregion
	}
}
