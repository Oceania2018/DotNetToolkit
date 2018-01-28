using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolkit.JwtHelper
{
    /// <summary>
    /// https://github.com/blowdart/AspNetAuthorizationWorkshop/tree/core2
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/
    /// </summary>
    public static class JwtServiceCollectionExtensions
    {
        /// <summary>
        /// Add JWT Authorization
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = configuration.GetSection("TokenAuthentication");

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtSecurityKey.Create(authConfig["SecretKey"]),

                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = authConfig["Issuer"],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = authConfig["Audience"],

                // Validate the token expiry  
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
