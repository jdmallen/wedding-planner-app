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
			throw new NotImplementedException();
		}
	}
}
