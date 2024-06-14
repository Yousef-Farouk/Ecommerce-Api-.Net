
using CloudinaryDotNet;
using E_commerce.Models;
using E_commerce.UnitOfWorks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var origin = "";
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(origin,
                    builder =>
                    {
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddDbContext<EcommerceContext>(options =>options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("con")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<EcommerceContext>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<Cloudinary>((provider) =>
            {
                Account acc = new Account("dctoktk1l", "416939737679971", "00P1jdEAfUPz3AML6MDCd8Ep7V4");
                return new Cloudinary(acc);
            });

            builder.Services.AddScoped<UnitOfWork>();


            builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "myscheme")
                .AddJwtBearer("myscheme",
            op =>
            {
                string key = "welcome to my secret key yousef farouk";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = Configuration["Jwt:Issuer"],
                    ValidateIssuer = false,
                    ValidateAudience = false
                };


            });
                            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(origin);
            app.MapControllers();

            app.Run();
        }
    }
}
