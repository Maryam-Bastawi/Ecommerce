﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Dtos.Orders
{
	public class DeliveryMethodDto
	{
		public string ShortName { get; set; }
		public string DeliveryTime { get; set; }
		public string Description { get; set; }
		public decimal Cost { get; set; }
	}
}
