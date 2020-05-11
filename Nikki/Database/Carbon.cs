using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Core;
using Nikki.Reflection.Abstract;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Framework;
using Nikki.Support.Carbon.Parts.CarParts;



namespace Nikki.Database
{
	/// <summary>
	/// Database of roots and collections for Need for Speed: Carbon.
	/// </summary>
	public class Carbon : ABasicBase
	{
        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public override string GameSTR => GameINT.Carbon.ToString();

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
		public Root<PresetRide> PresetRides { get; set; }
        
		/// <summary>
		/// 
		/// </summary>
		public Root<PresetSkin> PresetSkins { get; set; }
        
		/// <summary>
		/// 
		/// </summary>
		public Root<Collision> Collisions { get; set; }
        
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

		/// <summary>
		/// Initializes new instance of <see cref="Carbon"/>.
		/// </summary>
        public Carbon() { }

		/// <summary>
		/// Initializes new instance of <see cref="Carbon"/>.
		/// </summary>
		/// <param name="initialize">True if all roots should be auto-initialized.</param>
		public Carbon(bool initialize)
		{
			if (initialize) this.Initialize();
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
        ~Carbon()
        {
            this.Data_GlobalABUN = null;
            this.Data_GlobalBLZC = null;
            this.Data_LngGlobal = null;
            this.Data_LngLabels = null;
            this.CarTypeInfos = null;
            this.FNGroups = null;
            this.Materials = null;
            this.PresetRides = null;
            this.PresetSkins = null;
			this.Collisions = null;
            this.TPKBlocks = null;
            this.STRBlocks = null;
			this.ModelParts = null;
        }

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

			this.PresetRides = new Root<PresetRide>
			(
				"PresetRides",
				PresetRide.BaseClassSize,
				true,
				true,
				this
			);

			this.PresetSkins = new Root<PresetSkin>
			(
				"PresetSkins",
				PresetSkin.BaseClassSize,
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
			return "Database (Carbon)";
		}

		/// <summary>
		/// Gets information about <see cref="Carbon"/> database.
		/// </summary>
		/// <returns></returns>
		public override string GetDatabaseInfo()
		{
			string nl = Environment.NewLine;
			string equals = " = ";
			string collections = " collections.";
			string info = this.ToString() + nl;
			info += $"{this.CarTypeInfos.ThisName}{equals}{this.CarTypeInfos.Length}{collections}{nl}";
			info += $"{this.Materials.ThisName}{equals}{this.Materials.Length}{collections}{nl}";
			info += $"{this.PresetRides.ThisName}{equals}{this.PresetRides.Length}{collections}{nl}";
			info += $"{this.PresetSkins.ThisName}{equals}{this.PresetSkins.Length}{collections}{nl}";
			info += $"{this.Collisions.ThisName}{equals}{this.Collisions.Length}{collections}{nl}";
			info += $"{this.ModelParts.ThisName}{equals}{this.ModelParts.Length}{collections}{nl}";
			return info;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Load()
		{
			return false;
		}

		public bool Load(CarbonOptions options) => Loader.Invoke(options, this);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			return false;
		}
	}
}
