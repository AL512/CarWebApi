using FluentValidation;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Валидация данных команды удаления автомобиля
    /// </summary>
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        /// <summary>
        /// Валидация данных команды удаления автомобиля
        /// </summary>
        public DeleteCarCommandValidator()
        {
            RuleFor(deleteCarCommand =>
                deleteCarCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
