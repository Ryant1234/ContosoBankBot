using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ContosoBankBot.DataModels
{
    public class ConversionHistory
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "currencyFrom")]
        public string CurrencyFrom { get; set; }

        [JsonProperty(PropertyName = "currencyTo")]
        public string CurrencyTo { get; set; }

        [JsonProperty(PropertyName = "currencyAmount")]
        public double CurrencyAmount { get; set; }

        [JsonProperty(PropertyName = "exchangeRate")]
        public double ExchangeRate { get; set; }

        [JsonProperty(PropertyName = "dateChecked")]
        public DateTime DateChecked { get; set; }

        [JsonProperty(PropertyName = "convertedAmount")]
        public double ConvertedAmount { get; set; }
    }
}