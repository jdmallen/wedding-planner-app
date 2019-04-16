using System;
using JDMallen.Toolbox.EFCore.Patterns.Specification.Implementations;
using JDMallen.Toolbox.Extensions;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;

namespace WeddingPlanner.DataAccess.Specifications
{
	public sealed class PhotoFilterSpecification : BaseSpecification<Photo>
	{
		public PhotoFilterSpecification(
			PhotoQueryParameters queryParameters)
			: base(
				x =>
					(!queryParameters.Caption.HasValue()
					 || x.Caption.IndexOf(
						 queryParameters.Caption,
						 StringComparison.CurrentCultureIgnoreCase)
					 != -1)
					&& (!queryParameters.FileName.HasValue()
					    || x.FileName.IndexOf(
						    queryParameters.FileName,
						    StringComparison.CurrentCultureIgnoreCase)
					    != -1)
					&& (!queryParameters.DateTaken.HasValue
					    || (x.DateTaken.HasValue
					        && x.DateTaken.Value.Day
					        == queryParameters.DateTaken.Value.Day
					        && x.DateTaken.Value.Month
					        == queryParameters.DateTaken.Value.Month
					        && x.DateTaken.Value.Year
					        == queryParameters.DateTaken.Value.Year)))
		{
		}
	}
}
