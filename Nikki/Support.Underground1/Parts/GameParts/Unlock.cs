using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.GameParts
{
	/// <summary>
	/// A unit <see cref="Unlock"/> that is used in career races.
	/// </summary>
	public class Unlock : SubPart
	{
		/// <summary>
		/// Enum of unlockable types in Underground 1.
		/// </summary>
		public enum UnlockableType : int
		{
			/// <summary>
			/// An invalid unlockable type.
			/// </summary>
			Invalid = 0,

			/// <summary>
			/// Unlocks a performance or visual upgrade.
			/// </summary>
			Upgrade = 1,

			/// <summary>
			/// Unlocks a car.
			/// </summary>
			Car = 2,

			/// <summary>
			/// Unlocks a track.
			/// </summary>
			Track = 3,
		}

		/// <summary>
		/// Unlockable type of this <see cref="Unlock"/>.
		/// </summary>
		[AccessModifiable()]
		public UnlockableType UnlockType { get; set; }

		/// <summary>
		/// Unlockable name of this <see cref="Unlock"/>.
		/// </summary>
		[AccessModifiable()]
		public string UnlockName { get; set; } = String.Empty;

		/// <summary>
		/// Track ID that gets unlocked.
		/// </summary>
		[AccessModifiable()]
		public ushort TrackID { get; set; }

		/// <summary>
		/// Padding, perhaps
		/// </summary>
		public int Unknown { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Unlock()
			{
				UnlockType = this.UnlockType,
				UnlockName = this.UnlockName,
				TrackID = this.TrackID,
				Unknown = this.Unknown,
			};
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.UnlockType = br.ReadEnum<UnlockableType>();

			if (this.UnlockType == UnlockableType.Track)
			{
				this.TrackID = (ushort)br.ReadInt32();

			}
			else
			{
			
				this.UnlockName = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			
			}
			
			this.Unknown = br.ReadInt32();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.UnlockType);

			if (this.UnlockType == UnlockableType.Track)
			{

				bw.Write((int)this.TrackID);

			}
			else
			{

				bw.Write(this.UnlockName.BinHash());

			}

			bw.Write(this.Unknown);
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteEnum(this.UnlockType);
			bw.WriteNullTermUTF8(this.UnlockName);
			bw.Write(this.TrackID);
			bw.Write(this.Unknown);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.UnlockType = br.ReadEnum<UnlockableType>();
			this.UnlockName = br.ReadNullTermUTF8();
			this.TrackID = br.ReadUInt16();
			this.Unknown = br.ReadInt32();
		}
	}
}
