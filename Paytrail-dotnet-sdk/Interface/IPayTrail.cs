using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Interface
{
    interface IPayTrail
    {
        PaymentResponse CreatePayment(PaymentRequest paymentRequest);
        PaymentResponse CreateShopInShopPayment(ShopInShopPaymentRequest paymentRequest);
        GetPaymentResponse GetPaymentInfo(string transactionId);
        RefundResponse RefundPayment(RefundRequest refundRequest, string transactionId);
    }
}
