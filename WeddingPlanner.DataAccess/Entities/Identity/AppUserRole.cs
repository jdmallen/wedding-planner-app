using System;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Structs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models.Entities.Identity
{
	public class AppUserRole : IdentityUserRole<Guid>, IComplexEntityModel<Guid>
	{
		public Guid Id { get; set; }
		
		public string IdText => Id.ToString();
		
		[NotMapped]
		public MiniGuid ShortId
		{
			get => MiniGuid.Encode(Id);
			set => Id = MiniGuid.Decode(value);
		}

		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }

		public void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUserRole>(
				aur =>
				{
					aur.HasKey(
						ur => new
						{
							ur.UserId,
							ur.RoleId
						});
					aur.ToTable("AspNetUserRoles");
				});
		}
	}
}
