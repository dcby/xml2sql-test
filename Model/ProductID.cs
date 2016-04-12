using System.ComponentModel.DataAnnotations;

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
