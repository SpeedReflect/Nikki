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
	public class Opponent : ASubPart
	{
		/// <summary>
		/// Name of the opponent.
		/// </summary>
		[AccessModifiable()]
		public string Name { get; set; } = String.Empty;

		/// <summary>
		/// Unknown integer value 1.
		/// </summary>
		[AccessModifiable()]
		public uint UnknownInt1 { get; set; }

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
		public override ASubPart PlainCopy()
		{
			var result = new Opponent();
			
			foreach (var property in this.GetType().GetProperties())
			{

				property.SetValue(result, property.GetValue(this));

			}

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Name = br.ReadNullTermUTF8(0x8);
			this.UnknownInt1 = br.ReadUInt32();
			this.PresetRide = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
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
			bw.WriteNullTermUTF8(this.Name, 0x8);
			bw.Write(this.UnknownInt1);
			bw.Write(this.PresetRide.BinHash());
			bw.Write(this.SkillEasy);
			bw.Write(this.SkillMedium);
			bw.Write(this.SkillHard);
			bw.Write(this.CatchUpMayb);
			bw.Write(this.UnknownInt2);
		}
	}
}
