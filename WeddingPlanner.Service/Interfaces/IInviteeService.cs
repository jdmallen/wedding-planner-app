using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JDMallen.Toolbox.EFCore.Patterns.Repository.Interfaces;
using JDMallen.Toolbox.EFCore.Services.Interfaces;
using JDMallen.Toolbox.Interfaces;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;

namespace WeddingPlanner.Services.Interfaces
{
	public interface IInviteeService
		: IWriteService<IRepository<Invitee, Guid>>,
		  IReadService<IRepository<Invitee, Guid>>
	{
		IAsyncEnumerable<Invitee> ListAllInvitees();

		IAsyncEnumerable<MealChoice> ListMealChoices();

		IAsyncEnumerable<Relationship> GetAllRelationshipTypeObjects();

		Task<List<string>> GetAllRelationshipTypes();

		Task<Invitee> Read(Guid id);

		IAsyncEnumerable<Invitee> Find(InviteeQueryParameters parameters);

		Task<IPagedResult<Invitee>> FindPaged(
			InviteeQueryParameters parameters);

		Task<Invitee> Upsert(Invitee model);

		Task<Invitee> Update(Invitee model);

		Task<Invitee> Delete(Invitee model);
	}
}
