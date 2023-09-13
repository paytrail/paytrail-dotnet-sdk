using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class PaymentReportRequest
    {
        public string RequestType { get; set; }
        public string CallbackUrl { get; set; }
        public string PaymentStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Limit { get; set; }
        public List<string> ReportFields { get; set; }
        public int Submerchant { get; set; }
        public bool IncludeItems { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            try
            {
                if (String.IsNullOrEmpty(RequestType))
                {
                    ret = false;
                    message.Append(" RequestType cannot be empty.");
                }

                if (string.IsNullOrEmpty(CallbackUrl))
                {
                    ret = false;
                    message.Append(" CallbackUrl cannot be empty.");
                }

                if (Limit > 50000)
                {
                    ret = false;
                    message.Append(" Limit exceeds the maximum value of 50000.");
                }

                if (Limit < 0)
                {
                    ret = false; 
                    message.Append(" Limit must have a minimum value of 0.");
                }

                if (string.IsNullOrEmpty(StartDate) && !Regex.IsMatch(StartDate, @"^\d{4}(-\d{2}){2}T\d{2}(:\d{2}){2}(\.\d+)?\+\d{2}:\d{2}$"))
                {
                    ret = false;
                    message.Append(" StartDate must be in ATOM, ISO8601, or RFC3339 format.");
                }

                if (string.IsNullOrEmpty(EndDate) && !Regex.IsMatch(EndDate, @"^\d{4}(-\d{2}){2}T\d{2}(:\d{2}){2}(\.\d+)?\+\d{2}:\d{2}$"))
                {
                    ret = false;
                    message.Append(" EndDate must be in ATOM, ISO8601, or RFC3339 format.");
                }

                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate) && string.Compare(StartDate.Substring(0, 10), EndDate.Substring(0, 10)) > 0)
                {
                    ret = false;
                    message.Append(" StartDate cannot be later than EndDate.");
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
