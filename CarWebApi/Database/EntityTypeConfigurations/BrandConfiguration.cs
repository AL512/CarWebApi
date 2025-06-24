using CarWebApi.Models.Brands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWebApi.Database.EntityTypeConfigurations;

/// <summary>
/// Конфигурация сущности Brand
/// </summary>
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    /// <summary>
    /// Задает параметры конфигурации для Country
    /// </summary>
    /// <param name="builder">Билдер Brand в EntityFramework</param>
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(brand => brand.Id);
        builder.HasIndex(brand => brand.Id).IsUnique();
        builder.Property(brand => brand.Name).HasMaxLength(250).IsRequired();
    }
}