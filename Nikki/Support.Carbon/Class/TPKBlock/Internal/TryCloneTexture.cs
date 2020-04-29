using GlobalLib.Reflection.Enum;
using GlobalLib.Utils;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public override bool TryCloneTexture(string newname, uint key, eKeyType type)
        {
            if (string.IsNullOrWhiteSpace(newname)) return false;

            if (this.FindTexture(Bin.Hash(newname), type) != null)
                return false;

            var copyfrom = (Texture)this.FindTexture(key, type);
            if (copyfrom == null) return false;

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attempts to clone <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="newname">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to clone.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="error">Error occured when trying to clone a texture.</param>
        /// <returns>True if texture cloning was successful, false otherwise.</returns>
        public override bool TryCloneTexture(string newname, uint key, eKeyType type, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(newname))
            {
                error = $"CollectionName cannot be empty or whitespace.";
                return false;
            }

            if (this.FindTexture(Bin.Hash(newname), type) != null)
            {
                error = $"Texture with CollectionName {newname} already exists.";
                return false;
            }

            var copyfrom = (Texture)this.FindTexture(key, type);
            if (copyfrom == null)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }

            var texture = (Texture)copyfrom.MemoryCast(newname);
            this.Textures.Add(texture);
            return true;
        }
    }
}