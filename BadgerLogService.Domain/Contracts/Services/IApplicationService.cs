using System;
using System.Threading.Tasks;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.ViewModels.ApplicationViewModels;

namespace BadgerLogService.Domain.Contracts.Services
{

  public interface IApplicationService
  {
    Task<ShowApplicationViewModel> CreateApplication(
      CreateApplicationViewModel createApplicationViewModel
    );
  }
}
