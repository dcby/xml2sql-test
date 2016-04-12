using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class PaymentTerms
	{
		[Key]
		public int PaymentTermsId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(4000)]
		public string TermsDescription { get; set; }
	}
}
