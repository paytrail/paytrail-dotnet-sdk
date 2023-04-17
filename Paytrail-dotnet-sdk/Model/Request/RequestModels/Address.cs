using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Address
    {        
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (StreetAddress is null)
                {
                    message.Append(" address's streetAddress can't be null.");
                    ret = false;
                }
                else
                {
                    if (StreetAddress.Length > 50)
                    {
                        message.Append(" address's streetAddress is more than 100 characters.");
                        ret = false;
                    }
                }

                //
                if (PostalCode is null)
                {
                    message.Append(" address's postalCode can't be null");
                    ret = false;
                }
                else
                {
                    if (PostalCode.Length > 50)
                    {
                        message.Append(" address's postalCode is more than 15 characters.");
                        ret = false;
                    }
                }

                //
                if (City is null)
                {
                    message.Append(" address's city can't be null.");
                    ret = false;
                }
                else
                {
                    if (City.Length > 50)
                    {
                        message.Append(" address's city is more than 30 characters.");
                        ret = false;
                    }
                }

                //
                if (Country is null)
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
