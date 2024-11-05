 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using store4.Repository.Data.Contexts;
using Store4.Core.Entities.Identity;
using Store4.Core.Mapping.Baskets;
using Store4.Core.Mapping.Products;
using Store4.Core.Repositories.Contract;
using Store4.Core.Services.Contract;
using Store4.PIs.Errors;
using Store4.Repository;
using Store4.Repository.Identity.Contexts;
using Store4.Repository.Repositories;
using Store4.Service.Services.Caches;
using Store4.Service.Services.ProductS;
using Store4.Service.Services.Tokens;
using Store4.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Store4.Core.Mapping.Auth;
using Store4.Core.Mapping.Mapping;
using Store4.Service.Services.Orders;
using Store4.Core.Dtos.Basket;
using Store4.Service.Services.Baskets;
using Store4.Service.Services.Payments;
namespace Store4.PIs.Helper
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDependency(this IServiceCollection services ,IConfiguration configuration)
		{
			services.AddBuiltInService();
			services.AddSwaggerService();
			services.AddDbcontextService(configuration);
			services.AddUserDefinedService();
			services.AddAutoMapperService(configuration);
			services.AddConfigureInvalidModelStateResponseeService();
			services.AddAddRedisService(configuration);
			services.AddIdentityService();
			services.AddAuthantcationService(configuration);
			return services;
		}
		private static IServiceCollection AddBuiltInService(this IServiceCollection services)
		{
			services.AddControllers();
			return services;
		}
		private static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}
		private static IServiceCollection AddDbcontextService(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddDbContext<StoreDbContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

			});
			services.AddDbContext<StoreIdentityDbContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

			});
			return services;

		}
		private static IServiceCollection AddUserDefinedService(this IServiceCollection services )
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<IBasketRepository, BasketRepsitory>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IPaymentService, PaymentService>();
			return services;

		}
		private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
			services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
			services.AddAutoMapper(m => m.AddProfile(new AuthProfile()));
			services.AddAutoMapper(m => m.AddProfile(new OrderProfile(configuration)));

			return services;

		}
		private static IServiceCollection AddConfigureInvalidModelStateResponseeService(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(Options =>
			Options.InvalidModelStateResponseFactory = (ActionContext) =>
			{
				var ERRORs = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
													 .SelectMany(p => p.Value.Errors)
													 .Select(e => e.ErrorMessage);

				var response = new ApiValidationErrorResponse()
				{
					Errors = ERRORs
				};
				return new BadRequestObjectResult(response);
			});
			return services;

		}
		private static IServiceCollection AddAddRedisService(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddSingleton<IConnectionMultiplexer>((IServiceProvider) =>
			{
				var connection = configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});
			return services;

		}

		private static IServiceCollection AddIdentityService(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, IdentityRole>()
					.AddEntityFrameworkStores<StoreIdentityDbContext>();
			return services;

		}
		private static IServiceCollection AddAuthantcationService(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddAuthentication(Options =>
			{
				Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(Options =>
			{
				Options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:Audience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true ,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});
			
			return services;
			
		}

	}
}
