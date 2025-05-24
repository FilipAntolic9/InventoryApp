using FluentValidation;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Validators
{
    public class SupplierDtoValidator : AbstractValidator<SupplierDto>
    {
        public SupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
