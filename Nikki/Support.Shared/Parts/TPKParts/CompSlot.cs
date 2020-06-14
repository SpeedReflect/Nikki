using Nikki.Reflection;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.TPKParts
{
    /// <summary>
    /// Represents Compression slot of <see cref="Texture"/>.
    /// </summary>
    public class CompSlot
    {
        /// <summary>
        /// First compression setting.
        /// </summary>
        public int Var1  { get; set; } = 1;

        /// <summary>
        /// Second compression setting.
        /// </summary>
        public int Var2  { get; set; } = 5;

        /// <summary>
        /// Third compression setting.
        /// </summary>
        public int Var3  { get; set; } = 6;

        /// <summary>
        /// Compression of the <see cref="Texture"/>.
        /// </summary>
        public uint Comp { get; set; } = EAComp.DXT5_32;
    }
}