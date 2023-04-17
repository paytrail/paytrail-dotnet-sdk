using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Item
    {
        public int UnitPrice { get; set; }
        public int Units { get; set; }
        public int VatPercentage { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

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
                if (UnitPrice < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

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
                        message.Append(" item's productCode is more than 100 characters.");
                    }
                }

                //
                if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
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
