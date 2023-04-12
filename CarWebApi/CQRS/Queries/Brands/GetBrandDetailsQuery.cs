using CarWebApi.Models.Brands;

namespace CarWebApi.CQRS.Queries.Brands
{
    /// <summary>
    /// Получение марки авто
    /// </summary>
    public class GetBrandDetailsQuery : IRequest<BrandDetails>
    {
        /// <summary>
        /// ИД марки авто
        /// </summary>
        public Guid Id { get; set; }
    }
}
