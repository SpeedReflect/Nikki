using System;
using System.Collections.Generic;
using System.Text;
using Nikki.Support.Underground2.Class;



namespace Nikki.Database
{
	public class Underground2
	{
		public List<DBModelPart> DBModelPartList { get; set; }

		public Underground2()
		{
			this.DBModelPartList = new List<DBModelPart>();
		}
	}
}
