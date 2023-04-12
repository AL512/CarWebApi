using FluentValidation;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Валидация данных команды обновления автомобиля
    /// </summary>
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        /// <summary>
        /// Валидация данных команды обновления автомобиля
        /// </summary>
        public UpdateCarCommandValidator()
        {
            RuleFor(updateCarCommand =>
                updateCarCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateCarCommand =>
                updateCarCommand.Name).NotEmpty();
            RuleFor(updateCarCommand =>
                updateCarCommand.Pow).GreaterThan(0);
            RuleFor(updateCarCommand =>
                updateCarCommand.Long).GreaterThan(0);
            RuleFor(updateCarCommand =>
                updateCarCommand.Price).GreaterThan(0);

        }
    }
}
