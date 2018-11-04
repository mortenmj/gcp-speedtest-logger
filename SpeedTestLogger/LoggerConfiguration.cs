using System;
using System.Globalization;
using System.IO;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace SpeedTestLogger
{
    public class LoggerConfiguration
    {
        public readonly string UserId;
        public readonly int LoggerId;
        public readonly RegionInfo LoggerLocation;
        public readonly Uri ApiUrl;
        public readonly SubscriberConfiguration Subscriber;

        public LoggerConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true);

            var configuration = builder.Build();

            var countryCode = configuration["loggerLocationCountryCode"];
            LoggerLocation = new RegionInfo(countryCode);
            Console.WriteLine("Logger located in {0}", LoggerLocation.EnglishName);

            UserId = configuration["userId"];
            LoggerId = int.Parse(configuration["loggerId"]);
            ApiUrl = new Uri(configuration["speedTestApiUrl"]);
            Console.WriteLine($"API URL: {ApiUrl.AbsoluteUri}");

            Subscriber = new SubscriberConfiguration(configuration);
        }

        public class SubscriberConfiguration
        {
            public readonly string ProjectId;
            public readonly string SubscriptionId;

            public SubscriberConfiguration(IConfigurationRoot configuration)
            {
                ProjectId = configuration["PubSub:ProjectId"];
                SubscriptionId = configuration["PubSub:SubscriptionId"];
            }
        }
    }
}
