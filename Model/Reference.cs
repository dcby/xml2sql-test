using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class Reference
	{
		[Key]
		public int ReferenceId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string ReferenceQual { get; set; }

		[StringLength(100)]
		public string Reference_ID { get; set; }

		[StringLength(4000)]
		public string Description { get; set; }
	}
}
