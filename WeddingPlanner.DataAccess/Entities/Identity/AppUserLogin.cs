using System;
using JDMallen.Toolbox.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Models.Entities.Identity
{
    public class AppUserLogin : IdentityUserLogin<Guid>, IEntityModel<Guid>
    {
		public Guid Id { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }
	}
}
