using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerLogService.Data.Contexts;
using BadgerLogService.Domain.Contracts.Repositories;
using BadgerLogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadgerLogService.Implementation.Repositories
{
  public class LogRepository : ILogRepository
  {
    private readonly BadgerLogServiceDataContext _badgerLogServiceDataContext;

    public LogRepository(
      BadgerLogServiceDataContext badgerLogServiceDataContext
    )
    {
      this._badgerLogServiceDataContext = badgerLogServiceDataContext;
    }

    public async Task<Log> CreateLog(Log log)
    {
      this._badgerLogServiceDataContext.Logs.Add(log);

      await this._badgerLogServiceDataContext.SaveChangesAsync();

      return log;
    }

    public async Task<Log> FindLogById(Log log)
    {
      log = await this._badgerLogServiceDataContext.Logs
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id.Equals(log.Id));

      return log;
    }

    public List<Log> ListLogsByApplicationId(Application application)
    {
      var logs = this._badgerLogServiceDataContext.Logs
        .AsNoTracking()
        .Where(p => p.ApplicationId.Equals(application.Id))
        .ToList();

      return logs;
    }
  }
}
