using System;
using System.IO;
using System.Text;



namespace Nikki.Support.Shared.Parts.TPKParts
{
	public class TexturePage
	{
		public float U0 { get; set; }
		public float V0 { get; set; }
		public float U1 { get; set; }
		public float V1 { get; set; }
		public uint Flags { get; set; }
		public string TextureName { get; set; }

		public TexturePage()
		{
			this.TextureName = String.Empty;
		}
	}
}
