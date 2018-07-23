﻿using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitation : MySqlComplexEntityModel<Guid>
	{
		public string EnvelopeAddressee { get; set; }

		public Guid InvitationTypeId { get; set; }

		public Guid AddressId { get; set; }

		public virtual Address Address { get; set; }

		public virtual InvitationType InvitationType { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invitation>(i => {
				i.HasOne(x => x.InvitationType)
					.WithMany()
					.HasForeignKey(x => x.InvitationTypeId);
				i.HasOne(x => x.Address)
					.WithMany()
					.HasForeignKey(x => x.AddressId);
			});
		}
	}
}
