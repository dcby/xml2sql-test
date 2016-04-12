using System;
using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class Date
	{
		[Key]
		public int DateId { get; set; }
		
		public int? OrderId { get; set; }

		public Order Order { get; set; }

		public int? LineItemId { get; set; }

		public LineItem LineItem { get; set; }

		[StringLength(100)]
		public string DateTimeQualifier1 { get; set; }

		public DateTime? Date1 { get; set; }
	}
}
