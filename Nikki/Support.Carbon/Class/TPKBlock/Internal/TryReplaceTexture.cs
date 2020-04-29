using GlobalLib.Reflection.Enum;
using GlobalLib.Utils.EA;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Attemps to replace texture specified in the TPKBlock data with a new one.
        /// </summary>
        /// <param name="CName">Collection Name of the texture to be replaced.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public override bool TryReplaceTexture(uint key, eKeyType type, string filename)
        {
            var tex = (Texture)this.FindTexture(key, type);
            if (tex == null) return false;
            if (!Comp.IsDDSTexture(filename)) return false;
            tex.Reload(filename);
            return true;
        }

        /// <summary>
        /// Attemps to replace <see cref="Texture"/> specified in the <see cref="TPKBlock"/> data with a new one.
        /// </summary>
        /// <param name="key">Key of the Collection Name of the <see cref="Texture"/> to be replaced.</param>
        /// <param name="type">Type of the key passed.</param>
        /// <param name="filename">Path of the texture that replaces the current one.</param>
        /// <returns>True if texture replacing was successful, false otherwise.</returns>
        public override bool TryReplaceTexture(uint key, eKeyType type, string filename, out string error)
        {
            error = null;
            var tex = (Texture)this.FindTexture(key, type);
            if (tex == null)
            {
                error = $"Texture with key 0x{key:X8} does not exist.";
                return false;
            }

            if (!Comp.IsDDSTexture(filename))
            {
                error = $"File {filename} is not a valid DDS texture.";
                return false;
            }

            tex.Reload(filename);
            return true;
        }
    }
}