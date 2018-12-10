using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Interfaces;
using JDMallen.Toolbox.Structs;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.DataAccess.Entities.Identity
{
	public class AppUserLogin : IdentityUserLogin<Guid>, IEntityModel<Guid>
	{
		[Key]
		public Guid Id { get; set; }

		public string IdText => Id.ToString();

		[NotMapped]
		public MiniGuid ShortId
		{
			get => MiniGuid.Encode(Id);
			set => Id = MiniGuid.Decode(value);
		}

		public DateTime DateCreated { get; set; }

		public DateTime DateModified { get; set; }

		public bool IsDeleted { get; set; }
	}
}
