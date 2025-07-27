using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Taskify.API.Data;
using Taskify.API.Services;
using Taskify.API.Services.Interfaces;

namespace Taskify.API
{
    /// <summary>
    /// Entry point for the API
    /// </summary>
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TaskifyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISeedService, SeedService>();
            builder.Services.AddScoped<ITaskService, TasksService>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
                        ValidateAudience = true,

                        ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Token")!)),
                    };
                }
            );
            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<ISeedService>();
                await seeder.InsertSeedData();
            }

            if (app.Environment.IsDevelopment())
            {
                
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taskify API");
                    c.RoutePrefix = "docs";
                });
            }
            app.UseCors(options =>
            {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            
            app.Run();
        }
    }
}
