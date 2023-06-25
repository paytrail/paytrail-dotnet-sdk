using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.Interface
{
    interface IPayTrail
    {
        /// <summary>
        /// Create a payment request
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create</see>
        /// <param name="paymentRequest">A payment class instance</param>
        /// <returns>PaymentResponse</returns>
        PaymentResponse CreatePayment(PaymentRequest paymentRequest);

        /// <summary>
        ///  Create a Shop in shop payment request
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create</see>
        /// <param name="paymentRequest">A shop in shop class instance</param>
        /// <returns>PaymentResponse (of shop)</returns>
        PaymentResponse CreateShopInShopPayment(ShopInShopPaymentRequest paymentRequest);

        /// <summary>
        /// Get payment information
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=get</see>
        /// <param name="transactionId"></param>
        /// <returns>Information Payment (GetPaymentResponse)</returns>
        GetPaymentResponse GetPaymentInfo(string transactionId);

        /// <summary>
        /// Refund a payment by transaction ID
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=refund</see>
        /// <param name="refundRequest">A refund instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>RefundResponse</returns>
        RefundResponse RefundPayment(RefundRequest refundRequest, string transactionId);
        RefundResponse RefundPartiallyPayment(RefundRequest refundRequest, string transactionId, double refundRate);
    }
}
