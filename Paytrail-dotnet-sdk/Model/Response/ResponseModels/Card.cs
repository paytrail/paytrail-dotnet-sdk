using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response.ResponseModels
{
    public class Card
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("bin")]
        public string Bin { get; set; }

        [JsonProperty("partial_pan")]
        public string PartialPan { get; set; }

        [JsonProperty("expire_year")]
        public string ExpireYear { get; set; }

        [JsonProperty("expire_month")]
        public string ExpireMonth { get; set; }

        [JsonProperty("cvc_required")]
        public string CvcRequired { get; set; }

        [JsonProperty("funding")]
        public string Funding { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("pan_fingerprint")]
        public string PanFingerprint { get; set; }

        [JsonProperty("card_fingerprint")]
        public string CardFingerprint { get; set; }
    }
}
