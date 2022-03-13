using System.Collections.Generic;
using System.Linq;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Domain.ViewModels.ApplicationViewModels;
using BadgerLogService.Shared.Util.DataTypes;
using BadgerLogService.Shared.ViewModels;

namespace BadgerLogService.Domain.ViewModels.LogViewModels
{
  public class ShowLogViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string Data { get; set; }
    public string StackTrace { get; set; }
    protected List<string> Tags { get; set; }
    public int LogLevel { get; set; }
    public ShowApplicationViewModel Application { get; set; }

    public static ShowLogViewModel ConvertFromLog(Log log) =>
      new ShowLogViewModel()
      {
        Id = log.Id.ToString(),
        Data = log.Data,
        StackTrace = log.StackTrace,
        Message = log.Message,
        Source = log.Source,
        Tags = log.TagList.ToList(),
        LogLevel = ((int)log.Level),
        Application = ShowApplicationViewModel.ConvertFromApplication(
          log.Application
        )
      };
  }
}
