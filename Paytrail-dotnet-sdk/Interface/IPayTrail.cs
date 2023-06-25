using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.Interface
{
    interface IPayTrail
    {
        PaymentResponse CreatePayment(PaymentRequest paymentRequest);
        PaymentResponse CreateShopInShopPayment(ShopInShopPaymentRequest paymentRequest);
        GetPaymentResponse GetPaymentInfo(string transactionId);
        RefundResponse RefundPayment(RefundRequest refundRequest, string transactionId);
        RefundResponse RefundPartiallyPayment(RefundRequest refundRequest, string transactionId, double refundRate);
    }
}
