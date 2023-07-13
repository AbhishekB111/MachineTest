using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MachineTest.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> Options) : base(Options) 
		{

		}
		public DbSet<Product> Products { get; set;}
		public DbSet<Category> Categories { get; set;}
	}
}
