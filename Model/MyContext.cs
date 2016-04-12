using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Sql.Model
{
	public class MyContext : DbContext
	{
		public MyContext() : base("name=Xml2Sql")
		{

		}

		public DbSet<Order> Orders { get; set; }
	}
}
