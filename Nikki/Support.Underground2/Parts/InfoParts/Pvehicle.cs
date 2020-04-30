using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Pvehicle"/> used in car performance.
    /// </summary>
    public class Pvehicle : ASubPart, ICopyable<Pvehicle>
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Massx1000Multiplier { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TensorScaleX { get; set; } = 4;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TensorScaleY { get; set; } = 3;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TensorScaleZ { get; set; } = 2;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TensorScaleW { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float InitialHandlingRating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TopSpeedUnderflow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float StockTopSpeedLimiter { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Pvehicle"/>.
        /// </summary>
        public Pvehicle() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public Pvehicle PlainCopy()
        {
            var result = new Pvehicle();
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
