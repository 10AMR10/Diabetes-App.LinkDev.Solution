using DiabetesApp.Repositry.Data;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.API
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			// hello step 1
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<HospitalContext>((option) =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			var app = builder.Build();

			using var scope = app.Services.CreateScope();
			var service = scope.ServiceProvider;
			var _dbContext=service.GetRequiredService<HospitalContext>();
			var loggerFactory= service.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var logger= loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "error when appling migration");
				
			}



			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
