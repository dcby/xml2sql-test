using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class Address
	{
		[Key]
		public int AddressId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string AddressTypeCode { get; set; }

		[StringLength(100)]
		public string LocationCodeQualifier { get; set; }

		[StringLength(100)]
		public string AddressLocationNumber { get; set; }

		[StringLength(100)]
		public string AddressName { get; set; }

		[StringLength(100)]
		public string Address1 { get; set; }

		[StringLength(100)]
		public string Address2 { get; set; }

		[StringLength(100)]
		public string City { get; set; }

		[StringLength(100)]
		public string State { get; set; }

		[StringLength(100)]
		public string PostalCode { get; set; }

		[StringLength(100)]
		public string Country { get; set; }
	}
}
