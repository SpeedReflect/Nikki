using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
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
		/// Attempts to add <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddAttribute(uint key)
		{
			if (this.GetAttribute(key) != null) return false;
			if (!Map.CarPartKeys.TryGetValue(key, out var type)) return false;
			CPAttribute attribute = type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(eBoolean.False, this),
				eCarPartAttribType.Floating => new FloatAttribute((float)0, this),
				eCarPartAttribType.CarPartID => new PartIDAttribute((int)0, this),
				eCarPartAttribType.String => new StringAttribute(String.Empty, this),
				eCarPartAttribType.TwoString => new TwoStringAttribute(String.Empty, this),
				_ => new IntAttribute((int)0, this)
			};
			this.Attributes.Add(attribute);
			return true;
		}

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddAttribute(string label) => this.TryAddAttribute(label.BinHash());

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="error">Error occured when trying to add new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddAttribute(uint key, out string error)
		{
			error = null;
			if (this.GetAttribute(key) != null)
			{
				error = $"Attribute with key 0x{key:X8} already exists in this car part.";
				return false;
			}
			if (!Map.CarPartKeys.TryGetValue(key, out var type))
			{
				error = $"Attribute with key 0x{key:X8} is invalid.";
				return false;
			}
			CPAttribute attribute = type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(eBoolean.False, this),
				eCarPartAttribType.Floating => new FloatAttribute((float)0, this),
				eCarPartAttribType.CarPartID => new PartIDAttribute((int)0, this),
				eCarPartAttribType.String => new StringAttribute(String.Empty, this),
				eCarPartAttribType.TwoString => new TwoStringAttribute(String.Empty, this),
				_ => new IntAttribute((int)0, this)
			};
			this.Attributes.Add(attribute);
			return true;
		}

		/// <summary>
		/// Attempts to add <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="error">Error occured when trying to add new <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryAddAttribute(string label, out string error) =>
			this.TryAddAttribute(label.BinHash(), out error);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemoveAttribute(uint key) =>
			this.Attributes.RemoveWith(_ => _.Key == key);

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemoveAttribute(string label) => this.TryRemoveAttribute(label.BinHash());

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="key">Key of the <see cref="CPAttribute"/> to remove.</param>
		/// <param name="error">Error occured when trying to remove <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemoveAttribute(uint key, out string error)
		{
			error = null;
			if (!this.Attributes.RemoveWith(_ => _.Key == key))
			{
				error = $"Attribute with key 0x{key:X8} does not exist.";
				return false;
			}
			else return true;
		}

		/// <summary>
		/// Attempts to remove <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="label">Label of the <see cref="CPAttribute"/> to remove.</param>
		/// <param name="error">Error occured when trying to remove <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryRemoveAttribute(string label, out string error) =>
			this.TryRemoveAttribute(label.BinHash(), out error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(uint newkey, uint copykey)
		{
			var attribute = this.GetAttribute(copykey);
			if (attribute == null) return false;
			if (this.GetAttribute(newkey) != null) return false;
			var result = attribute.PlainCopy();
			result.Key = newkey;
			this.Attributes.Add(result);
			return true;
		}

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(uint newkey, string copylabel) =>
			this.TryCloneAttribute(newkey, copylabel.BinHash());

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(string newlabel, uint copykey) =>
			this.TryCloneAttribute(newlabel.BinHash(), copykey);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(string newlabel, string copylabel) =>
			this.TryCloneAttribute(newlabel.BinHash(), copylabel.BinHash());

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(uint newkey, uint copykey, out string error)
		{
			error = null;
			var attribute = this.GetAttribute(copykey);
			if (attribute == null)
			{
				error = $"Attribute with key 0x{copykey:X8} does not exist.";
				return false;
			}
			if (this.GetAttribute(newkey) != null)
			{
				error = $"Attribute with key 0x{newkey:X8} already exists.";
				return false;
			}
			var result = attribute.PlainCopy();
			result.Key = newkey;
			this.Attributes.Add(result);
			return true;
		}

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newkey">Key of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(uint newkey, string copylabel, out string error) =>
			this.TryCloneAttribute(newkey, copylabel.BinHash(), out error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with key provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copykey">Key of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(string newlabel, uint copykey, out string error) =>
			this.TryCloneAttribute(newlabel.BinHash(), copykey, out error);

		/// <summary>
		/// Attempts to clone <see cref="CPAttribute"/> with label provided.
		/// </summary>
		/// <param name="newlabel">Label of the new <see cref="CPAttribute"/>.</param>
		/// <param name="copylabel">Label of the <see cref="CPAttribute"/> to clone.</param>
		/// <param name="error">Error occured when trying to clone <see cref="CPAttribute"/>.</param>
		/// <returns>True on success; false otherwise.</returns>
		public override bool TryCloneAttribute(string newlabel, string copylabel, out string error) =>
			this.TryCloneAttribute(newlabel.BinHash(), copylabel.BinHash(), out error);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override Shared.Parts.CarParts.RealCarPart PlainCopy()
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
				result.Attributes.Add(attrib.PlainCopy());

			return result;
		}
	}
}
