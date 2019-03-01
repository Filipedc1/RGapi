using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RgApi.Models;
using RgApi.Services;
using Swashbuckle.AspNetCore.Swagger;

[assembly: ApiController]
namespace RgApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure swagger generator
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info() { Title = "RG API", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            // add jwt authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["Jwt:JwtIssuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["Jwt:JwtIssuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:JwtKey"])),
                            RequireSignedTokens = true,
                            // Ensure the token hasn't expired:
                            RequireExpirationTime = true,
                            ValidateLifetime = true,
                        };
                    });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin",     policy => { policy.RequireClaim("Admin");    policy.RequireAuthenticatedUser(); });
                opt.AddPolicy("Salons",    policy => { policy.RequireClaim("Salon");    policy.RequireAuthenticatedUser(); });
                opt.AddPolicy("Customers", policy => { policy.RequireClaim("Customer"); policy.RequireAuthenticatedUser(); });
            });

            // add auto mapper
            services.AddAutoMapper();

            services.AddScoped<IUser, UserService>();
            services.AddScoped<IProduct, ProductService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                opt.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
