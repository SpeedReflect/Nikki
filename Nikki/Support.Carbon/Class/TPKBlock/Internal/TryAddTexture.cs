using GlobalLib.Reflection.Enum;
using GlobalLib.Utils;
using GlobalLib.Utils.EA;

namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public override bool TryAddTexture(string CName, string filename)
        {
            if (string.IsNullOrWhiteSpace(CName)) return false;

            if (this.FindTexture(Bin.Hash(CName), eKeyType.BINKEY) != null)
                return false;

            if (!Comp.IsDDSTexture(filename))
                return false;

            var texture = new Texture(CName, this.CollectionName, filename, this.Database);
            this.Textures.Add(texture);
            return true;
        }

        /// <summary>
        /// Attempts to add <see cref="Texture"/> to the <see cref="TPKBlock"/> data.
        /// </summary>
        /// <param name="CName">Collection Name of the new <see cref="Texture"/>.</param>
        /// <param name="filename">Path of the texture to be imported.</param>
        /// <param name="error">Error occured when trying to add a texture.</param>
        /// <returns>True if texture adding was successful, false otherwise.</returns>
        public override bool TryAddTexture(string CName, string filename, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(CName))
            {
                error = $"Collection Name cannot be empty or whitespace.";
                return false;
            }

            if (this.FindTexture(Bin.Hash(CName), eKeyType.BINKEY) != null)
            {
                error = $"Texture named {CName} already exists.";
                return false;
            }

            if (!Comp.IsDDSTexture(filename))
            {
                error = $"Texture passed is not a DDS texture.";
                return false;
            }

            var texture = new Texture(CName, this.CollectionName, filename, this.Database);
            this.Textures.Add(texture);
            return true;
        }
    }
}