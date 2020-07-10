using System.IO;
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
        public int NOSUnknown { get; set; }

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
                NOSUnknown = this.NOSUnknown,
                NOSTorqueBoost = this.NOSTorqueBoost
            };

            return result;
        }

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.NOSCapacity = br.ReadSingle();
			this.NOSUnknown = br.ReadInt32();
            br.BaseStream.Position += 4;
			this.NOSTorqueBoost = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.NOSCapacity);
			bw.Write(this.NOSUnknown);
			bw.Write((int)0);
			bw.Write(this.NOSTorqueBoost);
		}
	}
}
