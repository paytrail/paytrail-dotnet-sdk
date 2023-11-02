using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Linq;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class AddCardFormRequest
    {
        [JsonProperty("checkout-account")]
        public int? CheckoutAccount { get; set; }

        [JsonProperty("checkout-algorithm")]
        public string CheckoutAlgorithm { get; set; }

        [JsonProperty("checkout-method")]
        public string CheckoutMethod { get; set; }

        [JsonProperty("checkout-nonce")]
        public string CheckoutNonce { get; set; }

        [JsonProperty("checkout-timestamp")]
        public string CheckoutTimestamp { get; set; }

        [JsonProperty("checkout-redirect-success-url")]
        public string CheckoutRedirectSuccessUrl { get; set; }

        [JsonProperty("checkout-redirect-cancel-url")]
        public string CheckoutRedirectCancelUrl { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("checkout-callback-success-url")]
        public string CheckoutCallbackSuccessUrl { get; set; }

        [JsonProperty("checkout-callback-cancel-url")]
        public string CheckoutCallbackCancelUrl { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            bool ret = true;
            try
            {
                string[] supportedLanguages = { "FI", "SV", "EN" };
                string[] supportedMethods = { "GET", "POST" };

                if (!CheckoutAccount.HasValue)
                {
                    ret = false;
                    message.Append("checkout-account is empty. ");
                }

                if (string.IsNullOrEmpty(CheckoutAlgorithm))
                {
                    ret = false;
                    message.Append(" checkout-algorithm is empty. ");
                }

                if (!supportedMethods.Contains(CheckoutMethod))
                {
                    ret = false;
                    message.Append(" unsupported method chosen. ");
                }

                if (string.IsNullOrEmpty(CheckoutTimestamp))
                {
                    ret = false;
                    message.Append(" checkout-timestamp is empty. ");
                }

                if (string.IsNullOrEmpty(CheckoutRedirectSuccessUrl))
                {
                    ret = false;
                    message.Append(" checkout-redirect success url is empty. ");
                }

                if (string.IsNullOrEmpty(CheckoutRedirectCancelUrl))
                {
                    ret = false;
                    message.Append(" checkout-redirect cancel url is empty. ");
                }

                if (!supportedLanguages.Contains(Language))
                {
                    ret = false;
                    message.Append(" unsupported language chosen. ");
                }

                return (ret, message);
            }
            catch (Exception ex)
            {
                message.Append(" " + ex.Message + ".");
                return (false, message);
            }

        }
    }
}
