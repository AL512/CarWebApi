using FluentValidation;

namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Валидация данных команды обновления страны производителя
    /// </summary>
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        /// <summary>
        /// Валидация данных команды обновления страны производителя
        /// </summary>
        public UpdateCountryCommandValidator()
        {
            RuleFor(updateCountryCommand =>
                updateCountryCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateCountryCommand =>
                updateCountryCommand.Name).NotEmpty();
        }
    }
}
