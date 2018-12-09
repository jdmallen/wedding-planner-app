using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Implementations;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Invitee : MySqlEntityModel<Guid>
	{
		[Required, Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string FirstName { get; set; }
		
		[Required, Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string LastName { get; set; }

		public bool IsPlusOne { get; set; }
		
		[Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string EmailAddress { get; set; }

		public Guid InvitationId { get; set; }

		public virtual Invitation Invitation { get; set; }
	}
}
