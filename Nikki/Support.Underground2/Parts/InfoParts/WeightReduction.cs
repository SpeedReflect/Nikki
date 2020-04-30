using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="WeightReduction"/> used in car performance.
    /// </summary>
	public class WeightReduction : ASubPart, ICopyable<WeightReduction>
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float WeightReductionMassMultiplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float WeightReductionGripAddon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float WeightReductionHandlingRating { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="WeightReduction"/>.
        /// </summary>
		public WeightReduction() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public WeightReduction PlainCopy()
        {
            var result = new WeightReduction();
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
