using System.IO;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="CarInfoWheel"/> used in car performance.
    /// </summary>
    public class CarInfoWheel : SubPart
	{
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
		public float XValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Springs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float RideHeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float UnknownVal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float Diameter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float TireSkidWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        internal eCarWheelType WheelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float YValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public float WideBodyYValue { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Brakes"/>.
        /// </summary>
        public CarInfoWheel() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new CarInfoWheel();

            foreach (var property in this.GetType().GetProperties())
            {

                property.SetValue(result, property.GetValue(this));

            }

            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.XValue = br.ReadSingle();
            this.Springs = br.ReadSingle();
            this.RideHeight = br.ReadSingle();
            this.UnknownVal = br.ReadSingle();
            this.Diameter = br.ReadSingle();
            this.TireSkidWidth = br.ReadSingle();
            this.WheelID = br.ReadEnum<eCarWheelType>();
            this.YValue = br.ReadSingle();
            this.WideBodyYValue = br.ReadSingle();
            br.BaseStream.Position += 0xC;
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.XValue);
            bw.Write(this.Springs);
            bw.Write(this.RideHeight);
            bw.Write(this.UnknownVal);
            bw.Write(this.Diameter);
            bw.Write(this.TireSkidWidth);
            bw.WriteEnum(this.WheelID);
            bw.Write(this.YValue);
            bw.Write(this.WideBodyYValue);
            bw.WriteBytes(0xC);
        }
    }
}