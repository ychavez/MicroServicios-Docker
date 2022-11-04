using Basket.Api.Repositories;
using Existance.Grpc.Protos;

namespace Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddStackExchangeRedisCache(options => {

                options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            builder.Services.AddScoped<IBasketReposiotory, BasketReposiotory>();

            // Configuramos el servicio de gRPC
            builder.Services.AddGrpcClient<ExistanceService.ExistanceServiceClient>
                (o => o.Address = new Uri(builder.Configuration["GrpcSettings:HostAddress"]));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}