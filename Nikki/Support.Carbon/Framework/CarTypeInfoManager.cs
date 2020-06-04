using System;
using System.IO;
using Nikki.Database;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="CarTypeInfo"/> collections.
	/// </summary>
	public class CarTypeInfoManager : Manager<CarTypeInfo>
	{
		/// <summary>
		/// Name of this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		public override string Name => "CarTypeInfos";

		/// <summary>
		/// Initializes new instance of <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="db"><see cref="FileBase"/> to which this manager belongs to.</param>
		public CarTypeInfoManager(FileBase db)
		{
			this.Database = db;
		}

		/// <summary>
		/// Assembles all collections in this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			bw.WriteEnum(eBlockID.CarTypeInfos);
			bw.Write(this.Count * CarTypeInfo.BaseClassSize + 8);
			bw.Write(0x11111111);
			bw.Write(0x11111111);

			foreach (var collection in this)
			{

				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Disassemble(BinaryReader br)
		{
			var id = br.ReadUInt32();
			var size = br.ReadInt32();

			if (id != (uint)eBlockID.CarTypeInfos)
			{

				throw new InvalidDataException("Processed block has invalid ID");

			}

			var count = (size - 8) / CarTypeInfo.BaseClassSize;
			this.Capacity = count;

			for (int loop = 0; loop < size / CarTypeInfo.BaseClassSize; ++loop)
			{

				var collection = new CarTypeInfo(br, this);
				this.Add(collection);

			}
		}

		/// <summary>
		/// Checks whether CollectionName provided allows creation of a new collection.
		/// </summary>
		/// <param name="cname">CollectionName to check.</param>
		internal override void CreationCheck(string cname)
		{
			if (String.IsNullOrWhiteSpace(cname))
			{

				throw new ArgumentNullException("CollectionName cannot be null, empty or whitespace");

			}

			if (cname.Contains(" "))
			{

				throw new ArgumentException("CollectionName cannot contain whitespace");

			}

			if (cname.Length > CarTypeInfo.MaxCNameLength)
			{

				throw new ArgumentLengthException(CarTypeInfo.MaxCNameLength);

			}

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}
		}
	}
}
