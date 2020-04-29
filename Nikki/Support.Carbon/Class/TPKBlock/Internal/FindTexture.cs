using GlobalLib.Reflection.Enum;
using System;

namespace GlobalLib.Support.Carbon.Class
{
	public partial class TPKBlock
	{
		/// <summary>
		/// Tries to find <see cref="Texture"/> based on the key passed.
		/// </summary>
		/// <param name="key">Key of the <see cref="Texture"/> Collection Name.</param>
		/// <param name="type">Type of the key passed.</param>
		/// <returns>Texture if it is found; null otherwise.</returns>
		public override Shared.Class.Texture FindTexture(uint key, eKeyType type)
		{
			switch (type)
			{
				case eKeyType.BINKEY:
					return this.Textures.Find(t => t.BinKey == key);
				case eKeyType.VLTKEY:
					return this.Textures.Find(t => t.VltKey == key);
				case eKeyType.CUSTOM:
					throw new NotImplementedException();
				default:
					return null;
			}
		}
	}
}
