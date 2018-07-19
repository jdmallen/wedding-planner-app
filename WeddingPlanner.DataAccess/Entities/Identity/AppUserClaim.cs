using System;
using JDMallen.Toolbox.Interfaces;
using JDMallen.Toolbox.Structs;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Models.Entities.Identity
{
	public class AppUserClaim : IdentityUserClaim<Guid>, IEntityModel<Guid>
	{
		public new Guid Id { get; set; }

		public string IdText => Id.ToString();

		public MiniGuid ShortId { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }
	}
}
