using FluentValidation;

namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Валидация данных команды создания страны производителя
    /// </summary>
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        /// <summary>
        /// Валидация данных команды создания страны производителя
        /// </summary>
        public CreateCountryCommandValidator()
        {
            RuleFor(createCountryCommand =>
                createCountryCommand.Name).NotEmpty();
        }
    }
}
