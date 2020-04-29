namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Sorts textures by their binary memory hashes.
        /// </summary>
        protected override void TextureSort()
        {
            for (int a1 = 0; a1 < this.keys.Count; ++a1)
            {
                for (int a2 = 0; a2 < this.keys.Count - 1; ++a2)
                {
                    if (this.keys[a2] > this.keys[a2 + 1])
                    {
                        // Switch keys
                        var tempkey = this.keys[a2 + 1];
                        this.keys[a2 + 1] = this.keys[a2];
                        this.keys[a2] = tempkey;

                        // Switch textures
                        var temptexture = this.Textures[a2 + 1];
                        this.Textures[a2 + 1] = this.Textures[a2];
                        this.Textures[a2] = temptexture;

                        // Switch compressions
                        var tempcompression = this.compressions[a2 + 1];
                        this.compressions[a2 + 1] = this.compressions[a2];
                        this.compressions[a2] = tempcompression;
                    }
                }
            }
        }
    }
}