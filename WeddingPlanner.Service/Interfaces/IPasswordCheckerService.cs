using JDMallen.Toolbox.Microservices.Models;
using WeddingPlanner.Models.Dtos;

namespace WeddingPlanner.Service.Interfaces
{
	public interface IPasswordCheckerService: IService
	{
		PasswordResult CheckPassword(string password, float threshold = 50F);
	}
}