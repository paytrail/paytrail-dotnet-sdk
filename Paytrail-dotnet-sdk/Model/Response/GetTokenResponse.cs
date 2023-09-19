using Paytrail_dotnet_sdk.Model.Response.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class GetTokenResponse : Response
    {
        public GetTokenData Data { get; set; }
    }

    public class GetTokenData
    {
        public string Token { get; set; }
        public Card Card { get; set; }
        public CustomerDetail Customer { get; set; }
    }
}
