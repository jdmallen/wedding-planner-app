using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JDMallen.Toolbox.Implementations;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Address : MySqlEntityModel<Guid>
	{
		[Required, Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string StreetLine1 { get; set; }

		[Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string StreetLine2 { get; set; }

		[Column(TypeName = "nvarchar(256)"), StringLength(256)]
		public string StreetLine3 { get; set; }

		[Required, Column(TypeName = "nvarchar(128)"), StringLength(128)]
		public string City { get; set; }

		[Required, Column("State", TypeName = "varchar(2)"), StringLength(2)]
		public string DbState { get; set; }

		[NotMapped]
		public USState State
		{
			get => (USState) Enum.Parse(typeof(USState), DbState, true);
			set => DbState = value.ToString("G");
		}
		
		[Required, Column(TypeName = "mediumint")]
		public int Zip { get; set; }
		
		[Column(TypeName = "smallint")]
		public short? Zip4 { get; set; }

		public virtual IEnumerable<Invitation> Invitations { get; set; }
	}
}
