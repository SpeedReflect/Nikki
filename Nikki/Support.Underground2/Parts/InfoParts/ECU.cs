using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="ECU"/> used in car performance.
    /// </summary
    public class ECU : ASubPart, ICopyable<ECU>
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
        public ECU PlainCopy()
        {
            var result = new ECU();
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
