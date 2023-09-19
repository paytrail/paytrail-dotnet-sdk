using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.Interface
{
    interface IPaytrail
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

        /// <summary>
        /// Refund partial a payment by transaction ID
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=refund</see>
        /// <param name="refundRequest">A refund instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>RefundResponse</returns>
        RefundResponse RefundPartiallyPayment(RefundRequest refundRequest, string transactionId, double refundRate);

        /// <summary>
        /// Get a list of payment providers
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=list-providers</see>
        /// <param name="getPaymentProvidersRequest">A GetPaymentProvidersRequest class instance</param>
        /// <returns>GetPaymentProvidersResponse</returns>
        GetPaymentProvidersResponse GetPaymentProviders(GetPaymentProvidersRequest getPaymentProvidersRequest);

        /// <summary>
        /// Returns an array of following grouped payment providers fields
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=list-grouped-providers</see>
        /// <param name="getGroupedPaymentProvidersRequest">A GetGroupedPaymentProvidersRequest class instance</param>
        /// <returns>GetGroupedPaymentProvidersResponse</returns>
        GetGroupedPaymentProvidersResponse GetGroupedPaymentProviders(GetGroupedPaymentProvidersRequest getGroupedPaymentProvidersRequest);

        /// <summary>
        /// Email refunds a payment by transaction ID
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=email-refunds</see>
        /// <param name="emailRefundRequest">A EmailRefundRequest class instance</param>
        /// <returns>EmailRefundResponse</returns>
        EmailRefundResponse EmailRefund(EmailRefundRequest emailRefundRequest, string transactionId);

        /// <summary>
        /// Returns the actual card token which can then be used to make payments on the card
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=get-token</see>
        /// <param name="getTokenRequest">A GetTokenRequest class instance</param>
        /// <returns>GetTokenResponse</returns>
        GetTokenResponse CreateGetTokenRequest(GetTokenRequest getTokenRequest);

        /// <summary>
        /// Returns merchant's settlement IDs and corresponding bank references
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=settlements</see>
        /// <param name="settlementsRequest">A SettlementsRequest class instance</param>
        /// <returns>SettlementsResponse</returns>
        SettlementsResponse RequestSettlements(SettlementsRequest settlementsRequest);

        /// <summary>
        /// Results in a callback containing the payment report
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=payment-reports</see>
        /// <param name="paymentReportRequest">A PaymentReportRequest class instance</param>
        /// <returns>PaymentReportResponse</returns>
        PaymentReportResponse RequestPaymentReport(PaymentReportRequest paymentReportRequest);

        /// <summary>
        /// Creates either direct charge for MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="createMitPaymentChargeRequest">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitPaymentResponse</returns>
        CreateMitOrCitPaymentResponse CreateMitPaymentCharge(CreateMitOrCitPaymentRequest createMitPaymentChargeRequest, string transactionId);

        /// <summary>
        /// Creates either an authorization hold MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="createMitPaymentAuthorizationHold">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitOrCitPaymentResponse</returns>
        CreateMitOrCitPaymentResponse CreateMitPaymentAuthorizationHold(CreateMitOrCitPaymentRequest createMitPaymentAuthorizationHold, string transactionId);
    }
}
