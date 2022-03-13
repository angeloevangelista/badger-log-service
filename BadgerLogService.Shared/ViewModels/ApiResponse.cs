using System;
using System.Collections.Generic;

namespace BadgerLogService.Shared.ViewModels
{
  public class ApiResponse<T>
  {
    public ApiResponse()
    {
      this.Success = true;
      this.Errors = new List<string>();
    }

    public ApiResponse(T data) : this()
    {
      this.Data = data;
    }

    public bool Success { get; set; }
    public T Data { get; set; }
    public IList<string> Errors { get; set; }
  }
}
