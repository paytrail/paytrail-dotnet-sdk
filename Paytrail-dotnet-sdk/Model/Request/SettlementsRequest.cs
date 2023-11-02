using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class SettlementsRequest
    {
        public string BankReference { get; set; }
        public int Limit { get; set; }
        public int Submerchant { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            try
            {
                if (!string.IsNullOrEmpty(StartDate) && !DateTime.TryParseExact(StartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    ret = false;
                    message.Append(" startDate must be in yyyy-MM-dd format.");
                }

                if (!string.IsNullOrEmpty(EndDate) && !DateTime.TryParseExact(EndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    ret = false;
                    message.Append(" endDate must be in yyyy-MM-dd format.");
                }

                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    var startDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", null);
                    var endDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", null);

                    if (startDate > endDate)
                    {
                        ret = false;
                        message.Append(" startDate cannot be later than endDate.");
                    }
                }

                if (Limit < 0)
                {
                    ret = false;
                    message.Append(" Limit must have a minimum value of 0.");
                }

                return (ret, message);
            }
            catch (Exception ex)
            {
                message.Append(" " + ex.Message + ".");
                return (false, message);
            }
        }

        public override string ToString()
        {
            string query = "";

            if (!String.IsNullOrEmpty(BankReference))
            {
                query += $"&bankReference={BankReference}";
            }

            if (Limit > 0)
            {
                query += $"&limit={Limit}";
            }

            if (Submerchant > 0)
            {
                query += $"&submerchant={Submerchant}";
            }

            if (!String.IsNullOrEmpty(StartDate))
            {
                query += $"&startDate={StartDate}";
            }

            if (!String.IsNullOrEmpty(EndDate))
            {
                query += $"&endDate={EndDate}";
            }

            return query;
        }
    }
}
