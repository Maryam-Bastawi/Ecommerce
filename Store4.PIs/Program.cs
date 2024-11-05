
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using store4.Repository.Data;
using store4.Repository.Data.Contexts;
using Store4.Core.Mapping.Products;
using Store4.Core.Repositories.Contract;
using Store4.PIs.Errors;
using Store4.PIs.Helper;
using Store4.PIs.Middlewares;
using Store4.Repository;
using Store4.Service.Services.ProductS;

namespace Store4.PIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDependency(builder.Configuration);

			var app = builder.Build();

		  await	app.ConfigureMiddlewaresAsync();
		}
	}
}
