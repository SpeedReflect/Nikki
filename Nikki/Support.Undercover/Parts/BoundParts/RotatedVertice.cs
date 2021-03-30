using System.IO;
using System.Collections.Generic;
using Nikki.Reflection.Abstract;
using Nikki.Support.Undercover.Class;
using Nikki.Reflection.Attributes;
using CoreExtensions.Conversions;

namespace Nikki.Support.Undercover.Parts.BoundParts
{
    /// <summary>
	/// <see cref="RotatedVertice"/> is a structure for <see cref="ConvexVerticesShape"/>.
	/// </summary>
    public class RotatedVertice : SubPart
    {
        /// <summary>
        /// X rotation of the X value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float XRotationX { get; set; }

        /// <summary>
        /// Y rotation of the X value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float XRotationY { get; set; }

        /// <summary>
        /// Z rotation of the X value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float XRotationZ { get; set; }

        /// <summary>
        /// W rotation of the X value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float XRotationW { get; set; }

        /// <summary>
        /// X rotation of the Y value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float YRotationX { get; set; }

        /// <summary>
        /// Y rotation of the Y value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float YRotationY { get; set; }

        /// <summary>
        /// Z rotation of the Y value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float YRotationZ { get; set; }

        /// <summary>
        /// W rotation of the Y value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float YRotationW { get; set; }

        /// <summary>
        /// X rotation of the Z value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float ZRotationX { get; set; }

        /// <summary>
        /// Y rotation of the Z value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float ZRotationY { get; set; }

        /// <summary>
        /// Z rotation of the Z value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float ZRotationZ { get; set; }

        /// <summary>
        /// W rotation of the Z value of this <see cref="RotatedVertice"/>.
        /// </summary>
        [AccessModifiable()]
        public float ZRotationW { get; set; }

        /// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
        {
            var result = new RotatedVertice()
            {
                XRotationX = this.XRotationX,
                XRotationY = this.XRotationY,
                XRotationZ = this.XRotationZ,
                XRotationW = this.XRotationW,
                YRotationX = this.YRotationX,
                YRotationY = this.YRotationY,
                YRotationZ = this.YRotationZ,
                YRotationW = this.YRotationW,
                ZRotationX = this.ZRotationX,
                ZRotationY = this.ZRotationY,
                ZRotationZ = this.ZRotationZ,
                ZRotationW = this.ZRotationW,
            };

            return result;
        }

        /// <summary>
        /// Disassembles array into <see cref="RotatedVertice"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="RotatedVertice"/> with.</param>
        public void Read(BinaryReader br)
        {
            this.XRotationX = br.ReadSingle();
            this.XRotationY = br.ReadSingle();
            this.XRotationZ = br.ReadSingle();
            this.XRotationW = br.ReadSingle();
            this.YRotationX = br.ReadSingle();
            this.YRotationY = br.ReadSingle();
            this.YRotationZ = br.ReadSingle();
            this.YRotationW = br.ReadSingle();
            this.ZRotationX = br.ReadSingle();
            this.ZRotationY = br.ReadSingle();
            this.ZRotationZ = br.ReadSingle();
            this.ZRotationW = br.ReadSingle();
        }

        /// <summary>
        /// Assembles <see cref="RotatedVertice"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="RotatedVertice"/> with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.XRotationX);
            bw.Write(this.XRotationY);
            bw.Write(this.XRotationZ);
            bw.Write(this.XRotationW);
            bw.Write(this.YRotationX);
            bw.Write(this.YRotationY);
            bw.Write(this.YRotationZ);
            bw.Write(this.YRotationW);
            bw.Write(this.ZRotationX);
            bw.Write(this.ZRotationY);
            bw.Write(this.ZRotationZ);
            bw.Write(this.ZRotationW);
        }

        /// <summary>
        /// Returns RotatedVertice string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString() => "RotatedVertice";
    }
}
