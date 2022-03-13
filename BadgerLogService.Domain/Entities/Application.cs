using System;
using System.Collections.Generic;
using BadgerLogService.Shared.Entities;

namespace BadgerLogService.Domain.Entities
{
  public class Application: BaseEntity
  {
    public string Code { get; set; }
    public string Name { get; set; }

    public List<Log> Logs { get; set; }
  }
}
