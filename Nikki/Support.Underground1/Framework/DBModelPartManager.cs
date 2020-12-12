using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Underground1.Class;
using Nikki.Support.Underground1.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;



namespace Nikki.Support.Underground1.Framework
{
	/// <summary>
	/// A static manager to assemble and disassemble <see cref="DBModelPart"/> collections.
	/// </summary>
	public class DBModelPartManager : Manager<DBModelPart>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Underground1;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Underground1.ToString();

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
			this.Alignment = new Alignment(0x8, Alignment.AlignmentType.Actual);
		}

		#region Private Assemble

		private byte[] MakeHeader(int attribcount, int partcount)
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
				
					string_dict[key] = len;      // write position to dictionary
					len += value.Length + 1;     // increase length
					bw.WriteNullTermUTF8(value); // write string value
			
				}
				
				return len;
			
			});

			// Iterate through each model in the database
			foreach (var model in this)
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
						{

							length = Inject(str_attrib.Value, length);

						}

						// Else if attribute is CustomAttribute, write its strings
						else if (attrib is CustomAttribute cust_attrib)
						{

							switch (cust_attrib.Type)
							{

								case CarPartAttribType.String:
									length = Inject(cust_attrib.ValueString, length);
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

		private int MakeAttribList(Dictionary<int, int> string_dict, out byte[] attrib_buffer)
		{
			// Initialize stack
			attrib_buffer = null;
			int length = 0;

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

						attribute.Assemble(bw, string_dict);
						++length;

					}

				}

			}

			// Return number of attributes
			var dif = 0x10 - ((int)ms.Length + 0x8) % 0x10;
			if (dif != 0x10) bw.WriteBytes(0, dif);

			attrib_buffer = ms.ToArray();
			return length;
		}

		private int MakeCPPartList(Dictionary<int, int> string_dict, out byte[] cppart_buffer)
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
			foreach (var model in this)
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
					bw.WriteEnum(realpart.CarPartGroupID);
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
			var result = new long[5];
			var offset = br.BaseStream.Position;
			result[4] = -1;

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

					case BinBlockID.DBCarParts_Attribs:
						result[2] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Array:
						result[3] = br.BaseStream.Position;
						goto default;

					case BinBlockID.DBCarParts_Custom:
						result[4] = br.BaseStream.Position;
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
					CarPartAttribType.Floating => new FloatAttribute(br, key),
					CarPartAttribType.String => new StringAttribute(br, str, key),
					CarPartAttribType.Key => new KeyAttribute(br, key),
					CarPartAttribType.Custom => new CustomAttribute(br, str, cp),
					_ => new IntAttribute(br, key),
				};

				++count;

			}

			return result;
		}

		private List<Parts.CarParts.TempPart> ReadTempParts(BinaryReader br,
			BinaryReader str_reader, int maxlen)
		{
			// Remove padding at the very end
			int size = br.ReadInt32(); // read current size
			var offset = br.BaseStream.Position;
			var result = new List<Parts.CarParts.TempPart>(maxlen); // initialize

			int count = 0;

			while (count < maxlen && br.BaseStream.Position < offset + size)
			{

				var part = new Parts.CarParts.TempPart();
				part.Disassemble(br, str_reader);
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
			// Sort by key
			this.SortByKey();

			// Get string map
			var string_dict = this.MakeStringList(mark, out var string_buffer);

			// Get attribute list
			var numattrs = this.MakeAttribList(string_dict, out var attrib_buffer);

			// Get temppart list
			var numparts = this.MakeCPPartList(string_dict, out var cppart_buffer);

			// Get custom attribute list
			var custattrs = this.MakeCustomAttrList(out var custat_buffer);

			// Get header
			var header_buffer = this.MakeHeader(numattrs, numparts);

			// Precalculate size
			int size = 0;
			size += header_buffer.Length + 8;
			size += string_buffer.Length + 8;
			size += attrib_buffer.Length + 8;
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

			// Write attributes
			bw.WriteEnum(BinBlockID.DBCarParts_Attribs);
			bw.Write(attrib_buffer.Length);
			bw.Write(attrib_buffer);

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
		/// <param name="size">Size of the car parts block.</param>
		private void Decode(BinaryReader br, int size)
		{
			long position = br.BaseStream.Position;
			var offsets = this.FindOffsets(br, size);

			// We need to read part0 as well
			br.BaseStream.Position = offsets[0] + 0x14;
			int maxcparts = br.ReadInt32();
			br.BaseStream.Position = offsets[0] + 0x1C;
			int maxattrib = br.ReadInt32();

			// Get all custom attributes
			var cpmap = this.ReadCustomCP(br, offsets[4]);

			// Initialize stream over string block
			br.BaseStream.Position = offsets[1];
			var length = br.ReadInt32();
			var strarr = br.ReadBytes(length);
			using var StrStream = new MemoryStream(strarr);
			using var StrReader = new BinaryReader(StrStream);

			// Read all car part attributes
			br.BaseStream.Position = offsets[2];
			var attrib_list = this.ReadAttribs(br, StrReader, maxattrib, cpmap);

			// Read all temporary parts
			br.BaseStream.Position = offsets[3];
			var temp_cparts = this.ReadTempParts(br, StrReader, maxcparts);

			// Generate Model Collections
			int index = -1;

			foreach (var group in temp_cparts.GroupBy(_ => _.CarNameHash))
			{

				++index;
				var collection = new DBModelPart(group.Key.BinString(LookupReturn.EMPTY), this);

				foreach (var temppart in group)
				{

					var realpart = new Parts.CarParts.RealCarPart(collection, temppart.AttribEnd - temppart.AttribStart)
					{
						PartLabel = temppart.PartNameHash.BinString(LookupReturn.EMPTY),
						BrandLabel = temppart.BrandNameHash.BinString(LookupReturn.EMPTY),
						DebugName = temppart.DebugName,
						CarPartGroupID = temppart.CarPartGroupID,
						UpgradeGroupID = temppart.UpgradeGroupID,
						UpgradeStyle = temppart.UpgradeStyle,
						GeometryLodA = temppart.LodAHash.BinString(LookupReturn.EMPTY),
						GeometryLodB = temppart.LodBHash.BinString(LookupReturn.EMPTY),
						GeometryLodC = temppart.LodCHash.BinString(LookupReturn.EMPTY),
						GeometryLodD = temppart.LodDHash.BinString(LookupReturn.EMPTY),
					};

					for (int a1 = temppart.AttribStart; a1 < temppart.AttribEnd; ++a1)
					{

						if (temppart.AttribOffset + a1 >= attrib_list.Length) continue;
						var addon = (CPAttribute)attrib_list[temppart.AttribOffset + a1].PlainCopy();
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
