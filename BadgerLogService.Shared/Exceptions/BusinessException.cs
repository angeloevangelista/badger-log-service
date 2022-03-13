using System;
using System.Collections.Generic;
using System.Linq;

namespace BadgerLogService.Shared.Exceptions
{
  public class BusinessException : CustomException
  {
    public BusinessException(params string[] brokenRules) : base(brokenRules)
    {
    }
  }
}
