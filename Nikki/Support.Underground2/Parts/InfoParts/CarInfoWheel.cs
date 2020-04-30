using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="CarInfoWheel"/> used in car performance.
    /// </summary>
    public class CarInfoWheel : ASubPart, ICopyable<CarInfoWheel>
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
        public int WheelID { get; set; }

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
        /// Initializes new instance of <see cref="Brakes"/>.
        /// </summary>
        /// <param name="ID">ID of the wheel.</param>
        public CarInfoWheel(int ID)
		{
			this.WheelID = ID;
		}

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public CarInfoWheel PlainCopy()
        {
            var result = new CarInfoWheel();
            var ThisType = this.GetType();
            var ResultType = result.GetType();
            foreach (var ThisProperty in ThisType.GetProperties())
            {
                var ResultField = ResultType.GetProperty(ThisProperty.Name);
                ResultField.SetValue(result, ThisProperty.GetValue(this));
            }
            return result;
        }
    }
}