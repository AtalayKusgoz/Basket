using AutoMapper;
using DefineX.Services.Discount.Business.Concrete;
using DefineX.Services.Discount.Business.Interfaces;
using DefineX.Services.Discount.Business.Mapping;
using DefineX.Services.Discount.Business.ValidationRules.FluentValidation;
using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Context;
using DefineX.Services.Discount.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using DefineX.Services.Discount.DataAccess.Concrete.Models;
using DefineX.Services.Discount.DataAccess.Concrete.Mongo;
using DefineX.Services.Discount.DataAccess.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Dtos;

namespace DefineX.Services.Discount.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<DefineXContext>(opt =>
            {
                opt.UseNpgsql(configuration["DefineXConfig:DbCon"], conf =>
                {
                    conf.MigrationsAssembly("DefineX.Services.Discount.Api");
                });
            });

            services.AddScoped<IMongoLog>(provider => new MongoDal(configuration["DefineXConfig:MongoConnectionString"], "DefineX", "RequestResponseLog"));
            services.AddScoped<IMongoService, MongoManager>();

            services.AddMvc();

            services.AddOptions();


            services.Configure<DefineXConfig>(configuration.GetSection("DefineXConfig"));
            services.AddSingleton<ICoreContext, CoreContext>();

            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<DefineXConfig>>().Value;
                var redis = new RedisService(redisSettings.RedisSettings.Host, redisSettings.RedisSettings.Port);
                redis.Connect();
                return redis;
            });


            services.AddScoped<IRedisDataService, RedisManager>();

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));


            services.AddScoped<IProductDal, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IBasketService, BasketManager>();

            services.AddTransient<IValidator<ProductAddDto>, ProductAddDtoValidator>();
        }
    }
}
