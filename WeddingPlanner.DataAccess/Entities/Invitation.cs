using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitation : MySqlComplexEntityModel<Guid>
	{
		[Required, Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string EnvelopeAddressee { get; set; }

		public Guid InvitationTypeId { get; set; }

		public Guid AddressId { get; set; }

		public bool IsInvitationSent { get; set; }

		public bool IsSaveTheDateSent { get; set; }

		public virtual Address Address { get; set; }

		public virtual InvitationType InvitationType { get; set; }

		public virtual ICollection<Invitee> Invitees { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invitation>(
				i =>
				{
					i.HasOne(x => x.InvitationType)
						.WithMany(x => x.Invitations)
						.HasForeignKey(x => x.InvitationTypeId);
					i.HasOne(x => x.Address)
						.WithMany(x => x.Invitations)
						.HasForeignKey(x => x.AddressId);
					i.HasMany(x => x.Invitees)
						.WithOne(x => x.Invitation)
						.HasForeignKey(x => x.InvitationId);
				});
		}
	}
}
