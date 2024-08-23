using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Item
    {
        public int UnitPrice { get; set; }
        public int Units { get; set; }
        public double VatPercentage { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        //public string orderId { get; set; }
        public string Stamp { get; set; }
        public string Reference { get; set; }
        //public string merchant { get; set; }
        //public Commission commission { get; set; }
        public virtual (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            //
            try
            {
                //
                if (Units < 0)
                {
                    ret = false;
                    message.Append(" item's units can't be a negative number.");
                }

                //
                if (ProductCode is null)
                {
                    ret = false;
                    message.Append(" item's productCode can't be null.");
                }
                else
                {
                    if (ProductCode.Length > 100)
                    {
                        ret = false;
                        message.Append(" item's productCode should have less than 100 characters.");
                    }
                }

                //
                if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
                {
                    ret = false;
                    message.Append(" item's description should have less than 1000 characters.");
                }

                //Merchant specific item category. Maximum of 100 characters.
                if (!string.IsNullOrEmpty(Category) && Category.Length > 100)
                {
                    ret = false;
                    message.Append(" item's Category should have less than 100 characters.");
                }
                // Check if value is within the specified range
                if (VatPercentage < 0 || VatPercentage > 100)
                {
                    ret = false;
                    message.Append("VAT percentage. Values between 0 and 100 are allowed with one number in decimal part.");
                    
                }

                // Check if value has one decimal place
                string[] parts = VatPercentage.ToString().Split('.');

                // If there is a decimal part and its length is 1, return true
                bool hasOneDecimal =  parts.Length == 2 && parts[1].Length == 1;
                if (!hasOneDecimal)
                {
                    ret = false;
                    message.Append("VAT percentage. Values between 0 and 100 are allowed with one number in decimal part.");
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
