using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Support.Carbon.Class;
using Nikki.Support.Carbon.Parts.CarParts;



namespace Nikki.Database
{
	public class Carbon
	{
		public List<DBModelPart> DBModelPartList { get; set; }
		public List<CPStruct> CarPartStructs { get; set; }

		public Carbon()
		{
			this.DBModelPartList = new List<DBModelPart>();
			this.CarPartStructs = new List<CPStruct>();
		}
	}
}
