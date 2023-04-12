using FluentValidation;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Валидация данных команды удаления марки авто
    /// </summary>
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        /// <summary>
        /// Валидация данных команды удаления марки авто
        /// </summary>
        public DeleteBrandCommandValidator()
        {
            RuleFor(deleteBrandCommand =>
                deleteBrandCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
