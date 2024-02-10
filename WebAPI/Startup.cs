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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using MediatR;
using Aplicacion.Cursos;
using FluentValidation.AspNetCore;
using WebAPI.Middleware;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Contratos;
using Seguridad;
using Microsoft.IdentityModel.Tokens;
using Seguridad.TokenSeguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;

namespace WebAPI
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
            services.AddDbContext<CursosOnlineContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.Configure<ConexionConfiguracion>(Configuration.GetSection("DefaultConnection")); 

            services.AddMediatR(typeof(Consulta.Manejador).Assembly);

            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddScoped<IJwtGenerador, JwtGenerador>();
            services.AddAutoMapper(typeof(Consulta.Manejador));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErrorMiddleware>();
            
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
