using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }

		[Required]
		[StringLength(100)]
		public string TradingPartnerId { get; set; }

		[Required]
		[StringLength(100)]
		public string PurchaseOrderNumber { get; set; }

		[StringLength(100)]
		public string TsetPurposeCode { get; set; }

		[StringLength(100)]
		public string PurchaseOrderTypeCode { get; set; }

		public DateTime? PurchaseOrderDate { get; set; }

		[StringLength(100)]
		public string ShipCompleteCode { get; set; }

		[StringLength(100)]
		public string SellersCurrency { get; set; }

		[StringLength(100)]
		public string Department { get; set; }

		[StringLength(100)]
		public string Vendor { get; set; }

		[StringLength(100)]
		public string Division { get; set; }

		[StringLength(100)]
		public string CustomerOrderNumber { get; set; }

		public Summary Summary { get; set; }

		public ICollection<PaymentTerms> PaymentTerms { get; set; }

		public ICollection<Date> Dates { get; set; }

		public ICollection<Contact> Contacts { get; set; }

		public ICollection<Address> Addresses { get; set; }

		public ICollection<FOBRelatedInstruction> FOBRelatedInstructions { get; set; }

		public ICollection<Packaging> Packagings { get; set; }

		public ICollection<CarrierInformation> CarrierInformations { get; set; }

		public ICollection<Reference> References { get; set; }

		public ICollection<Notes> Notes { get; set; }

		public ICollection<ChargesAllowances> ChargesAllowances { get; set; }

		public ICollection<LineItem> LineItems { get; set; }
	}
}
