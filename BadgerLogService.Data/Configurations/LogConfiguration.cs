using System;
using BadgerLogService.Domain.Entities;
using BadgerLogService.Domain.Enums;
using BadgerLogService.Shared.Util.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadgerLogService.Data.Configurations
{
  public class LogConfiguration : IEntityTypeConfiguration<Log>
  {
    public void Configure(EntityTypeBuilder<Log> builder)
    {
      builder.Ignore(p => p.TagList);

      builder.ToTable("logs");
      builder.HasKey(pre => pre.Id).HasName("pk_logs");

      builder.Property(pre => pre.Id)
        .HasColumnName("id");

      builder.Property(pre => pre.Message)
        .HasColumnName("message")
        .HasColumnType("VARCHAR(100)")
        .IsRequired();

      builder.Property(pre => pre.Source)
        .HasColumnName("source")
        .HasColumnType("VARCHAR(100)")
        .IsRequired();

      builder.Property(pre => pre.Data)
        .HasColumnName("data")
        .HasColumnType("VARCHAR(5000)")
        .IsRequired();

      builder.Property(pre => pre.StackTrace)
        .HasColumnName("stack_trace")
        .HasColumnType("VARCHAR(5000)")
        .IsRequired();

      builder.Property(pre => pre.Tags)
        .HasColumnName("tags")
        .HasColumnType("VARCHAR(250)")
        .IsRequired();

      builder.Property(pre => pre.Level)
        .HasColumnName("level")
        .HasColumnType("NUMERIC")
        .HasDefaultValue(LogLevel.Unknown)
        .HasConversion(
          enumValue => ((char)enumValue).ToString(),
          numericValue => UtilEnum.Parse<LogLevel>(numericValue)
        )
        .IsRequired();

      builder.Property(pre => pre.CreatedAt)
        .HasColumnName("created_at")
        .IsRequired();

      builder.Property(pre => pre.UpdatedAt)
        .HasColumnName("updated_at")
        .IsRequired();

      builder
        .HasOne(p => p.Application)
        .WithMany(p => p.Logs)
        .HasForeignKey(p => p.ApplicationId)
        .HasConstraintName("fk_logs_applications");
    }
  }
}
