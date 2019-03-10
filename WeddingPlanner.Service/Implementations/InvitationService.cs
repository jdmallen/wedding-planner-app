using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDMallen.Toolbox.EFCore.Patterns.Repository.Interfaces;
using JDMallen.Toolbox.Extensions;
using JDMallen.Toolbox.Interfaces;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.DataAccess.Constants;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;
using WeddingPlanner.DataAccess.Specifications;
using WeddingPlanner.Services.Interfaces;

namespace WeddingPlanner.Services.Implementations
{
	public class InvitationService : IInvitationService
	{
		private readonly Settings _settings;

		public InvitationService(
			IRepository<Invitation, Guid> repository,
			Settings settings)
		{
			Repository = repository;
			_settings = settings;
		}

		/// <summary>
		/// The <see cref="IRepository"/> used to perform all the CRUD actions
		/// </summary>
		public IRepository<Invitation, Guid> Repository { get; }

		public IEnumerable<string> GetAllInvitationTypes()
		{
			return Enum.GetNames(typeof(InvitationType));
		}

		public IAsyncEnumerable<Invitation> ListAllInvitations()
		{
			return Repository.ListAllAsync();
		}

		/// <summary>
		/// Fetch a single <see cref="Invitation"/> via its <see cref="id"/>
		/// </summary>
		/// <param name="id">The ID of the object to fetch</param>
		/// <returns>The fetched object</returns>
		public Task<Invitation> Read(Guid id) => Repository.GetByIdAsync(id);

		/// <summary>
		/// Fetch many <see cref="Invitation"/>s via a set of <see cref="parameters"/>
		/// </summary>
		/// <param name="parameters">The search parameters</param>
		/// <returns>The fetched list of objects</returns>
		public IAsyncEnumerable<Invitation> Find(
			InvitationQueryParameters parameters)
		{
			var specification =
				new InvitationFilterSpecification(parameters);
			return Repository.FindBySpecAsync(specification);
		}

		/// <summary> 
		/// Fetch many <see cref="Invitation"/>s via a set of <see cref="parameters"/> 
		/// and wrap the results in an <see cref="IPagedResult{TEntityModel}"/> suitable for UI pagination.
		/// </summary>
		/// <param name="parameters">The search parameters</param>
		/// <returns>The fetched list of objects in a paged result object</returns>
		public async Task<IPagedResult<Invitation>> FindPaged(
			InvitationQueryParameters parameters)
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
		/// Creates a new <see cref="Invitation"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Invitation> Upsert(Invitation model)
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
		/// Updates an existing <see cref="Invitation"/>
		/// </summary>
		/// <param name="model">The object to be created</param>
		/// <returns>The created object</returns>
		public async Task<Invitation> Update(Invitation model)
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
		/// Deletes an existing <see cref="Invitation"/>
		/// </summary>
		/// <param name="model">The object to be deleted</param>
		/// <returns>The deleted object</returns>
		public async Task<Invitation> Delete(Invitation model)
		{
			await Repository.Remove(model);
			return model;
		}

		/// <summary>
		/// Generate a unique 5-character invitation code to be included in the
		/// printed invitations so people can RSVP on this site.
		/// </summary>
		/// <remarks>
		/// The 31-character set includes all lowercase letters and numbers,
		/// except any that might be confused with one another:
		/// '0', 'o', '1', 'i', and 'l'.
		/// </remarks>
		public async Task<string> GenerateInvitationCode()
		{
			var codeLength = _settings.InvitationCodeLength;
			const string characterSet =
				"23456789abcdefghjkmnpqrstuvwxyz";
			bool codeAlreadyExists;
			var newCodeChars = new List<char>(codeLength);
			string code;

			do
			{
				var random = new Random();
				for (var i = 0; i < codeLength; i++)
				{
					var charToAdd =
						characterSet[random.Next(characterSet.Length - 1)];
					newCodeChars[i] = charToAdd;
				}

				code = new string(newCodeChars.ToArray());

				codeAlreadyExists = await Repository.FindBySpecAsync(
						new InvitationFilterSpecification(
							new InvitationQueryParameters
								{InvitationCode = code}))
					.Any();
			}
			while (codeAlreadyExists);

			return code;
		}
	}
}
