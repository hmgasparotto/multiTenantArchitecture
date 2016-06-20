using Domain.Models.Things;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.IoT.Mapping
{
    public class PublicIlluminationControllerConfiguration : EntityTypeConfiguration<PublicIlluminationController>
    {
        public PublicIlluminationControllerConfiguration(string schema)
        {
            ToTable("PublicIlluminationController", schema);

            HasKey(x => x.Id);

            Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Latitude)
                .IsRequired()
                .HasPrecision(10, 6);

            Property(x => x.Longitude)
                .IsRequired()
                .HasPrecision(10, 6);
        }
    }
}
