using FluentValidation;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class OrdersListQueryValidator : AbstractValidator<OrdersListQuery>
    {
        public OrdersListQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("El UserName no puede ser vacion no manche XD")
                .MinimumLength(3)
                .WithMessage("El tamaño debe de ser mayor a 3")
                .MaximumLength(10)
                .EmailAddress();
        }
    }
}
