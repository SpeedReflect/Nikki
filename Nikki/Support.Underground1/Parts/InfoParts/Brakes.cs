using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Brakes"/> used in car performance.
    /// </summary>
    public class Brakes : SubPart
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float FrontDownForce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float RearDownForce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float BumpJumpForce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float SteeringRatio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float BrakeStrength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float HandBrakeStrength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float BrakeBias { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Brakes"/>.
        /// </summary>
        public Brakes() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new Brakes();
            result.CloneValuesFrom(this);
            return result;
        }
    }
}