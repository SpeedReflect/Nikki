using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.MostWanted.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="Damages"/> used in preset rides.
	/// </summary>
	public class Damages : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFrontWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageBody { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageCopLights { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageCopSpoiler { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFrontWheel { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageLeftBrakelight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRightBrakelight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageLeftHeadlight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRightHeadlight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageHood { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageBushguard { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFrontBumper { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRightDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRightRearDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageTrunk { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRearBumper { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRearLeftWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFrontLeftWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageFrontRightWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageRearRightWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageLeftDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DamageLeftRearDoor { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Damages();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.DamageFrontWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageBody = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageCopLights = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageCopSpoiler = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageFrontWheel = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageLeftBrakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRightBrakelight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageLeftHeadlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRightHeadlight = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageHood = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageBushguard = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageFrontBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRightDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRightRearDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageTrunk = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRearBumper = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRearLeftWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageFrontLeftWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageFrontRightWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageRearRightWindow = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageLeftDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.DamageLeftRearDoor = br.ReadUInt32().BinString(LookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DamageFrontWindow.BinHash());
			bw.Write(this.DamageBody.BinHash());
			bw.Write(this.DamageCopLights.BinHash());
			bw.Write(this.DamageCopSpoiler.BinHash());
			bw.Write(this.DamageFrontWheel.BinHash());
			bw.Write(this.DamageLeftBrakelight.BinHash());
			bw.Write(this.DamageRightBrakelight.BinHash());
			bw.Write(this.DamageLeftHeadlight.BinHash());
			bw.Write(this.DamageRightHeadlight.BinHash());
			bw.Write(this.DamageHood.BinHash());
			bw.Write(this.DamageBushguard.BinHash());
			bw.Write(this.DamageFrontBumper.BinHash());
			bw.Write(this.DamageRightDoor.BinHash());
			bw.Write(this.DamageRightRearDoor.BinHash());
			bw.Write(this.DamageTrunk.BinHash());
			bw.Write(this.DamageRearBumper.BinHash());
			bw.Write(this.DamageRearLeftWindow.BinHash());
			bw.Write(this.DamageFrontLeftWindow.BinHash());
			bw.Write(this.DamageFrontRightWindow.BinHash());
			bw.Write(this.DamageRearRightWindow.BinHash());
			bw.Write(this.DamageLeftDoor.BinHash());
			bw.Write(this.DamageLeftRearDoor.BinHash());
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.DamageFrontWindow);
			bw.WriteNullTermUTF8(this.DamageBody);
			bw.WriteNullTermUTF8(this.DamageCopLights);
			bw.WriteNullTermUTF8(this.DamageCopSpoiler);
			bw.WriteNullTermUTF8(this.DamageFrontWheel);
			bw.WriteNullTermUTF8(this.DamageLeftBrakelight);
			bw.WriteNullTermUTF8(this.DamageRightBrakelight);
			bw.WriteNullTermUTF8(this.DamageLeftHeadlight);
			bw.WriteNullTermUTF8(this.DamageRightHeadlight);
			bw.WriteNullTermUTF8(this.DamageHood);
			bw.WriteNullTermUTF8(this.DamageBushguard);
			bw.WriteNullTermUTF8(this.DamageFrontBumper);
			bw.WriteNullTermUTF8(this.DamageRightDoor);
			bw.WriteNullTermUTF8(this.DamageRightRearDoor);
			bw.WriteNullTermUTF8(this.DamageTrunk);
			bw.WriteNullTermUTF8(this.DamageRearBumper);
			bw.WriteNullTermUTF8(this.DamageRearLeftWindow);
			bw.WriteNullTermUTF8(this.DamageFrontLeftWindow);
			bw.WriteNullTermUTF8(this.DamageFrontRightWindow);
			bw.WriteNullTermUTF8(this.DamageRearRightWindow);
			bw.WriteNullTermUTF8(this.DamageLeftDoor);
			bw.WriteNullTermUTF8(this.DamageLeftRearDoor);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.DamageFrontWindow = br.ReadNullTermUTF8();
			this.DamageBody = br.ReadNullTermUTF8();
			this.DamageCopLights = br.ReadNullTermUTF8();
			this.DamageCopSpoiler = br.ReadNullTermUTF8();
			this.DamageFrontWheel = br.ReadNullTermUTF8();
			this.DamageLeftBrakelight = br.ReadNullTermUTF8();
			this.DamageRightBrakelight = br.ReadNullTermUTF8();
			this.DamageLeftHeadlight = br.ReadNullTermUTF8();
			this.DamageRightHeadlight = br.ReadNullTermUTF8();
			this.DamageHood = br.ReadNullTermUTF8();
			this.DamageBushguard = br.ReadNullTermUTF8();
			this.DamageFrontBumper = br.ReadNullTermUTF8();
			this.DamageRightDoor = br.ReadNullTermUTF8();
			this.DamageRightRearDoor = br.ReadNullTermUTF8();
			this.DamageTrunk = br.ReadNullTermUTF8();
			this.DamageRearBumper = br.ReadNullTermUTF8();
			this.DamageRearLeftWindow = br.ReadNullTermUTF8();
			this.DamageFrontLeftWindow = br.ReadNullTermUTF8();
			this.DamageFrontRightWindow = br.ReadNullTermUTF8();
			this.DamageRearRightWindow = br.ReadNullTermUTF8();
			this.DamageLeftDoor = br.ReadNullTermUTF8();
			this.DamageLeftRearDoor = br.ReadNullTermUTF8();
		}
	}
}
