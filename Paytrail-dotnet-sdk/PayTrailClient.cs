using Newtonsoft.Json;
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

        public PaymentResponse CreatePayment(PaymentRequest paymentRequest)
        {
            PaymentResponse res = new PaymentResponse();
            if (paymentRequest == null)
            {
                res.ReturnCode = (int)ErrorMessage.RequestNull;
                res.ReturnMessage = ErrorMessage.RequestNull.GetEnumDescription();
                return res;
            }

            //
            try
            {
                (bool isSuccess, StringBuilder valMess) = paymentRequest.Validate();

                //validate before sending request
                if (isSuccess)
                {
                    res = Payment(JsonConvert.SerializeObject(paymentRequest));
                    return res;
                }
                else
                {
                    res.ReturnCode = (int)ErrorMessage.ValidateFail;
                    res.ReturnMessage = valMess.ToString();
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ErrorMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        public PaymentResponse CreateShopInShopPayment(ShopInShopPaymentRequest paymentRequest)
        {
            PaymentResponse res = new PaymentResponse();
            if (paymentRequest == null)
            {
                res.ReturnCode = (int)ErrorMessage.RequestNull;
                res.ReturnMessage = ErrorMessage.RequestNull.GetEnumDescription();
                return res;
            }

            //
            try
            {
                (bool isSuccess, StringBuilder valMess) = paymentRequest.Validate();
                //validate before sending request
                if (isSuccess)
                {
                    res = Payment(JsonConvert.SerializeObject(paymentRequest));
                    return res;
                }
                else
                {
                    res.ReturnCode = (int)ErrorMessage.ValidateFail;
                    res.ReturnMessage = valMess.ToString();
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ErrorMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        public GetPaymentResponse GetPaymentInfo(string transactionId)
        {
            GetPaymentResponse res = new GetPaymentResponse();
            if (string.IsNullOrEmpty(transactionId))
            {
                res.ReturnCode = (int)ErrorMessage.RequestNull;
                res.ReturnMessage = ErrorMessage.RequestNull.GetEnumDescription();
                return res;
            }

            //
            try
            {
                res = GetPayment(transactionId);
                return res;
            }
            catch (Exception ex)
            {
                res.ReturnCode = (int)ErrorMessage.Exception;
                res.ReturnMessage = ex.ToString();
                return res;
            }
        }

        public RefundResponse RefundPayment(RefundRequest refundRequest, string transactionId)
        {
            RefundResponse res = new RefundResponse();
            return res;
        }

        public override bool ValidateHmac(Dictionary<string, string> hparams, string body = "", string signature = "")
        {
            throw new NotImplementedException();
        }


        private PaymentResponse Payment(string bodyContent)
        {
            PaymentResponse res = new PaymentResponse();
            try
            {
                //get default headers
                Dictionary<string, string> hdparams = GetHeaders("POST");

                //sign data
                string signature = CalculateHmac(hdparams, bodyContent);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ErrorMessage.SignatureNull;
                    res.ReturnMessage = ErrorMessage.SignatureNull.GetEnumDescription();
                    return res;
                }

                //add signature into headers
                hdparams = GetHeaders(hdparams, "signature", signature);

                //create new request
                string url = API_ENDPOINT + "/payments";
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Post);
                request.AddParameter("application/json", bodyContent, ParameterType.RequestBody);

                //Response
                RestResponse response = client.Execute(request) as RestResponse;

                if (response == null)
                {
                    res.ReturnCode = (int)ErrorMessage.ResponseNull;
                    res.ReturnMessage = ErrorMessage.ResponseNull.GetEnumDescription();
                    return res;
                }

                //
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                {
                    res.ReturnCode = (int)ErrorMessage.ResponseError;
                    res.ReturnMessage = "Call service return: " + response.ErrorMessage + " Detail: " + JsonConvert.SerializeObject(response);
                    return res;
                }

                //
                try
                {
                    res.data = JsonConvert.DeserializeObject<PaymentData>(response.Content);
                }
                catch (Exception ex)
                {
                    res.ReturnCode = (int)ErrorMessage.Exception;
                    res.ReturnMessage = "Response's content: " + response.Content + ". Error: " + ex.Message;
                    return res;
                }

                res.ReturnCode = (int)ErrorMessage.Success;
                res.ReturnMessage = ErrorMessage.Success.GetEnumDescription();
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
                //get default headers
                Dictionary<string, string> hdparams = GetHeaders("GET", transactionId);

                // create request
                //sign data
                string signature = CalculateHmac(hdparams);
                if (string.IsNullOrEmpty(signature))
                {
                    res.ReturnCode = (int)ErrorMessage.SignatureNull;
                    res.ReturnMessage = ErrorMessage.SignatureNull.GetEnumDescription();
                    return res;
                }

                //add signature into headers
                hdparams = GetHeaders(hdparams, "signature", signature);

                //create new request
                string url = API_ENDPOINT + "/payments/" + transactionId;
                RestClient client = new RestClient();
                RestRequest request = SetHeaders(hdparams, url, Method.Get);

                //Response
                RestResponse response = client.Execute(request) as RestResponse;

                //response = null;

                if (response == null)
                {
                    res.ReturnCode = (int)ErrorMessage.ResponseNull;
                    res.ReturnMessage = ErrorMessage.ResponseNull.GetEnumDescription();
                    return res;
                }

                //
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                {
                    res.ReturnCode = (int)ErrorMessage.ResponseError;
                    res.ReturnMessage = "Call service return: " + response.StatusCode + " Detail: " + response.Content + JsonConvert.SerializeObject(request) + " . Response: " + JsonConvert.SerializeObject(response);
                    return res;
                }

                //
                try
                {
                    res.data = JsonConvert.DeserializeObject<GetPaymentData>(response.Content);
                }
                catch (Exception ex)
                {
                    res.ReturnCode = (int)ErrorMessage.Exception;
                    res.ReturnMessage = "Response's content: " + response.Content + ". Error: " + ex.Message;
                    return res;
                }
                res.ReturnCode = (int)ErrorMessage.Success;
                res.ReturnMessage = " Detail: " + response.Content + JsonConvert.SerializeObject(request) + " . Response: " + JsonConvert.SerializeObject(response);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
