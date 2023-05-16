using FluentValidation;

namespace CarWebApi.CQRS.Commands.Brands
{
    /// <summary>
    /// Валидация данных команды создания марки авто
    /// </summary>
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        /// <summary>
        /// Валидация данных команды создания мврки авто
        /// </summary>
        public CreateBrandCommandValidator()
        {
            RuleFor(createBrandCommand => createBrandCommand.Name).NotEmpty();
        }
    }
}
