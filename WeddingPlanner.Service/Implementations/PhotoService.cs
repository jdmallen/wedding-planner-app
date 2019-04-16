using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDMallen.Toolbox.EFCore.Patterns.Repository.Interfaces;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Interfaces;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;
using WeddingPlanner.DataAccess.Specifications;
using WeddingPlanner.Services.Interfaces;

namespace WeddingPlanner.Services.Implementations
{
	public class PhotoService : IPhotoService
	{
		private readonly Settings _settings;

		public PhotoService(
			Settings settings,
			IRepository<Photo, Guid> repository)
		{
			_settings = settings;
			Repository = repository;
		}

		public IRepository<Photo, Guid> Repository { get; }

		public IAsyncEnumerable<Photo> ListAllPhotos()
			=> Repository.ListAllAsync();

		public Task<Photo> Read(Guid id) => Repository.GetByIdAsync(id);

		public IAsyncEnumerable<Photo> Find(PhotoQueryParameters parameters)
			=> Repository.FindBySpecAsync(
				new PhotoFilterSpecification(parameters));

		public async Task<IPagedResult<Photo>> FindPaged(
			PhotoQueryParameters parameters)
		{
			if (!parameters.Skip.HasValue || !parameters.Take.HasValue)
			{
				parameters.Skip = 0;
				parameters.Take = 10;
			}

			var results = Find(parameters);

			var list = await results.ToList();
			return list.AsPaged(
				parameters.Skip.Value,
				parameters.Take.Value,
				0);
		}

		/// <summary>
		/// Creates a new <see cref="Photo"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Photo> Upsert(Photo model)
		{
			var exists = await Repository.ExistsByIdAsync(model.Id);
			if (exists)
			{
				// todo validation
				return await Repository.UpdateAsync(model);
			}

			// todo validation
			return await Repository.AddAsync(model);
		}

		/// <summary>
		/// Updates an existing <see cref="Photo"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Photo> Update(Photo model)
		{
			var exists = await Repository.ExistsByIdAsync(model.Id);
			if (exists)
			{
				// todo validation
				return await Repository.UpdateAsync(model);
			}

			throw new ArgumentException(
				"Cannot update model; id not found",
				nameof(model));
		}

		/// <summary>
		/// Deletes an existing <see cref="Photo"/>
		/// </summary>
		/// <param name="model">The object to be deleted</param>
		/// <returns>The deleted object</returns>
		public async Task<Photo> Delete(Photo model)
		{
			await Repository.Remove(model);
			return model;
		}
	}
}
