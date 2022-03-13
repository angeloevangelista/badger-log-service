using System.Collections.Generic;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Shared.Util.DataTypes;
using BadgerLogService.Shared.ViewModels;

namespace BadgerLogService.Domain.ViewModels.LogViewModels
{
  public class CreateLogViewModel : BaseViewModel
  {
    public string ApplicationCode { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string Data { get; set; }
    public List<string> Tags { get; set; }
    public int LogLevel { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.ApplicationCode?.Trim()))
        this._brokenRules.Add("ApplicationCode is required.");

      if (string.IsNullOrEmpty(this.Message?.Trim()))
        this._brokenRules.Add("Message is required.");

      if (string.IsNullOrEmpty(this.Source?.Trim()))
        this._brokenRules.Add("Source is required.");

      if (
        string.IsNullOrEmpty(this.Data?.Trim())
      )
      {
        this._brokenRules.Add("Data is required.");
      }

      if (!UtilEnum.TryParse<LogLevel>(
        this.LogLevel,
        out var logLevel
      ))
      {
        this._brokenRules.Add("Level is invalid.");
      }

      return this;
    }
  }
}
