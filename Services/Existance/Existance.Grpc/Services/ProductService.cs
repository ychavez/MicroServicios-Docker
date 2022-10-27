using Existance.Grpc.Protos;
using Grpc.Core;

namespace Existance.Grpc.Services
{
    public class ProductService : ExistanceService.ExistanceServiceBase
    {
        public override  Task<ExistanceResponse> CheckExistance(CheckExistanceRequest request, ServerCallContext context)
        {
            var rdn = new Random();
            return Task.FromResult(new ExistanceResponse { Existance = rdn.Next(0,100) });
        }
    }
}
