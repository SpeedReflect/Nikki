using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.ID;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.BoundParts;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
{
	/// <summary>
	/// <see cref="Collision"/> is a collection of settings related to a car's bounds.
	/// </summary>
	public class Collision : Shared.Class.Collision
	{
		#region Fields

		private string _collection_name;
		private int _number_of_bounds;
		private int _number_of_clouds;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Carbon.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Carbon Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (String.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (this.Database.Collisions.FindCollection(value) != null)
					throw new CollectionExistenceException();
				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// List of collision bounds.
		/// </summary>
		[Listable("Bounds", "COLLISION_BOUND")]
		public List<CollisionBound> CollisionBounds { get; set; }
		
		/// <summary>
		/// List of collision clouds.
		/// </summary>
		[Listable("Clouds", "COLLISION_CLOUD")]
		public List<CollisionCloud> CollisionClouds { get; set; }

		/// <summary>
		/// Number of collision bounds.
		/// </summary>
		public override int NumberOfBounds
		{
			get => this._number_of_bounds;
			set
			{
				this.CollisionBounds.Resize(value);
				this._number_of_bounds = value;
			}
		}

		/// <summary>
		/// Number of collision clouds.
		/// </summary>
		public override int NumberOfClouds
		{
			get => this._number_of_clouds;
			set
			{
				this.CollisionClouds.Resize(value);
				this._number_of_clouds = value;
			}
		}

		/// <summary>
		/// True if this <see cref="Collision"/> is resolved; false otherwise.
		/// </summary>
		public override eBoolean IsResolved { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		public Collision()
		{
			this.CollisionBounds = new List<CollisionBound>();
			this.CollisionClouds = new List<CollisionCloud>();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
		public Collision(string CName, Database.Carbon db)
		{
			this.Database = db;
			this.CollectionName = CName;
			this.CollisionBounds = new List<CollisionBound>();
			this.CollisionClouds = new List<CollisionCloud>();
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Collision"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Carbon"/> to which this instance belongs to.</param>
		public Collision(BinaryReader br, Database.Carbon db)
		{
			this.Database = db;
			this.CollisionBounds = new List<CollisionBound>();
			this.CollisionClouds = new List<CollisionCloud>();
			this.Disassemble(br);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Collision"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Collision"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			// Precalculate size
			int size = 0x28 + this._number_of_bounds * 0x30; // 0x28 = alignment (8) + headers
			for (int a1 = 0; a1 < this._number_of_clouds; ++a1)
				size += 0x10 + this.CollisionClouds[a1].NumberOfVertices * 0x10;

			// Write data
			bw.Write(CarParts.CollisionBound);
			bw.Write(size);
			bw.Write(0x1111111111111111);
			bw.Write(this.VltKey);
			bw.Write(this._number_of_bounds);
			bw.Write(this.IsResolved == eBoolean.False ? (int)0 : (int)1);
			bw.Write((int)0);

			for (int a1 = 0; a1 < this._number_of_bounds; ++a1)
				this.CollisionBounds[a1].Assemble(bw);

			bw.Write(this._number_of_clouds);
			bw.Write((int)0);
			bw.Write((long)0);

			for (int a1 = 0; a1 < this._number_of_clouds; ++a1)
				this.CollisionClouds[a1].Assemble(bw);
		}

		/// <summary>
		/// Disassembles array into <see cref="Collision"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Collision"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			br.BaseStream.Position += 0x10; // skip ID, size and alignment
			this._collection_name = br.ReadUInt32().VltString(eLookupReturn.EMPTY);
			this.NumberOfBounds = br.ReadInt32();
			this.IsResolved = br.ReadInt32() == 0 ? eBoolean.False : eBoolean.True;
			br.BaseStream.Position += 4;

			for (int a1 = 0; a1 < this._number_of_bounds; ++a1)
				this.CollisionBounds[a1].Disassemble(br);

			this.NumberOfClouds = br.ReadInt32();
			br.BaseStream.Position += 12;

			for (int a1 = 0; a1 < this._number_of_clouds; ++a1)
				this.CollisionClouds[a1].Disassemble(br);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new Collision(CName, this.Database)
			{
				NumberOfBounds = this.NumberOfBounds,
				NumberOfClouds = this.NumberOfClouds,
				IsResolved = this.IsResolved
			};

			for (int a1 = 0; a1 < this._number_of_bounds; ++a1)
				result.CollisionBounds[a1] = this.CollisionBounds[a1].PlainCopy();

			for (int a1 = 0; a1 < this._number_of_clouds; ++a1)
				result.CollisionClouds[a1] = this.CollisionClouds[a1].PlainCopy();

			return result;
		}

		#endregion
	}
}
