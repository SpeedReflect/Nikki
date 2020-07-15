using System;
using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Shared.Parts.BoundParts
{
	/// <summary>
	/// <see cref="CollisionBound"/> is a unit bound for <see cref="Collision"/>.
	/// </summary>
	public class CollisionBound : SubPart
	{
		/// <summary>
		/// Enum of Bound flags that are used for <see cref="CollisionBound"/>.
		/// </summary>
		[Flags()]
		public enum BoundFlags : short
		{
			/// <summary>
			/// 
			/// </summary>
			kBounds_Disabled = 1,

			/// <summary>
			/// 
			/// </summary>
			kBounds_PrimVsWorld = 2,

			/// <summary>
			/// 
			/// </summary>
			kBounds_PrimVsObjects = 4,

			/// <summary>
			/// 
			/// </summary>
			kBounds_PrimVsGround = 8,

			/// <summary>
			/// 
			/// </summary>
			kBounds_MeshVsGround = 16,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Internal = 32,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Box = 64,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Sphere = 128,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Constraint_Conical = 256,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Constraint_Prismatic = 512,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Joint_Female = 1024,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Joint_Male = 2048,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Male_Post = 4096,

			/// <summary>
			/// 
			/// </summary>
			kBounds_Joint_Invert = 8192,

			/// <summary>
			/// 
			/// </summary>
			kBounds_PrimVsOwnParts = 16384,
		}

		/// <summary>
		/// X value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short OrientationX { get; set; }

		/// <summary>
		/// Y value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short OrientationY { get; set; }

		/// <summary>
		/// Z value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short OrientationZ { get; set; }

		/// <summary>
		/// W value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short OrientationW { get; set; }

		/// <summary>
		/// X value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PositionX { get; set; }

		/// <summary>
		/// Y value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PositionY { get; set; }

		/// <summary>
		/// Z value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PositionZ { get; set; }

		/// <summary>
		/// Type of the bound.
		/// </summary>
		[AccessModifiable()]
		public BoundFlags BoundType { get; set; } = BoundFlags.kBounds_Box;

		/// <summary>
		/// X value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short HalfDimensionX { get; set; }

		/// <summary>
		/// Y value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short HalfDimensionY { get; set; }

		/// <summary>
		/// Z value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short HalfDimensionZ { get; set; }

		/// <summary>
		/// Indicates number of children <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public byte NumberOfChildren { get; set; }

		/// <summary>
		/// Index of binded <see cref="CollisionCloud"/>.
		/// </summary>
		[AccessModifiable()]
		public sbyte CollisionCloudIndex { get; set; }

		/// <summary>
		/// X value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PivotX { get; set; }

		/// <summary>
		/// Y value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PivotY { get; set; }

		/// <summary>
		/// Z value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short PivotZ { get; set; }

		/// <summary>
		/// Indicates children index of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short ChildrenIndex { get; set; }

		/// <summary>
		/// Vlt hash of attribute name.
		/// </summary>
		[AccessModifiable()]
		public uint AttributeName { get; set; }

		/// <summary>
		/// Vlt hash of surface name.
		/// </summary>
		[AccessModifiable()]
		public uint SurfaceName { get; set; }

		/// <summary>
		/// Vlt hash of bound name.
		/// </summary>
		[AccessModifiable()]
		public uint NameHash { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new CollisionBound();
			result.CloneValues(this);
			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="CollisionBound"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="CollisionBound"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.OrientationX = br.ReadInt16();
			this.OrientationY = br.ReadInt16();
			this.OrientationZ = br.ReadInt16();
			this.OrientationW = br.ReadInt16();
			this.PositionX = br.ReadInt16();
			this.PositionY = br.ReadInt16();
			this.PositionZ = br.ReadInt16();
			this.BoundType = (BoundFlags)br.ReadInt16();
			this.HalfDimensionX = br.ReadInt16();
			this.HalfDimensionY = br.ReadInt16();
			this.HalfDimensionZ = br.ReadInt16();
			this.NumberOfChildren = br.ReadByte();
			this.CollisionCloudIndex = br.ReadSByte();
			this.PivotX = br.ReadInt16();
			this.PivotY = br.ReadInt16();
			this.PivotZ = br.ReadInt16();
			this.ChildrenIndex = br.ReadInt16();
			this.AttributeName = br.ReadUInt32();
			this.SurfaceName = br.ReadUInt32();
			this.NameHash = br.ReadUInt32();
			br.BaseStream.Position += 4;
		}

		/// <summary>
		/// Assembles <see cref="CollisionBound"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="CollisionBound"/> with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.OrientationX);
			bw.Write(this.OrientationY);
			bw.Write(this.OrientationZ);
			bw.Write(this.OrientationW);
			bw.Write(this.PositionX);
			bw.Write(this.PositionY);
			bw.Write(this.PositionZ);
			bw.WriteEnum(this.BoundType);
			bw.Write(this.HalfDimensionX);
			bw.Write(this.HalfDimensionY);
			bw.Write(this.HalfDimensionZ);
			bw.Write(this.NumberOfChildren);
			bw.Write(this.CollisionCloudIndex);
			bw.Write(this.PivotX);
			bw.Write(this.PivotY);
			bw.Write(this.PivotZ);
			bw.Write(this.ChildrenIndex);
			bw.Write(this.AttributeName);
			bw.Write(this.SurfaceName);
			bw.Write(this.NameHash);
			bw.Write((int)0);
		}

		/// <summary>
		/// Returns CollisionBound string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "CollisionBound";
	}
}
