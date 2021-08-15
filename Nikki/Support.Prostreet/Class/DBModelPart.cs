using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Prostreet.Framework;
using Nikki.Support.Prostreet.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Reflection;
using CoreExtensions.Conversions;



namespace Nikki.Support.Prostreet.Class
{
	/// <summary>
	/// <see cref="DBModelPart"/> is a collection of car parts of a specific model.
	/// </summary>
	public class DBModelPart : Shared.Class.DBModelPart
	{
		#region Fields

		private string _collection_name;
		private List<RealCarPart> _realparts;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Prostreet;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Prostreet.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public DBModelPartManager Manager { get; set; }

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
		/// List of <see cref="RealCarPart"/>.
		/// </summary>
		[Browsable(false)]
		public override List<RealCarPart> ModelCarParts => this._realparts;

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPart"/>.
		/// </summary>
		public DBModelPart() => this._realparts = new List<RealCarPart>();

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPart"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="DBModelPartManager"/> to which this instance belongs to.</param>
		public DBModelPart(string CName, DBModelPartManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			this.CollectionName.BinHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Switches two parts and their indexes.
		/// </summary>
		/// <param name="part1">First <see cref="RealCarPart"/> to switch.</param>
		/// <param name="part2">Second <see cref="RealCarPart"/> to switch.</param>
		public override void SwitchParts(string part1, string part2)
		{
			var index1 = this.ModelCarParts.FindIndex(_ => _.PartName == part1);
			var index2 = this.ModelCarParts.FindIndex(_ => _.PartName == part2);

			if (index1 == -1)
			{

				throw new InfoAccessException(part1);

			}

			if (index2 == -1)
			{

				throw new InfoAccessException(part2);

			}

			var temp1 = this.ModelCarParts[index1];
			var temp2 = this.ModelCarParts[index2];
			this.ModelCarParts[index2] = temp1;
			this.ModelCarParts[index1] = temp2;
		}

		/// <summary>
		/// Reverses all parts in this <see cref="DBModelPart"/>.
		/// </summary>
		public override void ReverseParts() => this.ModelCarParts.Reverse();

		/// <summary>
		/// Sorts all parts by property name provided.
		/// </summary>
		/// <param name="property">Property to sort by.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override void SortByProperty(string property)
		{
			var field = typeof(Parts.CarParts.RealCarPart).GetProperty(property);

			if (field == null)
			{

				throw new InfoAccessException(property);

			}

			this.ModelCarParts.Sort((x, y) =>
			{

				var valueX = x.GetFastPropertyValue(property) as IComparable;
				var valueY = y.GetFastPropertyValue(property) as IComparable;
				return valueX.CompareTo(valueY);

			});
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new DBModelPart(CName, this.Manager);

			foreach (var part in this.ModelCarParts)
			{

				var copy = (Parts.CarParts.RealCarPart)part.PlainCopy();
				copy.Model = result;
				result.ModelCarParts.Add(copy);

			}

			return result;
		}

		/// <summary>
		/// Adds new <see cref="RealCarPart"/>.
		/// </summary>
		public override void AddRealPart()
		{
			this.ModelCarParts.Add(new Parts.CarParts.RealCarPart(this));
		}

		/// <summary>
		/// Removes <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="name">Name of the <see cref="RealCarPart"/> to remove.</param>
		public override void RemovePart(string name)
		{
			var result = this.ModelCarParts.RemoveWith(_ => _.PartName == name);

			if (!result)
			{

				throw new InfoAccessException(name);

			}
		}

		/// <summary>
		/// Removes <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="index">Index of <see cref="RealCarPart"/> to remove.</param>
		public override void RemovePart(int index)
		{
			if (index < 0 || index >= this.CarPartsCount)
			{

				throw new IndexOutOfRangeException(nameof(index));

			}
			else
			{

				this.ModelCarParts.RemoveAt(index);

			}
		}

		/// <summary>
		/// Attemps to clone a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="copyname">Name of <see cref="RealCarPart"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override void ClonePart(string copyname)
		{
			var part = this.GetRealPart(copyname);

			if (part == null)
			{

				throw new InfoAccessException(copyname);

			}

			var copy = (RealCarPart)part.PlainCopy();
			copy.Model = this;
			this.ModelCarParts.Add(copy);
		}

		/// <summary>
		/// Clones a <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="index">Index of <see cref="RealCarPart"/> to clone.</param>
		public override void ClonePart(int index)
		{
			if (index < 0 || index >= this.CarPartsCount)
			{

				throw new IndexOutOfRangeException(nameof(index));

			}
			else
			{

				var copy = (RealCarPart)this.ModelCarParts[index].PlainCopy();
				copy.Model = this;
				this.ModelCarParts.Add(copy);

			}
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="DBModelPart"/> 
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
			using (var ms = new MemoryStream(this.CarPartsCount << 5))
			using (var writer = new BinaryWriter(ms))
			{

				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this.CarPartsCount);

				for (int loop = 0; loop < this.CarPartsCount; ++loop)
				{

					var part = this.ModelCarParts[loop];
					writer.Write(part.Attributes.Count);

					for (int i = 0; i < part.Attributes.Count; ++i)
					{

						part.Attributes[i].Serialize(writer);

					}

				}

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

			this._collection_name = reader.ReadNullTermUTF8();
			var count = reader.ReadInt32();
			this.ModelCarParts.Capacity = count;

			for (int loop = 0; loop < count; ++loop)
			{

				var num = reader.ReadInt32();
				var part = new Parts.CarParts.RealCarPart(this, num);

				for (int i = 0; i < num; ++i)
				{

					var key = reader.ReadUInt32();

					if (!Map.CarPartKeys.TryGetValue(key, out var type))
					{

						type = CarPartAttribType.Custom;

					}

					CPAttribute attrib = type switch
					{
						CarPartAttribType.Boolean => new BoolAttribute(),
						CarPartAttribType.Color => new ColorAttribute(),
						CarPartAttribType.CarPartID => new PartIDAttribute(),
						CarPartAttribType.Integer => new IntAttribute(),
						CarPartAttribType.Floating => new FloatAttribute(),
						CarPartAttribType.String => new StringAttribute(),
						CarPartAttribType.TwoString => new TwoStringAttribute(),
						CarPartAttribType.Key => new KeyAttribute(),
						CarPartAttribType.ModelTable => new ModelTableAttribute(),
						_ => new CustomAttribute(),
					};

					attrib.Key = key;
					attrib.Deserialize(reader);
					part.Attributes.Add(attrib);

				}

				this.ModelCarParts.Add(part);

			}
		}

		/// <summary>
		/// Synchronizes all parts of this instance with another instance passed.
		/// </summary>
		/// <param name="other"><see cref="DBModelPart"/> to synchronize with.</param>
		internal void Synchronize(DBModelPart other)
		{
			var modelparts = new List<RealCarPart>(other.ModelCarParts);

			for (int i = 0; i < this.CarPartsCount; ++i)
			{

				bool found = false;

				for (int j = 0; j < other.CarPartsCount; ++j)
				{

					if (other.ModelCarParts[j].Equals(this.ModelCarParts[i]))
					{

						found = true;
						break;

					}

				}

				if (!found) modelparts.Add(this.ModelCarParts[i]);

			}

			this._realparts = modelparts;
		}

		#endregion
	}
}
