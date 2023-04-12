using FluentValidation;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Валидация данных команды обновления марки авто
    /// </summary>
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        /// <summary>
        /// Валидация данных команды обновления марки авто
        /// </summary>
        public UpdateBrandCommandValidator()
        {
            RuleFor(updateBrandCommand => updateBrandCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateBrandCommand => updateBrandCommand.Name).NotEmpty();
            RuleFor(updateBrandCommand => updateBrandCommand.Country).NotEmpty();
        }
    }
}
