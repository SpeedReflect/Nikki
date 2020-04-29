namespace GlobalLib.Support.Carbon.Class
{
	public partial class TPKBlock
	{
		/// <summary>
		/// Sorts <see cref="Texture"/> by their CollectionNames or BinKeys.
		/// </summary>
		/// <param name="by_name">True if sort by name; false is sort by hash.</param>
		public override void SortTexturesByType(bool by_name)
		{
			if (by_name)
				this.Textures.Sort((x, y) => x.CollectionName.CompareTo(y.CollectionName));
			else
				this.Textures.Sort((x, y) => x.BinKey.CompareTo(y.BinKey));
		}
	}
}