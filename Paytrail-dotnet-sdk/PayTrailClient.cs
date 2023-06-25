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

namespace Paytrail_dotnet_sdk
{
    public class PayTrailClient : PayTrail, IPayTrail
    {
        public const string API_ENDPOINT = @"https://services.paytrail.com";

        public PayTrailClient(string merchantId, string secretKey, string platformName)
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
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
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
        /// <returns></returns>
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
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
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
        /// <returns></returns>
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
        /// <returns></returns>
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
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
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
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
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
                response = CreatePayAndAddCardRequest(JsonConvert.SerializeObject(request,new JsonSerializerSettings
                    {
                       ContractResolver = new CamelCasePropertyNamesContractResolver()
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
