using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Nitrous"/> used in car performance.
    /// </summary>
    public class Nitrous : SubPart
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
        public override SubPart PlainCopy()
        {
            var result = new Nitrous()
            {
                NOSCapacity = this.NOSCapacity,
                NOSTorqueBoost = this.NOSTorqueBoost
            };

            return result;
        }
    }
}
