using System;
using Definitiva.Security.Identity.Athorization;
using Definitiva.Security.Identity.Data;
using Definitiva.Security.Identity.Interfaces;
using Definitiva.Security.Identity.Models;
using Definitiva.Security.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Uptec.Erp.Api.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();

            var tokenConfiguration = new TokenDescriptor();
            new ConfigureFromConfigurationOptions<TokenDescriptor>(configuration.GetSection("JwtTokenOptions"))
                .Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = SigningConfituration.Key;
                //paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidAudience = "https://uptec-front.azurewebsites.net";
                //paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidIssuer = "UptecTokenServer";
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                //Policies
                auth.AddPolicy("Cadastro", policy => policy.RequireRole("Master","Cadastro"));
                auth.AddPolicy("Estoque", policy => policy.RequireRole("Master","Estoque"));
                auth.AddPolicy("Fiscal", policy => policy.RequireRole("Master","Fiscal"));
                auth.AddPolicy("Producao", policy => policy.RequireRole("Master","Producao"));
                auth.AddPolicy("Qualidade", policy => policy.RequireRole("Master","Qualidade"));
                auth.AddPolicy("Financeiro", policy => policy.RequireRole("Master","Financeiro"));
                auth.AddPolicy("Seguranca", policy => policy.RequireRole("Master","Seguranca"));

                // Ativa o uso do token como forma de autorizar o acesso
                // a recursos deste projeto
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build()
                    );
            });
        }
    }
}
