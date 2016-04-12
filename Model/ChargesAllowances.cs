using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class ChargesAllowances
	{
		[Key]
		public int ChargesAllowancesId { get; set; }
		
		public int? OrderId { get; set; }

		public Order Order { get; set; }

		public int? LineItemId { get; set; }

		public LineItem LineItem { get; set; }

		[StringLength(100)]
		public string AllowChrgIndicator { get; set; }

		[StringLength(100)]
		public string AllowChrgCode { get; set; }
		
		public double? AllowChrgAmt { get; set; }

		[StringLength(4000)]
		public string AllowChrgHandlingDescription { get; set; }
	}
}
