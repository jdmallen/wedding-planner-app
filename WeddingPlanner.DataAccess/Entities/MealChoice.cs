using System.Collections.Generic;
using System.Collections.Immutable;
using JDMallen.Toolbox.Implementations;
using Newtonsoft.Json;

namespace WeddingPlanner.DataAccess.Entities
{
	public class MealChoice : EntityModel<int>
	{
		private HashSet<string> _alerts;

		private MealChoice()
		{
		}

		public MealChoice(
			int id,
			string displayName,
			string description = null,
			params string[] alerts)
		{
			Id = id;
			DisplayName = displayName;
			Description = description;
			_alerts = new HashSet<string>(alerts);
		}

		public string DisplayName { get; private set; }

		public string Description { get; private set; }

		public IReadOnlyList<string> Alerts => _alerts.ToImmutableList();

		[JsonIgnore]
		public string AlertsPipeDelimited
		{
			get => string.Join("|", _alerts);
			private set => _alerts = new HashSet<string>(value.Split('|'));
		}

		public void AddAlert(string alert)
		{
			if (!_alerts.Contains(alert))
			{
				_alerts.Add(alert);
			}
		}

		public void RemoveAlert(string alert)
		{
			if (_alerts.Contains(alert))
			{
				_alerts.Remove(alert);
			}
		}

		[JsonIgnore]
		public virtual ICollection<Invitee> Invitees { get; private set; }
	}
}
