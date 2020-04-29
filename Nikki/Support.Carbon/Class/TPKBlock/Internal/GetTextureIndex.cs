using GlobalLib.Reflection.Enum;
using System;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Gets all textures of this <see cref="TPKBlock"/>.
        /// </summary>
        /// <returns>Textures as an object.</returns>
        public override object GetTextures() => this.Textures;

        /// <summary>
        /// Gets index of the <see cref="Texture"/> in the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/>.</param>
        /// <param name="type">Key type passed.</param>
        /// <returns>Index number as an integer. If element does not exist, returns -1.</returns>
        public override int GetTextureIndex(uint key, eKeyType type)
        {
            switch (type)
            {
                case eKeyType.BINKEY:
                    for (int a1 = 0; a1 < this.Textures.Count; ++a1)
                    {
                        if (this.Textures[a1].BinKey == key) return a1;
                    }
                    break;

                case eKeyType.VLTKEY:
                    for (int a1 = 0; a1 < this.Textures.Count; ++a1)
                    {
                        if (this.Textures[a1].VltKey == key) return a1;
                    }
                    break;

                case eKeyType.CUSTOM:
                    throw new NotImplementedException();

                default:
                    break;
            }
            return -1;
        }
    }
}