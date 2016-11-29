using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ContosoBankBot.DataModels
{
    public class Country
    { // Helps us define the property name mapping between the client type to the table on our backend


        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "CurrencyName")]
        public string CurrencyName { get; set; }

        [JsonProperty(PropertyName = "CurrencyCode")]
        public string CurrencyCode { get; set; }


        [JsonProperty(PropertyName = "CountryName")]
        public string CountryName { get; set; }
    }
}