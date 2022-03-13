using System;
using System.Linq;
using System.Threading.Tasks;
using BadgerLogService.Domain.Contracts.Repositories;
using BadgerLogService.Domain.Contracts.Services;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Domain.ViewModels.LogViewModels;
using BadgerLogService.Shared.Exceptions;
using BadgerLogService.Shared.Util.DataTypes;

namespace BadgerLogService.Implementation.Services
{
  public class LogService : ILogService
  {
    private readonly ILogRepository _logRepository;
    private readonly IApplicationRepository _applicationRepository;

    public LogService(
      ILogRepository logRepository,
      IApplicationRepository applicationRepository
    )
    {
      this._logRepository = logRepository;
      this._applicationRepository = applicationRepository;
    }

    public async Task<string> CreateLog(CreateLogViewModel createLogViewModel)
    {
      var application = await this._applicationRepository.FindApplicationByCode(
        new Application()
        {
          Code = createLogViewModel.ApplicationCode
        }
      );

      if (application == null)
        throw new BusinessException(
          "Application not found, be sure your ApplicationCode is correct."
        );

      var log = new Log()
      {
        Id = Guid.NewGuid(),
        Application = application,
        ApplicationId = application.Id,
        Level = UtilEnum.Parse<LogLevel>(createLogViewModel.LogLevel),
        Message = createLogViewModel.Message,
        Source = createLogViewModel.Source,
        TagList = createLogViewModel.Tags
      };

      if (log.Level == LogLevel.Error)
        log.StackTrace = createLogViewModel.Data;
      else
        log.Data = createLogViewModel.Data;

      await this._logRepository.CreateLog(log);

      return log.Id.ToString();
    }

    public async Task<ShowLogViewModel> FindLog(string logId)
    {
      var log = new Log()
      {
        Id = Guid.Parse(logId)
      };

      log = await this._logRepository.FindLogById(log);

      if (log == null)
        throw new BusinessException("Log not found.");

      log.Application = await this._applicationRepository.FindApplicationById(
        new Application()
        {
          Id = log.ApplicationId
        }
      );

      return ShowLogViewModel.ConvertFromLog(log);
    }
  }
}
