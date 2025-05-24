using FluentValidation;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Validators
{
    public class InventoryAdjustDtoValidator : AbstractValidator<InventoryAdjustDto>
    {
        public InventoryAdjustDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Delta)
                .NotEqual(0)
                .WithMessage("Adjustment cannot be 0.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
