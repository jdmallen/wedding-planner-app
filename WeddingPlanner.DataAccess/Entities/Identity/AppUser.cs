using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models.Entities.Identity
{
	public class AppUser : IdUser, IComplexEntityModel<Guid>
	{
		public string DisplayName { get; set; }

		public string InvitationCode { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }

		public void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUser>(
				au =>
				{
					au.HasMany<AppUserRole>()
					.WithOne()
					.HasForeignKey(ur => ur.UserId)
					.IsRequired();
				});
		}
	}
}
