using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class Summary
	{
		public double? TotalAmount { get; set; }
		
		public int? TotalLineItemNumber { get; set; }
		
		public double? TotalQuantity { get; set; }
	}
}
