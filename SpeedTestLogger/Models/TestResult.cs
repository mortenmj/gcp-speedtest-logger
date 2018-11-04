using System;
using Newtonsoft.Json;

namespace SpeedTestLogger.Models
{
    public class TestResult
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("device")]
        public int Device { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("data")]
        public TestData Data { get; set; }
    }

    public class TestData
    {
        [JsonProperty("speeds")]
        public TestSpeeds Speeds { get; set; }

        [JsonProperty("client")]
        public TestClient Client { get; set; }

        [JsonProperty("server")]
        public TestServer Server { get; set; }
    }

    public class TestSpeeds
    {
        [JsonProperty("download")]
        public double Download { get; set; }
        
        [JsonProperty("upload")]
        public double Upload { get; set; }
    }

    public class TestClient
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("isp")]
        public string Isp { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class TestServer
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("ping")]
        public int Ping { get; set; }

        [JsonProperty("ip")]
        public int Id { get; set; }
    }
}
