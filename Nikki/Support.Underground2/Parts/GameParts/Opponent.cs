using System;
using Nikki.Reflection;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.GameParts
{
	/// <summary>
	/// A unit <see cref="Opponent"/> that is used in career races.
	/// </summary>
	public class Opponent : ASubPart, ICopyable<Opponent>
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
		public Opponent PlainCopy()
		{
			var result = new Opponent();
			var ThisType = this.GetType();
			var ResultType = result.GetType();
			foreach (var ThisProperty in ThisType.GetProperties())
			{
				var ResultField = ResultType.GetProperty(ThisProperty.Name);
				ResultField.SetValue(result, ThisProperty.GetValue(this));
			}
			return result;
		}
	}
}
