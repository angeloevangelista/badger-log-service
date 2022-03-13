using BadgerLogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadgerLogService.Data.Contexts
{
  public sealed class BadgerLogServiceContext : DbContext
  {
    public BadgerLogServiceContext(
      DbContextOptions<BadgerLogServiceContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .ApplyConfigurationsFromAssembly(
          typeof(BadgerLogServiceContext).Assembly
        );
    }

    public DbSet<Log> Logs { get; private set; }
    public DbSet<Application> Applications { get; private set; }
  }
}
