namespace GlobalLib.Support.Carbon.Class
{
    public partial class TPKBlock
    {
        /// <summary>
        /// Checks texture keys and tpk keys for matching.
        /// </summary>
        protected override void CheckKeys()
        {
            this.keys.Clear();
            for (int a1 = 0; a1 < this.Textures.Count; ++a1)
                this.keys.Add(this.Textures[a1].BinKey);
        }
    }
}