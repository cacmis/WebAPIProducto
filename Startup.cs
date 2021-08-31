
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiPrimerWebApi.Data;
using MiPrimerWebApi.Data.Interfaces;
using MiPrimerWebApi.Mapper;
using MiPrimerWebApi.Services;
using MiPrimerWebApi.Services.Interfaces;

namespace MiPrimerWebApi
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
            services.AddDbContext<DataContext>(x=> {
                            x.UseLazyLoadingProxies();
                            x.UseSqlite (Configuration.GetConnectionString("DefaultConnection"));
                        } );
            services.AddControllers();

            // se agrega el repositorio
            services.AddScoped<IApiRepository, ApiRepository>();
            // se agrega el repositorio de authenticacion 
            services.AddScoped<IAuthRepository, AuthRepository>();
            // se agrega  token service
            services.AddScoped<ITokenService, TokenService>();
            // se agrega configuracion de AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration["Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiPrimerWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiPrimerWebApi v1"));
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            
            app.UseRouting();

            // Agregamos la authenticacion antes de la autorización
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
