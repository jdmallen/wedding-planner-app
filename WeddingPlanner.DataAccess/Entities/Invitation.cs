using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitation : MySqlComplexEntityModel<Guid>
	{
		public Guid InvitationTypeId { get; set; }

		public virtual InvitationType InvitationType { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invitation>(i => {
				i.HasKey(x => x.Id);
				i.HasOne(x => x.InvitationType).WithMany().HasForeignKey(x => x.InvitationTypeId);
			});
		}
	}
}
