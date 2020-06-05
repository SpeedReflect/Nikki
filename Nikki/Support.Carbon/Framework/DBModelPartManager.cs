using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Database;
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
		/// Name of this <see cref="DBModelPartManager"/>.
		/// </summary>
		public override string Name => "DBModelParts";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="CollisionManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="DBModelPartManager"/>.
		/// </summary>
		/// <param name="db"><see cref="FileBase"/> to which this manager belongs to.</param>
		public DBModelPartManager(FileBase db)
		{
			this.Database = db;
			this.Extender = 5;
			this.Alignment = new Alignment(0xC, Alignment.eAlignType.Actual);
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
						bw.Write((ushort)offset.AttribOffsets.Count); // write count

						foreach (var attrib in offset.AttribOffsets)  // write all attributes
						{

							bw.Write(attrib);

						}

						length += 1 + offset.AttribOffsets.Count; // increase length
					
					}

					offset_dict[realpart.GetHashCode()] = offset_map[key]; // store to main map
				
				}
			
			}

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 4) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

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

			// Return prepared dictionary
			var dif = 0x10 - ((int)ms.Length + 8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(dif);

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
			if (dif != 0x10) bw.WriteBytes(dif);

			cppart_buffer = ms.ToArray();
			return length;
		}

		#endregion

		#region Private Disassemble

		private long[] FindOffsets(BinaryReader br, int size)
		{
			var result = new long[7];
			var offset = br.BaseStream.Position;
			
			while (br.BaseStream.Position < offset + size)
			{

				var id = br.ReadEnum<eBlockID>();

				switch (id)
				{
					case eBlockID.DBCarParts_Header:
						result[0] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Strings:
						result[1] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Offsets:
						result[2] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Attribs:
						result[3] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Structs:
						result[4] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Models:
						result[5] = br.BaseStream.Position;
						goto default;

					case eBlockID.DBCarParts_Array:
						result[6] = br.BaseStream.Position;
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

		private CPAttribute[] ReadAttribs(BinaryReader br, BinaryReader str, int maxlen)
		{
			var size = br.ReadInt32();
			var offset = br.BaseStream.Position;
			var result = new CPAttribute[size >> 3]; // set initial capacity

			int count = 0;

			while (count < maxlen && br.BaseStream.Position < offset + size)
			{
			
				var key = br.ReadUInt32();
				if (!Map.CarPartKeys.TryGetValue(key, out var type))
				{

					type = eCarPartAttribType.Integer;

				}

				result[count] = type switch
				{
					eCarPartAttribType.Boolean => new BoolAttribute(br, key),
					eCarPartAttribType.CarPartID => new PartIDAttribute(br, key),
					eCarPartAttribType.Floating => new FloatAttribute(br, key),
					eCarPartAttribType.String => new StringAttribute(br, str, key),
					eCarPartAttribType.TwoString => new TwoStringAttribute(br, str, key),
					eCarPartAttribType.Key => new KeyAttribute(br, key),
					eCarPartAttribType.ModelTable => new ModelTableAttribute(br, key),
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
				result[a1] = key.BinString(eLookupReturn.EMPTY);
			
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

			// Write ID and Size
			bw.WriteEnum(eBlockID.DBCarParts);
			bw.Write(size);

			// Write header
			bw.WriteEnum(eBlockID.DBCarParts_Header);
			bw.Write(header_buffer.Length);
			bw.Write(header_buffer);

			// Write strings
			bw.WriteEnum(eBlockID.DBCarParts_Strings);
			bw.Write(string_buffer.Length);
			bw.Write(string_buffer);

			// Write offsets
			bw.WriteEnum(eBlockID.DBCarParts_Offsets);
			bw.Write(offset_buffer.Length);
			bw.Write(offset_buffer);

			// Write attributes
			bw.WriteEnum(eBlockID.DBCarParts_Attribs);
			bw.Write(attrib_buffer.Length);
			bw.Write(attrib_buffer);

			// Write structs
			bw.WriteEnum(eBlockID.DBCarParts_Structs);
			bw.Write(struct_buffer.Length);
			bw.Write(struct_buffer);

			// Write models
			bw.WriteEnum(eBlockID.DBCarParts_Models);
			bw.Write(models_buffer.Length);
			bw.Write(models_buffer);

			// Write cpparts
			bw.WriteEnum(eBlockID.DBCarParts_Array);
			bw.Write(cppart_buffer.Length);
			bw.Write(cppart_buffer);
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
			var attrib_list = this.ReadAttribs(br, StrReader, maxattrib);

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
				var collection = new DBModelPart(models_list[a1], this);
				var tempparts = temp_cparts.FindAll(_ => _.Index == a1);

				int count = 0;
				
				foreach (var temppart in tempparts)
				{
				
					offset_dict.TryGetValue(temppart.AttribOffset, out var cpoff);
					
					var realpart = new Parts.CarParts.RealCarPart(a1, cpoff?.AttribOffsets.Count ?? 0, collection)
					{
						PartName = $"{models_list[a1]}_PART_{count++.ToString()}"
					};
					
					foreach (var attroff in cpoff?.AttribOffsets ?? Enumerable.Empty<ushort>())
					{
					
						if (attroff >= attrib_list.Length) continue;
						var addon = (CPAttribute)attrib_list[attroff].PlainCopy();
						addon.BelongsTo = realpart;

						if (addon is ModelTableAttribute tableattr)
						{

							tableattr.ReadStruct(TabReader, StrReader);

						}

						realpart.Attributes.Add(addon);
					
					}
					
					collection.ModelCarParts.Add(realpart);
				
				}

				collection.ResortNames();
				this.Add(collection);
			
			}

			br.BaseStream.Position = position + size;
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal void Assemble(BinaryWriter bw, string mark)
		{
			bw.GeneratePadding(mark, this.Alignment);
			this.Encode(bw, mark);
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="DBModelPartManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.DBCarParts) return;

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
	}
}
