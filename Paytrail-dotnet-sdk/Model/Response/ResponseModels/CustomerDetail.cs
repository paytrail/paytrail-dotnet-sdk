using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response.ResponseModels
{
    public class CustomerDetail
    {
        [JsonProperty("network_address")]
        public string NetworkAddress { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}
