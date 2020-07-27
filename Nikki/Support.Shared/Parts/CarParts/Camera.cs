using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.CarParts
{
	/// <summary>
	/// A unit <see cref="Camera"/> used in car performance.
	/// </summary>
	public class Camera : SubPart
	{
		/// <summary>
		/// Enum of camera types.
		/// </summary>
		public enum CameraType : short
		{
			/// <summary>
			/// Far camera.
			/// </summary>
			FAR = 0,

			/// <summary>
			/// Close camera.
			/// </summary>
			CLOSE = 1,

			/// <summary>
			/// Bumper camera.
			/// </summary>
			BUMPER = 2,

			/// <summary>
			/// Driver camera.
			/// </summary>
			DRIVER = 3,

			/// <summary>
			/// Hood camera.
			/// </summary>
			HOOD = 4,

			/// <summary>
			/// Drift camera.
			/// </summary>
			DRIFT = 5
		}

		/// <summary>
		/// 
		/// </summary>
		internal CameraType Type { get; set; }

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
		/// Clones values of another <see cref="Camera"/>.
		/// </summary>
		/// <param name="other"><see cref="Camera"/> to clone.</param>
		public override void CloneValuesFrom(SubPart other)
		{
			if (other is Camera camera)
			{

				this.Type = camera.Type;
				this.CameraAngle = camera.CameraAngle;
				this.CameraHeight = camera.CameraHeight;
				this.CameraLag = camera.CameraLag;
				this.CameraLatOffset = camera.CameraLatOffset;

			}
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Camera();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.Type = br.ReadEnum<CameraType>();
			this.CameraAngle = ((float)br.ReadInt16()) * 180 / 32768;
			this.CameraLag = br.ReadSingle();
			this.CameraHeight = br.ReadSingle();
			this.CameraLatOffset = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.Type);
			bw.Write((short)(this.CameraAngle / 180 * 32768));
			bw.Write(this.CameraLag);
			bw.Write(this.CameraHeight);
			bw.Write(this.CameraLatOffset);
		}
	}
}