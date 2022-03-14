using System;
using BadgerLogService.Shared.Enums;

namespace BadgerLogService.Shared.Entities
{
  public class BaseEntity
  {
    public BaseEntity()
    {
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
