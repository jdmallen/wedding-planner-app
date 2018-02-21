using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JDMallen.Toolbox.Infrastructure.EFCore.Config;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WeddingPlanner.DataAccess.Config
{
	public class WpDbContext : EFContextBase, IEFContext
	{
		public WpDbContext(DbContextOptions<WpDbContext> options) : base(options)
		{
		}
	}
}
