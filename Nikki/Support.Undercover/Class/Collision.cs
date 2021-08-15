using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Undercover.Framework;
using Nikki.Support.Undercover.Parts.BoundParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Undercover.Class
{
	/// <summary>
	/// <see cref="Collision"/> is a collection of settings related to a car's bounds.
	/// </summary>
	public class Collision : Shared.Class.Collision
	{
		#region Fields

		private string _collection_name;
		private int _start;
		private int _localFixUpsOffset;
		private int _virtualFixUpsOffset;
		private int _endOffset;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Undercover;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Undercover.ToString();

		/// <summary>
		/// Magic of the collision header.
		/// </summary>
		[Browsable(false)]
		public byte[] Magic = { 0x67, 0xE0, 0xE0, 0x67, 0x20, 0xC0, 0xC0, 0x20 };

		/// <summary>
		/// Version of the collision header.
		/// </summary>
		[Browsable(false)]
		public int Version = 16;

		/// <summary>
		/// Layout rules of the collision header.
		/// </summary>
		[Browsable(false)]
		public byte[] LayoutRules = { 0x04, 0x01, 0x00, 0x01 };

		/// <summary>
		/// Class name hash for the content of the collision header.
		/// </summary>
		[Browsable(false)]
		public uint ContentsClassNameCRC = (uint)CollisionClass.scgHeader;

		/// <summary>
		/// Version of the contents of the collision header.
		/// </summary>
		[Browsable(false)]
		public byte[] ContentsVersion = { 0x00, 0x05, 0x05, 0x00 };

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public CollisionManager Manager { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				this.Manager?.CreationCheck(value);
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// List of collision bounds.
		/// </summary>
		[Category("Secondary")]
		public List<CollisionBound> CollisionBounds { get; }

		/// <summary>
		/// List of convex vertices shapes.
		/// </summary>
		[Category("Secondary")]
		public List<ConvexVerticesShape> ConvexVerticesShapes { get; }

		/// <summary>
		/// List of convex translate shapes.
		/// </summary>
		[Category("Secondary")]
		public List<ConvexTranslateShape> ConvexTranslateShapes { get; }

		/// <summary>
		/// List of convex transform shapes.
		/// </summary>
		[Category("Secondary")]
		public List<ConvexTransformShape> ConvexTransformShapes { get; }

		/// <summary>
		/// List of box shapes.
		/// </summary>
		[Category("Secondary")]
		public List<BoxShape> BoxShapes { get; }

		/// <summary>
		/// List of sphere shapes.
		/// </summary>
		[Category("Secondary")]
		public List<SphereShape> SphereShapes { get; }

		/// <summary>
		/// List of local FixUps.
		/// </summary>
		[Browsable(false)]
		[Category("Secondary")]
		public List<LocalFixUp> LocalFixUps { get; }

		/// <summary>
		/// List of virtual FixUps.
		/// </summary>
		[Browsable(false)]
		[Category("Secondary")]
		public List<VirtualFixUp> VirtualFixUps { get; }

		/// <summary>
		/// X of an unknown vector before the first bound.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public float UnknownX { get; set; }

		/// <summary>
		/// Y of an unknown vector before the first bound.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public float UnknownY { get; set; }

		/// <summary>
		/// Z of an unknown vector before the first bound.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public float UnknownZ { get; set; }

		/// <summary>
		/// W of an unknown vector before the first bound.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public float UnknownW { get; set; }

		/// <summary>
		/// Number of collision bounds.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public override int NumberOfBounds
		{
			get => this.CollisionBounds.Count;
			set => this.CollisionBounds.Resize(value);
		}

		/// <summary>
		/// Number of convex vertices shapes.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public int NumberOfConvexVerticesShapes
		{
			get => this.ConvexVerticesShapes.Count;
			set => this.ConvexVerticesShapes.Resize(value);
		}

		/// <summary>
		/// Number of convex translate shapes.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public int NumberOfConvexTranslateShapes
		{
			get => this.ConvexTranslateShapes.Count;
			set => this.ConvexTranslateShapes.Resize(value);
		}

		/// <summary>
		/// Number of convex transform shapes.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public int NumberOfConvexTransformShapes
		{
			get => this.ConvexTransformShapes.Count;
			set => this.ConvexTransformShapes.Resize(value);
		}

		/// <summary>
		/// Number of box shapes.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public int NumberOfBoxShapes
		{
			get => this.BoxShapes.Count;
			set => this.BoxShapes.Resize(value);
		}

		/// <summary>
		/// Number of box shapes.
		/// </summary>
		[AccessModifiable()]
		[Category("Primary")]
		public int NumberOfSphereShapes
		{
			get => this.SphereShapes.Count;
			set => this.SphereShapes.Resize(value);
		}

		/// <summary>
		/// Number of local FixUps.
		/// </summary>
		[Browsable(false)]
		[Category("Primary")]
		public int NumberOfLocalFixUps
		{
			get => this.LocalFixUps.Count;
			set => this.LocalFixUps.Resize(value);
		}

		/// <summary>
		/// Number of virtual FixUps.
		/// </summary>
		[Browsable(false)]
		[Category("Primary")]
		public int NumberOfVirtualFixUps
		{
			get => this.VirtualFixUps.Count;
			set => this.VirtualFixUps.Resize(value);
		}

		/// <summary>
		/// True if this <see cref="Collision"/> is resolved; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public override eBoolean IsResolved { get; set; }

		/// <summary>
		/// List of collision clouds (not used in Undercover).
		/// </summary>
		[Browsable(false)]
		public override int NumberOfClouds { get; set; } = 0;
        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="Collision"/>.
        /// </summary>
        public Collision()
		{
			this.CollisionBounds = new List<CollisionBound>();
			this.ConvexVerticesShapes = new List<ConvexVerticesShape>();
			this.ConvexTranslateShapes = new List<ConvexTranslateShape>();
			this.ConvexTransformShapes = new List<ConvexTransformShape>();
			this.BoxShapes = new List<BoxShape>();
			this.SphereShapes = new List<SphereShape>();
			this.LocalFixUps = new List<LocalFixUp>();
			this.VirtualFixUps = new List<VirtualFixUp>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="CollisionManager"/> to which this instance belongs to.</param>
		public Collision(string CName, CollisionManager manager) : this()
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="CollisionManager"/> to which this instance belongs to.</param>
		public Collision(BinaryReader br, CollisionManager manager) : this()
		{
			this.Manager = manager;
			this.Disassemble(br);
			this.CollectionName.BinHash();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Collision"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Collision"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			this._start = (int)bw.BaseStream.Position;
			bw.WriteEnum(BinBlockID.Collision);
			bw.Write(-1); // skip, will be calculated later
			bw.Write(0x11111111);
			bw.Write(0x11111111);
			bw.Write(this.Magic);
			bw.Write(0);
			bw.Write(this.Version);
			bw.Write(this.LayoutRules);
			bw.Write(0);
			bw.Write(this.ContentsClassNameCRC);
			bw.Write(this.ContentsVersion);
			bw.Write(48); // Absolute Data Start
			bw.Write(-1); // Local FixUps Offset
			bw.Write(-1); // Virtual FixUps Offset
			bw.Write(-1); // End Offset
			bw.Write(this.VltKey);
			bw.Write(this.NumberOfBounds);
			bw.Write(this.IsResolved == eBoolean.False ? (int)0 : (int)1);
			bw.Write(0); // RigHash
			bw.Write(0); // Root
			bw.Write(0); // DebugName
			bw.Write(0); // arrBones data
			bw.Write(0); // arrBones size
			bw.Write(0xC0000000); // arrBones cap and flags
			bw.Write(0); // arrRenderNodes data
			bw.Write(1); // arrRenderNodes size
			bw.Write(0xC0000001); // arrRenderNodes cap and flags
			bw.WriteNullTermUTF8(this._collection_name, 0x20);
			bw.Write(UnknownX);
			bw.Write(UnknownY);
			bw.Write(UnknownZ);
			bw.Write(UnknownW);

			// Create a new fixup table
			var NewVirtualFixUps = new List<VirtualFixUp>();

			// Write existing data according to old virtual FixUps
			int WrittenBounds = 0;
			int WrittenBoxShapes = 0;
			int WrittenConvexTransformShapes = 0;
			int WrittenConvexTranslateShapes = 0;
			int WrittenConvexVerticesShapes = 0;
			int WrittenSphereShapes = 0;

			for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				bw.BaseStream.Position = this._start + this.VirtualFixUps[loop].fromOffset + 0x40;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				switch (this.VirtualFixUps[loop].ClassID)
				{

					case CollisionClass.scgBounds:
						if (WrittenBounds < this.NumberOfBounds)
						{
							NewFixUp.ClassID = CollisionClass.scgBounds;
							this.CollisionBounds[WrittenBounds++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.hkpBoxShape:
						if (WrittenBoxShapes < this.NumberOfBoxShapes)
                        {
							NewFixUp.ClassID = CollisionClass.hkpBoxShape;
							this.BoxShapes[WrittenBoxShapes++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.hkpConvexTransformShape:
						if (WrittenConvexTransformShapes < this.NumberOfConvexTransformShapes)
                        {
							NewFixUp.ClassID = CollisionClass.hkpConvexTransformShape;
							this.ConvexTransformShapes[WrittenConvexTransformShapes++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.hkpConvexTranslateShape:
						if (WrittenConvexTranslateShapes < this.NumberOfConvexTranslateShapes)
                        {
							NewFixUp.ClassID = CollisionClass.hkpConvexTranslateShape;
							this.ConvexTranslateShapes[WrittenConvexTranslateShapes++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.hkpConvexVerticesShape:
						if (WrittenConvexVerticesShapes < this.NumberOfConvexVerticesShapes)
                        {
							NewFixUp.ClassID = CollisionClass.hkpConvexVerticesShape;
							this.ConvexVerticesShapes[WrittenConvexVerticesShapes++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.hkpSphereShape:
						if (WrittenSphereShapes < this.NumberOfBoxShapes)
                        {
							NewFixUp.ClassID = CollisionClass.hkpSphereShape;
							this.SphereShapes[WrittenSphereShapes++].Write(bw);
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					case CollisionClass.scgHeader:
						if (loop == 0)
                        {
							NewFixUp.fromOffset = 0;
							NewFixUp.ClassID = CollisionClass.scgHeader;
							NewVirtualFixUps.Add(NewFixUp);
						}
						break;
					default:
						break;
				}
			}

			// Write new ones

			// Write bounds
			for (int loop = WrittenBounds; loop < this.NumberOfBounds; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.scgBounds;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.CollisionBounds[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write box shapes
			for (int loop = WrittenBoxShapes; loop < this.NumberOfBoxShapes; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.hkpBoxShape;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.BoxShapes[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write convex transform shapes
			for (int loop = WrittenConvexTransformShapes; loop < this.NumberOfConvexTransformShapes; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.hkpConvexTransformShape;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.ConvexTransformShapes[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write convex translate shapes
			for (int loop = WrittenConvexTranslateShapes; loop < this.NumberOfConvexTranslateShapes; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.hkpConvexTranslateShape;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.ConvexTranslateShapes[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write convex vertices shapes
			for (int loop = WrittenConvexVerticesShapes; loop < this.NumberOfConvexVerticesShapes; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.hkpConvexVerticesShape;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.ConvexVerticesShapes[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write sphere shapes
			for (int loop = WrittenSphereShapes; loop < this.NumberOfSphereShapes; ++loop)
			{
				var NewFixUp = new VirtualFixUp();
				NewFixUp.ClassID = CollisionClass.hkpSphereShape;
				NewFixUp.fromOffset = (int)bw.BaseStream.Position - this._start - 0x40;
				this.SphereShapes[loop].Write(bw);
				NewVirtualFixUps.Add(NewFixUp);
			}

			// Write local fixups
			this._localFixUpsOffset = (int)bw.BaseStream.Position - this._start - 0x40;

			for (int loop = 0; loop < this.NumberOfLocalFixUps; ++loop)
			{

				this.LocalFixUps[loop].Write(bw);
			}
			// add alignment if needed
			if (this.NumberOfLocalFixUps % 2 != 0) bw.WriteBytes(0xFF, 8);

			// Write virtual fixups
			this._virtualFixUpsOffset = (int)bw.BaseStream.Position - this._start - 0x40;

			for (int loop = 0; loop < NewVirtualFixUps.Count; ++loop)
			{

				NewVirtualFixUps[loop].Write(bw);
			}
			// add alignment if needed
			if (NewVirtualFixUps.Count % 2 != 0) bw.WriteBytes(0xFF, 8);

			// Get file end
			this._endOffset = (int)bw.BaseStream.Position - this._start - 0x40;

			// Go back to write size and offsets
			bw.BaseStream.Position = this._start + 4;
			bw.Write(this._endOffset + 0x38); // size
			bw.BaseStream.Position = this._start + 0x34;
			bw.Write(this._localFixUpsOffset);
			bw.Write(this._virtualFixUpsOffset);
			bw.Write(this._endOffset);
			// Go to the end again
			bw.BaseStream.Position = this._start + this._endOffset + 0x40;
		}

		/// <summary>
		/// Disassembles array into <see cref="Collision"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Collision"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			this._start = (int)br.BaseStream.Position;
			br.BaseStream.Position += 0x34; // skip ID, size, alignment and common stuff between 0x10 - 0x34
			this._localFixUpsOffset = br.ReadInt32() + this._start + 0x40;
			this._virtualFixUpsOffset = br.ReadInt32() + this._start + 0x40;
			this._endOffset = br.ReadInt32() + this._start + 0x40;

			this._collection_name = br.ReadUInt32().VltString(LookupReturn.EMPTY);
			//this.NumberOfBounds = br.ReadInt32();
			br.BaseStream.Position += 0x4;
			this.IsResolved = br.ReadInt32() == 0 ? eBoolean.False : eBoolean.True;
			br.BaseStream.Position += 0x24; // skip RigHash and some more common stuff

			// Get name from the file if we can't find it from the hash
			if (string.IsNullOrEmpty(this._collection_name)) this._collection_name = br.ReadNullTermUTF8(0x20);
			else br.BaseStream.Position += 0x20;

			this.UnknownX = br.ReadSingle(); // UnknownX
			this.UnknownY = br.ReadSingle(); // UnknownY
			this.UnknownZ = br.ReadSingle(); // UnknownZ
			this.UnknownW = br.ReadSingle(); // UnknownW

			// Read local FixUps
			br.BaseStream.Position = this._localFixUpsOffset;
			this.NumberOfLocalFixUps = (this._virtualFixUpsOffset - this._localFixUpsOffset) / 8;

			for (int loop = 0; loop < this.NumberOfLocalFixUps; ++loop)
			{

				this.LocalFixUps[loop].Read(br);
				if (this.LocalFixUps[loop].fromOffset == -1)
                {
					NumberOfLocalFixUps--;
					break;
                }

			}

			// Read virtual FixUps
			br.BaseStream.Position = this._virtualFixUpsOffset;
			this.NumberOfVirtualFixUps = (this._endOffset - this._virtualFixUpsOffset) / 8;

			for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
			{

				this.VirtualFixUps[loop].Read(br);
				if (this.VirtualFixUps[loop].fromOffset == -1)
				{
					NumberOfVirtualFixUps--;
					break;
				}

			}

			// Read data according to virtual FixUps
			for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
			{
				br.BaseStream.Position = this._start + this.VirtualFixUps[loop].fromOffset + 0x40;
				switch(this.VirtualFixUps[loop].ClassID)
				{
					
					case CollisionClass.scgBounds:
						this.CollisionBounds[NumberOfBounds++].Read(br);
						break;
					case CollisionClass.hkpBoxShape:
						this.BoxShapes[NumberOfBoxShapes++].Read(br);
						break;
					case CollisionClass.hkpConvexTransformShape:
						this.ConvexTransformShapes[NumberOfConvexTransformShapes++].Read(br);
						break;
					case CollisionClass.hkpConvexTranslateShape:
						this.ConvexTranslateShapes[NumberOfConvexTranslateShapes++].Read(br);
						break;
					case CollisionClass.hkpConvexVerticesShape:
						this.ConvexVerticesShapes[NumberOfConvexVerticesShapes++].Read(br);
						break;
					case CollisionClass.hkpSphereShape:
						this.SphereShapes[NumberOfSphereShapes++].Read(br);
						break;
					case CollisionClass.scgHeader:
					default:
						break;
				}
			}

			br.BaseStream.Position = this._endOffset;
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new Collision(CName, this.Manager)
			{
				NumberOfBounds = this.NumberOfBounds,
				NumberOfBoxShapes = this.NumberOfBoxShapes,
				NumberOfConvexTransformShapes = this.NumberOfConvexTransformShapes,
				NumberOfConvexTranslateShapes = this.NumberOfConvexTranslateShapes,
				NumberOfConvexVerticesShapes = this.NumberOfConvexVerticesShapes,
				NumberOfSphereShapes = this.NumberOfSphereShapes,
				NumberOfLocalFixUps = this.NumberOfLocalFixUps,
				NumberOfVirtualFixUps = this.NumberOfVirtualFixUps,
				IsResolved = this.IsResolved,
				UnknownX = this.UnknownX,
				UnknownY = this.UnknownY,
				UnknownZ = this.UnknownZ,
				UnknownW = this.UnknownW
			};

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				result.CollisionBounds[loop] = (CollisionBound)this.CollisionBounds[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfBoxShapes; ++loop)
			{

				result.BoxShapes[loop] = (BoxShape)this.BoxShapes[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfConvexTransformShapes; ++loop)
			{

				result.ConvexTransformShapes[loop] = (ConvexTransformShape)this.ConvexTransformShapes[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfConvexTranslateShapes; ++loop)
			{

				result.ConvexTranslateShapes[loop] = (ConvexTranslateShape)this.ConvexTranslateShapes[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfConvexVerticesShapes; ++loop)
			{

				result.ConvexVerticesShapes[loop] = (ConvexVerticesShape)this.ConvexVerticesShapes[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfSphereShapes; ++loop)
			{

				result.SphereShapes[loop] = (SphereShape)this.SphereShapes[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfLocalFixUps; ++loop)
			{

				result.LocalFixUps[loop] = (LocalFixUp)this.LocalFixUps[loop].PlainCopy();

			}

			for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
			{

				result.VirtualFixUps[loop] = (VirtualFixUp)this.VirtualFixUps[loop].PlainCopy();

			}

			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="Collision"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Serialize(BinaryWriter bw)
		{
			byte[] array;
			using (var ms = new MemoryStream(0x300))
			using (var writer = new BinaryWriter(ms))
			{

				writer.WriteNullTermUTF8(this._collection_name);
				writer.Write(this.NumberOfBounds);
				writer.Write(this.NumberOfBoxShapes);
				writer.Write(this.NumberOfConvexTransformShapes);
				writer.Write(this.NumberOfConvexTranslateShapes);
				writer.Write(this.NumberOfConvexVerticesShapes);
				writer.Write(this.NumberOfSphereShapes);
				writer.Write(this.NumberOfLocalFixUps);
				writer.Write(this.NumberOfVirtualFixUps);

				for (int loop = 0; loop < this.NumberOfBounds; ++loop)
				{

					this.CollisionBounds[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfBoxShapes; ++loop)
				{

					this.BoxShapes[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfConvexTransformShapes; ++loop)
				{

					this.ConvexTransformShapes[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfConvexTranslateShapes; ++loop)
				{

					this.ConvexTranslateShapes[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfConvexVerticesShapes; ++loop)
				{

					this.ConvexVerticesShapes[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfSphereShapes; ++loop)
				{

					this.SphereShapes[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfLocalFixUps; ++loop)
				{

					this.LocalFixUps[loop].Write(writer);

				}

				for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
				{

					this.VirtualFixUps[loop].Write(writer);

				}

				writer.WriteEnum(this.IsResolved);
				writer.Write(this.UnknownX);
				writer.Write(this.UnknownY);
				writer.Write(this.UnknownZ);
				writer.Write(this.UnknownW);

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.RAWW);

			var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
			header.Write(bw);
			bw.Write(array.Length);
			bw.Write(array);
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Deserialize(BinaryReader br)
		{
			int size = br.ReadInt32();
			var array = br.ReadBytes(size);

			array = Interop.Decompress(array);

			using var ms = new MemoryStream(array);
			using var reader = new BinaryReader(ms);

			this._collection_name = reader.ReadNullTermUTF8();
			this.NumberOfBounds = reader.ReadInt32();
			this.NumberOfBoxShapes = reader.ReadInt32();
			this.NumberOfConvexTransformShapes = reader.ReadInt32();
			this.NumberOfConvexTranslateShapes = reader.ReadInt32();
			this.NumberOfConvexVerticesShapes = reader.ReadInt32();
			this.NumberOfSphereShapes = reader.ReadInt32();
			this.NumberOfLocalFixUps = reader.ReadInt32();
			this.NumberOfVirtualFixUps = reader.ReadInt32();

			for (int loop = 0; loop < this.NumberOfBounds; ++loop)
			{

				this.CollisionBounds[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfBoxShapes; ++loop)
			{

				this.BoxShapes[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfConvexTransformShapes; ++loop)
			{

				this.ConvexTransformShapes[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfConvexTranslateShapes; ++loop)
			{

				this.ConvexTranslateShapes[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfConvexVerticesShapes; ++loop)
			{

				this.ConvexVerticesShapes[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfSphereShapes; ++loop)
			{

				this.SphereShapes[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfLocalFixUps; ++loop)
			{

				this.LocalFixUps[loop].Read(reader);

			}

			for (int loop = 0; loop < this.NumberOfVirtualFixUps; ++loop)
			{

				this.VirtualFixUps[loop].Read(reader);

			}

			this.IsResolved = reader.ReadEnum<eBoolean>();
			this.UnknownX = reader.ReadSingle();
			this.UnknownY = reader.ReadSingle();
			this.UnknownZ = reader.ReadSingle();
			this.UnknownW = reader.ReadSingle();
		}

		#endregion
	}
}
