using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Nitrous"/> used in car performance.
    /// </summary>
    public class Nitrous : ASubPart, ICopyable<Nitrous>
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float NOSCapacity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float NOSTorqueBoost { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Nitrous"/>.
        /// </summary>
		public Nitrous() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public Nitrous PlainCopy()
        {
            var result = new Nitrous();
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
