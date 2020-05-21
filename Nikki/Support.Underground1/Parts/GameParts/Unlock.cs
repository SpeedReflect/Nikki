using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Parts.GameParts
{
	/// <summary>
	/// A unit <see cref="Unlock"/> that is used in career races.
	/// </summary>
	public class Unlock : ASubPart, ICopyable<Unlock>
	{
		/// <summary>
		/// Unlockable type of this <see cref="Unlock"/>.
		/// </summary>
		[AccessModifiable()]
		public eUnlockableType UnlockType { get; set; }

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
		/// Padding, perhaps?
		/// </summary>
		public int Padding0 { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public Unlock PlainCopy()
		{
			var result = new Unlock()
			{
				UnlockType = this.UnlockType,
				UnlockName = this.UnlockName,
				TrackID = this.TrackID,
				Padding0 = this.Padding0
			};
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.UnlockType = br.ReadEnum<eUnlockableType>();
			if (this.UnlockType == eUnlockableType.Track)
				this.TrackID = (ushort)br.ReadInt32();
			else
				this.UnlockName = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Padding0 = br.ReadInt32();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.UnlockType);
			if (this.UnlockType == eUnlockableType.Track)
				bw.Write((int)this.TrackID);
			else
				bw.Write(this.UnlockName.BinHash());
			bw.WriteEnum(this.Padding0);
		}
	}
}
