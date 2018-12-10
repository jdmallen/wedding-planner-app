using System;
using JDMallen.Toolbox.EFCore.Patterns.Specification.Implementations;
using JDMallen.Toolbox.Extensions;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;

namespace WeddingPlanner.DataAccess.Specifications
{
	public sealed class InviteeFilterSpecification : BaseSpecification<Invitee>
	{
		public InviteeFilterSpecification(
			InviteeQueryParameters queryParameters)
			: base(
				x =>
					(!queryParameters.NameSearch.HasValue()
					 || x.LastName.IndexOf(
						 queryParameters.NameSearch,
						 StringComparison.CurrentCultureIgnoreCase)
					 > 0
					 || x.FirstName.IndexOf(
						 queryParameters.NameSearch,
						 StringComparison.CurrentCultureIgnoreCase)
					 > 0)
					&& (!queryParameters.Email.HasValue()
					    || string.Equals(
						    x.EmailAddress,
						    queryParameters.Email,
						    StringComparison.CurrentCultureIgnoreCase))
					&& (!queryParameters.DietaryRestrictions.HasValue
					    || x.DietaryRestrictions.HasFlag(
						    queryParameters.DietaryRestrictions))
					&& (!queryParameters.FamilySide.HasValue
					    || x.FamilySide == queryParameters.FamilySide)
					&& (!queryParameters.MealChoice.HasValue
					    || x.MealChoiceId == (int) queryParameters.MealChoice)
					&& (!queryParameters.IsPlusOne.HasValue
					    || x.IsPlusOne == queryParameters.IsPlusOne))
		{
			AddInclude(x => x.Relationship);
			AddInclude(x => x.MealChoice);
		}
	}
}
