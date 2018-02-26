using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Interfaces;
using JDMallen.Toolbox.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeddingPlanner.Models.Entities;

namespace WeddingPlanner.DataAccess.Config
{
	public class WpIdentityContext : IdentityDbContext<AppUser,AppRole,Guid>, IEFContext
	{
		public WpIdentityContext(DbContextOptions<WpIdentityContext> options) : base(options)
		{
		}

		public IQueryable<TEntityModel> GetQueryable<TEntityModel>()
			where TEntityModel : class, IEntityModel
			=> Set<TEntityModel>();

		public Task<int> SaveAllChanges(CancellationToken cancellationToken = default(CancellationToken))
			=> SaveChangesAsync(true, cancellationToken);

		public Task<EntityEntry<TEntityModel>> AddAsync<TEntityModel, TId>(
			TEntityModel model,
			CancellationToken cancellationToken = default(CancellationToken))
			where TEntityModel : class, IEntityModel<TId>
			where TId : struct
			=> base.AddAsync(model, cancellationToken);

		public IQueryable<TEntityModel> BuildQuery<TEntityModel>()
			where TEntityModel : class, IEntityModel
			=> Set<TEntityModel>();

		public EntityEntry Update<TEntityModel, TId>(TEntityModel modelToUpdate)
			where TEntityModel : class, IEntityModel<TId>
			where TId : struct => Update(modelToUpdate);

		public EntityEntry Remove<TEntityModel, TId>(TEntityModel modelToDelete)
			where TEntityModel : class, IEntityModel<TId>
			where TId : struct => Remove(modelToDelete);
	}
}
