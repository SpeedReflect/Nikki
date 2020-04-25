using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Support.Carbon.Class;



namespace Nikki.Database
{
	public class Carbon
	{
		public List<DBModelPart> DBModelPartList { get; set; }

		public Carbon()
		{
			this.DBModelPartList = new List<DBModelPart>();
		}
	}
}
