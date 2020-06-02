using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
    /// <summary>
    /// A unit <see cref="Vinyl"/> used in preset rides.
    /// </summary>
    public class Vinyl : ASubPart
    {
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string VectorVinyl { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public short PositionY { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public short PositionX { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public sbyte Rotation { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public sbyte Skew { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public sbyte ScaleY { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public sbyte ScaleX { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Saturation1 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Brightness1 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Saturation2 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Brightness2 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Saturation3 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Brightness3 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Saturation4 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte Brightness4 { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchColor1 { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchColor2 { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchColor3 { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchColor4 { get; set; } = String.Empty;

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override ASubPart PlainCopy()
        {
            var result = new Vinyl();

            foreach (var property in this.GetType().GetProperties())
            {

                property.SetValue(result, property.GetValue(this));

            }

            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.VectorVinyl = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.PositionY = br.ReadInt16();
            this.PositionX = br.ReadInt16();
            this.Rotation = br.ReadSByte();
            this.Skew = br.ReadSByte();
            this.ScaleY = br.ReadSByte();
            this.ScaleX = br.ReadSByte();
            this.SwatchColor1 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation1 = br.ReadByte();
            this.Brightness1 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation2 = br.ReadByte();
            this.Brightness2 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation3 = br.ReadByte();
            this.Brightness3 = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchColor4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.Saturation4 = br.ReadByte();
            this.Brightness4 = br.ReadByte();
            br.BaseStream.Position += 2;
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.VectorVinyl.BinHash());
            bw.Write(this.PositionY);
            bw.Write(this.PositionX);
            bw.Write(this.Rotation);
            bw.Write(this.Skew);
            bw.Write(this.ScaleY);
            bw.Write(this.ScaleX);
            bw.Write(this.SwatchColor1.BinHash());
            bw.Write(this.Saturation1);
            bw.Write(this.Brightness1);
            bw.Write((short)0);
            bw.Write(this.SwatchColor2.BinHash());
            bw.Write(this.Saturation2);
            bw.Write(this.Brightness2);
            bw.Write((short)0);
            bw.Write(this.SwatchColor3.BinHash());
            bw.Write(this.Saturation3);
            bw.Write(this.Brightness3);
            bw.Write((short)0);
            bw.Write(this.SwatchColor4.BinHash());
            bw.Write(this.Saturation4);
            bw.Write(this.Brightness4);
            bw.Write((short)0);
        }
    }
}
