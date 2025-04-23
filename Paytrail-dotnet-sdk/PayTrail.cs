using Paytrail_dotnet_sdk.Util;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Paytrail_dotnet_sdk
{
    public abstract class Paytrail
    {
        public string merchantId;
        public string secretKey;
        public string platformName;

        public abstract bool ValidateHmac(Dictionary<string, string> hparams, string body = "", string signature = "");

        public Dictionary<string, string> GetHeaders(string method, string transactionId = null, string checkoutTokenizationId = null)
        {
            var headers = new Dictionary<string, string>();
            try
            {
                headers["checkout-account"] = this.merchantId;
                headers["checkout-algorithm"] = "sha256";
                headers["checkout-method"] = method.ToUpper();
                headers["checkout-nonce"] = Guid.NewGuid().ToString();
                string iso8601String = DateTimeOffset.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz");
                headers["checkout-timestamp"] = iso8601String;
                headers["platform-name"] = this.platformName;
                headers["content-type"] = "application/json; charset=utf-8";

                if (!string.IsNullOrEmpty(transactionId))
                {
                    headers["checkout-transaction-id"] = transactionId;
                }

                if (!string.IsNullOrEmpty(checkoutTokenizationId))
                {
                    headers["checkout-tokenization-id"] = checkoutTokenizationId;
                }

                return headers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Dictionary<string, string> GetHeaders(Dictionary<string, string> headers, string headerName, string headerValue)
        {
            headers[headerName] = headerValue;
            return headers;
        }

        public RestRequest SetHeaders(Dictionary<string, string> hdparams, string url, Method method)
        {
            RestRequest request = new RestRequest(url, method);
            if (hdparams.ContainsKey("checkout-account"))
            {
                request.AddHeader("checkout-account", hdparams["checkout-account"]);
            }
            if (hdparams.ContainsKey("checkout-algorithm"))
            {
                request.AddHeader("checkout-algorithm", hdparams["checkout-algorithm"]);
            }
            if (hdparams.ContainsKey("checkout-method"))
            {
                request.AddHeader("checkout-method", hdparams["checkout-method"]);
            }
            if (hdparams.ContainsKey("checkout-nonce"))
            {
                request.AddHeader("checkout-nonce", hdparams["checkout-nonce"]);
            }
            if (hdparams.ContainsKey("checkout-timestamp"))
            {
                request.AddHeader("checkout-timestamp", hdparams["checkout-timestamp"]);
            }
            if (hdparams.ContainsKey("platform-name"))
            {
                request.AddHeader("platform-name", hdparams["platform-name"]);
            }
            if (hdparams.ContainsKey("content-type") && method != Method.Get)
            {
                request.AddHeader("content-type", hdparams["content-type"]);
            }
            if (hdparams.ContainsKey("checkout-transaction-id"))
            {
                request.AddHeader("checkout-transaction-id", hdparams["checkout-transaction-id"]);
            }
            if (hdparams.ContainsKey("checkout-tokenization-id"))
            {
                request.AddHeader("checkout-tokenization-id", hdparams["checkout-tokenization-id"]);
            }
            if (hdparams.ContainsKey("signature"))
            {
                request.AddHeader("signature", hdparams["signature"]);
            }

            return request;
        }

        public string CalculateHmac(Dictionary<string, string> hparams, string body = "")
        {
            return Signature.CalculateHmac(this.secretKey, hparams, body);
        }
    }
}
