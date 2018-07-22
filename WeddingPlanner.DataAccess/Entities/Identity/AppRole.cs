using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models.Entities.Identity
{
	public class AppRole : IdRole, IComplexEntityModel<Guid>
	{
		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }

		public void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppRole>(
				ar =>
				{
					ar.HasKey(r => r.Id);
					ar.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
					ar.ToTable("AspNetRoles");
					ar.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

					ar.Property(r => r.Name).HasMaxLength(191);
					ar.Property(r => r.NormalizedName).HasMaxLength(191);

					ar.HasMany<AppUserRole>()
						.WithOne()
						.HasForeignKey(ur => ur.RoleId)
						.IsRequired();
					ar.HasMany<AppRoleClaim>()
						.WithOne()
						.HasForeignKey(rc => rc.RoleId)
						.IsRequired();
				});
		}

		public string IdText => Id.ToString();
	}
}
