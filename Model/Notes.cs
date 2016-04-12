using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class Notes
	{
		[Key]
		public int NotesId { get; set; }
		
		[Required]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[StringLength(100)]
		public string NoteCode { get; set; }

		[StringLength(4000)]
		public string NoteInformationField { get; set; }
	}
}
