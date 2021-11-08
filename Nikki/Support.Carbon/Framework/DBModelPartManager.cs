using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Reflection;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A static manager to assemble and disassemble <see cref="DBModelPart"/> collections.
	/// </summary>
	public class DBModelPartManager : Manager<DBModelPart>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Carbon.ToString();

		/// <summary>
		/// Name of this <see cref="DBModelPartManager"/>.
		/// </summary>
		public override string Name => "DBModelParts";

		/// <summary>
		/// If true, manager can export and import non-serialized collection; otherwise, false.
		/// </summary>
		public override bool AllowsNoSerialization => false;

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="DBModelPartManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Gets a collection and unit element type in this <see cref="DBModelPartManager"/>.
		/// </summary>
		public override Type CollectionType => typeof(DBModelPart);

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPartManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public DBModelPartManager(Datamap db) : base(db)
		{
			this.Extender = 5;
			this.Alignment = new Alignment(0xC, Alignment.AlignmentType.Actual);
		}

		#region Private Assemble

		private byte[] MakeHeader(int attribcount, int modelcount, int structcount, int partcount)
		{
			var result = new byte[0x40];

			using var ms = new MemoryStream(result);
			using var bw = new BinaryWriter(ms);

			bw.BaseStream.Position = 0x08;
			bw.Write(6); // write C version

			bw.BaseStream.Position = 0x20;
			bw.Write(attribcount); // write attribute count

			bw.BaseStream.Position = 0x28;
			bw.Write(modelcount); // write model count

			bw.BaseStream.Position = 0x30;
			bw.Write(structcount); // write struct count

			bw.BaseStream.Position = 0x38;
			bw.Write(partcount); // write part count

			return result;
		}

		private Dictionary<int, int> MakeStringList(string mark, out byte[] string_buffer)
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
				
					string_dict[key] = len >> 2; // write position to dictionary
					len += value.Length + 1;     // increase length
					int diff = 4 - len % 4;      // calculate padding
					if (diff != 4) len += diff;  // add padding
					bw.WriteNullTermUTF8(value); // write string value
					bw.FillBuffer(4);            // fill buffer to % 4
				
				}
				
				return len;
			
			});

			// Iterate through each model in the database
			foreach (var model in this)
			{

				// Iterate through each RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
				
					// Iterate through attributes
					foreach (var attrib in realpart.Attributes)
					{
					
						// If attribute is a StringAttribute, write its value
						if (attrib is StringAttribute str_attrib)
						{

							if (str_attrib.ValueExists == eBoolean.True)
							{

								length = Inject(str_attrib.Value, length);

							}
						
						}

						// Else if attribute is a TwoStringAttribute, write its values
						else if (attrib is TwoStringAttribute two_attrib)
						{

							if (two_attrib.Value1Exists == eBoolean.True)
							{

								length = Inject(two_attrib.Value1, length);

							}

							if (two_attrib.Value2Exists == eBoolean.True)
							{

								length = Inject(two_attrib.Value2, length);

							}
						
						}

						// Else if attribute is a ModelTableAttribute, write its settings
						else if (attrib is ModelTableAttribute table_attrib)
						{

							if (table_attrib.Templated == eBoolean.True)
							{

								if (table_attrib.ConcatenatorExists == eBoolean.True)
								{

									length = Inject(table_attrib.Concatenator, length);

								}

								for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
								{

									for (int index = 0; index <= 11; ++index)
									{
									
										var lodname = $"Geometry{index}Lod{lod}";
										var lodexists = $"{lodname}Exists";
										
										if ((eBoolean)table_attrib.GetFastPropertyValue(lodexists) == eBoolean.True)
										{

											length = Inject(table_attrib.GetValue(lodname), length);

										}
									
									}
								
								}
							
							}
						
						}

						// Else if attribute is CustomAttribute, write its strings
						else if (attrib is CustomAttribute cust_attrib)
						{

							switch (cust_attrib.Type)
							{

								case CarPartAttribType.String:
									length = Inject(cust_attrib.ValueString, length);
									break;

								case CarPartAttribType.TwoString:
									length = Inject(cust_attrib.ValueString1, length);
									length = Inject(cust_attrib.ValueString2, length);
									break;

								default:
									break;

							}

						}
					
					}
				
				}
			
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			string_buffer = ms.ToArray();
			return string_dict;
		}

		private Dictionary<int, int> MakeOffsetList(Dictionary<int, int> attrib_dict, out byte[] offset_buffer)
		{
			// Initialize stack
			var offset_map = new Dictionary<int, int>();  // CPOffset to AttribOffset
			var offset_dict = new Dictionary<int, int>(); // RealCarPart to AttribOffset
			var offset_list = new Dictionary<int, CPOffset>(); // for debugging
			offset_buffer = null;
			int length = 0;
			int key = 0;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through every model in the database
			foreach (var model in this)
			{

				// Iterate through every RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
				
					// Skip if no attributes
					if (realpart.Attributes.Count == 0)
					{
					
						offset_dict[realpart.GetHashCode()] = -1;
						continue;
					
					}

					// Initialize new CPOffset and store all attribute offsets in it
					var offset = new CPOffset(realpart.Attributes.Count);
					
					foreach (var attrib in realpart.Attributes)
					{
					
						var index = attrib_dict[attrib.GetHashCode()]; // get index
						offset.AttribOffsets.Add((ushort)index);       // add to CPOffset
					
					}

					offset.AttribOffsets.Sort();
					key = offset.GetHashCode();
					
					if (!offset_map.ContainsKey(key)) // if CPOffset exists, skip
					{
					
						offset_map[key] = length; // write length to map
						offset_list[key] = offset; // save offset for debug

						bw.Write((ushort)offset.AttribOffsets.Count); // write count

						foreach (var attrib in offset.AttribOffsets)  // write all attributes
						{

							bw.Write(attrib);

						}

						length += 1 + offset.AttribOffsets.Count; // increase length
					
					}
					else
					{

						var original = offset_list[key];

						if (original != offset)
						{

							var logger = new Logger("MainLog.txt", "Nikki.dll : Carbon DatabaseSaver", true);

							logger.WriteLine("Internal error has occurred: DBMP:AO Key Exception");
							logger.WriteLine($"Comparing keys 9x{original.GetHashCode():X8} vs 0x{offset.GetHashCode():X8}");

							logger.WriteLine($"Currently stored: {original}");
							logger.WriteLine($"Attempted to add: {offset}");

							logger.WriteLine("---------------------------------------------------------------------------");
							logger.WriteLine("Currently stored information");
							original.WriteToLog(logger);

							logger.WriteLine("---------------------------------------------------------------------------");
							logger.WriteLine("Attempted to add information");
							offset.WriteToLog(logger);

							throw new Exception("Internal error has occurred: DBMP:AO Key Exception. Please report to the developer immediately, " +
								"and try resaving the file!");
						
						}

					}

					offset_dict[realpart.GetHashCode()] = offset_map[key]; // store to main map
				
				}
			
			}

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 4) % 0x10;
			if (dif != 0x10) bw.WriteBytes(0, dif);

			offset_buffer = ms.ToArray();
			return offset_dict;
		}

		private Dictionary<int, int> MakeAttribList(Dictionary<int, int> string_dict, out byte[] attrib_buffer)
		{
			// Initialize stack
			var attrib_list = new Dictionary<int, int>();
			attrib_buffer = null;
			int length = 0;
			int key = 0;

			// Initialize attrib stream
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through each model in the database
			foreach (var model in this)
			{
				
				// Iterate through each RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
				
					// If attribute count is 0, continue
					if (realpart.Attributes.Count == 0) continue;

					// Iterate through all attributes in RealCarPart
					foreach (var attribute in realpart.Attributes)
					{
					
						key = attribute.GetHashCode();
						
						if (!attrib_list.ContainsKey(key)) // if it already exists, skip
						{
						
							attrib_list[key] = length++;
							attribute.Assemble(bw, string_dict);
						
						}
					
					}
				
				}
			}

			// Return prepared dictionary
			bw.FillBuffer(0x10);
			attrib_buffer = ms.ToArray();
			return attrib_list;
		}

		private int MakeStructList(Dictionary<int, int> string_dict, out byte[] struct_buffer)
		{
			// Initialize stack
			struct_buffer = null;
			int count = 0;
			var table_list = new Dictionary<int, int>(); // map of hashcodes to index

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			// Iterate through each model in the database
			foreach (var model in this)
			{

				// Iterate through each RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
				
					// If attribute count is 0, continue
					if (realpart.Attributes.Count == 0) continue;

					var temp_attr = realpart.GetAttribute((uint)eAttribModelTable.MODEL_TABLE_OFFSET);
					if (temp_attr == null) continue; // if no ModelTableAttribute in the part

					// If there is ModelTableAttribute
					if (temp_attr is ModelTableAttribute table_attr)
					{
					
						var code = table_attr.GetHashCode(); // get hash code
						
						if (!table_list.TryGetValue(code, out int index)) // check if it already exists
						{
						
							table_attr.Index = count; // if no, set new index, increment count
							table_list[code] = count++;
							table_attr.WriteStruct(bw, string_dict); // write struct
						
						}
						else
						{
						
							table_attr.Index = index; // if exists, set index in the attribute
						
						}
					
					}
				
				}
			}

			// If no ModelOffsetAttributes, return empty array
			if (count == 0)
			{

				struct_buffer = new byte[0];
				return count;

			}

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(0, dif);

			struct_buffer = ms.ToArray();
			return count;
		}
		
		private int MakeModelsList(out byte[] models_buffer)
		{
			// Precalculate size; offset should be at 0xC
			var size = this.Count * 4;
			var dif = 0x10 - (size + 8) % 0x10;
			if (dif != 0x10) size += dif;
			models_buffer = new byte[size];

			// Initialize streams
			using var ms = new MemoryStream(models_buffer);
			using var bw = new BinaryWriter(ms);

			// Write all BinKeys of models
			for (int loop = 0; loop < this.Count; ++loop)
			{

				bw.Write(this[loop].BinKey);

			}

			// Return prepared list
			return this.Count;
		}

		private int MakeCPPartList(Dictionary<int, int> offset_dict, out byte[] cppart_buffer)
		{
			// Initialize stack
			cppart_buffer = null;
			int length = 0;
			int empty = String.Empty.GetHashCode();
			const ushort negative = 0xFFFF;

			// Initialize streams
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			byte count = 0;

			// Iterate through every model in the database
			foreach (var model in this)
			{
			
				// Iterate through every RealCarPart in a model
				foreach (Parts.CarParts.RealCarPart realpart in model.ModelCarParts)
				{
				
					bw.Write((byte)0);
					bw.Write(count);

					// Write attribute offset
					if (realpart.Attributes.Count == 0) bw.Write(negative);
					else bw.Write((ushort)offset_dict[realpart.GetHashCode()]);

					++length;
				
				}
			
				++count;
			
			}

			// Return number of parts and buffer
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(0, dif);

			cppart_buffer = ms.ToArray();
			return length;
		}

		private int MakeCustomAttrList(out byte[] custattr_buffer)
		{
			custattr_buffer = null;
			var map = new Dictionary<int, CustomAttribute>();
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);

			bw.Write(-1); // total number of custom attributes
			int count = 0;

			foreach (var model in this)
			{

				foreach (var part in model.ModelCarParts)
				{

					foreach (var attrib in part.Attributes)
					{

						if (attrib.AttribType == CarPartAttribType.Custom)
						{

							var custom = attrib as CustomAttribute;
							var code = custom.GetHashCode();
							if (!map.ContainsKey(code)) map.Add(code, custom);

						}

					}

				}

			}

			foreach (var custom in map.Values)
			{

				bw.WriteNullTermUTF8(custom.Name);
				bw.Write(custom.Key);
				bw.WriteEnum(custom.Type);
				++count;

			}

			// Return buffer and its length
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(0, dif);

			bw.BaseStream.Position = 0;
			bw.Write(count);
			custattr_buffer = ms.ToArray();
			return custattr_buffer.Length;
		}

		#endregion

		#region Private Disassemble

		private long[] FindOffsets(BinaryReader br, int size)
		{
			var result = new long[8];
			var offset = br.BaseStream.Position;
			result[7] = -1;
			
			while (br.BaseStream.Position < offset + size)
			{

				var id = br.ReadEnum<BinBlockID>();

				switch (id)
				{
					case BinBlockID.DBCarParts_Header:
						result[0] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Strings:
						result[1] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Offsets:
						result[2] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Attribs:
						result[3] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Structs:
						result[4] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Models:
						result[5] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Array:
						result[6] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Custom:
						result[7] = br.BaseStream.Position;
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

		private Dictionary<int, CPOffset> ReadOffsets(BinaryReader br)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var result = new Dictionary<int, CPOffset>(size >> 3); // set initial capacity

			while (br.BaseStream.Position < offset + size)
			{

				var position = (int)(br.BaseStream.Position - offset);
				var count = br.ReadUInt16();
				var cpoff = new CPOffset(count, position);

				for (int a1 = 0; a1 < count; ++a1)
				{

					cpoff.AttribOffsets.Add(br.ReadUInt16());

				}

				result[position >> 1] = cpoff;
			}

			return result;
		}

		private CPAttribute[] ReadAttribs(BinaryReader br, BinaryReader str, int maxlen, Dictionary<uint, CustomCP> cpmap)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var result = new CPAttribute[size >> 3]; // set initial capacity

			int count = 0;

			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
			
				var key = br.ReadUInt32();
				CustomCP cp = null;

				// If existing map does not have attribute key, it might be a custom key then
				if (!Map.CarPartKeys.TryGetValue(key, out var type))
				{

					type = CarPartAttribType.Custom;
					if (!cpmap.TryGetValue(key, out cp)) cp = new CustomCP(key);

				}

				result[count] = type switch
				{
					CarPartAttribType.Boolean => new BoolAttribute(br, key),
					CarPartAttribType.CarPartID => new PartIDAttribute(br, key),
					CarPartAttribType.Floating => new FloatAttribute(br, key),
					CarPartAttribType.String => new StringAttribute(br, str, key),
					CarPartAttribType.TwoString => new TwoStringAttribute(br, str, key),
					CarPartAttribType.Color => new ColorAttribute(br, key),
					CarPartAttribType.Key => new KeyAttribute(br, key),
					CarPartAttribType.ModelTable => new ModelTableAttribute(br, key),
					CarPartAttribType.Custom => new CustomAttribute(br, str, cp),
					_ => new IntAttribute(br, key),
				};

				++count;
			
			}
			
			return result;
		}

		private string[] ReadModels(BinaryReader br, int maxlen)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var count = size >> 2;

			count = (count > maxlen) ? maxlen : count;
			var result = new string[count];

			for (int a1 = 0; a1 < count; ++a1)
			{

				var key = br.ReadUInt32();
				result[a1] = key.BinString(LookupReturn.EMPTY);
			
			}
			
			return result;
		}

		private List<Parts.CarParts.TempPart> ReadTempParts(BinaryReader br, int maxlen)
		{
			// Remove padding at the very end
			int size = br.ReadInt32(); // read current size
			var offset = br.BaseStream.Position;
			var result = new List<Parts.CarParts.TempPart>(maxlen); // initialize

			int count = 0;
			
			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
			
				var part = new Parts.CarParts.TempPart();
				part.Disassemble(br);
				result.Add(part);
				++count;
			
			}
			
			return result;
		}

		private Dictionary<uint, CustomCP> ReadCustomCP(BinaryReader br, long offset)
		{
			if (offset != -1)
			{

				var map = new Dictionary<uint, CustomCP>();
				br.BaseStream.Position = offset;
				var len = br.ReadInt32();

				if (len > 0)
				{

					var count = br.ReadInt32();

					for (int i = 0; i < count; ++i)
					{

						var cp = new CustomCP();
						cp.Read(br);
						map[cp.Key] = cp;

					}

				}

				return map;

			}

			return null;
		}

		#endregion

		/// <summary>
		/// Assembles entire root of <see cref="DBModelPart"/> into a byte array and 
		/// writes it with <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark written when saving.</param>
		private void Encode(BinaryWriter bw, string mark)
		{
			// Get string map
			var string_dict = this.MakeStringList(mark, out var string_buffer);

			// Get struct map
			var numstructs = this.MakeStructList(string_dict, out var struct_buffer);

			// Get attribute map
			var attrib_dict = this.MakeAttribList(string_dict, out var attrib_buffer);

			// Get offset map
			var offset_dict = this.MakeOffsetList(attrib_dict, out var offset_buffer);

			// Get models list
			var nummodels = this.MakeModelsList(out var models_buffer);

			// Get temppart list
			var numparts = this.MakeCPPartList(offset_dict, out var cppart_buffer);

			// Get custom attribute list
			var custattrs = this.MakeCustomAttrList(out var custat_buffer);

			// Get header
			var header_buffer = this.MakeHeader(attrib_dict.Count, nummodels, numstructs, numparts);

			// Precalculate size
			int size = 0;
			size += header_buffer.Length + 8;
			size += string_buffer.Length + 8;
			size += offset_buffer.Length + 8;
			size += attrib_buffer.Length + 8;
			size += struct_buffer.Length + 8;
			size += models_buffer.Length + 8;
			size += cppart_buffer.Length + 8;
			size += custat_buffer.Length + 8;

			// Write ID and Size
			bw.WriteEnum(BinBlockID.DBCarParts);
			bw.Write(size);

			// Write header
			bw.WriteEnum(BinBlockID.DBCarParts_Header);
			bw.Write(header_buffer.Length);
			bw.Write(header_buffer);

			// Write strings
			bw.WriteEnum(BinBlockID.DBCarParts_Strings);
			bw.Write(string_buffer.Length);
			bw.Write(string_buffer);

			// Write offsets
			bw.WriteEnum(BinBlockID.DBCarParts_Offsets);
			bw.Write(offset_buffer.Length);
			bw.Write(offset_buffer);

			// Write attributes
			bw.WriteEnum(BinBlockID.DBCarParts_Attribs);
			bw.Write(attrib_buffer.Length);
			bw.Write(attrib_buffer);

			// Write structs
			bw.WriteEnum(BinBlockID.DBCarParts_Structs);
			bw.Write(struct_buffer.Length);
			bw.Write(struct_buffer);

			// Write models
			bw.WriteEnum(BinBlockID.DBCarParts_Models);
			bw.Write(models_buffer.Length);
			bw.Write(models_buffer);

			// Write cpparts
			bw.WriteEnum(BinBlockID.DBCarParts_Array);
			bw.Write(cppart_buffer.Length);
			bw.Write(cppart_buffer);

			// Write custom attributes
			bw.WriteEnum(BinBlockID.DBCarParts_Custom);
			bw.Write(custattrs);
			bw.Write(custat_buffer);
		}

		/// <summary>
		/// Disassembles entire car parts block using <see cref="BinaryReader"/> provided 
		/// into <see cref="DBModelPart"/> collections.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="size">Size of the block.</param>
		private void Decode(BinaryReader br, int size)
		{
			long position = br.BaseStream.Position;
			var offsets = this.FindOffsets(br, size);

			// We need to read part0 as well
			br.BaseStream.Position = offsets[0] + 0x24;
			int maxattrib = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x2C;
			int maxmodels = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x34;
			int maxstruct = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x3C;
			int maxcparts = br.ReadInt32();

			// Get all custom attributes
			var cpmap = this.ReadCustomCP(br, offsets[7]);

			// Initialize stream over string block
			br.BaseStream.Position = offsets[1];
			var strlen = br.ReadInt32();
			var strarr = br.ReadBytes(strlen);
			using var StrStream = new MemoryStream(strarr);
			using var StrReader = new BinaryReader(StrStream);

			// Read all attribute offsets
			br.BaseStream.Position = offsets[2];
			var offset_dict = this.ReadOffsets(br);

			// Read all car part attributes
			br.BaseStream.Position = offsets[3];
			var attrib_list = this.ReadAttribs(br, StrReader, maxattrib, cpmap);

			// Read all models
			br.BaseStream.Position = offsets[5];
			var models_list = this.ReadModels(br, maxmodels);

			// Read all car part structs
			br.BaseStream.Position = offsets[4];
			var tablen = br.ReadInt32();
			var tabarr = br.ReadBytes(tablen);
			using var TabStream = new MemoryStream(tabarr);
			using var TabReader = new BinaryReader(TabStream);

			// Read all temporary parts
			br.BaseStream.Position = offsets[6];
			var temp_cparts = this.ReadTempParts(br, maxcparts);

			// Increase capacity based on model count
			this.Capacity = models_list.Length;

			// Generate Model Collections
			for (int a1 = 0; a1 < models_list.Length; ++a1)
			{

				if (String.IsNullOrEmpty(models_list[a1])) continue;

				DBModelPart collection;

				try { collection = new DBModelPart(models_list[a1], this); }
				catch { continue; } // in case exists

				var tempparts = temp_cparts.FindAll(_ => _.Index == a1);
				
				foreach (var temppart in tempparts)
				{
				
					offset_dict.TryGetValue(temppart.AttribOffset, out var cpoff);
					
					var realpart = new Parts.CarParts.RealCarPart(collection, cpoff?.AttribOffsets.Count ?? 0);
					
					foreach (var attroff in cpoff?.AttribOffsets ?? Enumerable.Empty<ushort>())
					{
					
						if (attroff >= attrib_list.Length) continue;
						var addon = (CPAttribute)attrib_list[attroff].PlainCopy();

						if (addon is ModelTableAttribute tableattr)
						{

							tableattr.ReadStruct(TabReader, StrReader);

						}

						realpart.Attributes.Add(addon);
					
					}
					
					collection.ModelCarParts.Add(realpart);
				
				}

				try { this.Add(collection); }
				catch { } // skip if exists

			}

			br.BaseStream.Position = position + size;
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal override void Assemble(BinaryWriter bw, string mark)
		{
			if (this.Count == 0) return;
			bw.GeneratePadding(mark, this.Alignment);
			this.Encode(bw, mark);
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="DBModelPartManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != BinBlockID.DBCarParts) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();
				this.Decode(br, size);

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

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}
		}

		/// <summary>
		/// Exports collection with CollectionName specified to a filename provided.
		/// </summary>
		/// <param name="cname">CollectionName of a collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="serialized">True if collection exported should be serialized; 
		/// false otherwise.</param>
		public override void Export(string cname, BinaryWriter bw, bool serialized = true)
		{
			var index = this.IndexOf(cname);

			if (index == -1)
			{

				throw new Exception($"Collection named {cname} does not exist");

			}
			else
			{

				if (serialized) this[index].Serialize(bw);
				else throw new NotSupportedException("Collection supports only serialization and no plain export");

			}
		}

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="Manager{T}"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(SerializeType type, BinaryReader br)
		{
			var position = br.BaseStream.Position;
			var header = new SerializationHeader();
			header.Read(br);

			var collection = new DBModelPart();

			if (header.ID != BinBlockID.Nikki)
			{

				throw new Exception($"Missing serialized header in the imported collection");

			}
			else
			{

				if (header.Game != this.GameINT)
				{

					throw new Exception($"Stated game inside collection is {header.Game}, while should be {this.GameINT}");

				}

				if (header.Name != this.Name)
				{

					throw new Exception($"Imported collection is not a collection of type {this.Name}");

				}

				collection.Deserialize(br);

			}

			var index = this.IndexOf(collection);

			if (index == -1)
			{

				collection.Manager = this;
				this.Add(collection);

			}
			else
			{

				switch (type)
				{
					case SerializeType.Negate:
						break;

					case SerializeType.Override:
						collection.Manager = this;
						this.Replace(collection, index);
						break;

					case SerializeType.Synchronize:
						this[index].Synchronize(collection);
						break;

					default:
						break;
				}

			}
		}
	}
}
