﻿using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Helpers;
using EventYojana.Infrastructure.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventYojana.Infrastructure.Core
{
    public static class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings object
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddTransient<IMessageSenderUtility, MessageSenderUtility>();
            services.AddTransient<IRequestContext, RequestContextAdapter>();

            // configure DI for application services
            //services.AddScoped<, > ();
        }

        //public static void SetConfigurationValues(IConfiguration configuration)
        //{
        //    var v = new CommonConfigurationSetting();
        //    configuration.GetSection("appSettings").Bind(v);
        //}
    }
}
