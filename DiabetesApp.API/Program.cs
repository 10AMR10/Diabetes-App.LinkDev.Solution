using DiabetesApp.API.Helpers;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Repositry;
using DiabetesApp.Repositry.Data;
using DiabetesApp.Repositry.Identity;
using DiabetesApp.Repositry.Identity.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Talabat.APIs.Errors;
using System.Text.Json;
using System.Runtime.Intrinsics.X86;
using DiabetesApp.Core.Enitities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DiabetesApp.Core.Service.Contract;
using DiabetesApp.Service;


namespace DiabetesApp.API
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			// hello step 2
			#region Services
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
					});
			});

			// Add services to the container.

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<HospitailContext>((option) =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>();
			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;

			});
			builder.Services.Configure<ApiBehaviorOptions>((option) =>
			{
				option.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var Errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() > 0)
						.SelectMany(e => e.Value.Errors)
						.Select(m => m.ErrorMessage).ToList();
					var response = new ValidationErrorApiResponse()
					{
						errors = Errors
					};
					return new BadRequestObjectResult(response);
				};
			});
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddHttpClient();
			#region Identity
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
				{
					options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
				});
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
						ValidateAudience = true,
						ValidAudience = builder.Configuration["JWT:ValidAudience"],
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
						ClockSkew = TimeSpan.Zero
					};
				}); 
			#endregion

			// Register the Token Service
			builder.Services.AddScoped<ITokentService, TokentService>();


			#endregion

			var app = builder.Build();

			using var scope = app.Services.CreateScope();
			var service = scope.ServiceProvider;
			var _dbContext = service.GetRequiredService<HospitailContext>();
			var loggerFactory = service.GetRequiredService<ILoggerFactory>();

			try
			{
				var userManger = service.GetRequiredService<UserManager<ApplicationUser>>();
				var roleManger= service.GetRequiredService<RoleManager<IdentityRole>>();
				await AppIdentityDbContextSeeding.SeedingIdentityAsync(userManger, roleManger);
				await HospitailContextSeeding.SeedingAsync(_dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "error when appling migration");

			}



			// Configure the HTTP request pipeline.

			app.UseSwagger();
			app.UseSwaggerUI();


			app.UseHttpsRedirection();

			// Use the CORS policy
			app.UseCors("AllowAll");


			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
