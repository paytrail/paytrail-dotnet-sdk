using Paytrail_dotnet_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Paytrail_dotnet_sdk.Util
{
    public class Signature
    {
        static readonly string[] supportedEnc = { "sha256", "sha512" };
        private static string ComputeShaHash(string message, string secret, string encType = "sha256")
        {
            //if (!Crypto.supportedEnc.Any(e => e.Equals(encType, StringComparison.InvariantCultureIgnoreCase)))
            //{
            //    throw new Exception("Not supported encryption");
            //}

            var key = Encoding.UTF8.GetBytes(secret);
            string outMsg = "";
            if (encType.Equals("sha512", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var hmac = new HMACSHA512(key))
                {
                    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                    outMsg = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            else
            {
                using (var hmac = new HMACSHA256(key))
                {
                    // ComputeHash - returns byte array
                    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                    outMsg = BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            return outMsg;
        }

        public static string CalculateHmac(string secret, Dictionary<string, string> hparams, string body = "", string encType = "sha256")
        {
            // Keep only checkout- params, more relevant for response validation.Filter query
            // string parameters the same way - the signature includes only checkout- values.
            // Keys must be sorted alphabetically
            var includedKeys = hparams.Where(h => h.Key.StartsWith("checkout-")).OrderBy(h => h.Key).ToList();
            List<string> data = new List<string>();
            foreach (var pair in includedKeys)
            {
                var row = String.Format("{0}:{1}", pair.Key, hparams[pair.Key]);
                data.Add(row);
            }
            data.Add(body);

            return ComputeShaHash(string.Join("\n", data.ToArray()), secret, encType);
        }

        public static bool ValidateHmac(Dictionary<string, string> hparams, string body = "", string signature = "", string secretKey = "")
        {
            try
            {
                var hmac = CalculateHmac(secretKey, hparams, body);
                if (hmac != signature)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
