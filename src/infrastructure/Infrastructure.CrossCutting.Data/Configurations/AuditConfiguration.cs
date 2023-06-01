using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.CrossCutting.Data.Configurations
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("audit");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.UserId).HasColumnType("INT");
            builder.Property(p => p.Date).HasColumnType("DATE").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.DateTime).HasColumnType("SMALLDATETIME").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd().IsRequired();

            builder.HasOne(p => p.User).WithMany(p => p.Audits).HasForeignKey(p => p.UserId);
        }
    }
}
