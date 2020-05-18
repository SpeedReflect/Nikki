using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Support.Prostreet.Class;
using Nikki.Support.Prostreet.Framework;
using Nikki.Support.Prostreet.Parts.CarParts;



namespace Nikki.Database
{
    /// <summary>
    /// Database of roots and collections for Need for Speed: Most Wanted.
    /// </summary>
	public class Prostreet : ABasicBase
	{
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Prostreet;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Prostreet.ToString();

        #region Roots

        /// <summary>
        /// 
        /// </summary>
        public Root<Material> Materials { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<CarTypeInfo> CarTypeInfos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<Collision> Collisions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Root<SunInfo> SunInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Root<Track> Tracks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Root<FNGroup> FNGroups { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<TPKBlock> TPKBlocks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<STRBlock> STRBlocks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Root<DBModelPart> ModelParts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CPStruct> CarPartStructs { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="Prostreet"/>.
        /// </summary>
        public Prostreet() { }

        /// <summary>
        /// Initializes new instance of <see cref="Prostreet"/>.
        /// </summary>
        /// <param name="initialize">True if all roots should be auto-initialized.</param>
        public Prostreet(bool initialize)
        {
            if (initialize) this.Initialize();
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~Prostreet()
        {
            this.Buffer = null;
            this.CarTypeInfos = null;
            this.FNGroups = null;
            this.Materials = null;
            this.Collisions = null;
            this.TPKBlocks = null;
            this.STRBlocks = null;
            this.ModelParts = null;
            this.CarPartStructs = null;
        }

        #endregion

		#region Methods

		private void Initialize()
        {
			this.Materials = new Root<Material>
            (
            	"Materials",
            	Material.BaseClassSize,
            	true,
            	true,
            	this
            );

			this.CarTypeInfos = new Root<CarTypeInfo>
			(
				"CarTypeInfos",
				CarTypeInfo.BaseClassSize,
				true,
				true,
				this
			);

			this.Collisions = new Root<Collision>
			(
				"Collisions",
				-1,
				true,
				true,
				this
			);

            this.SunInfos = new Root<SunInfo>
			(
				"SunInfos",
				SunInfo.BaseClassSize,
				true,
				true,
				this
			);

			this.Tracks = new Root<Track>
			(
				"Tracks",
				Track.BaseClassSize,
				true,
				false,
				this
			);

			this.FNGroups = new Root<FNGroup>
			(
				"FNGroups",
				-1,
				false,
				false,
				this
			);

			this.TPKBlocks = new Root<TPKBlock>
			(
				"TPKBlocks",
				-1,
				true,
				true,
				this
			);

			this.STRBlocks = new Root<STRBlock>
			(
				"STRBlocks",
				-1,
				true,
				true,
				this
			);

			this.ModelParts = new Root<DBModelPart>
			(
				"ModelParts",
				-1,
				true,
				false,
				this
			);

            this.CarPartStructs = new List<CPStruct>();
        }

        /// <summary>
        /// Returns name of the database.
        /// </summary>
        /// <returns>Name of this database as a string value.</returns>
        public override string ToString()
        {
            return "Database (Prostreet)";
        }

        /// <summary>
        /// Gets information about <see cref="Prostreet"/> database.
        /// </summary>
        /// <returns></returns>
        public override string GetDatabaseInfo()
        {
            string nl = Environment.NewLine;
            string equals = " = ";
            string collections = " collections.";
            string info = this.ToString() + nl;
            info += $"{this.CarTypeInfos.ThisName}{equals}{this.CarTypeInfos.Length}{collections}{nl}";
            info += $"{this.Collisions.ThisName}{equals}{this.Collisions.Length}{collections}{nl}";
            info += $"{this.Materials.ThisName}{equals}{this.Materials.Length}{collections}{nl}";
            info += $"{this.ModelParts.ThisName}{equals}{this.ModelParts.Length}{collections}{nl}";
            info += $"{this.SunInfos.ThisName}{equals}{this.SunInfos.Length}{collections}{nl}";
            info += $"{this.Tracks.ThisName}{equals}{this.Tracks.Length}{collections}{nl}";
            return info;
        }

        /// <summary>
        /// Loads all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to load data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public override bool Load(Options options) => Loader.Invoke(options, this);

        /// <summary>
        /// Saves all data in the database using options passed.
        /// </summary>
        /// <param name="options"><see cref="Options"/> that are used to save data.</param>
        /// <returns>True on success; false otherwise.</returns>
        public override bool Save(Options options) => Saver.Invoke(options, this);

		#endregion
	}
}
