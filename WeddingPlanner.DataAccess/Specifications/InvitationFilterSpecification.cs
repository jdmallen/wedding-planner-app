using System;
using JDMallen.Toolbox.EFCore.Patterns.Specification.Implementations;
using JDMallen.Toolbox.Extensions;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;

namespace WeddingPlanner.DataAccess.Specifications
{
	public sealed class InvitationFilterSpecification
		: BaseSpecification<Invitation>
	{
		public
			InvitationFilterSpecification(
				InvitationQueryParameters queryParameters)
			: base(
				x => (!queryParameters.InviteeNameSearch.HasValue()
				      || x.EnvelopeAddressee.IndexOf(
					      queryParameters.InviteeNameSearch,
					      StringComparison.CurrentCultureIgnoreCase)
				      > 0
				      && (!queryParameters.InvitationType.HasValue
				          || x.InvitationType == queryParameters.InvitationType)
				      && (!queryParameters.InvitationCode.HasValue()
				          || x.InvitationCode == queryParameters.InvitationCode)
				      && (!queryParameters.IsMissingPlusOne.HasValue
				          || x.IsMissingPlusOne
				          == queryParameters.IsMissingPlusOne)
				      && (!queryParameters.IsInvitationEnvelopePrinted.HasValue
				          || x.IsInvitationEnvelopePrinted
				          == queryParameters.IsInvitationEnvelopePrinted)
				      && (!queryParameters.IsInvitationSent.HasValue
				          || x.IsInvitationSent
				          == queryParameters.IsInvitationSent)
				      && (!queryParameters.InviteeHasDietaryRestriction.HasValue
				          || x.InviteeHasDietaryRestriction
				          == queryParameters.InviteeHasDietaryRestriction)
					)
			)
		{
			AddInclude(x => x.Invitees);
			AddInclude(x => x.Address);

			if (queryParameters.Skip.HasValue && queryParameters.Take.HasValue)
			{
				ApplyPaging(
					queryParameters.Skip.Value,
					queryParameters.Take.Value);
			}
		}
	}
}
