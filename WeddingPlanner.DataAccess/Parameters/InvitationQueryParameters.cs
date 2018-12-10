using System;
using JDMallen.Toolbox.Models;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Parameters
{
	public class InvitationQueryParameters : QueryParameters<Guid>
	{
		public string InviteeNameSearch { get; set; }

		public string InvitationCode { get; set; }

		public InvitationType? InvitationType { get; set; }

		public bool? IsInvitationEnvelopePrinted { get; set; }

		public bool? IsInvitationSent { get; set; }

		public bool? IsSaveTheDateEnvelopePrinted { get; set; }

		public bool? IsSaveTheDateSent { get; set; }

		public bool? IsMissingPlusOne { get; set; }

		public bool? InviteeHasDietaryRestriction { get; set; }
	}
}
