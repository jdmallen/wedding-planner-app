using System;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Structs;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Models.Domain
{
	public class AppRole : IdentityRole<Guid>
	{
		private Guid _id;

		public AppRole()
		{
		}

		public AppRole(string roleName) : base(roleName)
		{
		}

		public override Guid Id
		{
			get => _id;
			set => _id = value;
		}

		[NotMapped]
		public MiniGuid ShortId
		{
			get => MiniGuid.Encode(_id);
			set => _id = MiniGuid.Decode(value);
		}
	}
}
