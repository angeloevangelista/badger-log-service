using BadgerLogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadgerLogService.Data.Contexts
{
  public sealed class BadgerLogServiceDataContext : DbContext
  {
    public BadgerLogServiceDataContext(
      DbContextOptions<BadgerLogServiceDataContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .ApplyConfigurationsFromAssembly(
          typeof(BadgerLogServiceDataContext).Assembly
        );
    }

    public DbSet<Log> Logs { get; private set; }
    public DbSet<Application> Applications { get; private set; }
  }
}
