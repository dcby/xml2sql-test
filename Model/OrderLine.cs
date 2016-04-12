using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	[ComplexType]
	public class OrderLine
	{
		[StringLength(100)]
		public string LineSequenceNumber { get; set; }

		[StringLength(100)]
		public string BuyerPartNumber { get; set; }

		[StringLength(100)]
		public string VendorPartNumber { get; set; }

		[StringLength(100)]
		public string ConsumerPackageCode { get; set; }

		[StringLength(100)]
		public string GTIN { get; set; }

		[StringLength(100)]
		public string UPCCaseCode { get; set; }

		public ProductID ProductID { get; set; }

		public double? OrderQty { get; set; }

		[StringLength(100)]
		public string OrderQtyUOM { get; set; }
		
		public double? PurchasePrice { get; set; }
	}
}
