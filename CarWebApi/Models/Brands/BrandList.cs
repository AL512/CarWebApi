namespace CarWebApi.Models.Brands
{
    /// <summary>
    /// Список марок автомобилей
    /// </summary>
    public class BrandList
    {
        /// <summary>
        /// Список марок автомодилей
        /// </summary>
        public ICollection<BrandLookupDto> Brands { get; set; }
    }
}
