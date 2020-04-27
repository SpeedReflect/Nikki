using System;
using System.IO;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Support.Shared.Parts.FNGParts;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="FNGroup"/> is a collection of frontend group elements and scripts.
    /// </summary>
    public abstract class FNGroup : ACollectable
    {
        #region Main Properties

        /// <summary>
        /// Collection Name of the class.
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
        /// Binary memory hash of the Collection Name.
        /// </summary>
        public virtual uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the Collection Name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Indicates whether current instance can be destroyed.
        /// </summary>
        public bool Destroy { get; set; }

        /// <summary>
        /// List of all <see cref="FEngColor"/> that <see cref="FNGroup"/> contains.
        /// </summary>
        protected List<FEngColor> _colorinfo = new List<FEngColor>();

        /// <summary>
        /// Length of the color information array.
        /// </summary>
        public int InfoLength => this._colorinfo.Count;

        #endregion

        #region Methods

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override ACollectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assembles <see cref="FNGroup"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="FNGroup"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="FNGroup"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="FNGroup"/> with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Gets <see cref="FEngColor"/> from a specific index.
        /// </summary>
        /// <param name="index">Index of the color.</param>
        /// <returns><see cref="FEngColor"/> class.</returns>
        public virtual FEngColor GetColor(int index) => index >= this.InfoLength ? null : this._colorinfo[index];

        /// <summary>
        /// Attempts to set new <see cref="FEngColor"/> at specific color.
        /// </summary>
        /// <param name="index">Index of the color to set.</param>
        /// <param name="color">New <see cref="FEngColor"/> to set.</param>
        /// <returns>True if setting new color was successful; false otherwise.</returns>
        public virtual bool TrySetOne(int index, FEngColor color)
        {
            var thiscolor = this.GetColor(index);
            if (thiscolor == null) return false;
            thiscolor.Alpha = color.Alpha;
            thiscolor.Red = color.Red;
            thiscolor.Green = color.Green;
            thiscolor.Blue = color.Blue;
            return true;
        }

        /// <summary>
        /// Attempts to set new <see cref="FEngColor"/> at specific color.
        /// </summary>
        /// <param name="index">Index of the color to set.</param>
        /// <param name="color">New <see cref="FEngColor"/> to set.</param>
        /// <param name="error">Error occured when trying to set one color.</param>
        /// <returns>True if setting new color was successful; false otherwise.</returns>
        public virtual bool TrySetOne(int index, FEngColor color, out string error)
        {
            error = null;
            var thiscolor = this.GetColor(index);
            if (thiscolor == null)
            {
                error = $"Color with index {index} does not exist.";
                return false;
            }
            thiscolor.Alpha = color.Alpha;
            thiscolor.Red = color.Red;
            thiscolor.Green = color.Green;
            thiscolor.Blue = color.Blue;
            return true;
        }

        /// <summary>
        /// Attempts to set same <see cref="FEngColor"/> to a specific color.
        /// </summary>
        /// <param name="index">Index of the <see cref="FEngColor"/> to match.</param>
        /// <param name="newbase">New <see cref="FEngColor"/> to set.</param>
        /// <param name="keepalpha">True if alpha value should be kept; false otherwise.</param>
        /// <returns>True if setting same colors was successful; false otherwise.</returns>
        public virtual bool TrySetSame(int index, FEngColor newbase, bool keepalpha)
        {
            var excolor = this.GetColor(index);
            if (excolor == null) return false;
            var sample = new FEngColor(null)
            {
                Alpha = excolor.Alpha,
                Red = excolor.Red,
                Green = excolor.Green,
                Blue = excolor.Blue
            };
            foreach (var color in this._colorinfo)
            {
                if (color == sample)
                {
                    color.Red = newbase.Red;
                    color.Green = newbase.Green;
                    color.Blue = newbase.Blue;
                    if (!keepalpha) color.Alpha = newbase.Alpha;
                }
            }
            return true;
        }

        /// <summary>
        /// Attempts to set same <see cref="FEngColor"/> to a specific color.
        /// </summary>
        /// <param name="index">Index of the <see cref="FEngColor"/> to match.</param>
        /// <param name="newbase">New <see cref="FEngColor"/> to set.</param>
        /// <param name="keepalpha">True if alpha value should be kept; false otherwise.</param>
        /// <param name="error">Error occured when trying to set same colors.</param>
        /// <returns>True if setting same colors was successful; false otherwise.</returns>
        public virtual bool TrySetSame(int index, FEngColor newbase, bool keepalpha, out string error)
        {
            error = null;
            var excolor = this.GetColor(index);
            if (excolor == null)
            {
                error = $"Color with index {index} does not exist.";
                return false;
            }
            var sample = new FEngColor(null)
            {
                Alpha = excolor.Alpha,
                Red = excolor.Red,
                Green = excolor.Green,
                Blue = excolor.Blue
            };
            foreach (var color in this._colorinfo)
            {
                if (color == sample)
                {
                    color.Red = newbase.Red;
                    color.Green = newbase.Green;
                    color.Blue = newbase.Blue;
                    if (!keepalpha) color.Alpha = newbase.Alpha;
                }
            }
            return true;
        }

        /// <summary>
        /// Attempts to set all <see cref="FEngColor"/> to a specific color.
        /// </summary>
        /// <param name="color"><see cref="FEngColor"/> to set for all colors.</param>
        /// <param name="keepalpha">True if alpha value should be kept; false otherwise.</param>
        /// <returns>True if setting all colors was successful; false otherwise.</returns>
        public virtual bool TrySetAll(FEngColor color, bool keepalpha)
        {
            foreach (var thiscolor in this._colorinfo)
            {
                thiscolor.Red = color.Red;
                thiscolor.Green = color.Green;
                thiscolor.Blue = color.Blue;
                if (!keepalpha) thiscolor.Alpha = color.Alpha;
            }
            return true;
        }

        /// <summary>
        /// Attempts to set all <see cref="FEngColor"/> to a specific color.
        /// </summary>
        /// <param name="color"><see cref="FEngColor"/> to set for all colors.</param>
        /// <param name="keepalpha">True if alpha value should be kept; false otherwise.</param>
        /// <param name="error">Error occured when trying to set all colors.</param>
        /// <returns>True if setting all colors was successful; false otherwise.</returns>
        public virtual bool TrySetAll(FEngColor color, bool keepalpha, out string error)
        {
            error = null;
            foreach (var thiscolor in this._colorinfo)
            {
                thiscolor.Red = color.Red;
                thiscolor.Green = color.Green;
                thiscolor.Blue = color.Blue;
                if (!keepalpha) thiscolor.Alpha = color.Alpha;
            }
            return true;
        }

        #endregion
    }
}