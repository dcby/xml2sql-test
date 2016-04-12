using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class Packaging
	{
		[Key]
		public int PackagingsId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string UnitLoadOptionCode { get; set; }
	}
}
