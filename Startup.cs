using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using apiEsFeDemostracion.Entities;
using apiEsFeDemostracion.Helpers;
using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace apiEsFeDemostracion
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
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<MiFiltroDeAccion>();
            services.AddDbContext<DbContextFe>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EsFeConnectionString")));

            services.AddMvc(options =>
                {
                    options.Filters.Add(new MiFiltroDeExcepcion());
                    // Si hubiese Inyección de dependencias en el filtro
                    //options.Filters.Add(typeof(MiFiltroDeExcepcion)); 
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).
                AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                        ClockSkew = TimeSpan.Zero
                    });

            //services.AddSwaggerGen(config =>
            //{
            //    config.SwaggerDoc("v1", new Info
            //    {
            //        Version = "V1",
            //        Title = "Mi Web API",
            //        Description = "Esta es una descripción del Web API",
            //        TermsOfService = "https://www.udemy.com/user/felipegaviln/",
            //        License = new License()
            //        {
            //            Name = "MIT",
            //            Url = "http://bfy.tw/4nqh"
            //        },
            //        Contact = new Contact()
            //        {
            //            Name = "Felipe Gavilán",
            //            Email = "felipe_gavilan887@hotmail.com",
            //            Url = "https://gavilan.blog/"
            //        }
            //    });

            //    config.SwaggerDoc("v2", new Info { Title = "Mi Web API", Version = "v2" });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    config.IncludeXmlComments(xmlPath);

            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors(builder => builder.WithOrigins("http://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            app.UseMvc();
        }
    }
}
