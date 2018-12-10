using System;
using JDMallen.Toolbox.Models;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Parameters
{
	public class InviteeQueryParameters : QueryParameters<Guid>
	{
		public string NameSearch { get; set; }

		public string Email { get; set; }

		public DietaryRestrictions? DietaryRestrictions { get; set; }

		public FamilySide? FamilySide { get; set; }

		public MealChoiceEnum? MealChoice { get; set; }

		public bool? IsPlusOne { get; set; }
	}
}
