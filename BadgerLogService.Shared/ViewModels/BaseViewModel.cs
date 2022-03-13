using System.Collections.Generic;
using System.Linq;
using BadgerLogService.Shared.Exceptions;

namespace BadgerLogService.Shared.ViewModels
{
  public abstract class BaseViewModel
  {
    protected IList<string> _brokenRules;

    public bool IsValid
    {
      get
      {
        this.DoValidation();

        return !this._brokenRules.Any();
      }
    }

    public bool IsInvalid { get => !this.IsValid; }

    public BaseViewModel()
    {
      this._brokenRules = new List<string>();
    }

    public virtual BaseViewModel DoValidation()
    {
      this._brokenRules = new List<string>();

      return this;
    }

    public IList<string> GetBrokenRules() => this._brokenRules.ToArray();

    public void ThrowBusinessExceptionIfNotValid()
    {
      if (this.IsInvalid)
        throw new BusinessException(this.GetBrokenRules().ToArray());
    }
  }
}
