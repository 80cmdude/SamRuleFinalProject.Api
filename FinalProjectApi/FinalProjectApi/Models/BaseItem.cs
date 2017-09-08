using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Models
{
	public class BaseItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}
