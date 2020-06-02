using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.MostWanted.Class;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.Conversions;



namespace Nikki.Support.MostWanted.Parts.CarParts
{
	/// <summary>
	/// A unit CarPart attribute of <see cref="DBModelPart"/>.
	/// </summary>
	public class RealCarPart : Shared.Parts.CarParts.RealCarPart
	{
		/// <summary>
		/// Name of this <see cref="RealCarPart"/>.
		/// </summary>
		public override string PartName { get; set; } = String.Empty;

		/// <summary>
		/// Index of <see cref="DBModelPart"/> to which this part belongs to.
		/// </summary>
		public override int Index { get; set; }

		/// <summary>
		/// Collection of <see cref="CPAttribute"/> of this <see cref="RealCarPart"/>.
		/// </summary>
		public override List<CPAttribute> Attributes { get; set; }

		/// <summary>
		/// A <see cref="CPStruct"/> of this <see cref="RealCarPart"/>.
		/// </summary>
		[Expandable("LodGeometry")]
		public CPStruct Struct { get; set; }

		/// <summary>
		/// <see cref="DBModelPart"/> to which this part belongs to.
		/// </summary>
		public DBModelPart Model { get; set; }

		/// <summary>
		/// Label of the car part.
		/// </summary>
		[AccessModifiable()]
		public string PartLabel { get; set; } = String.Empty;

		/// <summary>
		/// Car Part ID Group to which this part belongs to.
		/// </summary>
		[AccessModifiable()]
		public byte CarPartGroupID { get; set; }

		/// <summary>
		/// Upgrade group ID of this <see cref="RealCarPart"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort UpgradeGroupID { get; set; }

		/// <summary>
		/// Debug name of this <see cref="RealCarPart"/>.
		/// </summary>
		[AccessModifiable()]
		public string DebugName { get; set; }

		/// <summary>
		/// Initialize new instance of <see cref="RealCarPart"/>.
		/// </summary>
		public RealCarPart()
		{
			this.Attributes = new List<CPAttribute>();
			this.Struct = new CPStruct();
		}

		/// <summary>
		/// Initialize new instance of <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="index">Index of the <see cref="DBModelPart"/> in the database.</param>
		/// <param name="model"><see cref="DBModelPart"/> to which this part belongs to.</param>
		public RealCarPart(int index, DBModelPart model)
		{
			this.Index = index;
			this.Model = model;
			this.Attributes = new List<CPAttribute>();
			this.Struct = new CPStruct();
		}

		/// <summary>
		/// Initialize new instance of <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="index">Index of the <see cref="DBModelPart"/> in the database.</param>
		/// <param name="capacity">Initial capacity of the attribute list.</param>
		/// <param name="model"><see cref="DBModelPart"/> to which this part belongs to.</param>
		public RealCarPart(int index, int capacity, DBModelPart model)
		{
			this.Index = index;
			this.Model = model;
			this.Attributes = new List<CPAttribute>(capacity);
			this.Struct = new CPStruct();
		}

		/// <summary>
		/// Returns PartName, Attributes count and CarPartGroupID as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() =>
			$"PartName: {this.PartName} | AttribCount: {this.Attributes.Count} | GroupID: {this.CarPartGroupID}";

		/// <summary>
		/// Returns the hash code for this <see cref="RealCarPart"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = this.PartLabel?.GetHashCode() ?? String.Empty.GetHashCode();
			result *= this.Index + 7;
			result ^= this.Struct.GetHashCode();
			return result;
		}

		/// <summary>
		/// Gets <see cref="CPAttribute"/> with the key provided.
		/// </summary>
		/// <param name="key">Key of a <see cref="CPAttribute"/> to find.</param>
		/// <returns>A <see cref="CPAttribute"/> with key provided.</returns>
		public override CPAttribute GetAttribute(uint key) => this.Attributes.Find(_ => _.Key == key);

		/// <summary>
		/// Gets <see cref="CPAttribute"/> with the label provided.
		/// </summary>
		/// <param name="label">Label of a <see cref="CPAttribute"/> to find.</param>
		/// <returns>A <see cref="CPAttribute"/> with label provided.</returns>
		public override CPAttribute GetAttribute(string label) => this.GetAttribute(label.BinHash());

