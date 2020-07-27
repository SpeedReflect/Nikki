using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
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
        public float ECUx1000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx2000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx3000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx4000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx5000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx6000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx7000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx8000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx9000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx10000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx11000Add { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float ECUx12000Add { get; set; }

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
            this.ECUx1000Add = br.ReadSingle();
            this.ECUx2000Add = br.ReadSingle();
            this.ECUx3000Add = br.ReadSingle();
            this.ECUx4000Add = br.ReadSingle();
            this.ECUx5000Add = br.ReadSingle();
            this.ECUx6000Add = br.ReadSingle();
            this.ECUx7000Add = br.ReadSingle();
            this.ECUx8000Add = br.ReadSingle();
            this.ECUx9000Add = br.ReadSingle();
            this.ECUx10000Add = br.ReadSingle();
            this.ECUx11000Add = br.ReadSingle();
            this.ECUx12000Add = br.ReadSingle();
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.ECUx1000Add);
            bw.Write(this.ECUx2000Add);
            bw.Write(this.ECUx3000Add);
            bw.Write(this.ECUx4000Add);
            bw.Write(this.ECUx5000Add);
            bw.Write(this.ECUx6000Add);
            bw.Write(this.ECUx7000Add);
            bw.Write(this.ECUx8000Add);
            bw.Write(this.ECUx9000Add);
            bw.Write(this.ECUx10000Add);
            bw.Write(this.ECUx11000Add);
            bw.Write(this.ECUx12000Add);
        }
    }
}
