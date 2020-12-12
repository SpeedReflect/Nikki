using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.CarParts
{
    /// <summary>
    /// A unit <see cref="CarInfoWheel"/> used in car performance.
    /// </summary>
    public class CarInfoWheel : SubPart
	{
        /// <summary>
        /// Enum of wheel IDs for Underground1 and Underground2 supports.
        /// </summary>
        public enum CarWheelType : int
        {
            /// <summary>
            /// Front left wheel id
            /// </summary>
            FRONT_LEFT = 0,

            /// <summary>
            /// Front right wheel id
            /// </summary>
            FRONT_RIGHT = 1,

            /// <summary>
            /// Rear right wheel id
            /// </summary>
            REAR_RIGHT = 2,

            /// <summary>
            /// Rear left wheel id
            /// </summary>
            REAR_LEFT = 3,
        }

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
        internal CarWheelType WheelID { get; set; }

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
        /// Initializes new instance of <see cref="CarInfoWheel"/>.
        /// </summary>
        public CarInfoWheel() { }

        /// <summary>
        /// Clones values of another <see cref="CarInfoWheel"/>.
        /// </summary>
        /// <param name="other"><see cref="CarInfoWheel"/> to clone.</param>
        public override void CloneValuesFrom(SubPart other)
        {
            if (other is CarInfoWheel wheel)
            {

                this.Diameter = wheel.Diameter;
                this.RideHeight = wheel.RideHeight;
                this.Springs = wheel.Springs;
                this.TireSkidWidth = wheel.TireSkidWidth;
                this.UnknownVal = wheel.UnknownVal;
                this.WheelID = wheel.WheelID;
                this.WideBodyYValue = wheel.WideBodyYValue;
                this.XValue = wheel.XValue;
                this.YValue = wheel.YValue;

            }
        }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new CarInfoWheel();
            result.CloneValuesFrom(this);
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
            this.WheelID = br.ReadEnum<CarWheelType>();
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
            bw.WriteBytes(0, 0xC);
        }
    }
}