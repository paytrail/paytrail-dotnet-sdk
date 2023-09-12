using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public enum PaymentStatus
    {
        [JsonProperty("default")]
        DEFAULT,

        [JsonProperty("paid")]
        PAID,

        [JsonProperty("all")]
        ALL,

        [JsonProperty("settled")]
        SETTLED
    }
}
