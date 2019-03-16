using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Libraries.Business.Impl;
using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Business.Validations.Impl;
using Microsoft.Libraries.Business.Validations.Interfaces;
using Microsoft.Libraries.Models;
using Microsoft.Libraries.ORM.Impl;
using Microsoft.Libraries.ORM.Interfaces;
using Microsoft.Libraries.Repositories.Impl;
using Microsoft.Libraries.Repositories.Interfaces;

namespace ProductServicesHosting
{
    public class Startup
    {
        private const string PRODUCTS_DB_CONN_STRING = "ProductsDBConnectionString";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductsContext>(
                dbContextOptionsBuilder =>
                {
                    var decodedConnectionString = Encoding.ASCII.GetString(Convert.FromBase64String(
                        Environment.GetEnvironmentVariable(PRODUCTS_DB_CONN_STRING)));

                    dbContextOptionsBuilder.UseSqlServer(decodedConnectionString);
                });

            services.AddScoped<IProductsContext, ProductsContext>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IBusinessValidator<string>, ProductSearchStringValidator>();
            services.AddScoped<IBusinessValidator<Product>, ProductValidator>();
            services.AddScoped<IProductsBusinessComponent, ProductsBusinessComponent>();
            
            /*
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    jwtBearerOptions =>
                    {
                        jwtBearerOptions.Audience = Environment.GetEnvironmentVariable("Audience");
                        jwtBearerOptions.Authority = Environment.GetEnvironmentVariable("Authority");
                    });
            */
            
            services.AddSwaggerGen(
               swaggerGenOptions =>
               {
                   swaggerGenOptions.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                   {
                       Title = "Products API",
                       Version = "v1",
                       Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Email = "jd.ramkumar@gmail.com", Name = "Ramkumar JD", Url = @"http://people.intsol.in/jdramkumar" },
                       Description = "Simple Products System Service",
                       License = new Swashbuckle.AspNetCore.Swagger.License { Name = "MIT", Url = "http://licenses.intsol.in/apis/mit" },
                       TermsOfService = "All Rights Reserved"
                   });

                   swaggerGenOptions.IncludeXmlComments(GetXmlCommentsPath());
               });

            services.AddMvc();
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;

            return Path.Combine(app.ApplicationBasePath, @"Microsoft.Libraries.Api.Controllers.Impl.xml");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                swaggerUIOptions =>
                {
                    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
                });

            // app.UseAuthentication();
            app.UseMvc();
        }
    }
}
