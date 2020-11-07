using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Pvehicle"/> used in car performance.
    /// </summary>
    public class Pvehicle : SubPart
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
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Unknown1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Unknown2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Unknown3 { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Pvehicle"/>.
        /// </summary>
        public Pvehicle() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new Pvehicle();
            result.CloneValuesFrom(this);
            return result;
        }
    }
}
