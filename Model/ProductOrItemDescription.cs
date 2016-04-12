using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class ProductOrItemDescription
	{
		[Key]
		public int ProductOrItemDescriptionId { get; set; }
		
		[Required]
		public int LineItemId { get; set; }

		public LineItem LineItem { get; set; }

		[StringLength(4000)]
		public string ProductDescription { get; set; }
	}
}
