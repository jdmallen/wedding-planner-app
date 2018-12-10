using System.Collections.Generic;
using JDMallen.Toolbox.Implementations;
using Newtonsoft.Json;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Relationship : EntityModel<int>
	{
		private Relationship()
		{
		}

		public Relationship(int id, string name, byte priority)
		{
			Id = id;
			RelationshipName = name;
			PriorityWeightOutOf100 = priority;
		}

		public string RelationshipName { get; set; }

		public byte PriorityWeightOutOf100 { get; set; }

		[JsonIgnore]
		public virtual ICollection<Invitee> Invitees { get; set; }
	}
}
