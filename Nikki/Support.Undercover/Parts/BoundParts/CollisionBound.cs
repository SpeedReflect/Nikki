using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Undercover.Parts.BoundParts
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
		public enum BoundFlags : byte
		{
			kFlag_None = 0,
			kFlag_Disabled = 1,
			kFlag_Internal = 2,
			kFlag_Joint_Invert = 4,
			kFlag_NoCollision_Ground = 32,
			kFlag_NoCollision_Barrier = 64,
			kFlag_NoCollision_Object = 128,
			kFlag_NoCollision_MASK = 224,
		}

		public enum BoundType : byte
		{
			kBounds_Invalid = 0,
			kBounds_Node = 1,
			kBounds_Geometry = 2,
			kBounds_Joint = 3,
			kBounds_Constraint = 4,
			kBounds_Emitter = 5,
			kBounds_Type_Count = 6,
		}

		public enum BoundShape : byte
		{
			kShape_Invalid = 0,
			kShape_Box = 1,
			kShape_Sphere = 2,
			kShape_Cylinder = 3,
			kShape_Mesh = 4,
			kShape_Type_Count = 5,
		}

		public enum BoundJointType : byte
		{
			kBounds_Joint_Invalid = 0,
			kBounds_Constraint_Conical = 1,
			kBounds_Constraint_Prismatic = 2,
			kBounds_Joint_Female = 3,
			kBounds_Joint_Male = 4,
			kBounds_Male_Post = 5,
			kBounds_Joint_Subtype_Count = 6,
		}

		public enum BoundNodeType : byte
		{
			kBounds_Node_Invalid = 0,
			kBounds_Node_Heirarchy = 1,
			kBounds_Node_Fragment = 2,
			kBounds_Node_Subtype_Count = 3,
		}

		public enum BoundConstraintType : byte
		{
			kBounds_Constraint_Invalid = 0,
			kBounds_Constraint_Hinge = 1,
			kBounds_Constraint_BallSocket = 2,
			kBounds_Constraint_Subtype_Count = 3,
		}

		/// <summary>
		/// X value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float OrientationX { get; set; }

		/// <summary>
		/// Y value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float OrientationY { get; set; }

		/// <summary>
		/// Z value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float OrientationZ { get; set; }

		/// <summary>
		/// W value of the orientation of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float OrientationW { get; set; }

		/// <summary>
		/// X value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PositionX { get; set; }

		/// <summary>
		/// Y value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PositionY { get; set; }

		/// <summary>
		/// Z value of the position of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PositionZ { get; set; }

		/// <summary>
		/// Type of the bound.
		/// </summary>
		[AccessModifiable()]
		public BoundType Type { get; set; } = BoundType.kBounds_Geometry;

		/// <summary>
		/// Node type of the bound if it's set as a node.
		/// </summary>
		[AccessModifiable()]
		public BoundNodeType NodeType { get; set; } = BoundNodeType.kBounds_Node_Invalid;

		/// <summary>
		/// Shape of the bound if it's set as a geometry.
		/// </summary>
		[AccessModifiable()]
		public BoundShape Shape { get; set; } = BoundShape.kShape_Mesh;

		/// <summary>
		/// Joint type of the bound if it's set as a joint.
		/// </summary>
		[AccessModifiable()]
		public BoundJointType JointType { get; set; } = BoundJointType.kBounds_Joint_Invalid;

		/// <summary>
		/// Constraint type of the bound if it's set as a constraint.
		/// </summary>
		[AccessModifiable()]
		public BoundConstraintType ConstraintType { get; set; } = BoundConstraintType.kBounds_Constraint_Invalid;

		/// <summary>
		/// Flags of the bound.
		/// </summary>
		[AccessModifiable()]
		public BoundFlags Flags { get; set; } = BoundFlags.kFlag_None;

		/// <summary>
		/// Flags of the children of the bound.
		/// </summary>
		[AccessModifiable()]
		public BoundFlags ChildrenFlags { get; set; } = BoundFlags.kFlag_None;


		/// <summary>
		/// X value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfDimensionX { get; set; }

		/// <summary>
		/// Y value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfDimensionY { get; set; }

		/// <summary>
		/// Z value of the half dimension of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float HalfDimensionZ { get; set; }

		/// <summary>
		/// Indicates number of children <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public byte NumberOfChildren { get; set; }

		/// <summary>
		/// X value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PivotX { get; set; }

		/// <summary>
		/// Y value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PivotY { get; set; }

		/// <summary>
		/// Z value of pivot of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float PivotZ { get; set; }

		/// <summary>
		/// X value of bone offset of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float BoneOffsetX { get; set; }

		/// <summary>
		/// Y value of bone offset of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float BoneOffsetY { get; set; }

		/// <summary>
		/// Z value of bone offset of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public float BoneOffsetZ { get; set; }

		/// <summary>
		/// Indicates bone index of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short BoneIndex { get; set; }

		/// <summary>
		/// Indicates render heirarchy index of this <see cref="CollisionBound"/>.
		/// </summary>
		[AccessModifiable()]
		public short RenderHeirarchyIndex { get; set; }

		/// <summary>
		/// Vlt hash of attribute name.
		/// </summary>
		[AccessModifiable()]
		public string AttributeName { get; set; }

		/// <summary>
		/// Vlt hash of surface name.
		/// </summary>
		[AccessModifiable()]
		public string SurfaceName { get; set; }

		/// <summary>
		/// Vlt hash of bound name.
		/// </summary>
		[AccessModifiable()]
		public string NameHash { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new CollisionBound();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Disassembles array into <see cref="CollisionBound"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="CollisionBound"/> with.</param>
		public void Read(BinaryReader br)
		{
			this.OrientationX = br.ReadSingle();
			this.OrientationY = br.ReadSingle();
			this.OrientationZ = br.ReadSingle();
			this.OrientationW = br.ReadSingle() * 180.0f;			
			this.PositionX = br.ReadSingle();
			this.PositionY = br.ReadSingle();
			this.PositionZ = br.ReadSingle();
			br.BaseStream.Position += 4; // PositionW
			this.HalfDimensionX = br.ReadSingle();
			this.HalfDimensionY = br.ReadSingle();
			this.HalfDimensionZ = br.ReadSingle();
			br.BaseStream.Position += 4; // HalfDimensionW
			this.PivotX = br.ReadSingle();
			this.PivotY = br.ReadSingle();
			this.PivotZ = br.ReadSingle();
			br.BaseStream.Position += 4; // PivotW
			this.BoneOffsetX = br.ReadSingle();
			this.BoneOffsetY = br.ReadSingle();
			this.BoneOffsetZ = br.ReadSingle();
			br.BaseStream.Position += 4; // BoneOffsetW
			this.Type = (BoundType)br.ReadByte();
			br.BaseStream.Position += 1; // SubType
			this.Shape = (BoundShape)br.ReadByte();
			this.Flags = (BoundFlags)br.ReadByte();
			this.AttributeName = br.ReadUInt32().VltString(LookupReturn.EMPTY);
			this.SurfaceName = br.ReadUInt32().VltString(LookupReturn.EMPTY);
			this.NameHash = br.ReadUInt32().VltString(LookupReturn.EMPTY);
			this.BoneIndex = br.ReadInt16();
			this.RenderHeirarchyIndex = br.ReadInt16();
			br.BaseStream.Position += 24;
			this.NumberOfChildren = br.ReadByte();
			br.BaseStream.Position += 6;
			this.ChildrenFlags = (BoundFlags)br.ReadByte();
			br.BaseStream.Position += 12;
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
			bw.Write((this.OrientationW / 180.0f));
			bw.Write(this.PositionX);
			bw.Write(this.PositionY);
			bw.Write(this.PositionZ);
			bw.Write(0); // PositionW
			bw.Write(this.HalfDimensionX);
			bw.Write(this.HalfDimensionY);
			bw.Write(this.HalfDimensionZ);
			bw.Write(0); // HalfDimensionW
			bw.Write(this.PivotX);
			bw.Write(this.PivotY);
			bw.Write(this.PivotZ);
			bw.Write(0); // PivotW
			bw.Write(this.BoneOffsetX);
			bw.Write(this.BoneOffsetY);
			bw.Write(this.BoneOffsetZ);
			bw.Write(0); // BoneOffsetW
			bw.Write((byte)this.Type);
			bw.Write((byte)0);
			bw.Write((byte)this.Shape);
			bw.Write((byte)this.Flags);
			bw.Write(this.AttributeName.VltHash());
			bw.Write(this.SurfaceName.VltHash());
			bw.Write(this.NameHash.VltHash());
			bw.Write(this.BoneIndex);
			bw.Write(this.RenderHeirarchyIndex);
			bw.Write(0); // Collection
			bw.Write(0); // ParentBounds
			bw.Write(0); // ConstraintInfo
			bw.Write(0); // Shape
			bw.Write(0); // DebugName
			bw.Write(0); // ChildrenData
			bw.Write((int)this.NumberOfChildren);
			bw.Write((short)this.NumberOfChildren); // CapacityAndFlags
			bw.Write((byte)0);
			bw.Write((byte)this.ChildrenFlags);
			bw.Write(0); // Padding
			bw.Write(0); // Padding
			bw.Write(0); // Padding
		}

		/// <summary>
		/// Returns CollisionBound string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => "CollisionBound";
	}
}
