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
	/// <see cref="GCarUnlock"/> is a collection of settings related to car unlock requirements.
	/// </summary>
	public class GCarUnlock : Collectable
	{
		#region Fields

		private string _collection_name;

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
				if (this.Career.GetCollection(value, nameof(this.Career.GCarUnlocks)) != null)
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
		/// One of the two required events completed to unlock the car.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string ReqEventCompleted1 { get; set; }

		/// <summary>
		/// One of the two required events completed to unlock the car.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string ReqEventCompleted2 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="GCarUnlock"/>.
		/// </summary>
		public GCarUnlock() { }

		/// <summary>
		/// Initializes new instance of <see cref="GCarUnlock"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GCarUnlock(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="GCarUnlock"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public GCarUnlock(BinaryReader br, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="GCarUnlock"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="GCarUnlock"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			bw.Write(this.BinKey);
			bw.Write(this.ReqEventCompleted1.BinHash());
			bw.Write(this.ReqEventCompleted2.BinHash());
		}

		/// <summary>
		/// Disassembles array into <see cref="GCarUnlock"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="GCarUnlock"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			this._collection_name = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ReqEventCompleted1 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.ReqEventCompleted2 = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new GCarUnlock(CName, this.Career)
			{
				ReqEventCompleted1 = this.ReqEventCompleted1,
				ReqEventCompleted2 = this.ReqEventCompleted2,
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
			bw.WriteNullTermUTF8(this.ReqEventCompleted1);
			bw.WriteNullTermUTF8(this.ReqEventCompleted2);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Deserialize(BinaryReader br)
		{
			this._collection_name = br.ReadNullTermUTF8();
			this.ReqEventCompleted1 = br.ReadNullTermUTF8();
			this.ReqEventCompleted2 = br.ReadNullTermUTF8();
		}

		#endregion
	}
}