using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Engine"/> used in car performance.
	/// </summary>
	public class Engine : ASubPart, ICopyable<Engine>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpeedRefreshRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineTorque9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float EngineBraking3 { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Engine"/>.
		/// </summary>
		public Engine() { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public Engine PlainCopy()
		{
			var result = new Engine();
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
