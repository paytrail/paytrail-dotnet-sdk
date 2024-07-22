using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Paytrail_dotnet_sdk.Interface;
using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;
using Paytrail_dotnet_sdk.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Transactions;

namespace Paytrail_dotnet_sdk
{
    public class PaytrailClient : Paytrail, IPaytrail
    {
        public const string API_ENDPOINT = @"https://services.paytrail.com";

        public PaytrailClient(string merchantId, string secretKey, string platformName)
        {
            this.merchantId = merchantId;
            this.secretKey = secretKey;
            this.platformName = platformName;
        }

        /// <summary>
        /// Create a payment request
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create</see>
        /// <param name="paymentRequest">A payment class instance</param>
        /// <returns></returns>
        public PaymentResponse CreatePayment(PaymentRequest paymentRequest)
        {
            PaymentResponse res = new PaymentResponse();
            try
            {
                // Validate payment request
                if (!ValidateCreatePaymentRequest(res, paymentRequest))
                {
                    return res;
                }

                // Create payment
                res = CreatePayment(JsonConvert.SerializeObject(paymentRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }));
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        ///  Create a Shop in shop payment request
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create</see>
        /// <param name="paymentRequest">A shop in shop class instance</param>
        /// <returns>PaymentResponse (of shop)</returns>
        public PaymentResponse CreateShopInShopPayment(ShopInShopPaymentRequest paymentRequest)
        {
            PaymentResponse res = new PaymentResponse();
            try
            {
                // Validate payment SiS request
                if (!ValidateCreateSiSPaymentRequest(res, paymentRequest))
                {
                    return res;
                }

                // Create SiS payment
                res = CreatePayment(JsonConvert.SerializeObject(paymentRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }));
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Get payment information
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=get</see>
        /// <param name="transactionId"></param>
        /// <returns>Information Payment (GetPaymentResponse)</returns>
        public GetPaymentResponse GetPaymentInfo(string transactionId)
        {
            GetPaymentResponse res = new GetPaymentResponse();
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = ResponseMessage.RequestNull.GetEnumDescription();
                return res;
            }

            try
            {
                res = GetPayment(transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Refund a payment by transaction ID
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=refund</see>
        /// <param name="refundRequest">A refund instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>RefundResponse</returns>
        public RefundResponse RefundPayment(RefundRequest refundRequest, string transactionId)
        {
            RefundResponse res = new RefundResponse();
            try
            {
                // Validate refund request
                if (!ValidateRefundRequest(res, refundRequest, transactionId))
                {
                    return res;
                }

                // Create refund request
                res = CreateRefundRequest(JsonConvert.SerializeObject(refundRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Refund partial a payment by transaction ID
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=refund</see>
        /// <param name="refundRequest">A refund instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>RefundResponse</returns>
        public RefundResponse RefundPartiallyPayment(RefundRequest refundRequest, string transactionId, double refundRate)
        {
            RefundResponse res = new RefundResponse();
            try
            {
                // Validate refund request
                if (!ValidateRefundRequest(res, refundRequest, transactionId) || refundRate < Convert.ToDouble(0))
                {
                    return res;
                }

                refundRequest.Amount = Convert.ToInt32(Math.Round((Convert.ToDouble(refundRequest.Amount) * refundRate), 0));

                // Create refund request
                res = CreateRefundRequest(JsonConvert.SerializeObject(refundRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        public PayAddCardResponse PayAndAddCard(PayAddCardRequest request)
        {
            PayAddCardResponse response = new PayAddCardResponse();
            try
            {
                // Validate pay and add card request
                if (!ValidatePayAndAddCardRequest(response, request))
                {
                    return response;
                }

                // Create pay and add card request
                response = CreatePayAndAddCardRequest(JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }));
                return response;
            }
            catch (Exception ex)
            {
                response.ReturnCode = (int)ResponseMessage.Exception;
                response.ReturnMessage = ex.ToString();
                throw;
            }
        }

        /// <summary>
        /// Get a list of payment providers.
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=list-providers</see>
        /// <param name="getPaymentProvidersRequest">A GetPaymentProvidersRequest class instance</param>
        /// <returns>GetPaymentProvidersResponse</returns>
        public GetPaymentProvidersResponse GetPaymentProviders(GetPaymentProvidersRequest getPaymentProvidersRequest)
        {
            GetPaymentProvidersResponse res = new GetPaymentProvidersResponse();
            try
            {
                if (!ValidateGetPaymentProviders(res, getPaymentProvidersRequest))
                {
                    return res;
                }

                // Get Payment Providers
                res = HandleGetPaymentProviders(getPaymentProvidersRequest);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Returns an array of following grouped payment providers fields
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=list-grouped-providers</see>
        /// <param name="getGroupedPaymentProvidersRequest">A GetGroupedPaymentProvidersRequest class instance</param>
        /// <returns>GetGroupedPaymentProvidersResponse</returns>
        public GetGroupedPaymentProvidersResponse GetGroupedPaymentProviders(GetGroupedPaymentProvidersRequest getGroupedPaymentProvidersRequest)
        {
            GetGroupedPaymentProvidersResponse res = new GetGroupedPaymentProvidersResponse();

            try
            {
                if (!ValidateGetGroupdPaymentProviders(res, getGroupedPaymentProvidersRequest))
                {
                    return res;
                }

                res = HandleGetGroupedPaymentProviders(getGroupedPaymentProvidersRequest);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Email refunds a payment by transaction ID:
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=email-refunds</see>
        /// <param name="emailRefundRequest">A EmailRefundRequest class instance</param>
        /// <returns>EmailRefundRequest</returns>
        public EmailRefundResponse EmailRefund(EmailRefundRequest emailRefundRequest, string transactionId)
        {
            EmailRefundResponse res = new EmailRefundResponse();
            try
            {
                // Validate email refund request
                if (!ValidateEmailRefundRequest(res, emailRefundRequest, transactionId))
                {
                    return res;
                }

                // Create email refund request
                res = HandleEmailRefund(JsonConvert.SerializeObject(emailRefundRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Returns the actual card token which can then be used to make payments on the card
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=get-token</see>
        /// <param name="getTokenRequest">A GetTokenRequest class instance</param>
        /// <returns>GetTokenResponse</returns>
        public GetTokenResponse CreateGetTokenRequest(GetTokenRequest getTokenRequest)
        {
            GetTokenResponse res = new GetTokenResponse();
            try
            {
                // Validate get token request
                if (!ValidateCreateGetTokenRequest(res, getTokenRequest))
                {
                    return res;
                }

                // Create get token request
                res = HandleCreateGetTokenRequest(getTokenRequest);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Returns merchant's settlement IDs and corresponding bank references
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=settlements</see>
        /// <param name="settlementsRequest">A SettlementsRequest class instance</param>
        /// <returns>SettlementsResponse</returns>
        public SettlementsResponse GetSettlements(SettlementsRequest settlementsRequest)
        {
            SettlementsResponse res = new SettlementsResponse();
            try
            {
                // Validate request settlements request
                if (!ValidateRequestSettlements(res, settlementsRequest))
                {
                    return res;
                }

                // Create request settlements
                res = HandleRequestSettlements(settlementsRequest);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Results in a callback containing the payment report
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=payment-reports</see>
        /// <param name="paymentReportRequest">A PaymentReportRequest class instance</param>
        /// <returns>PaymentReportResponse</returns>
        public PaymentReportResponse RequestPaymentReport(PaymentReportRequest paymentReportRequest)
        {
            PaymentReportResponse res = new PaymentReportResponse();
            try
            {
                // Validate payment report request
                if (!ValidateRequestPaymentReport(res, paymentReportRequest))
                {
                    return res;
                }

                // Create payment report
                res = HandleRequestPaymentReport(JsonConvert.SerializeObject(paymentReportRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }));
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Creates either direct charge for MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="paymentReportRequest">A PaymentReportRequest class instance</param>
        /// <returns>CreateMitPaymentChargeResponse</returns>
        public CreateMitOrCitPaymentResponse CreateMitPaymentCharge(CreateMitOrCitPaymentRequest createMitPaymentChargeRequest, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create mit payment charge
                if (!ValidateCreateMitOrCitPaymentRequest(res, createMitPaymentChargeRequest, transactionId))
                {
                    return res;
                }

                // Create create mit payment charge
                res = HandleCreateMitPaymentCharge(JsonConvert.SerializeObject(createMitPaymentChargeRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Creates either an authorization hold MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="createMitPaymentAuthorizationHold">A CreateMitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitPaymentResponse</returns>
        public CreateMitOrCitPaymentResponse CreateMitPaymentAuthorizationHold(CreateMitOrCitPaymentRequest createMitPaymentAuthorizationHold, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create mit payment authorization hold
                if (!ValidateCreateMitOrCitPaymentRequest(res, createMitPaymentAuthorizationHold, transactionId))
                {
                    return res;
                }

                // Create create mit payment authorization hold
                res = HandleCreateMitPaymentAuthorizationHold(JsonConvert.SerializeObject(createMitPaymentAuthorizationHold, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Creates either direct charge for MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="createCitPaymentChargeRequest">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitOrCitPaymentResponse</returns>
        public CreateMitOrCitPaymentResponse CreateCitPaymentCharge(CreateMitOrCitPaymentRequest createCitPaymentChargeRequest, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create mit payment charge
                if (!ValidateCreateMitOrCitPaymentRequest(res, createCitPaymentChargeRequest, transactionId))
                {
                    return res;
                }
                // Create create mit payment charge
                res = HandleCreateCitPaymentCharge(JsonConvert.SerializeObject(createCitPaymentChargeRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Creates either an authorization hold MIT payments
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=create-authorization-hold-or-charge</see>
        /// <param name="createCitPaymentAuthorizationHold">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitOrCitPaymentResponse</returns>
        public CreateMitOrCitPaymentResponse CreateCitPaymentAuthorizationHold(CreateMitOrCitPaymentRequest createCitPaymentAuthorizationHold, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create cit payment authorization hold
                if (!ValidateCreateMitOrCitPaymentRequest(res, createCitPaymentAuthorizationHold, transactionId))
                {
                    return res;
                }

                // Create create cit payment authorization hold
                res = HandleCreateCitPaymentAuthorizationHold(JsonConvert.SerializeObject(createCitPaymentAuthorizationHold, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Commits an existing authorization hold
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=commit-authorization-hold</see>
        /// <param name="createMitPaymentCommitRequest">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitOrCitPaymentResponse</returns>
        public CreateMitOrCitPaymentResponse CreateMitPaymentCommit(CreateMitOrCitPaymentRequest createMitPaymentCommitRequest, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create mit payment commit
                if (!ValidateCreateMitOrCitPaymentRequest(res, createMitPaymentCommitRequest, transactionId))
                {
                    return res;
                }

                // Create mit payment commit
                res = HandleCreateMitOrCitPaymentCommit(JsonConvert.SerializeObject(createMitPaymentCommitRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Commits an existing authorization hold
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=commit-authorization-hold</see>
        /// <param name="createMitPaymentCommitRequest">A CreateMitOrCitPaymentRequest class instance</param>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>CreateMitOrCitPaymentResponse</returns>
        public CreateMitOrCitPaymentResponse CreateCitPaymentCommit(CreateMitOrCitPaymentRequest createCitPaymentCommitRequest, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Validate create mit payment commit
                if (!ValidateCreateMitOrCitPaymentRequest(res, createCitPaymentCommitRequest, transactionId))
                {
                    return res;
                }

                // Create mit payment commit
                res = HandleCreateMitOrCitPaymentCommit(JsonConvert.SerializeObject(createCitPaymentCommitRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Reverts an existing authorization hold
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=revert-authorization-hold</see>
        /// <param name="transactionId">the transaction ID</param>
        /// <returns>RevertAuthorizationHoldResponse</returns>
        public RevertAuthorizationHoldResponse RevertPaymentAuthorizationHold(string transactionId)
        {
            RevertAuthorizationHoldResponse res = new RevertAuthorizationHoldResponse();
            try
            {
                // Validate revert payment authorization hold
                if (!ValidateRevertPaymentAuthorizationHoldRequest(res, transactionId))
                {
                    return res;
                }

                // Revert payment authorization hold
                res = HandleRevertPaymentAuthorizationHold(transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// Results in a callback containing the payment report
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=payment-report-request-by-settlement-id</see>
        /// <param name="paymentReportBySettlementRequest">A PaymentReportBySettlementRequest class instance</param>
        /// <returns>RevertAuthorizationHoldResponse</returns>
        public PaymentReportResponse RequestPaymentReportBySettlement(PaymentReportBySettlementRequest paymentReportBySettlementRequest, int settlementId)
        {
            PaymentReportResponse res = new PaymentReportResponse();
            try
            {
                // Validate request payment report by settlement
                if (!ValidateRequestPaymentReportBySettlement(res, paymentReportBySettlementRequest, settlementId))
                {
                    return res;
                }

                // Handle request payment report by settlement
                res = HandleRequestPaymentReportBySettlement(JsonConvert.SerializeObject(paymentReportBySettlementRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }), settlementId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        /// <summary>
        /// A form post requested from the user's browser
        /// </summary>
        /// <see>https://docs.paytrail.com/#/?id=add-card-form</see>
        /// <param name="addCardFormRequest">A AddCardFormRequest class instance</param>
        /// <returns>AddCardFormResponse</returns>
        public AddCardFormResponse CreateAddCardFormRequest(AddCardFormRequest addCardFormRequest)
        {
            AddCardFormResponse res = new AddCardFormResponse();
            try
            {
                // Validate add card form request
                if (!ValidateAddCardFormRequest(res, addCardFormRequest))
                {
                    return res;
                }

                // Handle add card form request
                res = HandleAddCardFormRequest(JsonConvert.SerializeObject(addCardFormRequest, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }));
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ResponseMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        public override bool ValidateHmac(Dictionary<string, string> hparams, string body = "", string signature = "")
        {
            throw new NotImplementedException();
        }

        private PaymentResponse CreatePayment(string bodyContent)
        {
            PaymentResponse res = new PaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST");

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<PaymentData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GetPaymentResponse GetPayment(string transactionId)
        {
            GetPaymentResponse res = new GetPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("GET", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/" + transactionId;
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Get);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<GetPaymentData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = "Detail: " + response.Content + JsonConvert.SerializeObject(request) + " . Response: " + JsonConvert.SerializeObject(response);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RefundResponse CreateRefundRequest(string bodyContent, string transactionId)
        {
            RefundResponse res = new RefundResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/" + transactionId + "/refund";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<RefundData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PayAddCardResponse CreatePayAndAddCardRequest(string bodyContent)
        {
            PayAddCardResponse res = new PayAddCardResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST");

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                // TODO: update to string builder
                string url = API_ENDPOINT + "/tokenization/pay-and-add-card";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<PayAddCardData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GetPaymentProvidersResponse HandleGetPaymentProviders(GetPaymentProvidersRequest getPaymentProvidersRequest)
        {
            GetPaymentProvidersResponse res = new GetPaymentProvidersResponse();

            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("GET");

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + $"/merchants/payment-providers?{getPaymentProvidersRequest.ToString()}";

                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Get);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<List<GetPaymentProvidersData>>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GetGroupedPaymentProvidersResponse HandleGetGroupedPaymentProviders(GetGroupedPaymentProvidersRequest getGroupedPaymentProvidersRequest)
        {
            GetGroupedPaymentProvidersResponse res = new GetGroupedPaymentProvidersResponse();

            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("GET");

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + $"/merchants/grouped-payment-providers?{getGroupedPaymentProvidersRequest.ToString()}";

                Console.WriteLine(url);

                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Get);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<GetGroupedPaymentProvidersData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private EmailRefundResponse HandleEmailRefund(string bodyContent, string transactionId)
        {
            EmailRefundResponse res = new EmailRefundResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/" + transactionId + "/refund/email";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<EmailRefundData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GetTokenResponse HandleCreateGetTokenRequest(GetTokenRequest getTokenRequest)
        {
            GetTokenResponse res = new GetTokenResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", null, getTokenRequest.CheckoutTokenizationId);

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/tokenization/" + getTokenRequest.CheckoutTokenizationId;
                RestClient client = new RestClient();

                RestRequest request = SetHeaders(hdparams, url, Method.Post);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<GetTokenData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SettlementsResponse HandleRequestSettlements(SettlementsRequest settlementsRequest)
        {
            SettlementsResponse res = new SettlementsResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("GET");

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + $"/settlements?{settlementsRequest.ToString()}";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Get);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<List<SettlementsData>>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PaymentReportResponse HandleRequestPaymentReport(string bodyContent)
        {
            PaymentReportResponse res = new PaymentReportResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST");

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                // TODO: update to string builder
                string url = API_ENDPOINT + "/payments/report";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<PaymentReportData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CreateMitOrCitPaymentResponse HandleCreateMitPaymentCharge(string bodyContent, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/token/mit/charge";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<CreateMitPaymentChargeData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CreateMitOrCitPaymentResponse HandleCreateMitPaymentAuthorizationHold(string bodyContent, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/token/mit/authorization-hold";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<CreateMitPaymentChargeData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CreateMitOrCitPaymentResponse HandleCreateCitPaymentCharge(string bodyContent, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/token/cit/charge";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<CreateMitPaymentChargeData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CreateMitOrCitPaymentResponse HandleCreateCitPaymentAuthorizationHold(string bodyContent, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/token/cit/authorization-hold";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<CreateMitPaymentChargeData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CreateMitOrCitPaymentResponse HandleCreateMitOrCitPaymentCommit(string bodyContent, string transactionId)
        {
            CreateMitOrCitPaymentResponse res = new CreateMitOrCitPaymentResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/" + transactionId + "/token/commit";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<CreateMitPaymentChargeData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RevertAuthorizationHoldResponse HandleRevertPaymentAuthorizationHold(string transactionId)
        {
            RevertAuthorizationHoldResponse res = new RevertAuthorizationHoldResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST", transactionId);

                // Add signature for header
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/payments/" + transactionId + "/token/revert";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<RevertAuthorizationHoldData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PaymentReportResponse HandleRequestPaymentReportBySettlement(string bodyContent, int settlementId)
        {
            PaymentReportResponse res = new PaymentReportResponse();
            try
            {
                // Create header
                Dictionary<string, string> hdparams = GetHeaders("POST");

                // Add signature for header
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ResponseMessage.SignatureNull;
                    res.ReturnMessage = ResponseMessage.SignatureNull.GetEnumDescription();
                    return res;
                }
                hdparams = GetHeaders(hdparams, "signature", signature);

                // Create new request
                string url = API_ENDPOINT + "/settlements/" + settlementId + "/payments/report";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = JsonConvert.DeserializeObject<PaymentReportData>(response.Content);
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private AddCardFormResponse HandleAddCardFormRequest(string bodyContent)
        {
            AddCardFormResponse res = new AddCardFormResponse();
            try
            {
                // Create new request
                string url = API_ENDPOINT + "/tokenization/addcard-form";
                RestClient client = new RestClient();
                RestRequest request = new RestRequest(url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                // Execute to Paytrail API 
                RestResponse response = client.Execute(request) as RestResponse;
                if (!ValidateResponse(response, res))
                    return res;

                res.Data = new AddCardFormData
                {
                    Content = response.Content
                };
                res.ReturnCode = (int)ResponseMessage.Success;
                res.ReturnMessage = ResponseMessage.Success.GetEnumDescription();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Validate Methods
        private bool ValidateRefundRequest(RefundResponse res, RefundRequest refundRequest, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "transactionId can not be null";
                return false;
            }

            if (refundRequest is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "Refund request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = refundRequest.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateCreateSiSPaymentRequest(PaymentResponse res, ShopInShopPaymentRequest paymentRequest)
        {
            if (paymentRequest is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "Shop in shop payment request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = paymentRequest.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateCreatePaymentRequest(PaymentResponse res, PaymentRequest paymentRequest)
        {
            if (paymentRequest is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "Payment request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = paymentRequest.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidatePayAndAddCardRequest(PayAddCardResponse response, PayAddCardRequest request)
        {
            if (request is null)
            {
                response.ReturnCode = (int)ResponseMessage.RequestNull;
                response.ReturnMessage = "Payment request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = request.Validate();
            if (!isValid)
            {
                response.ReturnCode = (int)ResponseMessage.ValidateFail;
                response.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateGetPaymentProviders(GetPaymentProvidersResponse res, GetPaymentProvidersRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "Get payment providers request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateGetGroupdPaymentProviders(GetGroupedPaymentProvidersResponse res, GetGroupedPaymentProvidersRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "Get payment grouped providers request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateEmailRefundRequest(EmailRefundResponse res, EmailRefundRequest req, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "transactionId can not be null";
                return false;
            }

            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "email refund request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateCreateGetTokenRequest(GetTokenResponse res, GetTokenRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "create get token request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateRequestSettlements(SettlementsResponse res, SettlementsRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "request settlements can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateRequestPaymentReport(PaymentReportResponse res, PaymentReportRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "payment report request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateCreateMitOrCitPaymentRequest(CreateMitOrCitPaymentResponse res, CreateMitOrCitPaymentRequest req, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "transactionId can not be null";
                return false;
            }

            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "create mit payment charge request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }
        private bool ValidateRevertPaymentAuthorizationHoldRequest(RevertAuthorizationHoldResponse res, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "transactionId can not be null";
                return false;
            }

            return true;
        }

        private bool ValidateRequestPaymentReportBySettlement(PaymentReportResponse res, PaymentReportBySettlementRequest req, int? settlementId)
        {
            if (!settlementId.HasValue)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "settlementId can not be null";
                return false;
            }

            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "payment report by settlement request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateAddCardFormRequest(AddCardFormResponse res, AddCardFormRequest req)
        {
            if (req is null)
            {
                res.ReturnCode = (int)ResponseMessage.RequestNull;
                res.ReturnMessage = "add card form request can not be null";
                return false;
            }

            (bool isValid, StringBuilder valMess) = req.Validate();
            if (!isValid)
            {
                res.ReturnCode = (int)ResponseMessage.ValidateFail;
                res.ReturnMessage = valMess.ToString();
                return false;
            }

            return true;
        }

        private bool ValidateResponse(RestResponse response, Response res)
        {
            if (response == null)
            {
                res.ReturnCode = (int)ResponseMessage.ResponseNull;
                res.ReturnMessage = ResponseMessage.ResponseNull.GetEnumDescription();
                return false;
            }
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                res.ReturnCode = (int)ResponseMessage.ResponseError;
                res.ReturnMessage = "Call service return: " + response.ErrorMessage + " Detail: " + JsonConvert.SerializeObject(response);
                return false;
            }

            return true;
        }
        #endregion
    }
}
