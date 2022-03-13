using System;
using System.Collections.Generic;
using System.Linq;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Shared.Entities;

namespace BadgerLogService.Domain.Entities
{
  public class Log : BaseEntity
  {
    public Guid ApplicationId { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string Data { get; set; }
    public string StackTrace { get; set; }
    public string Tags { get; set; }
    public LogLevel Level { get; set; }
    public Application Application { get; set; }

    public IReadOnlyCollection<string> TagList
    {
      get => this.Tags?.Split(',') ?? new string[] { };
      set => this.Tags = string.Join(
        ',',
        value
          .Where(p => !string.IsNullOrEmpty(p))
          .Select(p => p.Trim())
      );
    }
  }
}
