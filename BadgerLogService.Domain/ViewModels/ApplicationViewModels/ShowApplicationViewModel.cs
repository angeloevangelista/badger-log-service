using System.Collections.Generic;
using System.Linq;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Shared.ViewModels;

namespace BadgerLogService.Domain.ViewModels.ApplicationViewModels
{
  public class ShowApplicationViewModel : BaseViewModel
  {
    public string Code { get; set; }
    public string Name { get; set; }

    public static ShowApplicationViewModel ConvertFromApplication(Application application) =>
      new ShowApplicationViewModel()
      {
        Code = application.Code,
        Name = application.Name,
      };
  }
}
