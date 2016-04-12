using System.Data.Entity;

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
