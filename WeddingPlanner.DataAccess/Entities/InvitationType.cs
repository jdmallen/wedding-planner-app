using System;
using JDMallen.Toolbox.Infrastructure.EFCore.Models;
using JDMallen.Toolbox.Interfaces;

namespace WeddingPlanner.DataAccess.Entities
{
	public class InvitationType : MySqlEntityModel<Guid>
	{
		public string TypeName { get; set; }
	}
}
