using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.Conversions;
using Nikki.Reflection.Enum.CP;



namespace Nikki.Support.Carbon.Parts.CarParts
{
	/// <summary>
	/// A unit CarPart attribute of <see cref="DBModelPart"/>.
	/// </summary>
	[DebuggerDisplay("PartName: {PartName} | AttribCount: {Attributes.Count}")]
	public class RealCarPart : Shared.Parts.CarParts.RealCarPart
	{
		/// <summary>
		/// Name of this <see cref="RealCarPart"/>.
		/// </summary>
		[Browsable(false)]
		public override string PartName => this.GetPartName();

		/// <summary>
		/// <see cref="DBModelPart"/> to which this instance belongs to.
		/// </summary>
		public override Shared.Class.DBModelPart Model { get; set; }

		/// <summary>
		/// Collection of <see cref="CPAttribute"/> of this <see cref="RealCarPart"/>.
		/// </summary>
		public override List<CPAttribute> Attributes { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="RealCarPart"/>.
		/// </summary>
		public RealCarPart() => this.Attributes = new List<CPAttribute>();

		/// <summary>
		/// Initializes new instance of <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="model"><see cref="DBModelPart"/> to which this instance belongs to.</param>
		public RealCarPart(DBModelPart model) : this() { this.Model = model; }

		/// <summary>
		/// Initializes new instance of <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="capacity">Initial capacity of the attribute list.</param>
		public RealCarPart(int capacity) => this.Attributes = new List<CPAttribute>(capacity);

		/// <summary>
		/// Initializes new instance of <see cref="RealCarPart"/>.
		/// </summary>
		/// <param name="model"><see cref="DBModelPart"/> to which this instance belongs to.</param>
		/// <param name="capacity">Initial capacity of the attribute list.</param>
		public RealCarPart(DBModelPart model, int capacity) : this(capacity) { this.Model = model; }

		/// <summary>
		/// Returns PartName of this <see cref="RealCarPart"/>.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.GetPartName();

		/// <summary>
		/// Returns the hash code for this <see cref="RealCarPart"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = this.PartName?.GetHashCode() ?? String.Empty.GetHashCode();
			
			foreach (var attribute in this.Attributes)
			{

				result = HashCode.Combine(result, attribute.GetHashCode());

			}

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
			if (!Map.CarPartKeys.TryGetValue(key, out var type))
			{

				throw new MappingFailException($"Attribute of key type 0x{key:X8} is not a valid attribute");

			}

			CPAttribute attribute = type switch
			{
				eCarPartAttribType.Boolean => new BoolAttribute(eBoolean.False),
				eCarPartAttribType.Floating => new FloatAttribute((float)0),
				eCarPartAttribType.CarPartID => new PartIDAttribute((int)0),
				eCarPartAttribType.String => new StringAttribute(String.Empty),
				eCarPartAttribType.TwoString => new TwoStringAttribute(String.Empty),
				eCarPartAttribType.Key => new KeyAttribute(String.Empty),
				eCarPartAttribType.ModelTable => new ModelTableAttribute(eBoolean.False),
				_ => new IntAttribute((int)0)
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

			if (!Map.CarPartKeys.TryGetValue(newkey, out var type))
			{

				throw new MappingFailException($"Attribute of key type 0x{newkey:X8} is not a valid attribute");

			}

			var result = (CPAttribute)attribute.PlainCopy();
			result = result.ConvertTo(type);
			result.Key = newkey;
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
		/// Compares two <see cref="RealCarPart"/> and checks whether the equal.
		/// </summary>
		/// <param name="other"><see cref="RealCarPart"/> to compare this instance to.</param>
		/// <returns>True if this instance equals another instance passed; false otherwise.</returns>
		public override bool PartEquals(Shared.Parts.CarParts.RealCarPart other)
		{
			if (other is RealCarPart part)
			{

				bool result = true;

				if (part.Attributes.Count != this.Attributes.Count) return false;

				for (int loop = 0; loop < this.Length; ++loop)
				{

					result &= this.Attributes[loop] == part.Attributes[loop];

				}

				return result;

			}
			else return false;
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new RealCarPart(this.Length);

			foreach (var attrib in this.Attributes)
			{
			
				result.Attributes.Add((CPAttribute)attrib.PlainCopy());
			
			}
			
			return result;
		}

		#region Get Part Name

		private string GetPartName()
		{
			var name = this.GetNameUsingLodOffsets();
			return String.IsNullOrEmpty(name) ? this.GetNameUsingPartOffsets() : name;
		}

		private string GetNameUsingLodOffsets()
		{
			CPAttribute attrib;

			attrib = this.GetAttribute((uint)eAttribInt.LOD_NAME_PREFIX_SELECTOR);

			if (attrib is IntAttribute selector)
			{

				string realpart = String.Empty;
				attrib = this.GetAttribute((uint)eAttribTwoString.LOD_BASE_NAME);
				var offsets = attrib as TwoStringAttribute ?? null;

				switch (selector.Value)
				{

					case 0:
						realpart = this.Model?.CollectionName ?? String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					case 1:
						attrib = this.GetAttribute((uint)eAttribKey.BRAND_NAME);
						realpart = attrib is KeyAttribute unkhash ? unkhash.Value : String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					case 2:
						attrib = this.GetAttribute((uint)eAttribKey.LOD_NAME_PREFIX_NAMEHASH);
						realpart = attrib is KeyAttribute basehash ? basehash.Value : String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					default:
						break;
				}

			}

			return null;
		}

		private string GetNameUsingPartOffsets()
		{
			CPAttribute attrib;
			var realpart = "REAL_CAR_PART";

			attrib = this.GetAttribute((uint)eAttribInt.PART_NAME_SELECTOR);

			if (attrib is IntAttribute selector)
			{

				attrib = this.GetAttribute((uint)eAttribTwoString.PART_NAME_OFFSETS);
				var offsets = attrib as TwoStringAttribute ?? null;

				switch (selector.Value)
				{

					case 0:
						realpart = this.Model?.CollectionName ?? String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					case 1:
						attrib = this.GetAttribute((uint)eAttribKey.BRAND_NAME);
						realpart = attrib is KeyAttribute unkhash ? unkhash.Value : String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					case 2:
						attrib = this.GetAttribute((uint)eAttribKey.PART_NAME_BASE_HASH);
						realpart = attrib is KeyAttribute basehash ? basehash.Value : String.Empty;
						realpart += offsets.Value1Exists == eBoolean.True ? "_" + offsets.Value1 : String.Empty;
						realpart += offsets.Value2Exists == eBoolean.True ? "_" + offsets.Value2 : String.Empty;
						return realpart;

					default:
						break;
				}

			}

			attrib = this.GetAttribute((uint)eAttribKey.PART_NAME_BASE_HASH);
			return attrib is KeyAttribute finalhash ? finalhash.Value : realpart;
		}

		#endregion
	}
}
