using CarWebApi.Models.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWebApi.Database.EntityTypeConfigurations;

/// <summary>
/// Конфигурация сущности Country
/// </summary>
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    /// <summary>
    /// Задает параметры конфигурации для Country
    /// </summary>
    /// <param name="builder">Билдер Country в EntityFramework</param>
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(country => country.Id);
        builder.HasIndex(country => country.Id).IsUnique();
        builder.Property(country => country.Name).HasMaxLength(250).IsRequired();
    }
}