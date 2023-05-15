using lab.component.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab.component.EfCore;

public partial class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> entity)
    {
        entity.HasKey(e => e.Date);

        entity.ToTable("WeatherForecast");

        entity.Property(e => e.Summary)
              .HasMaxLength(50)
              .IsUnicode(true);

        entity.Property(e => e.TemperatureC);

        this.OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<WeatherForecast> entity);
}