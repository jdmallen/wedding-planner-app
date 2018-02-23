﻿using System.Linq;
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
		public WpDbContext(DbContextOptions options) : base(options)
		{
		}
		
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
