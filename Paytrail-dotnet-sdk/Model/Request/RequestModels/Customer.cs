using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Customer
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string VatId { get; set; }
        public string CompanyName { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (Email is null)
                {
                    ret = false;
                    message.Append(" customer's email can't be null.");
                }
                else
                {
                    if (Email.Length > 200)
                    {
                        ret = false;
                        message.Append(" customer's email is more than 100 characters.");
                    }
                }

                //
                if (FirstName is null)
                {
                    ret = false;
                    message.Append(" customer's firstName can't be null.");
                }
                else
                {
                    if (FirstName.Length > 50)
                    {
                        ret = false;
                        message.Append(" customer's firstName is more than 100 characters.");
                    }
                }

                //
                if (LastName is null)
                {
                    ret = false;
                    message.Append(" customer's lastName can't be null.");
                }
                else
                {
                    if (LastName.Length > 50)
                    {
                        ret = false;
                        message.Append(" customer's lastName is more than 100 characters.");
                    }
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
