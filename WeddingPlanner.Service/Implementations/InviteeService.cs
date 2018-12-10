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
	public class InviteeService : IInviteeService
	{
		private readonly Settings _settings;
		private readonly IRepository<Relationship, int> _relationshipRepository;
		private readonly IRepository<Invitation, Guid> _invitationRepository;
		private readonly IRepository<MealChoice, int> _mealChoiceRepository;

		public InviteeService(
			IRepository<Invitee, Guid> repository,
			Settings settings,
			IRepository<Relationship, int> relationshipRepository,
			IRepository<MealChoice, int> mealChoiceRepository,
			IRepository<Invitation, Guid> invitationRepository)
		{
			Repository = repository;
			_settings = settings;
			_relationshipRepository = relationshipRepository;
			_mealChoiceRepository = mealChoiceRepository;
			_invitationRepository = invitationRepository;
		}

		/// <summary>
		/// The <see cref="IRepository"/> used to perform all the CRUD actions
		/// </summary>
		public IRepository<Invitee, Guid> Repository { get; }

		public IAsyncEnumerable<Relationship> GetAllRelationshipTypeObjects()
		{
			return _relationshipRepository.ListAllAsync();
		}

		public Task<List<string>> GetAllRelationshipTypes()
		{
			return _relationshipRepository.ListAllAsync()
				.Select(x => x.RelationshipName)
				.Distinct()
				.ToList();
		}

		public IAsyncEnumerable<Invitee> ListAllInvitees()
		{
			return Repository.ListAllAsync();
		}

		public IAsyncEnumerable<MealChoice> ListMealChoices()
		{
			return _mealChoiceRepository.ListAllAsync();
		}

		/// <summary>
		/// Fetch a single <see cref="Invitee"/> via its <see cref="id"/>
		/// </summary>
		/// <param name="id">The ID of the object to fetch</param>
		/// <returns>The fetched object</returns>
		public Task<Invitee> Read(Guid id) => Repository.GetByIdAsync(id);

		/// <summary>
		/// Fetch many <see cref="Invitee"/>s via a set of <see cref="parameters"/>
		/// </summary>
		/// <param name="parameters">The search parameters</param>
		/// <returns>The fetched list of objects</returns>
		public IAsyncEnumerable<Invitee> Find(InviteeQueryParameters parameters)
		{
			var specification =
				new InviteeFilterSpecification(parameters);
			return Repository.FindBySpecAsync(specification);
		}

		/// <summary> 
		/// Fetch many <see cref="Invitee"/>s via a set of <see cref="parameters"/> 
		/// and wrap the results in an <see cref="IPagedResult{TEntityModel}"/> suitable for UI pagination.
		/// </summary>
		/// <param name="parameters">The search parameters</param>
		/// <returns>The fetched list of objects in a paged result object</returns>
		public async Task<IPagedResult<Invitee>> FindPaged(
			InviteeQueryParameters parameters)
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
		/// Creates a new <see cref="Invitee"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Invitee> Upsert(Invitee model)
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
		/// Updates an existing <see cref="Invitee"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Invitee> Update(Invitee model)
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
		/// Deletes an existing <see cref="Invitee"/>
		/// </summary>
		/// <param name="model">The object to be deleted</param>
		/// <returns>The deleted object</returns>
		public async Task<Invitee> Delete(Invitee model)
		{
			await Repository.Remove(model);
			return model;
		}
	}
}
