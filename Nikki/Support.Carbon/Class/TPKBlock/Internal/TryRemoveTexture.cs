using GlobalLib.Reflection.Enum;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type fo the key passed.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public override bool TryRemoveTexture(uint key, eKeyType type)
        {
            var index = this.GetTextureIndex(key, type);
            if (index == -1) return false;
            this.Textures.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Attempts to remove <see cref="Texture"/> specified from <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be deleted.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to remove a texture.</param>
        /// <returns>True if texture removing was successful, false otherwise.</returns>
        public override bool TryRemoveTexture(uint key, eKeyType type, out string error)
        {
            error = null;
            var index = this.GetTextureIndex(key, type);
            if (index == -1)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }
            this.Textures.RemoveAt(index);
            return true;
        }
    }
}