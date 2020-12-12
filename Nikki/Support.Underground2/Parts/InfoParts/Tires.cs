using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="Tires"/> used in car performance.
    /// </summary>
    public class Tires : SubPart
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
        public override SubPart PlainCopy()
        {
            var result = new Tires();
            result.CloneValuesFrom(this);
            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.StaticGripScale = br.ReadSingle();
            this.YawSpeedScale = br.ReadSingle();
            this.SteeringAmplifier = br.ReadSingle();
            this.DynamicGripScale = br.ReadSingle();
            this.SteeringResponse = br.ReadSingle();
            br.BaseStream.Position += 0xC;
            this.DriftYawControl = br.ReadSingle();
            this.DriftCounterSteerBuildUp = br.ReadSingle();
            this.DriftCounterSteerReduction = br.ReadSingle();
            this.PowerSlideBreakThru1 = br.ReadSingle();
            this.PowerSlideBreakThru2 = br.ReadSingle();
            br.BaseStream.Position += 0xC;
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.StaticGripScale);
            bw.Write(this.YawSpeedScale);
            bw.Write(this.SteeringAmplifier);
            bw.Write(this.DynamicGripScale);
            bw.Write(this.SteeringResponse);
            bw.WriteBytes(0, 0xC);
            bw.Write(this.DriftYawControl);
            bw.Write(this.DriftCounterSteerBuildUp);
            bw.Write(this.DriftCounterSteerReduction);
            bw.Write(this.PowerSlideBreakThru1);
            bw.Write(this.PowerSlideBreakThru2);
            bw.WriteBytes(0, 0xC);
        }
    }
}