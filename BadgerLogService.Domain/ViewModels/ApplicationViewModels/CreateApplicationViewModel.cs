using System.Collections.Generic;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Shared.Util.DataTypes;
using BadgerLogService.Shared.ViewModels;

namespace BadgerLogService.Domain.ViewModels.ApplicationViewModels
{
  public class CreateApplicationViewModel : BaseViewModel
  {
    public string Code { get; set; }
    public string Name { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.Code?.Trim()))
        this._brokenRules.Add("Code is required.");

      if (string.IsNullOrEmpty(this.Name?.Trim()))
        this._brokenRules.Add("Name is required.");

      return this;
    }
  }
}
