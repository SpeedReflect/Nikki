using System;
using System.Collections.Generic;
using System.Text;

namespace Nikki.Core
{
	/// <summary>
	/// 
	/// </summary>
	public class CarbonOptions
	{
		/// <summary>
		/// 
		/// </summary>
		public CarbonOptions Default => new CarbonOptions()
		{
			File = String.Empty,
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:00/00/0000}",
			MessageShow = false,
			CarTypeInfos = false,
			Collisions = true,
			ModelParts = false,
			Materials = false,
			PresetRides = false,
			PresetSkins = false,
			STRBlocks = false,
			TPKBlocks = false
		};

		/// <summary>
		/// 
		/// </summary>
		public string File { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		public string Watermark { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		public bool MessageShow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool CarTypeInfos { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool Collisions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool ModelParts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool FNGroups { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool Materials { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool PresetRides { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool PresetSkins { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool STRBlocks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool TPKBlocks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public CarbonOptions() { }
	}
}
