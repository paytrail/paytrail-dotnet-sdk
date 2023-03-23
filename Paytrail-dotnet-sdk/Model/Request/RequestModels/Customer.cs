using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Customer
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string vatId { get; set; }
        public string companyName { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (email is null)
                {
                    ret = false;
                    message.Append(" customer's email can't be null.");
                }
                else
                {
                    if (email.Length > 200)
                    {
                        ret = false;
                        message.Append(" customer's email is more than 100 characters.");
                    }
                }

                //
                if (firstName is null)
                {
                    ret = false;
                    message.Append(" customer's firstName can't be null.");
                }
                else
                {
                    if (firstName.Length > 50)
                    {
                        ret = false;
                        message.Append(" customer's firstName is more than 100 characters.");
                    }
                }

                //
                if (lastName is null)
                {
                    ret = false;
                    message.Append(" customer's lastName can't be null.");
                }
                else
                {
                    if (lastName.Length > 50)
                    {
                        ret = false;
                        message.Append(" customer's lastName is more than 100 characters.");
                    }
                }

                //
                if (phone is null)
                {
                    ret = false;
                    message.Append(" customer's phone can't be null.");
                }

                //
                if (vatId is null)
                {
                    ret = false;
                    message.Append(" customer's vatId can't be null.");
                }

                //
                if (companyName is null)
                {
                    ret = false;
                    message.Append(" customer's companyName can't be null.");
                }

                //
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
