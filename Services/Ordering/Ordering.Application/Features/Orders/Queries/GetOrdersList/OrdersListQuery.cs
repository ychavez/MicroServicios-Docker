using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class OrdersListQuery : IRequest<List<OrdersViewModel>>
    {
        public string UserName { get; set; } = null!;

        public OrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
