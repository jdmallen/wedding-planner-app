using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitee : MySqlComplexEntityModel<Guid>
	{
		public Guid InvitationId { get; set; }

		public Guid AddressId { get; set; }

		public virtual Invitation Invitation { get; set; }

		public virtual Address Address { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invitee>(i => {
				i.HasOne(x => x.Invitation)
					.WithMany()
					.HasForeignKey(x => x.InvitationId);
				i.HasOne(x => x.Address)
					.WithMany()
					.HasForeignKey(x => x.AddressId);
			});
		}
	}
}
