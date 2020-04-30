using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
    /// <summary>
    /// A unit <see cref="DriftControl"/> used in car performance.
    /// </summary>
    public class DriftControl : ASubPart, ICopyable<DriftControl>
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
        public DriftControl PlainCopy()
        {
            var result = new DriftControl();
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