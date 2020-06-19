using System;
using Nikki.Support.Shared.Parts.BoundParts;



namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of Bound flags that are used for <see cref="CollisionBound"/>.
    /// </summary>
    [Flags()]
	public enum eBoundFlags : short
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
}
