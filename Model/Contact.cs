using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class Contact
	{
		[Key]
		public int ContactId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string ContactTypeCode { get; set; }

		[StringLength(100)]
		public string ContactName { get; set; }

		[StringLength(100)]
		public string PrimaryPhone { get; set; }
	}
}
