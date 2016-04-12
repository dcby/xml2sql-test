using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class ProductID
	{
		[StringLength(100)]
		public string PartNumberQual { get; set; }

		[StringLength(100)]
		public string PartNumber { get; set; }
	}
}
