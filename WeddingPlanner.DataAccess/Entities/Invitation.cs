using System;
using System.Collections.Generic;
using System.Linq;
using JDMallen.Toolbox.Implementations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitation : EntityModel<Guid>
	{
		public Invitation()
		{
		}

		public Invitation(
			string envelopeAddressee,
			InvitationType invitationType,
			Guid addressId,
			string invitationCode,
			bool isInvitationEnvelopePrinted = false,
			bool isInvitationSent = false)
		{
			Id = Guid.NewGuid();
			EnvelopeAddressee = envelopeAddressee;
			InvitationType = invitationType;
			AddressId = addressId;
			InvitationCode = invitationCode;
			IsInvitationEnvelopePrinted = isInvitationEnvelopePrinted;
			IsInvitationSent = isInvitationSent;
		}

		#region Public Properties

		[JsonIgnore]
		public string DbInvitationType { get; set; }

		public string EnvelopeAddressee { get; set; }

		public string InvitationCode { get; set; }

		public byte? InviteeCount => (byte?) Invitees?.Count;

		public bool? InviteeHasDietaryRestriction
			=> Invitees?.Any(
				x => x.DietaryRestrictions != DietaryRestrictions.None);

		[JsonConverter(typeof(StringEnumConverter))]
		public InvitationType InvitationType
		{
			get => (InvitationType) Enum.Parse(
				typeof(InvitationType),
				DbInvitationType,
				true);
			set => DbInvitationType = value.ToString("G");
		}

		public bool IsInvitationEnvelopePrinted { get; set; }

		public bool IsInvitationSent { get; set; }

		public bool? IsMissingPlusOne
			=> InviteeCount == null
				? (bool?) null
				: InviteeCount == 1 && InvitationType == InvitationType.PlusOne;

		public Guid AddressId { get; set; }

		#endregion

		#region Entity Relationships

		public virtual Address Address { get; set; }

		public virtual ICollection<Invitee> Invitees { get; set; }

		#endregion
	}
}
