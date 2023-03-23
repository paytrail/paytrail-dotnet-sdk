using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Item
    {
        public int unitPrice { get; set; }
        public int units { get; set; }
        public int vatPercentage { get; set; }
        public string productCode { get; set; }
        public string description { get; set; }
        public string category { get; set; }

        //public string orderId { get; set; }
        //public string stamp { get; set; }
        //public string reference { get; set; }
        //public string merchant { get; set; }
        //public Commission commission { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            //
            try
            {
                if (unitPrice < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                //
                if (units < 0)
                {
                    ret = false;
                    message.Append(" item's units can't be a negative number.");
                }

                //
                if (productCode is null)
                {
                    ret = false;
                    message.Append(" item's productCode can't be null.");
                }
                else
                {
                    if (productCode.Length > 100)
                    {
                        ret = false;
                        message.Append(" item's productCode is more than 100 characters.");
                    }
                }

                //
                if (!string.IsNullOrEmpty(description) && description.Length > 1000)
                {
                    ret = false;
                    message.Append(" item's description is more than 1000 characters.");
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