		/// <summary>
		/// Gets <see cref="CPAttribute"/> at index specified.
		/// </summary>
		/// <param name="index">Index in the list of <see cref="CPAttribute"/>.</param>
		/// <returns>A <see cref="CPAttribute"/> at index specified.</returns>
		public override CPAttribute GetAttribute(int index) =>
			(index >= 0 && index < this.Length) ? this.Attributes[index] : null;

		/// <summary>
		/// Adds <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		public override void AddAttribute(uint key)
		{
			if (this.GetAttribute(key) != null)
			{

				throw new InfoAccessException($"Attribute with key type 0x{key:X8} already exist");

			}

			if (!Map.CarPartKeys.TryGetValue(key, out var type))
			{

				throw new MappingFailException($"Attribute of key type 0x{key:X8} is not a valid attribute");

			}

			CPAttribute attribute = type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(eBoolean.False, this),
				eCarPartAttribType.Floating => new FloatAttribute((float)0, this),
				eCarPartAttribType.CarPartID => new PartIDAttribute((int)0, this),
				eCarPartAttribType.String => new StringAttribute(String.Empty, this),
				eCarPartAttribType.TwoString => new TwoStringAttribute(String.Empty, this),
				eCarPartAttribType.Key => new KeyAttribute(String.Empty, this),
				eCarPartAttribType.ModelTable => new ModelTableAttribute(eBoolean.False, this),
				_ => new IntAttribute((int)0, this)
			};

			attribute.Key = key;
			this.Attributes.Add(attribute);
		}

		/// <summary>
		/// Adds <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		public override void AddAttribute(string label) =>
			this.AddAttribute(label.BinHash());

		/// <summary>
		/// Removes <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		public override void RemoveAttribute(uint key)
		{
			if (!this.Attributes.RemoveWith(_ => _.Key == key))
			{

				throw new InfoAccessException($"Attribute with key type 0x{key:X8} does not exist");

			}
		}

		/// <summary>
		/// Removes <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		public override void RemoveAttribute(string label) =>
			this.RemoveAttribute(label.BinHash());

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		public override void CloneAttribute(uint newkey, uint copykey)
		{
			var attribute = this.GetAttribute(copykey);

			if (attribute == null)
			{

				throw new InfoAccessException($"Attribute with key type 0x{copykey:X8} does not exist");

			}

			if (this.GetAttribute(newkey) != null)
			{

				throw new InfoAccessException($"Attribute with key type 0x{newkey:X8} already exists");

			}

			if (!Map.CarPartKeys.TryGetValue(newkey, out var type))
			{

				throw new MappingFailException($"Attribute of key type 0x{newkey:X8} is not a valid attribute");

			}

			var result = (CPAttribute)attribute.PlainCopy();
			result = result.ConvertTo(type);
			result.Key = newkey;
			result.BelongsTo = this;
			this.Attributes.Add(result);
		}

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		public override void CloneAttribute(uint newkey, string copylabel) =>
			this.CloneAttribute(newkey, copylabel.BinHash());

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		public override void CloneAttribute(string newlabel, uint copykey) =>
			this.CloneAttribute(newlabel.BinHash(), copykey);

		/// <summary>
		/// Clones <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		public override void CloneAttribute(string newlabel, string copylabel) =>
			this.CloneAttribute(newlabel.BinHash(), copylabel.BinHash());

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override ASubPart PlainCopy()
		{
			var result = new RealCarPart(this.Index, this.Length, this.Model)
			{
				CarPartGroupID = this.CarPartGroupID,
				DebugName = this.DebugName,
				PartName = this.PartName,
				PartLabel = this.PartLabel,
				UpgradeGroupID = this.UpgradeGroupID,
				Struct = (CPStruct)this.Struct.PlainCopy()
			};

			foreach (var attrib in this.Attributes)
			{

				result.Attributes.Add((CPAttribute)attrib.PlainCopy());

			}

			return result;
		}
	}
}
