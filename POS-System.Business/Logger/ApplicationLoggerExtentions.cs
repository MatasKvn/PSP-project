using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace POS_System.Business.Logger
{
    public static class ApplicationLoggerExtentions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ApplicationLoggerProvider>()
            );

            LoggerProviderOptions.RegisterProviderOptions
                <ApplicationLoggerOptions, ApplicationLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<ApplicationLoggerOptions> configure)
        {
            builder.AddFile();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
