using System;
using System.Threading.Tasks;
using BadgerLogService.Domain.Contracts.Repositories;
using BadgerLogService.Domain.Contracts.Services;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.ViewModels.ApplicationViewModels;
using BadgerLogService.Shared.Exceptions;

namespace BadgerLogService.Implementation.Services
{
  public class ApplicationService : IApplicationService
  {
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(
      IApplicationRepository applicationRepository
    )
    {
      this._applicationRepository = applicationRepository;
    }

    public async Task<ShowApplicationViewModel> CreateApplication(
      CreateApplicationViewModel createApplicationViewModel
    )
    {
      createApplicationViewModel.Code =
        createApplicationViewModel.Code.ToUpper();

      var codeAlreadyInUse = await this._applicationRepository
        .FindApplicationByCode(
          new Application()
          {
            Code = createApplicationViewModel.Code
          }
        );

      if (codeAlreadyInUse != null)
        throw new BusinessException("Code already in use.");

      var application = new Application()
      {
        Id = Guid.NewGuid(),
        Code = createApplicationViewModel.Code,
        Name = createApplicationViewModel.Name,
      };

      await this._applicationRepository.CreateApplication(application);

      return ShowApplicationViewModel.ConvertFromApplication(application);
    }
  }
}
