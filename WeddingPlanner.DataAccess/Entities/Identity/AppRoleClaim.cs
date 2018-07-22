using System;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Structs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models.Entities.Identity
{
	public class AppRoleClaim : IdentityRoleClaim<Guid>, IComplexEntityModel<Guid>
	{
		public new Guid Id { get; set; }

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
			modelBuilder.Entity<AppRoleClaim>(
				arc =>
				{
					arc.HasKey(rc => rc.Id);
					arc.ToTable("AspNetRoleClaims");
				});
		}
	}
}
