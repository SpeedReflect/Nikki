using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="WeightReduction"/> used in car performance.
    /// </summary>
	public class WeightReduction : SubPart
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
        public override SubPart PlainCopy()
        {
            var result = new WeightReduction();
            result.CloneValuesFrom(this);
            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.WeightReductionMassMultiplier = br.ReadSingle();
            this.WeightReductionGripAddon = br.ReadSingle();
            this.WeightReductionHandlingRating = br.ReadSingle();
            br.BaseStream.Position += 4;
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.WeightReductionMassMultiplier);
            bw.Write(this.WeightReductionGripAddon);
            bw.Write(this.WeightReductionHandlingRating);
            bw.Write((int)0);
        }
    }
}
