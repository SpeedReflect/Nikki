using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="Texture"/> is a collection of dds image data used by the game.
    /// </summary>
    public abstract class Texture : ACollectable
    {
        #region Main Properties

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public override string CollectionName { get; set; }

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.None;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.None.ToString();

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        public virtual uint BinKey { get; set; }

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Represents data offset of the block in Global data.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Represents data size of the block in Global data.
        /// </summary>
        public int Size { get; protected set; } = 0;

        /// <summary>
        /// Represents palette offset of the block in Global data.
        /// </summary>
        public int PaletteOffset { get; set; } = 0;

        /// <summary>
        /// Represents palette size of the block in Global data.
        /// </summary>
        public int PaletteSize { get; protected set; } = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets compression type of the texture.
        /// </summary>
        /// <returns>Compression type as a string.</returns>
        public abstract string Compression { get; }

        /// <summary>
        /// Represents height in pixels of the texture.
        /// </summary>
        public short Width { get; protected set; }

        /// <summary>
        /// Represents height in pixels of the texture.
        /// </summary>
        public short Height { get; protected set; }

        /// <summary>
        /// Represents base 2 value of the width of the texture.
        /// </summary>
        public byte Log_2_Width => (byte)Math.Log(this.Width, 2);

        /// <summary>
        /// Represents base 2 value of the height of the texture.
        /// </summary>
        public byte Log_2_Height => (byte)Math.Log(this.Height, 2);

        /// <summary>
        /// Represents number of mipmaps in the texture.
        /// </summary>
        public byte Mipmaps { get; protected set; }

        /// <summary>
        /// Represents mipmap bias type of the texture.
        /// </summary>
        public eTextureMipmapBiasType MipmapBiasType { get; protected set; }

        /// <summary>
        /// Represents tileable level of the texture.
        /// </summary>
        [AccessModifiable()]
        public byte TileableUV { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Texture"/> header into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Texture"/> header with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="Texture"/> header properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Texture"/> header with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Gets .dds data along with the .dds header.
        /// </summary>
        /// <returns>.dds texture as a byte array.</returns>
        public abstract byte[] GetDDSArray();

        /// <summary>
        /// Initializes all properties of the new <see cref="Texture"/>.
        /// </summary>
        /// <param name="filename">Filename of the .dds texture passed.</param>
        protected abstract void Initialize(string filename);

        /// <summary>
        /// Reads .dds data from the <see cref="TPKBlock"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="forced">If forced, ignores internal offset and reads data 
        /// starting at the pointer passed.</param>
        public abstract void ReadData(BinaryReader br, bool forced);

        /// <summary>
        /// Reloads <see cref="Texture"/> properties based on the new file passed.
        /// </summary>
        /// <param name="filename">Filename of .dds texture passed.</param>
        public virtual void Reload(string filename) => this.Initialize(filename);

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}