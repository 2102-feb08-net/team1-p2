using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LooseLeaf.DataAccess;
using LooseLeaf.DataAccess.Repositories;
using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business;
using LooseLeaf.Business.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LooseLeaf.Web
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
            services.AddDbContext<LooseLeafContext>(BuildDbOptions);
            services.AddScoped<IOwnedBookRepository, OwnedBookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();

            services.AddCors(options => options.AddDefaultPolicy(config => config
               .WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()));

            services.AddHttpClient<GoogleBooks>();
            services.Configure<GoogleBooksOptions>(Configuration.GetSection(GoogleBooksOptions.ApiKeyConfiguration));

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier,
                        RoleClaimType = ClaimTypes.Role,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:wishlist", policy => policy.Requirements.Add(new HasScopeRequirement("read:wishlist", domain)));
                options.AddPolicy("read:loans", policy => policy.Requirements.Add(new HasScopeRequirement("read:loans", domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LooseLeaf.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = new PathString("/index.html");
                    await next.Invoke();
                }
            });
            //Turned off temporarily.
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LooseLeaf.Web v1"));
            }
            //else
            //{
            //    {
            //        app.UseExceptionHandler("/home/error");
            //    }
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void BuildDbOptions(DbContextOptionsBuilder builder)
        {
            string connectionString = Configuration.GetConnectionString("LooseLeafDatabase");
            builder.UseSqlServer(connectionString);
        }
    }
}