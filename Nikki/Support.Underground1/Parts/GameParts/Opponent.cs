using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.GameParts
{
	/// <summary>
	/// A unit <see cref="Opponent"/> that is used in career races.
	/// </summary>
	public class Opponent : SubPart
	{
		/// <summary>
		/// Name of the opponent.
		/// </summary>
		[AccessModifiable()]
		public string Name { get; set; } = String.Empty;

		/// <summary>
		/// Preset ride of the opponent.
		/// </summary>
		[AccessModifiable()]
		public string PresetRide { get; set; } = BaseArguments.RANDOM;

		/// <summary>
		/// Easy skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public short SkillEasy { get; set; }

		/// <summary>
		/// Medium skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public short SkillMedium { get; set; }

		/// <summary>
		/// Hard skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public short SkillHard { get; set; }

		/// <summary>
		/// Catch up of the opponent, maybe?
		/// </summary>
		[AccessModifiable()]
		public short CatchUpMayb { get; set; }

		/// <summary>
		/// Unknown integer value 2.
		/// </summary>
		[AccessModifiable()]
		public uint UnknownInt2 { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Opponent();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8(0xC);
			this.PresetRide = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.SkillEasy = br.ReadInt16();
			this.SkillMedium = br.ReadInt16();
			this.SkillHard = br.ReadInt16();
			this.CatchUpMayb = br.ReadInt16();
			this.UnknownInt2 = br.ReadUInt32();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.Name, 0xC);
			bw.Write(this.PresetRide.BinHash());
			bw.Write(this.SkillEasy);
			bw.Write(this.SkillMedium);
			bw.Write(this.SkillHard);
			bw.Write(this.CatchUpMayb);
			bw.Write(this.UnknownInt2);
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.Name);
			bw.WriteNullTermUTF8(this.PresetRide);
			bw.Write(this.SkillEasy);
			bw.Write(this.SkillMedium);
			bw.Write(this.SkillHard);
			bw.Write(this.CatchUpMayb);
			bw.Write(this.UnknownInt2);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8();
			this.PresetRide = br.ReadNullTermUTF8();
			this.SkillEasy = br.ReadInt16();
			this.SkillMedium = br.ReadInt16();
			this.SkillHard = br.ReadInt16();
			this.CatchUpMayb = br.ReadInt16();
			this.UnknownInt2 = br.ReadUInt32();
		}
	}
}
