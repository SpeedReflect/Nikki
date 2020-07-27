using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="DriftControl"/> used in car performance.
    /// </summary>
    public class DriftControl : SubPart
    {
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl1 { get; set; } = 0.015F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl2 { get; set; } = 0.2F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl3 { get; set; } = 1.25F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl4 { get; set; } = 0.05F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl5 { get; set; } = 0.05F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl6 { get; set; } = 0.05F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl7 { get; set; } = 0.05F;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float DriftAdditionalYawControl8 { get; set; } = 0.05F;

        /// <summary>
        /// Initializes new instance of <see cref="DriftControl"/>.
        /// </summary>
        public DriftControl() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new DriftControl();
            result.CloneValuesFrom(this);
            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.DriftAdditionalYawControl1 = br.ReadSingle();
            this.DriftAdditionalYawControl2 = br.ReadSingle();
            this.DriftAdditionalYawControl3 = br.ReadSingle();
            this.DriftAdditionalYawControl4 = br.ReadSingle();
            this.DriftAdditionalYawControl5 = br.ReadSingle();
            this.DriftAdditionalYawControl6 = br.ReadSingle();
            this.DriftAdditionalYawControl7 = br.ReadSingle();
            this.DriftAdditionalYawControl8 = br.ReadSingle();
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.DriftAdditionalYawControl1);
            bw.Write(this.DriftAdditionalYawControl2);
            bw.Write(this.DriftAdditionalYawControl3);
            bw.Write(this.DriftAdditionalYawControl4);
            bw.Write(this.DriftAdditionalYawControl5);
            bw.Write(this.DriftAdditionalYawControl6);
            bw.Write(this.DriftAdditionalYawControl7);
            bw.Write(this.DriftAdditionalYawControl8);
        }
    }
}