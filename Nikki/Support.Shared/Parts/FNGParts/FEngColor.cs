using System;
using Nikki.Utils.EA;
using Nikki.Support.Shared.Class;



namespace Nikki.Support.Shared.Parts.FNGParts
{
    /// <summary>
    /// <see cref="FEngColor"/> represents a single frontend group color.
    /// </summary>
    public class FEngColor
    {
        /// <summary>
        /// Red value.
        /// </summary>
        public byte Red { get; set; } = 0;

        /// <summary>
        /// Green value.
        /// </summary>
        public byte Green { get; set; } = 0;

        /// <summary>
        /// Blue value.
        /// </summary>
        public byte Blue { get; set; } = 0;

        /// <summary>
        /// Alpha value.
        /// </summary>
        public byte Alpha { get; set; } = 0;

        /// <summary>
        /// Offset of the <see cref="FEngColor"/> in its frontend group.
        /// </summary>
        public uint Offset { get; set; } = 0;

        /// <summary>
        /// <see cref="FNGroup"/> to which this <see cref="FEngColor"/> belongs to.
        /// </summary>
        public FNGroup ThisFNGroup { get; set; }

        /// <summary>
        /// Initialize new instance of <see cref="FEngColor"/>.
        /// </summary>
        /// <param name="fng"><see cref="FNGroup"/> to which this <see cref="FEngColor"/> belongs to.</param>
        public FEngColor(FNGroup fng)
        {
            this.ThisFNGroup = fng;
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a
        /// <see cref="FEngColor"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The <see cref="FEngColor"/> to compare to this instance.</param>
        /// <returns>True if obj is a <see cref="FEngColor"/> and its value is the same as 
        /// this instance; false otherwise. If obj is null, the method returns false.
        /// </returns>
        public override bool Equals(object obj) => obj is FEngColor color && this == color;

        /// <summary>
        /// Returns the hash code for this <see cref="FEngColor"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() =>
            Tuple.Create(this.Alpha, this.Red, this.Green, this.Blue, this.Offset).GetHashCode();

        /// <summary>
        /// Determines whether two specified <see cref="FEngColor"/> have the same value.
        /// </summary>
        /// <param name="c1">The first <see cref="FEngColor"/> to compare, or null.</param>
        /// <param name="c2">The second <see cref="FEngColor"/> to compare, or null.</param>
        /// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
        public static bool operator ==(FEngColor c1, FEngColor c2)
		{
            if (c1 is null) return c2 is null;
            else if (c2 is null) return false;

            bool result = true;
            result &= c1.Red == c2.Red;
            result &= c1.Green == c2.Green;
            result &= c1.Blue == c2.Blue;
            return result;
		}

        /// <summary>
        /// Determines whether two specified <see cref="FEngColor"/> have different values.
        /// </summary>
        /// <param name="c1">The first <see cref="FEngColor"/> to compare, or null.</param>
        /// <param name="c2">The second <see cref="FEngColor"/> to compare, or null.</param>
        /// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
        public static bool operator !=(FEngColor c1, FEngColor c2) => !(c1 == c2);

        /// <summary>
        /// Returns offset and hex color representation of this <see cref="FEngColor"/> as a string.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Offset: {this.Offset:X8} | Color: " +
                $"{SAT.ColorToHex(this.Alpha, this.Red, this.Green, this.Blue)}";
        }
    }
}
