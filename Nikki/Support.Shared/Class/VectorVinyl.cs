using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.VinylParts;



namespace Nikki.Support.Shared.Class
{
    /// <summary>
    /// <see cref="VectorVinyl"/> is a collection of vectors that form a vinyl.
    /// </summary>
	public abstract class VectorVinyl : Collectable, IAssembly
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
        public virtual uint BinKey => this.CollectionName.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        public virtual uint VltKey => this.CollectionName.VltHash();

        /// <summary>
        /// Number of <see cref="PathSet"/> in this <see cref="VectorVinyl"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public abstract int NumberOfPaths { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="AcidEffect"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="AcidEffect"/> with.</param>
        public abstract void Assemble(BinaryWriter bw);

        /// <summary>
        /// Disassembles array into <see cref="AcidEffect"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="AcidEffect"/> with.</param>
        public abstract void Disassemble(BinaryReader br);

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public abstract void Serialize(BinaryWriter bw);

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public abstract void Deserialize(BinaryReader br);

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Gets <see cref="PathSet"/> in this <see cref="VectorVinyl"/> at index specified.
		/// </summary>
		/// <param name="index">Index of the <see cref="PathSet"/> to get.</param>
		/// <returns><see cref="PathSet"/> at index specified.</returns>
		public abstract PathSet GetPathSet(int index);

		/// <summary>
		/// Adds <see cref="PathSet"/> to the end.
		/// </summary>
		public abstract void AddPathSet();

		/// <summary>
		/// Removes <see cref="PathSet"/> at index specified.
		/// </summary>
		/// <param name="index">Index of <see cref="PathSet"/> to remove.</param>
		public abstract void RemovePathSet(int index);

		/// <summary>
		/// Removes all <see cref="PathSet"/> from the vinyl.
		/// </summary>
		public abstract void ClearPaths();

		/// <summary>
		/// Swaps two <see cref="PathSet"/> with indexes provided.
		/// </summary>
		/// <param name="index1">Index of the first <see cref="PathSet"/> to switch.</param>
		/// <param name="index2">Index of the second <see cref="PathSet"/> to switch.</param>
		public abstract void SwitchPaths(int index1, int index2);

		/// <summary>
		/// Gets data of this <see cref="VectorVinyl"/> as an SVG-formatted string.
		/// </summary>
		/// <param name="resolution">Resolution of the SVG image.</param>
		/// <returns>Data as an SVG-formatted string.</returns>
		public string GetSVGString(int resolution)
		{
			if (resolution > 0x10000) resolution = 0x10000;
			var difference = 0x10000 / resolution;
			var bitshift = (byte)Math.Log2(difference);

			var builder = new StringBuilder(0x1000);

			var defs = new List<string>();
			var gs = new List<string>();

			for (int setnum = 0; setnum < this.NumberOfPaths; ++setnum)
			{

				var set = this.GetPathSet(setnum);
				var id = $"set{setnum}";
				builder.Append($"<path id=\"{id}\" d=\"" + Environment.NewLine);

				for (int datnum = 0; datnum < set.NumPathDatas; ++datnum)
				{

					var data = set.PathDatas[datnum];
					builder.Append("M ");

					for (int i = 0, index = data.StartIndex; i < data.NumCurves; ++i)
					{

						var px = set.PathPoints[index].X >> bitshift;
						var py = set.PathPoints[index++].Y >> bitshift;
						var cx = set.PathPoints[index].X >> bitshift;
						var cy = set.PathPoints[index++].Y >> bitshift;
						var mx = set.PathPoints[index].X >> bitshift;
						var my = set.PathPoints[index++].Y >> bitshift;

						var str = $"{px} {py} C {cx} {cy} {mx} {my} ";
						builder.Append(str);

					}

					var last = data.StartIndex + data.NumCurves * 3;
					var lx = set.PathPoints[last].X >> bitshift;
					var ly = set.PathPoints[last].Y >> bitshift;

					builder.Append($"{lx} {ly} Z " + Environment.NewLine);

				}

				builder.Append("\" />" + Environment.NewLine);
				defs.Add(builder.ToString());
				builder.Clear();
				gs.Add(this.GetFormattedSetG(set, id, resolution));

			}

			builder.Clear();

			builder.Append(this.GetSVGHeaderString(resolution));
			builder.Append("<defs>" + Environment.NewLine);
			foreach (var def in defs) builder.Append(def);
			builder.Append("</defs>" + Environment.NewLine);
			builder.Append("<g>" + Environment.NewLine);
			foreach (var g in gs) builder.Append(g);
			builder.Append("</g>" + Environment.NewLine);
			builder.Append("</svg>" + Environment.NewLine);
			return builder.ToString();
		}

		/// <summary>
		/// Reads paths from an SVG file provided and sets all its data into this <see cref="VectorVinyl"/>.
		/// </summary>
		/// <param name="file">SVG file to read.</param>
		public void ReadFromFile(string file)
		{
			using var svgreader = new SVGReader(file);
			svgreader.ReadAllContents();
			this.ClearPaths();
			svgreader.ToVectorVinyl(this);
		}

		private string GetSVGHeaderString(int resolution)
		{
			var builder = new StringBuilder(0x200);
			builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + Environment.NewLine);
			builder.Append("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" + Environment.NewLine);
			builder.Append("<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\"" + Environment.NewLine);
			builder.Append("xmlns:xlink=\"http://www.w3.org/1999/xlink\"" + Environment.NewLine);
			builder.Append("preserveAspectRatio=\"xMidYMid meet\"" + Environment.NewLine);
			builder.Append($"viewBox=\"0 0 {resolution} {resolution}\"" + Environment.NewLine);
			builder.Append($"width=\"{resolution}\" height=\"{resolution}\">" + Environment.NewLine);
			return builder.ToString();
		}

		private string GetFormattedSetG(PathSet set, string id, int resolution)
		{
			var result = $"<use xlink:href=\"#{id}\" fill-rule=\"evenodd\"";

			if (set.FillEffectExists == Reflection.Enum.eBoolean.True)
			{
			
				result += $" fill=\"{set.FillEffect.GetHTMLColor()}\"";
			
			}

			if (set.StrokeEffectExists == Reflection.Enum.eBoolean.True)
			{

				var thick = set.StrokeEffect.Thickness * resolution;
				if (thick == 0) thick = (float)resolution / 2048.0f; // this should crash the game btw

				result += $" stroke=\"{set.StrokeEffect.GetHTMLColor()}\" stroke-width=\"{thick:0.00}\"";

			}

			return result + " />" + Environment.NewLine;
		}

		#endregion
	}
}
