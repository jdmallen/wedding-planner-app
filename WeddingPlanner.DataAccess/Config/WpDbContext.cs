using JDMallen.Toolbox.Infrastructure.EFCore.Config;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.DataAccess.Config
{
	public class WpDbContext : EFContextBase
	{
		public WpDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
