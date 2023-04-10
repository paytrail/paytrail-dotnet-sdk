using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class PaymentRequest: Request
    {
        public string stamp { get; set; }
        public string reference { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string language { get; set; }
        public string orderId { get; set; }
        public Item[] items { get; set; }
        public Customer customer { get; set; }
        public Address deliveryAddress { get; set; }
        public Address invoicingAddress { get; set; }
        public bool manualInvoiceActivation { get; set; }
        public CallbackUrl redirectUrls { get; set; }
        public CallbackUrl callbackUrls { get; set; }
        public int callbackDelay { get; set; }
        public string[] groups { get; set; }
        public bool usePricesWithoutVat { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            try
            {
                bool ret = true;
                if (string.IsNullOrEmpty(stamp))
                {
                    ret = false;
                    message.Append(" stamp can't be null or empty.");
                }
                else
                {
                    if (stamp.Length > 200)
                    {
                        ret = false;
                        message.Append(" stamp is more than 200 characters.");
                    }
                }

                //
                if (reference is null)
                {
                    ret = false;
                    message.Append(" reference can't be null.");
                }
                else
                {
                    if (reference.Length > 200)
                    {
                        ret = false;
                        message.Append(" reference is more than 200 characters.");
                    }
                }

                //
                if (orderId is null)
                {
                    ret = false;
                    message.Append(" orderId can't be null.");
                }

                //
                if (amount < 0)
                {
                    ret = false;
                    message.Append(" amount can't be less than zero.");
                }
                else
                {
                    if (amount > 99999999)
                    {
                        ret = false;
                        message.Append(" amount can't be more than 99999999.");
                    }
                }

                //
                if (customer is null)
                {
                    ret = false;
                    message.Append(" object customer can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = customer.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (items is null)
                {
                    ret = false;
                    message.Append(" object items can't be null.");
                }
                else
                {
                    foreach (var item in items)
                    {
                        (bool isSuccess, StringBuilder valMess) = item.Validate();
                        if (!isSuccess)
                        {
                            ret = false;
                            message.Append(valMess);
                            break;
                        }
                            
                    }
                }

                //
                if (deliveryAddress is null)
                {
                    ret = false;
                    message.Append(" object deliveryAddress can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = deliveryAddress.Validate();

                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (invoicingAddress is null)
                {
                    ret = false;
                    message.Append(" object invoicingAddress can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = invoicingAddress.Validate();

                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (redirectUrls is null)
                {
                    ret = false;
                    message.Append(" object redirectUrls can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = redirectUrls.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (callbackUrls is null)
                {
                    ret = false;
                    message.Append(" object callbackUrls can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = callbackUrls.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (groups is null)
                {
                    ret = false;
                    message.Append(" object groups can't be null.");
                }
                else
                {
                    for (int i = 0; i < groups.Length; i++)
                    {
                        bool flagContain = false;

                        foreach (var item in Enum.GetValues(typeof(PaymentMethodGroup)))
                        {
                            if (groups[i] == item.ToString())
                            {
                                flagContain = true;
                            }
                        }

                        //
                        if(!flagContain)
                        {
                            ret = false;
                            message.Append(" value " + groups[i] + " is not in list payment method");
                        }
                    }
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
