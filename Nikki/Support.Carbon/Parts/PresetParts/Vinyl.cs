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
    public class Vinyl : SubPart
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
        public byte SaturationFillEffect { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte BrightnessFillEffect { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte SaturationStrokeEffect { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte BrightnessStrokeEffect { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte SaturationInnerShadow { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte BrightnessInnerShadow { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte SaturationInnerGlow { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte BrightnessInnerGlow { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchFillEffect { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchStrokeEffect { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchInnerShadow { get; set; } = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public string SwatchInnerGlow { get; set; } = String.Empty;

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
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
            this.SwatchFillEffect = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationFillEffect = br.ReadByte();
            this.BrightnessFillEffect = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchStrokeEffect = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationStrokeEffect = br.ReadByte();
            this.BrightnessStrokeEffect = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchInnerShadow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationInnerShadow = br.ReadByte();
            this.BrightnessInnerShadow = br.ReadByte();
            br.BaseStream.Position += 2;
            this.SwatchInnerGlow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SaturationInnerGlow = br.ReadByte();
            this.BrightnessInnerGlow = br.ReadByte();
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
            bw.Write(this.SwatchFillEffect.BinHash());
            bw.Write(this.SaturationFillEffect);
            bw.Write(this.BrightnessFillEffect);
            bw.Write((short)0);
            bw.Write(this.SwatchStrokeEffect.BinHash());
            bw.Write(this.SaturationStrokeEffect);
            bw.Write(this.BrightnessStrokeEffect);
            bw.Write((short)0);
            bw.Write(this.SwatchInnerShadow.BinHash());
            bw.Write(this.SaturationInnerShadow);
            bw.Write(this.BrightnessInnerShadow);
            bw.Write((short)0);
            bw.Write(this.SwatchInnerGlow.BinHash());
            bw.Write(this.SaturationInnerGlow);
            bw.Write(this.BrightnessInnerGlow);
            bw.Write((short)0);
        }
    }
}
