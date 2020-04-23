using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Support.MostWanted.Class;



namespace Nikki.Database
{
	public class MostWanted
	{
		public List<DBModelPart> DBModelPartList { get; set; }

		public MostWanted()
		{
			this.DBModelPartList = new List<DBModelPart>();
		}
	}
}
