using System;
using System.Net.Http;

namespace GTech.Weather.Forecast.AI.Domain.WeatherForecast
{
    public class City
    {
        public int Version { get; set; }
        public string? Key { get; set; }
        public string? Type { get; set; }
        public int Rank { get; set; }
        public string? LocalizedName { get; set; }
        public string? EnglishName { get; set; }
        public string? PrimaryPostalCode { get; set; }
        public bool IsAlias { get; set; }
    }
}
