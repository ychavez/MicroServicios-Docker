using Basket.Api.Entities;
using Basket.Api.Repositories;
using Existance.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketReposiotory basketReposiotory;
        private readonly ExistanceService.ExistanceServiceClient existanceService;

        public BasketController(IBasketReposiotory basketReposiotory, ExistanceService.ExistanceServiceClient existanceService)
            => (this.basketReposiotory, this.existanceService) = (basketReposiotory, existanceService);

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShopingCart>> GetBasket(string userName)
        {
            var basket = await basketReposiotory.GetBasket(userName);


            return Ok(basket ?? new ShopingCart(userName));

        }


        [HttpPost]

        public async Task<ActionResult<ShopingCart>> UpdateBasket([FromBody] ShopingCart shopingCart)
        {

            foreach (var item in shopingCart.shoppingCartItems)
            {
              var existance = existanceService.CheckExistance(new CheckExistanceRequest { Id = item.ProductId });
                if (existance.Existance < item.Quantity)
                {
                    throw new Exception("No hay existencia de este articulo");
                }
            } 

            await basketReposiotory.UpdateBasket(shopingCart);

            return Ok(shopingCart);

        }


    }
}
