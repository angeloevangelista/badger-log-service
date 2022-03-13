using System;
using System.Collections.Generic;
using System.Linq;

namespace BadgerLogService.Shared.Exceptions
{
  public abstract class CustomException : Exception
  {
    private readonly IList<string> _brokenRules;

    public IReadOnlyCollection<string> BrokenRules
    {
      get => this._brokenRules.ToArray();
    }

    public CustomException()
    {
      this._brokenRules = new List<string>();
    }

    public CustomException(params string[] brokenRules) : this()
    {
      foreach (var brokenRule in brokenRules)
        this.AddBrokenRule(brokenRule);
    }

    public CustomException AddBrokenRule(string brokenRule)
    {
      this._brokenRules.Add(brokenRule);

      return this;
    }
  }
}
