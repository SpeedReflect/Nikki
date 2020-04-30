using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Transmission"/> used in car performance.
	/// </summary>
	public class Transmission : ASubPart, ICopyable<Transmission>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ClutchSlip { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float OptimalShift { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float FinalDriveRatio { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float FinalDriveRatio2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TorqueSplit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float BurnoutStrength { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public int NumberOfGears { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearEfficiency { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatioR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatioN { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float GearRatio6 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Transmission"/>.
		/// </summary>
        public Transmission() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public Transmission PlainCopy()
		{
			var result = new Transmission();
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