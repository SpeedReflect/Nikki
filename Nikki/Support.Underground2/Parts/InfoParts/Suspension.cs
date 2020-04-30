using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Suspension"/> used in car performance.
	/// </summary>
	public class Suspension : ASubPart, ICopyable<Suspension>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockStiffnessFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockExtStiffnessFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpringProgressionFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockValvingFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SwayBarFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TrackWidthFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CounterBiasFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockDigressionFront { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockStiffnessRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockExtStiffnessRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpringProgressionRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockValvingRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SwayBarRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TrackWidthRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CounterBiasRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockDigressionRear { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Suspension"/>.
		/// </summary>
        public Suspension() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public Suspension PlainCopy()
        {
            var result = new Suspension();
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
