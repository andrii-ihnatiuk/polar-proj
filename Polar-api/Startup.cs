using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polar.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Polar
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Polar-api", Version = "v1" }); });
            services.AddCors(); // CORS enabled
            // Adding configuration class to be injected
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            string connection;
            if (Environment.IsDevelopment()) {
                connection = Configuration.GetConnectionString("DevConnection");
            } else {
                connection = Configuration.GetConnectionString("DefaultConnection");
            }

            services.AddDbContext<PolarContext>(options =>
                options.UseMySql(connection, new MySqlServerVersion(new Version())));

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<PolarContext>();
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<String>("ApplicationSettings:AuthOptions:Secret"));
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = true;
                options.SaveToken = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                   
                    ValidateIssuer = false,
                    // ValidIssuer = Configuration.GetValue<String>("ApplicationSettings:AuthOptions:Issuer"),

                    ValidateAudience = false,
                    // ValidAudience = Configuration.GetValue<String>("ApplicationSettings:AuthOptions:Audience"),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polar-api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
        
            app.Run(async (context) => {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(System.IO.Path.Combine(env.WebRootPath, "index.html"));
            });
        }
    }
}