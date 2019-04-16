using System;
using JDMallen.Toolbox.Implementations;

namespace WeddingPlanner.DataAccess.Entities
{
	public class Photo : EntityModel<Guid>
	{
		public string FileName { get; set; }

		public string Caption { get; set; }

		public bool IsCaptionHtml { get; set; }

		public DateTime? DateTaken { get; set; }

		public int? Order { get; set; }
	}
}
