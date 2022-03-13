using System;
using System.Threading.Tasks;
using BadgerLogService.Domain.Contracts.Repositories;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.ViewModels.LogViewModels;

namespace BadgerLogService.Domain.Contracts.Services
{

  public interface ILogService
  {
    Task<ShowLogViewModel> FindLog(string logId);
    Task<string> CreateLog(CreateLogViewModel createLogViewModel);
  }
}
