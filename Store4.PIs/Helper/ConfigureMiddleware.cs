using Microsoft.AspNetCore.Builder;
using Store4.PIs.Middlewares;
using store4.Repository.Data.Contexts;
using store4.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store4.Repository.Identity.Contexts;
using Store4.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Store4.Core.Entities.Identity;

namespace Store4.PIs.Helper
{
	public static class ConfigureMiddleware
	{
		public  static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
		{
			using var scpoe = app.Services.CreateScope();
			var services = scpoe.ServiceProvider;
			var context = services.GetRequiredService<StoreDbContext>();
			var usermanger = services.GetRequiredService<UserManager<AppUser>>();
			var identityContext = services.GetRequiredService<StoreIdentityDbContext>();
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await context.Database.MigrateAsync(); //UpDate-database
				await StoreDbContextSeed.SeedAsync(context);	
				await identityContext.Database.MigrateAsync(); //UpDate-database
				await StoreIdentityDbContextSeed.SeedAppUserAsync(usermanger);
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "there are problems during apply migration");
			}
			app.UseMiddleware<ExceptionMiddleware>(); //configure user-defined middleware
													  // Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStatusCodePagesWithReExecute("/error/{0}");
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();

			app.MapControllers();

			app.Run();
			return app;
		}
	}
}
