﻿namespace Paytrail_dotnet_sdk.Model.Response
{
    public class PayAddCardResponse : Response
    {
        public PayAddCardData Data { get; set; }
    }

    public class PayAddCardData
    {
        public string TransactionId { get; set; }
        public string RedirectUrl { get; set; } 
    }
}
