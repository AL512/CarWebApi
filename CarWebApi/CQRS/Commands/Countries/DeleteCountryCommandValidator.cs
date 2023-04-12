using FluentValidation;

namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Валидация данных команды удаления страны производителя
    /// </summary>
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        /// <summary>
        /// Валидация данных команды удаления страны производителя
        /// </summary>
        public DeleteCountryCommandValidator()
        {
            RuleFor(deleteCountryCommand =>
                deleteCountryCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
