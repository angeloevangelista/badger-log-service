using System.Threading.Tasks;
using BadgerLogService.Domain.Entities;

namespace BadgerLogService.Domain.Contracts.Repositories
{
  public interface IApplicationRepository
  {
    Task<Application> CreateApplication(Application application);
    Task<Application> FindApplicationById(Application application);
    Task<Application> FindApplicationByCode(Application application);
  }
}
