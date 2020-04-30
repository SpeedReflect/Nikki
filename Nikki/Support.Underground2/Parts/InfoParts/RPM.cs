using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="RPM"/> used in car performance.
	/// </summary
	public class RPM : ASubPart, ICopyable<RPM>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float IdleRPMAdd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float RedLineRPMAdd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float MaxRPMAdd { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="RPM"/>.
		/// </summary>
		public RPM() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public RPM PlainCopy()
		{
			var result = new RPM();
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
