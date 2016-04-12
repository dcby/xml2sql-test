using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class CarrierInformation
	{
		[Key]
		public int CarrierInformationId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string CarrierTransMethodCode { get; set; }

		[StringLength(100)]
		public string CarrierAlphaCode { get; set; }

		[StringLength(100)]
		public string CarrierRouting { get; set; }

		[StringLength(100)]
		public string ServiceLevelCodes { get; set; }
	}
}
