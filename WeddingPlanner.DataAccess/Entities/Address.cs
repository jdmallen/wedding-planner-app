using System;
using System.Collections.Generic;
using JDMallen.Toolbox.Implementations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeddingPlanner.DataAccess.Constants;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Address : EntityModel<Guid>
	{
		public Address()
		{
		}

		public Address(
			string streetLine1,
			string city,
			USState state,
			string zip10,
			string streetLine2 = null,
			string streetLine3 = null)
		{
			Id = Guid.NewGuid();
			StreetLine1 = streetLine1;
			StreetLine2 = streetLine2;
			StreetLine3 = streetLine3;
			City = city;
			State = state;
			Zip = zip10;
		}

		public string StreetLine1 { get; set; }

		public string StreetLine2 { get; set; }

		public string StreetLine3 { get; set; }

		public string City { get; set; }

		[JsonIgnore]
		public string DbState { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public USState State
		{
			get => (USState) Enum.Parse(typeof(USState), DbState, true);
			set => DbState = value.ToString("G");
		}

		public string Zip { get; set; }

		[JsonIgnore]
		public virtual ICollection<Invitation> Invitations { get; set; }
	}
}
