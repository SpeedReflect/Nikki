using System;
using System.IO;
using Nikki.Core;
using Nikki.Reflection.Enum;
using CoreExtensions.IO;



namespace Nikki.Utils
{
	internal class SerializationHeader
	{
		public eBlockID ID { get; set; } = eBlockID.Nikki;
		public int Size { get; set; }
		public GameINT Game { get; set; }
		public string Name { get; set; } = String.Empty;

		public SerializationHeader() : this(0, GameINT.None, String.Empty) { }

		public SerializationHeader(int size, GameINT game, string name)
		{
			this.Size = size + 12;
			this.Game = game;
			this.Name = name;
		}

		public void Read(BinaryReader br)
		{
			this.ID = br.ReadEnum<eBlockID>();
			this.Size = br.ReadInt32();
			this.Game = br.ReadEnum<GameINT>();
			this.Name = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.ID);
			bw.Write(this.Size);
			bw.WriteEnum(this.Game);
			bw.Write(this.Name.BinHash());
		}
	}
}
