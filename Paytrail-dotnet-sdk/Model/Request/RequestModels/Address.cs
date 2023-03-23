using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Address
    {
        public string streetAddress { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string country { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (streetAddress is null)
                {
                    message.Append(" address's streetAddress can't be null.");
                    ret = false;
                }
                else
                {
                    if (streetAddress.Length > 50)
                    {
                        message.Append(" address's streetAddress is more than 100 characters.");
                        ret = false;
                    }
                }

                //
                if (postalCode is null)
                {
                    message.Append(" address's postalCode can't be null");
                    ret = false;
                }
                else
                {
                    if (postalCode.Length > 50)
                    {
                        message.Append(" address's postalCode is more than 15 characters.");
                        ret = false;
                    }
                }

                //
                if (city is null)
                {
                    message.Append(" address's city can't be null.");
                    ret = false;
                }
                else
                {
                    if (city.Length > 50)
                    {
                        message.Append(" address's city is more than 30 characters.");
                        ret = false;
                    }
                }

                //
                if (country is null)
                {
                    message.Append(" address's country can't be null.");
                    ret = false;
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
