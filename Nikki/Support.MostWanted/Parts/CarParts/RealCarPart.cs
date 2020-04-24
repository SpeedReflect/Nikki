using System;
using System.Collections.Generic;
using Nikki.Support.MostWanted.Class;
using Nikki.Support.Shared.Parts.CarParts;



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
		public override string PartName { get; set; }

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
		public override Shared.Parts.CarParts.CPStruct Struct { get; set; }

		/// <summary>
		/// <see cref="DBModelPart"/> to which this part belongs to.
		/// </summary>
		public DBModelPart Model { get; set; }

		/// <summary>
		/// Car Part ID Group to which this part belongs to.
		/// </summary>
		public byte CarPartGroupID { get; set; }

		/// <summary>
		/// Upgrade group ID of this <see cref="RealCarPart"/>.
		/// </summary>
		public ushort UpgradeGroupID { get; set; }

		/// <summary>
		/// Debug name of this <see cref="RealCarPart"/>.
		/// </summary>
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
			int result = this.PartName?.GetHashCode() ?? String.Empty.GetHashCode();
			result *= this.Index + 7;
			result ^= this.Struct.GetHashCode();
			return result;
		}
	}
}
