using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Support.Underground1.Class;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Framework
{
	/// <summary>
	/// A static manager to assemble and disassemble <see cref="DBModelPart"/> collections.
	/// </summary>
	public static class CarPartManager
	{
		#region Private Assemble

		private static byte[] MakeHeader(int attribcount, int partcount)
		{
			var result = new byte[0x20];

			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);

			bw.BaseStream.Position = 0x08;
			bw.Write(5); // write UG1 version

			bw.BaseStream.Position = 0x10;
			bw.Write(partcount); // write part count

			bw.BaseStream.Position = 0x18;
			bw.Write(attribcount); // write attribute count

			return result;
		}

		private static Dictionary<int, int> MakeStringList(Database.Underground1 db, 
			string mark, out byte[] string_buffer)
		{
			// Prepare stack
			var string_dict = new Dictionary<int, int>();
			string_buffer = null;
			int length = 0;
			int empty = String.Empty.GetHashCode();

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Fill empty string in the beginning
			string_dict[empty] = 1;
			length += 0x28;
			bw.Write(0);
			bw.Write(0);
			bw.WriteNullTermUTF8(mark, 0x20);

			// Function to write strings to dictionary and return its length
			var Inject = new Func<string, int, int>((value, len) =>
			{
				int key = value?.GetHashCode() ?? empty; // null = String.Empty in this case
				if (!string_dict.ContainsKey(key)) // skip if string is in the dictionary
				{
					string_dict[key] = len; // write position to dictionary
					len += value.Length + 1;     // increase length
					bw.WriteNullTermUTF8(value); // write string value
				}
				return len;
			});

			// Iterate through each model in the database
			foreach (var model in db.ModelParts.Collections)
			{
				// Iterate through each RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
					// Write debug name
					length = Inject(realpart.DebugName, length);

					// Iterate through attributes
					foreach (var attrib in realpart.Attributes)
					{
						// If attribute is a StringAttribute, write its value
						if (attrib is StringAttribute str_attrib)
							length = Inject(str_attrib.Value, length);

						// Else if attribute is a TwoStringAttribute, write its values
						else if (attrib is TwoStringAttribute two_attrib)
						{
							length = Inject(two_attrib.Value1, length);
							length = Inject(two_attrib.Value2, length);
						}
					}
				}
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			string_buffer = ms.ToArray();
			return string_dict;
		}

		private static int MakeAttribList(Dictionary<int, int> string_dict,
			Database.Underground1 db, out byte[] attrib_buffer)
		{
			// Initialize stack
			var attrib_list = new Dictionary<int, int>();
			attrib_buffer = null;
			int length = 0;

			// Initialize attrib stream
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through each model in the database
			foreach (var model in db.ModelParts.Collections)
			{
				// Iterate through each RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
					// If attribute count is 0, continue
					if (realpart.Attributes.Count == 0) continue;

					// Iterate through all attributes in RealCarPart
					foreach (var attribute in realpart.Attributes)
					{
						attribute.Assemble(bw, string_dict);
						++length;
					}
				}
			}

			// Return number of attributes
			var dif = 0x10 - ((int)ms.Length + 0x8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

			attrib_buffer = ms.ToArray();
			return length;
		}

		private static int MakeCPPartList(Dictionary<int, int> string_dict,
			Database.Underground1 db, out byte[] cppart_buffer)
		{
			// Initialize stack
			cppart_buffer = null;
			int key = 0;
			int count = 0; // for attribute count
			int length = 0; // for number of parts
			int empty = String.Empty.GetHashCode();

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through every model in the database
			foreach (var model in db.ModelParts.Collections)
			{
				// Iterate through every RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
					// Write main properties
					key = realpart.DebugName?.GetHashCode() ?? empty;
					bw.Write(string_dict[key]);
					bw.Write(model.BinKey);
					bw.Write(realpart.PartLabel.BinHash());
					bw.Write(realpart.BrandLabel.BinHash());
					bw.Write(realpart.CarPartGroupID);
					bw.Write(realpart.UpgradeGroupID);
					bw.Write(realpart.UpgradeStyle);
					bw.Write((byte)0);

					// Write offsets
					bw.Write(realpart.Length == 0 ? (int)0 : count);
					bw.Write((int)0);
					bw.Write(realpart.Length);
					count += realpart.Length;

					// Write geometries
					bw.Write(realpart.GeometryLodA.BinHash());
					bw.Write(realpart.GeometryLodB.BinHash());
					bw.Write(realpart.GeometryLodC.BinHash());
					bw.Write(realpart.GeometryLodD.BinHash());

					++length;
				}
			}

			// Return number of parts and buffer
			bw.FillBuffer(0x10);
			cppart_buffer = ms.ToArray();
			return length;
		}

		#endregion

		#region Private Disassemble

		private static long[] FindOffsets(BinaryReader br, int size)
		{
			var result = new long[4];
			var offset = br.BaseStream.Position;
			while (br.BaseStream.Position < offset + size)
			{
				switch (br.ReadUInt32())
				{
					case CarParts.DBCARPART_HEADER:
						result[0] = br.BaseStream.Position;
						goto default;

					case CarParts.DBCARPART_STRINGS:
						result[1] = br.BaseStream.Position;
						goto default;

					case CarParts.DBCARPART_ATTRIBS:
						result[2] = br.BaseStream.Position;
						goto default;

					case CarParts.DBCARPART_ARRAY:
						result[3] = br.BaseStream.Position;
						goto default;

					default:
						var skip = br.ReadInt32();
						br.BaseStream.Position += skip;
						break;
				}
			}
			br.BaseStream.Position = offset;
			return result;
		}

		private static Dictionary<int, string> ReadStrings(BinaryReader br)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var result = new Dictionary<int, string>(size >> 3);

			while (br.BaseStream.Position < offset + size)
			{
				var position = (int)(br.BaseStream.Position - offset);
				var value = br.ReadNullTermUTF8();
				result[position] = value;
			}
			return result;
		}

		private static CPAttribute[] ReadAttribs(BinaryReader br, 
			Dictionary<int, string> str, int maxlen)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var result = new CPAttribute[size >> 3]; // set initial capacity

			int count = 0;
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
				var key = br.ReadUInt32();
				if (!Map.CarPartKeys.TryGetValue(key, out var type))
					type = eCarPartAttribType.Integer;
				result[count] = type switch
				{
					eCarPartAttribType.Boolean => new BoolAttribute(br, key),
					eCarPartAttribType.CarPartID => new PartIDAttribute(br, key),
					eCarPartAttribType.Floating => new FloatAttribute(br, key),
					eCarPartAttribType.String => new StringAttribute(br, str, key),
					eCarPartAttribType.TwoString => new TwoStringAttribute(br, str, key),
					eCarPartAttribType.Key => new KeyAttribute(br, key),
					_ => new IntAttribute(br, key),
				};
				++count;
			}
			return result;
		}

		private static List<Parts.CarParts.TempPart> ReadTempParts(BinaryReader br,
			Dictionary<int, string> string_dict, int maxlen)
		{
			// Remove padding at the very end
			int size = br.ReadInt32(); // read current size
			var offset = br.BaseStream.Position;
			var result = new List<Parts.CarParts.TempPart>(maxlen); // initialize

			int count = 0;
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
				var part = new Parts.CarParts.TempPart();
				part.Disassemble(br, string_dict);
				result.Add(part);
				++count;
			}
			return result;
		}

		#endregion

		/// <summary>
		/// Assembles entire root of <see cref="DBModelPart"/> into a byte array and 
		/// writes it with <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the strings block.</param>
		/// <param name="db"><see cref="Database.Underground1"/> database with roots 
		/// and collections.</param>
		public static void Assemble(BinaryWriter bw, string mark, Database.Underground1 db)
		{
			// Get string map
			var string_dict = MakeStringList(db, mark, out var string_buffer);

			// Get attribute list
			var numattrs = MakeAttribList(string_dict, db, out var attrib_buffer);

			// Get temppart list
			var numparts = MakeCPPartList(string_dict, db, out var cppart_buffer);

			// Get header
			var header_buffer = MakeHeader(numattrs, numparts);

			// Precalculate size
			int size = 0;
			size += header_buffer.Length + 8;
			size += string_buffer.Length + 8;
			size += attrib_buffer.Length + 8;
			size += cppart_buffer.Length + 8;

			// Write ID and Size
			bw.Write(CarParts.MAINID);
			bw.Write(size);

			// Write header
			bw.Write(CarParts.DBCARPART_HEADER);
			bw.Write(header_buffer.Length);
			bw.Write(header_buffer);

			// Write strings
			bw.Write(CarParts.DBCARPART_STRINGS);
			bw.Write(string_buffer.Length);
			bw.Write(string_buffer);

			// Write attributes
			bw.Write(CarParts.DBCARPART_ATTRIBS);
			bw.Write(attrib_buffer.Length);
			bw.Write(attrib_buffer);

			// Write cpparts
			bw.Write(CarParts.DBCARPART_ARRAY);
			bw.Write(cppart_buffer.Length);
			bw.Write(cppart_buffer);
		}

		/// <summary>
		/// Disassembles entire car parts block using <see cref="BinaryReader"/> provided 
		/// into <see cref="DBModelPart"/> collections and stores them in 
		/// <see cref="Database.Underground1"/> passed.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="size">Size of the car parts block.</param>
		/// <param name="db"><see cref="Database.Underground1"/> where all collections 
		/// should be stored.</param>
		public static void Disassemble(BinaryReader br, int size, Database.Underground1 db)
		{
			long position = br.BaseStream.Position;
			var offsets = FindOffsets(br, size);

			// We need to read part0 as well
			br.BaseStream.Position = offsets[0] + 0x14;
			int maxcparts = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x1C;
			int maxattrib = br.ReadInt32();

			// Initialize stream over string block
			br.BaseStream.Position = offsets[1];
			var string_dict = ReadStrings(br);

			// Read all car part attributes
			br.BaseStream.Position = offsets[2];
			var attrib_list = ReadAttribs(br, string_dict, maxattrib);

			// Read all temporary parts
			br.BaseStream.Position = offsets[3];
			var temp_cparts = ReadTempParts(br, string_dict, maxcparts);

			// Generate Model Collections
			int index = -1;
			foreach (var group in temp_cparts.GroupBy(_ => _.CarNameHash))
			{
				++index;
				var collection = new DBModelPart(group.Key.BinString(eLookupReturn.EMPTY), db);
				foreach (var temppart in group)
				{
					var realpart = new Parts.CarParts.RealCarPart(index, temppart.AttribEnd - temppart.AttribStart, collection)
					{
						PartLabel = temppart.PartNameHash.BinString(eLookupReturn.EMPTY),
						BrandLabel = temppart.BrandNameHash.BinString(eLookupReturn.EMPTY),
						DebugName = temppart.DebugName,
						CarPartGroupID = temppart.CarPartGroupID,
						UpgradeGroupID = temppart.UpgradeGroupID,
						UpgradeStyle = temppart.UpgradeStyle,
						GeometryLodA = temppart.LodAHash.BinString(eLookupReturn.EMPTY),
						GeometryLodB = temppart.LodBHash.BinString(eLookupReturn.EMPTY),
						GeometryLodC = temppart.LodCHash.BinString(eLookupReturn.EMPTY),
						GeometryLodD = temppart.LodDHash.BinString(eLookupReturn.EMPTY),
					};
					for (int a1 = temppart.AttribStart; a1 < temppart.AttribEnd; ++a1)
					{
						if (temppart.AttribOffset + a1 >= attrib_list.Length) continue;
						var tempattrib = attrib_list[temppart.AttribOffset + a1];
						var addon = attrib_list[temppart.AttribOffset + a1].PlainCopy();
						addon.BelongsTo = realpart;
						realpart.Attributes.Add(addon);
					}
					collection.ModelCarParts.Add(realpart);
				}
				collection.ResortNames();
				db.ModelParts.Collections.Add(collection);
			}

			br.BaseStream.Position = position + size;
		}
	}
}
