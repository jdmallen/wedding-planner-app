using System;
using JDMallen.Toolbox.Implementations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitee : EntityModel<Guid>
	{
		public Invitee()
		{
		}

		public Invitee(
			string firstName,
			string lastName,
			bool isPlusOne,
			DietaryRestrictions dietaryRestrictions,
			FamilySide familySide,
			RelationshipType relationshipType,
			Guid? invitationId = null,
			int? mealChoiceId = null,
			string emailAddress = null,
			string notes = null,
			byte? jessePriority = null,
			byte? kristenPriority = null,
			string dietaryRestrictionOtherDescription = null)
		{
			Id = Guid.NewGuid();
			FirstName = firstName;
			LastName = lastName;
			IsPlusOne = isPlusOne;
			DietaryRestrictions = dietaryRestrictions;
			FamilySide = familySide;
			RelationshipId = (int) relationshipType;
			InvitationId = invitationId;
			MealChoiceId = mealChoiceId;
			EmailAddress = emailAddress;
			Notes = notes;
			JessePriority = jessePriority;
			KristenPriority = kristenPriority;
			DietaryRestrictionOtherDescription =
				dietaryRestrictionOtherDescription;
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool IsPlusOne { get; set; }

		public string EmailAddress { get; set; }

		public string Notes { get; set; }

		public DietaryRestrictions DietaryRestrictions { get; set; }

		public bool DietaryRestrictionOtherRequired
			=> DietaryRestrictions.HasFlag(DietaryRestrictions.Other);

		public string DietaryRestrictionOtherDescription { get; set; }

		[JsonIgnore]
		public string DbFamilySide { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public FamilySide FamilySide
		{
			get => (FamilySide) Enum.Parse(
				typeof(FamilySide),
				DbFamilySide,
				true);
			set => DbFamilySide = value.ToString("G");
		}

		public Guid? InvitationId { get; set; }

		public int? MealChoiceId { get; set; }

		[JsonIgnore]
		public byte? JessePriority { get; set; }

		[JsonIgnore]
		public byte? KristenPriority { get; set; }

		public double? WeightedPriority
			=> Relationship == null
			   || JessePriority == null && KristenPriority == null
				? null
				: (JessePriority
				   + KristenPriority)
				  / 2
				  * (Relationship.PriorityWeightOutOf100 / 100);

		public int RelationshipId { get; set; }

		[JsonIgnore]
		public virtual Invitation Invitation { get; set; }

		public virtual Relationship Relationship { get; set; }

		public virtual MealChoice MealChoice { get; set; }
	}
}
