using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Implementations;

namespace WeddingPlanner.DataAccess.Entities
{
	public class InvitationType : MySqlEntityModel<Guid>
	{
		[Required, Column("TypeName", TypeName = "nvarchar(256)"),
		 StringLength(256)]
		public string DbTypeName { get; set; }

		[NotMapped]
		public InviteType TypeName
		{
			get => (InviteType) Enum.Parse(
				typeof(InviteType),
				DbTypeName,
				true);
			set => DbTypeName = value.ToString("G");
		}

		public virtual ICollection<Invitation> Invitations { get; set; }
	}

	public enum InviteType
	{
		Solo,
		PlusOne,
		Couple,
		Family
	}
}
