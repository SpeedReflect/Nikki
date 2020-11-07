using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="ECU"/> used in car performance.
    /// </summary>
    public class ECU : SubPart
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUTorque9 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUBraking1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUBraking2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUBraking3 { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="ECU"/>.
        /// </summary>
        public ECU() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new ECU();
            result.CloneValuesFrom(this);
            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.ECUTorque1 = br.ReadSingle();
            this.ECUTorque2 = br.ReadSingle();
            this.ECUTorque3 = br.ReadSingle();
            this.ECUTorque4 = br.ReadSingle();
            this.ECUTorque5 = br.ReadSingle();
            this.ECUTorque6 = br.ReadSingle();
            this.ECUTorque7 = br.ReadSingle();
            this.ECUTorque8 = br.ReadSingle();
            this.ECUTorque9 = br.ReadSingle();
            this.ECUBraking1 = br.ReadSingle();
            this.ECUBraking2 = br.ReadSingle();
            this.ECUBraking3 = br.ReadSingle();
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.ECUTorque1);
            bw.Write(this.ECUTorque2);
            bw.Write(this.ECUTorque3);
            bw.Write(this.ECUTorque4);
            bw.Write(this.ECUTorque5);
            bw.Write(this.ECUTorque6);
            bw.Write(this.ECUTorque7);
            bw.Write(this.ECUTorque8);
            bw.Write(this.ECUTorque9);
            bw.Write(this.ECUBraking1);
            bw.Write(this.ECUBraking2);
            bw.Write(this.ECUBraking3);
        }
    }
}
