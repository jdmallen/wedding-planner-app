using System;
using FluentValidation;
using JDMallen.Toolbox.Extensions;
using WeddingPlanner.DataAccess.Config;
using WeddingPlanner.DataAccess.Constants;
using WeddingPlanner.DataAccess.Entities;

namespace WeddingPlanner.DataAccess.Validation
{
	public class InvitationValidator : AbstractValidator<Invitation>
	{
		public InvitationValidator(Settings settings = null)
		{
			RuleFor(invitation => invitation.AddressId).NotNull();
			RuleFor(invitation => invitation.EnvelopeAddressee).NotEmpty();
			RuleFor(invitation => invitation.InvitationCode)
				.Length(settings?.InvitationCodeLength ?? 5)
				.Unless(
					invitation
						=> invitation.InvitationCode.IsNullOrWhiteSpace());
			RuleFor(invitation => invitation.InvitationType).IsInEnum();
		}
	}

	public class InviteeValidator : AbstractValidator<Invitee>
	{
		public InviteeValidator()
		{
			RuleFor(invitee => invitee.FirstName).NotEmpty().MaximumLength(256);
			RuleFor(invitee => invitee.LastName).NotEmpty().MaximumLength(256);
			RuleFor(invitee => invitee.EmailAddress)
				.EmailAddress()
				.MaximumLength(256)
				.Unless(x => x.EmailAddress.IsNullOrWhiteSpace());
			RuleFor(invitee => invitee.DietaryRestrictions).IsInEnum();
			RuleFor(invitee => invitee.DietaryRestrictionOtherDescription)
				.NotEmpty()
				.When(
					invitee => invitee.DietaryRestrictions.HasFlag(
						DietaryRestrictions.Other));
			RuleFor(invitee => invitee.FamilySide).IsInEnum();
			RuleFor(invitee => invitee.MealChoiceId)
				.Must(
					((invitee, id)
						=> id.HasValue
						   && Enum.IsDefined(typeof(MealChoiceEnum), id)))
				.When(invitee => invitee.MealChoiceId.HasValue);
			RuleFor(invitee => invitee.RelationshipId)
				.Must(
					((invitee, id) => Enum.IsDefined(
						typeof(RelationshipType),
						id)));
			RuleFor(invitee => invitee.JessePriority)
				.InclusiveBetween<Invitee, byte>(1, 5)
				.Unless(invitee => !invitee.JessePriority.HasValue);
			RuleFor(invitee => invitee.KristenPriority)
				.InclusiveBetween<Invitee, byte>(1, 5)
				.Unless(invitee => !invitee.JessePriority.HasValue);
			

			// don't really need to validate anything for these two
			// RuleFor(invitee => invitee.Notes).NotNull();
			// RuleFor(invitee => invitee.InvitationId).NotNull();
		}
	}
}
