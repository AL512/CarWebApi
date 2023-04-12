namespace CarWebApi.Models.Countries
{
    /// <summary>
    /// Список странп производителей
    /// </summary>
    public class CountryList
    {
        /// <summary>
        /// Список странп производителей
        /// </summary>
        public IList<CountryLookupDto> Countries { get; set; } = new List<CountryLookupDto>();
    }
}
