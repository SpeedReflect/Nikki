using System.IO;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.GameParts
{
	/// <summary>
	/// A unit <see cref="Stage"/> that is used in career races.
	/// </summary>
	public class Stage : SubPart
	{
		/// <summary>
		/// Track ID of this <see cref="Stage"/>.
		/// </summary>
		[AccessModifiable()]
		public ushort TrackID { get; set; }

		/// <summary>
		/// Number of laps in this <see cref="Stage"/>.
		/// </summary>
		[AccessModifiable()]
		public byte NumberOfLaps { get; set; }

		/// <summary>
		/// True if this <see cref="Stage"/> is in reverse direction; false otherwise.
		/// </summary>
		[AccessModifiable()]
		public eBoolean InReverseDirection { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Stage()
			{
				TrackID = this.TrackID,
				NumberOfLaps = this.NumberOfLaps,
				InReverseDirection = this.InReverseDirection
			};
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.TrackID = br.ReadUInt16();
			this.NumberOfLaps = br.ReadByte();
			this.InReverseDirection = (eBoolean)(br.ReadByte() % 2);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.TrackID);
			bw.Write(this.NumberOfLaps);
			bw.WriteEnum(this.InReverseDirection);
		}
	}
}
