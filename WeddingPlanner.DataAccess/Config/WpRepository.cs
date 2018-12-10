using JDMallen.Toolbox.EFCore.Patterns.Repository.Implementations;
using JDMallen.Toolbox.Interfaces;

namespace WeddingPlanner.DataAccess.Config
{
	public class WpRepository<TEntityModel, TId>
		: EFRepositoryBase<WpDbContext, TEntityModel, TId>
		where TEntityModel : class, IEntityModel<TId>
		where TId : struct
	{
		public WpRepository(WpDbContext context) : base(context)
		{
		}
	}
}
