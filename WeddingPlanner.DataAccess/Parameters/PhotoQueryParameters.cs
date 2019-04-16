using System;
using JDMallen.Toolbox.Models;

namespace WeddingPlanner.DataAccess.Parameters
{
	public class PhotoQueryParameters : QueryParameters<Guid>
	{
		public string FileName { get; set; }

		public string Caption { get; set; }

		public DateTime? DateTaken { get; set; }
	}
}
