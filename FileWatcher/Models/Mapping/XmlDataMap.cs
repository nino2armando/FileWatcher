using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FileWatcher.Models.Mapping
{
    public class XmlDataMap : EntityTypeConfiguration<XmlData>
    {
        public XmlDataMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Heading)
                .HasMaxLength(255);

            this.Property(t => t.To)
                .HasMaxLength(50);

            this.Property(t => t.From)
                .HasMaxLength(50);

            this.Property(t => t.Body)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("XmlData");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AppId).HasColumnName("AppId");
            this.Property(t => t.Heading).HasColumnName("Heading");
            this.Property(t => t.To).HasColumnName("To");
            this.Property(t => t.From).HasColumnName("From");
            this.Property(t => t.Body).HasColumnName("Body");
        }
    }
}
