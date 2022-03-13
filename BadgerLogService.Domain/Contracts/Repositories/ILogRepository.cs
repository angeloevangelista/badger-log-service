using System.Collections.Generic;
using System.Threading.Tasks;
using BadgerLogService.Domain.Entities;

namespace BadgerLogService.Domain.Contracts.Repositories
{
  public interface ILogRepository
  {
    Task<Log> CreateLog(Log log);
    Task<Log> FindLogById(Log log);
    Task<List<Log>> ListLogsByApplicationId(Application application);
  }
}
