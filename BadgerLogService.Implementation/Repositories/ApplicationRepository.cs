using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerLogService.Data.Contexts;
using BadgerLogService.Domain.Contracts.Repositories;
using BadgerLogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadgerLogService.Implementation.Repositories
{
  public class ApplicationRepository : IApplicationRepository
  {
    private readonly BadgerLogServiceDataContext _badgerLogServiceDataContext;

    public ApplicationRepository(
      BadgerLogServiceDataContext badgerLogServiceDataContext
    )
    {
      this._badgerLogServiceDataContext = badgerLogServiceDataContext;
    }

    public async Task<Application> CreateApplication(Application application)
    {
      this._badgerLogServiceDataContext.Applications.Add(application);

      await this._badgerLogServiceDataContext.SaveChangesAsync();

      return application;
    }

    public async Task<Application> FindApplicationByCode(Application application)
    {
      application = await this._badgerLogServiceDataContext.Applications
        .AsNoTracking()
        .FirstOrDefaultAsync(p =>
          p.Code.ToUpper() == application.Code.ToUpper()
        );

      return application;
    }

    public async Task<Application> FindApplicationById(Application application)
    {
      application = await this._badgerLogServiceDataContext.Applications
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id.Equals(application.Id));

      return application;
    }
  }
}
