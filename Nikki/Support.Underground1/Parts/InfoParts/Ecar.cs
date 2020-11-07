using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Ecar"/> used in car performance.
    /// </summary>
    public class Ecar : SubPart
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Unknown1 { get; set; } = 2F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Unknown2 { get; set; } = 3F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float HandlingBuffer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TopSuspFrontHeightReduce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TopSuspRearHeightReduce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public int NumPlayerCameras { get; set; } = 6;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public int NumAICameras { get; set; } = 6;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public int Cost { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Ecar"/>.
        /// </summary>
        public Ecar() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new Ecar();
            result.CloneValuesFrom(this);
            return result;
        }
    }
}
