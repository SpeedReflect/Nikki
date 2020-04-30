using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Turbo"/> used in car performance.
	/// </summary>
	public class Turbo : ASubPart, ICopyable<Turbo>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboBraking { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboVacuum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHeatHigh { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHeatLow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboHighBoost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboLowBoost { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpool { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpoolTimeDown { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TurboSpoolTimeUp { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Turbo"/>.
		/// </summary>
		public Turbo() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public Turbo PlainCopy()
		{
			var result = new Turbo();
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
