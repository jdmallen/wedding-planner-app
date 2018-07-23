using System;
using Microsoft.EntityFrameworkCore;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using System.Collections.Generic;

namespace WeddingPlanner.DataAccess.Entities 
{
	public class Address : MySqlComplexEntityModel<Guid> 
	{
		public string StreetLine1 { get; set; }

		public string StreetLine2 { get; set; }

		public string StreetLine3 { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public byte Zip { get; set; }

		public byte Zip4 { get; set; }

		public virtual IEnumerable<Invitation> Invitations { get; set; }

		public virtual IEnumerable<Invitee> Invitees { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Address>(a => {
			});
		}

	}
}
