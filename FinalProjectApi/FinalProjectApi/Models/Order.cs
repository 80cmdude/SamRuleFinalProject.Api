﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Models
{
    public class Order : BaseItem
    {
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public string Item { get; set; }
	}
}
