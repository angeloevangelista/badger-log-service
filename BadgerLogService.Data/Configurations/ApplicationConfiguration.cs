using System;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Shared.Util.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadgerLogService.Data.Configurations
{
  public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
  {
    public void Configure(EntityTypeBuilder<Application> builder)
    {
      builder.ToTable("applications");
      builder.HasKey(pre => pre.Id).HasName("pk_applications");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.Code)
        .HasColumnName("code")
        .HasColumnType("VARCHAR(10)")
        .IsRequired();

      builder.Property(pre => pre.Name)
        .HasColumnName("name")
        .HasColumnType("VARCHAR(60)")
        .IsRequired();

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();

      builder
        .HasMany(p => p.Logs)
        .WithOne(p => p.Application)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
