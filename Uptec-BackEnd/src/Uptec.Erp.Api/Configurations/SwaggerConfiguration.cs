using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Uptec.Erp.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Uptec API",
                    Description = "API Uptec",
                    TermsOfService = "Nenhum",
                    Contact = new Contact
                    {
                        Name = "A Definitiva Tecnologia",
                        Email = "contato@adefinitiva.com.br",
                        Url = "http://adefinitiva.com.br"
                    },
                    License = new License { Name = "MIT", Url = "http://adefinitiva.com.br/license" }
                });

                //s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            //services.ConfigureSwaggerGen(opt =>
            //{
            //    opt.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            //});
        }
    }
}
