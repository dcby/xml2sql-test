using System.ComponentModel.DataAnnotations;

namespace Xml2Sql.Model
{
	public class FOBRelatedInstruction
	{
		[Key]
		public int FOBRelatedInstructionId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string FOBPayCode { get; set; }

		[StringLength(100)]
		public string FOBLocationQualifier { get; set; }

		[StringLength(4000)]
		public string FOBLocationDescription { get; set; }

		[StringLength(100)]
		public string FOBTitlePassageCode { get; set; }
	}
}
