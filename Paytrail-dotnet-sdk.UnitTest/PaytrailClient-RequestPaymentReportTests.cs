using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_RequestPaymentReportTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Request Payment Report

        [Fact]
        public void RequestPaymentReport_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentReportRequest? request = null;
            PaymentReportResponse res = payTrail.RequestPaymentReport(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestPaymentReport_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentReportRequest request = new PaymentReportRequest();
            PaymentReportResponse res = payTrail.RequestPaymentReport(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestPaymentReport_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            PaymentReportRequest request = new PaymentReportRequest
            {
                RequestType = RequestType.csv.ToString(),
                CallbackUrl = "https://ecom.example.org/refund/success",
                PaymentStatus = PaymentStatus.@default.ToString(),
                StartDate = "2023-07-16T02:32:23.894Z",
                EndDate = "2023-08-16T02:32:23.894Z",
                Limit = 5000,
            };

            PaymentReportResponse res = payTrail.RequestPaymentReport(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestPaymentReport_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            PaymentReportRequest request = new PaymentReportRequest
            {
                RequestType = RequestType.csv.ToString(),
                CallbackUrl = "https://ecom.example.org/refund/success",
                PaymentStatus = PaymentStatus.@default.ToString(),
                StartDate = "2023-07-16T02:32:23.894Z",
                EndDate = "2023-08-16T02:32:23.894Z",
                Limit = 5000,
            };

            PaymentReportResponse res = payTrail.RequestPaymentReport(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Request Payment Report
}
