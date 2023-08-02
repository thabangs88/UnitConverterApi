using System;

namespace UnitConverterAPI.Options
{
    public class TokenOptions
    {
        public string Type { get; set; } = "Bearer";
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(30);
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
    }
}
