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
	public interface IInvitationService
		: IWriteService<IRepository<Invitation, Guid>>,
		  IReadService<IRepository<Invitation, Guid>>
	{
		IEnumerable<string> GetAllInvitationTypes();

		IAsyncEnumerable<Invitation> ListAllInvitations();

		Task<Invitation> Read(Guid id);

		IAsyncEnumerable<Invitation> Find(InvitationQueryParameters parameters);

		Task<IPagedResult<Invitation>> FindPaged(
			InvitationQueryParameters parameters);

		Task<Invitation> Upsert(Invitation model);

		Task<Invitation> Update(Invitation model);

		Task<Invitation> Delete(Invitation model);

		Task<string> GenerateInvitationCode();
	}
}
