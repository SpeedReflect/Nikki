using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.GameParts
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
		/// Opponent's performance stats multiplier.
		/// </summary>
		[AccessModifiable()]
		public ushort StatsMultiplier { get; set; }

		/// <summary>
		/// Preset ride of the opponent.
		/// </summary>
		[AccessModifiable()]
		public string PresetRide { get; set; } = BaseArguments.RANDOM;

		/// <summary>
		/// Easy skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public byte SkillEasy { get; set; }

		/// <summary>
		/// Medium skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public byte SkillMedium { get; set; }

		/// <summary>
		/// Hard skill of the opponent.
		/// </summary>
		[AccessModifiable()]
		public byte SkillHard { get; set; }

		/// <summary>
		/// Catch up of the opponent.
		/// </summary>
		[AccessModifiable()]
		public byte CatchUp { get; set; }

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
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Read(BinaryReader br, BinaryReader strr)
		{
			var position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.Name = strr.ReadNullTermUTF8();
			this.StatsMultiplier = br.ReadUInt16();
			this.PresetRide = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.SkillEasy = br.ReadByte();
			this.SkillMedium = br.ReadByte();
			this.SkillHard = br.ReadByte();
			this.CatchUp = br.ReadByte();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw, BinaryWriter strw)
		{
			if (!String.IsNullOrEmpty(this.Name))
			{
				var pointer = (ushort)strw.BaseStream.Position;
				strw.WriteNullTermUTF8(this.Name);
				bw.Write(pointer);
			}
			else bw.Write((ushort)0);
			
			bw.Write(this.StatsMultiplier);
			bw.Write(this.PresetRide.BinHash());
			bw.Write(this.SkillEasy);
			bw.Write(this.SkillMedium);
			bw.Write(this.SkillHard);
			bw.Write(this.CatchUp);
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteNullTermUTF8(this.Name);
			bw.WriteNullTermUTF8(this.PresetRide);
			bw.Write(this.StatsMultiplier);
			bw.Write(this.SkillEasy);
			bw.Write(this.SkillMedium);
			bw.Write(this.SkillHard);
			bw.Write(this.CatchUp);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8();
			this.PresetRide = br.ReadNullTermUTF8();
			this.StatsMultiplier = br.ReadUInt16();
			this.SkillEasy = br.ReadByte();
			this.SkillMedium = br.ReadByte();
			this.SkillHard = br.ReadByte();
			this.CatchUp = br.ReadByte();
		}
	}
}
