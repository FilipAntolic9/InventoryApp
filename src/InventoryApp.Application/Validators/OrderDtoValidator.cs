using FluentValidation;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty();
                items.RuleFor(i => i.Quantity).GreaterThan(0);
                items.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0m);
            });
        }
    }
}