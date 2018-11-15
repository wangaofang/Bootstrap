using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using Bootstrap.Services;
using Bootstrap.Entities;
using Microsoft.EntityFrameworkCore;
using Bootstrap.Repository;
using Bootstrap.Dtos;

namespace Bootstrap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddFormatterMappings().AddJsonOptions((options) =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .AddJsonFormatters().AddMvcOptions(Options =>
            {
                Options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

#if debug
            services.AddTransient<IMailService, LocalMailService>();      
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            //  services.AddDbContext<MyContext>();
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ProductDB;Trusted_Connection=True";
            services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MyContext myContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            myContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductWithoutMaterialDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Material, MaterialDto>();
                cfg.CreateMap<ProductCreation, Product>();
                cfg.CreateMap<Product, ProductCreation>();
                cfg.CreateMap<Product, ProductModification>();
                cfg.CreateMap<ProductModification, Product>();
            });


            loggerFactory.AddNLog();

            app.UseStatusCodePages(); // !!!
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
