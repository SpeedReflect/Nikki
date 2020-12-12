using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Enum;
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
			/// Unlocks a performance or visual upgrade.
			/// </summary>
			Upgrade = 0, // Performance

			/// <summary>
			/// Unlocks a magazine.
			/// </summary>
			Magazine = 1,

			/// <summary>
			/// Unlocks a car.
			/// </summary>
			Car = 2,

			/// <summary>
			/// Unlocks a track.
			/// </summary>
			Track = 3,

			/// <summary>
			/// Unlocks unknown.
			/// </summary>
			Unknown = 4,
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
		/// Index of an unlockable part.
		/// </summary>
		[AccessModifiable()]
		public int PartIndex { get; set; }

		/// <summary>
		/// Track ID that gets unlocked.
		/// </summary>
		[AccessModifiable()]
		public ushort TrackID { get; set; }

		/// <summary>
		/// Upgrade level of an unlock.
		/// </summary>
		[AccessModifiable()]
		public int UpgradeLevel { get; set; }

		/// <summary>
		/// Indicates whether track unlocked is in reverse.
		/// </summary>
		[AccessModifiable()]
		public eBoolean IsInReverse { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new Unlock()
			{
				PartIndex = this.PartIndex,
				TrackID = this.TrackID,
				UnlockName = this.UnlockName,
				UnlockType = this.UnlockType,
				UpgradeLevel = this.UpgradeLevel,
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

			switch (this.UnlockType)
			{

				case UnlockableType.Upgrade:
					this.PartIndex = br.ReadInt32();
					this.UpgradeLevel = br.ReadInt32();
					break;

				case UnlockableType.Track:
					this.TrackID = (ushort)br.ReadInt32();
					this.IsInReverse = (eBoolean)br.ReadInt32();
					break;

				default:
					this.UnlockName = br.ReadUInt32().BinString(LookupReturn.EMPTY);
					br.BaseStream.Position += 4;
					break;

			}
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.UnlockType);

			switch (this.UnlockType)
			{

				case UnlockableType.Upgrade:
					bw.Write(this.PartIndex);
					bw.Write(this.UpgradeLevel);
					break;

				case UnlockableType.Track:
					bw.Write((int)this.TrackID);
					bw.Write((int)this.IsInReverse);
					break;

				default:
					bw.Write(this.UnlockName.BinHash());
					bw.Write((int)0);
					break;

			}
		}

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public void Serialize(BinaryWriter bw)
		{
			bw.WriteEnum(this.UnlockType);

			switch (this.UnlockType)
			{

				case UnlockableType.Upgrade:
					bw.Write(this.PartIndex);
					bw.Write(this.UpgradeLevel);
					break;

				case UnlockableType.Track:
					bw.Write(this.TrackID);
					bw.WriteEnum(this.IsInReverse);
					break;

				default:
					bw.WriteNullTermUTF8(this.UnlockName);
					break;

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public void Deserialize(BinaryReader br)
		{
			this.UnlockType = br.ReadEnum<UnlockableType>();

			switch (this.UnlockType)
			{

				case UnlockableType.Upgrade:
					this.PartIndex = br.ReadInt32();
					this.UpgradeLevel = br.ReadInt32();
					break;

				case UnlockableType.Track:
					this.TrackID = br.ReadUInt16();
					this.IsInReverse = br.ReadEnum<eBoolean>();
					break;

				default:
					this.UnlockName = br.ReadNullTermUTF8();
					break;

			}
		}
	}
}
