using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Tires"/> used in car performance.
    /// </summary>
    public class Tires : ASubPart, ICopyable<Tires>
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float StaticGripScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float YawSpeedScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float SteeringAmplifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DynamicGripScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float SteeringResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftYawControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftCounterSteerBuildUp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftCounterSteerReduction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float PowerSlideBreakThru1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float PowerSlideBreakThru2 { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Tires"/>.
        /// </summary>
        public Tires() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public Tires PlainCopy()
        {
            var result = new Tires();
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