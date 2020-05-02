using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.Text;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="PerfSliderTuning"/> is a collection of settings related to performance sliders.
	/// </summary>
	public class PerfSliderTuning : ACollectable
	{
		#region Fields

		// CollectionName here is an 8-digit hexadecimal containing 4 first major indexes of the slider.
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
				if (value.Length != 10 && !value.IsHexString())
					throw new Exception("Unable to parse value provided as a hexadecimal containing tuning settings.");
				if (this.Database.PerfSliderTunings.FindCollection(value) != null)
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
		/// Minimum ratio slider value.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float MinSliderValueRatio { get; set; }

		/// <summary>
		/// Maximum ratio slider value.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float MaxSliderValueRatio { get; set; }

		/// <summary>
		/// Value spread 1.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float ValueSpread1 { get; set; }

		/// <summary>
		/// Value spread 2.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		public float ValueSpread2 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="PerfSliderTuning"/>.
		/// </summary>
		public PerfSliderTuning() { }

		/// <summary>
		/// Initializes new instance of <see cref="PerfSliderTuning"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public PerfSliderTuning(string CName, Database.Underground2 db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="PerfSliderTuning"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Underground2"/> to which this instance belongs to.</param>
		public unsafe PerfSliderTuning(BinaryReader br, Database.Underground2 db)
		{
			this.Database = db;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~PerfSliderTuning() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="PerfSliderTuning"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="PerfSliderTuning"/> with.</param>
		public void Assemble(BinaryWriter bw)
		{
			int i1 = SAT.GetAlpha(this._collection_name);
			var i2 = SAT.GetRed(this._collection_name);
			var i3 = SAT.GetGreen(this._collection_name);
			var i4 = SAT.GetBlue(this._collection_name);
			bw.Write(i1);
			bw.Write(i2);
			bw.Write(i3);
			bw.Write(i4);
			bw.Write((byte)0);
			bw.Write(this.MinSliderValueRatio);
			bw.Write(this.MaxSliderValueRatio);
			bw.Write(this.ValueSpread1);
			bw.Write(this.ValueSpread2);
		}

		/// <summary>
		/// Disassembles array into <see cref="PerfSliderTuning"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="PerfSliderTuning"/> with.</param>
		public void Disassemble(BinaryReader br)
		{
			byte index = (byte)br.ReadInt32();
			byte level = br.ReadByte();
			byte slide = br.ReadByte();
			byte slamp = br.ReadByte();
			++br.BaseStream.Position;

			// CollectionName
			this._collection_name = $"0x{index:X2}{level:X2}{slide:X2}{slamp:X2}";

			// Slider Settings
			this.MinSliderValueRatio = br.ReadSingle();
			this.MaxSliderValueRatio = br.ReadSingle();
			this.ValueSpread1 = br.ReadSingle();
			this.ValueSpread2 = br.ReadSingle();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new PerfSliderTuning(CName, this.Database);
			uint maxkey = 0;
			foreach (var slider in this.Database.PerfSliderTunings.Collections)
				if (slider.BinKey > maxkey) maxkey = slider.BinKey;
			result._collection_name = (maxkey + 1).ToString();
			result.MaxSliderValueRatio = this.MaxSliderValueRatio;
			result.MinSliderValueRatio = this.MinSliderValueRatio;
			result.ValueSpread1 = this.ValueSpread1;
			result.ValueSpread2 = this.ValueSpread2;
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
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}