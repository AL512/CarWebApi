using FluentValidation;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Валидация данных команды создания автомобиля
    /// </summary>
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        /// <summary>
        /// Валидация данных команды создания автомобиля
        /// </summary>
        public CreateCarCommandValidator()
        {
            RuleFor(createCarCommand =>
                createCarCommand.Name).NotEmpty();
            RuleFor(createCarCommand =>
                createCarCommand.Pow).GreaterThan(0);
            RuleFor(createCarCommand =>
                createCarCommand.Long).GreaterThan(0);
            RuleFor(createCarCommand =>
                createCarCommand.Price).GreaterThan(0);

        }
    }
}
