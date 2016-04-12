﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class LineItem
	{
		[Key]
		public int LineItemId { get; set; }

		public OrderLine OrderLine { get; set; }

		public ICollection<Date> Dates { get; set; }

		public ICollection<ProductOrItemDescription> ProductOrItemDescriptions { get; set; }

		public ICollection<ChargesAllowances> ChargesAllowances { get; set; }
	}
}
