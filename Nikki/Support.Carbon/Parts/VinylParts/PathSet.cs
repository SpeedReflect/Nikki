using System.Diagnostics;
using Nikki.Reflection.Abstract;



namespace Nikki.Support.Carbon.Parts.VinylParts
{
	/// <summary>
	/// A unit <see cref="PathSet"/> that is used in <see cref="Class.VectorVinyl"/>.
	/// </summary>
	[DebuggerDisplay("PathDatas: {NumPathDatas} | PathPoints: {NumPathPoints}")]
	public class PathSet : Shared.Parts.VinylParts.PathSet
	{
		/// <summary>
		/// Initializes new instance of <see cref="PathSet"/>.
		/// </summary>
		public PathSet() : base(new StrokeEffect()) { }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PathSet();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Clones values of another <see cref="SubPart"/>.
		/// </summary>
		/// <param name="other"><see cref="SubPart"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is PathSet set)
			{

				this.DropShadowEffectExists = set.DropShadowEffectExists;
				this.FillEffectExists = set.FillEffectExists;
				this.GradientEffectExists = set.GradientEffectExists;
				this.InnerGlowEffectExists = set.InnerGlowEffectExists;
				this.InnerShadowEffectExists = set.InnerShadowEffectExists;
				this.StrokeEffectExists = set.StrokeEffectExists;
				this.DropShadowEffect.CloneValuesFrom(set.DropShadowEffect);
				this.FillEffect.CloneValuesFrom(set.FillEffect);
				this.GradientEffect.CloneValuesFrom(set.GradientEffect);
				this.InnerGlowEffect.CloneValuesFrom(set.InnerGlowEffect);
				this.InnerShadowEffect.CloneValuesFrom(set.InnerShadowEffect);
				this.StrokeEffect.CloneValuesFrom(set.StrokeEffect);

				foreach (var data in set.PathDatas)
				{

					this.PathDatas.Add((Shared.Parts.VinylParts.PathData)data.PlainCopy());

				}

				foreach (var point in set.PathPoints)
				{

					this.PathPoints.Add((Shared.Parts.VinylParts.PathPoint)point.PlainCopy());

				}

			}
		}

		/// <summary>
		/// Returns name of the class a string value.
		/// </summary>
		/// <returns>Name of the class a string value.</returns>
		public override string ToString() => "PathSet";
	}
}
