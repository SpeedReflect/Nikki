using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Carbon.Parts.PresetParts
{
    /// <summary>
    /// A unit <see cref="Autosculpt"/> used in preset rides.
    /// </summary>
    public class Autosculpt : SubPart
    {
        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone6 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone7 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone8 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AccessModifiable()]
        public byte AutosculptZone9 { get; set; }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public override SubPart PlainCopy()
        {
            var result = new Autosculpt();
            result.CloneValuesFrom(this);
            return result;
        }

        /// <summary>
        /// Reads data using <see cref="BinaryReader"/> provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public void Read(BinaryReader br)
        {
            this.AutosculptZone1 = br.ReadByte();
            this.AutosculptZone2 = br.ReadByte();
            this.AutosculptZone3 = br.ReadByte();
            this.AutosculptZone4 = br.ReadByte();
            this.AutosculptZone5 = br.ReadByte();
            this.AutosculptZone6 = br.ReadByte();
            this.AutosculptZone7 = br.ReadByte();
            this.AutosculptZone8 = br.ReadByte();
            this.AutosculptZone9 = br.ReadByte();
        }

        /// <summary>
        /// Writes data using <see cref="BinaryWriter"/> provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.AutosculptZone1);
            bw.Write(this.AutosculptZone2);
            bw.Write(this.AutosculptZone3);
            bw.Write(this.AutosculptZone4);
            bw.Write(this.AutosculptZone5);
            bw.Write(this.AutosculptZone6);
            bw.Write(this.AutosculptZone7);
            bw.Write(this.AutosculptZone8);
            bw.Write(this.AutosculptZone9);
        }
    }
}
