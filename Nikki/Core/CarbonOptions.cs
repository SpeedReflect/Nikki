using System;
using System.Collections.Generic;
using System.Text;

namespace Nikki.Core
{
	public class CarbonOptions
	{
		public CarbonOptions Default => new CarbonOptions()
		{
			GlobalA = String.Empty,
			GlobalB = String.Empty,
			LangFile = String.Empty,
			Watermark = $"Nikki by MaxHwoy | {DateTime.Today:00/00/0000}",
			MessageShow = false,
			CarTypeInfos = false,
			Collisions = false,
			DBModelParts = false,
			Materials = false,
			PresetRides = false,
			PresetSkins = false,
			STRBlocks = false,
			TPKBlocks = false
		};

		public string GlobalA;

		public string GlobalB;

		public string LangFile;

		public string Watermark;

		public bool MessageShow;

		public bool CarTypeInfos;

		public bool Collisions;

		public bool DBModelParts;

		public bool FNGroups;

		public bool Materials;

		public bool PresetRides;

		public bool PresetSkins;

		public bool STRBlocks;

		public bool TPKBlocks;

		public CarbonOptions() { }
	}
}
