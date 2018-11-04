using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpeedTestLogger.Models;
using Google.Cloud.PubSub.V1;

namespace SpeedTestLogger
{
    class Program
    {
        private static LoggerConfiguration _loggerConfig;
        private static SubscriberClient _subscriber;

        static async Task Main(string[] args)
        {
            _loggerConfig = new LoggerConfiguration();

            var subscriberClient = SubscriberServiceApiClient.Create();
            _subscriber = await SubscriberClient.CreateAsync(
                new SubscriptionName(_loggerConfig.Subscriber.ProjectId, _loggerConfig.Subscriber.SubscriptionId));

            await _subscriber.StartAsync(HandleMessage);

            Console.ReadKey();

            await _subscriber.StopAsync(CancellationToken.None);
        }

        static async Task<SubscriberClient.Reply> HandleMessage(PubsubMessage message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Data.ToByteArray());
            if (messageBody != "RUN_SPEEDTEST") return SubscriberClient.Reply.Nack;

            Console.WriteLine("Running speedtest!");

            var runner = new SpeedTestRunner(_loggerConfig.LoggerLocation);
            var testData = runner.RunSpeedTest();
            var results = new TestResult
            {
                User = _loggerConfig.UserId,
                Device = _loggerConfig.LoggerId,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Data = testData
            };

            var success = false;
            using (var client = new SpeedTestApiClient(_loggerConfig.ApiUrl))
            {
                success = await client.PublishTestResult(results);
            }

            Console.WriteLine($"SpeedTest {(success == true ? "complete" : "failed")}!");
            return success == true ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack;
        }
    }
}