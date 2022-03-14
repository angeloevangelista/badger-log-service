using System.Threading.Tasks;
using BadgerLogService.Domain.Contracts.Services;
using BadgerLogService.Domain.ViewModels.LogViewModels;
using BadgerLogService.Shared.Contracts.Services;
using BadgerLogService.Shared.Controllers;
using BadgerLogService.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BadgerLogService.WebApi.Controllers
{
  [Route("/v1/logs")]
  public class LogController : CustomControllerBase
  {
    private readonly ILogService _logService;

    public LogController(
      IJwtService jwtService,
      ILogService logService
    ) : base(jwtService)
    {
      this._logService = logService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ShowLogViewModel>>> CreateLog(
      [FromBody] CreateLogViewModel createLogViewModel
    )
    {
      ValidateViewModel(createLogViewModel);

      var response = await this._logService.CreateLog(createLogViewModel);

      return Ok(response);
    }
  }
}
