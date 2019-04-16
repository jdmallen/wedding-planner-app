using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JDMallen.Toolbox.EFCore.Patterns.Repository.Interfaces;
using JDMallen.Toolbox.EFCore.Services.Interfaces;
using JDMallen.Toolbox.Interfaces;
using WeddingPlanner.DataAccess.Entities;
using WeddingPlanner.DataAccess.Parameters;

namespace WeddingPlanner.Services.Interfaces
{
	public interface IPhotoService
		: IWriteService<IRepository<Photo, Guid>>,
		  IReadService<IRepository<Photo, Guid>>
	{
		IAsyncEnumerable<Photo> ListAllPhotos();

		Task<Photo> Read(Guid id);

		IAsyncEnumerable<Photo> Find(PhotoQueryParameters parameters);

		Task<IPagedResult<Photo>> FindPaged(
			PhotoQueryParameters parameters);

		Task<Photo> Upsert(Photo model);

		Task<Photo> Update(Photo model);

		Task<Photo> Delete(Photo model);
	}
}
