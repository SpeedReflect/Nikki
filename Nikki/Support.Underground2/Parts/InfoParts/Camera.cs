using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Camera"/> used in car performance.
	/// </summary>
	public class Camera : ASubPart, ICopyable<Camera>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CameraAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CameraLag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CameraHeight { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CameraLatOffset { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Camera"/>.
		/// </summary>
		public Camera() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public Camera PlainCopy()
		{
			var result = new Camera();
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