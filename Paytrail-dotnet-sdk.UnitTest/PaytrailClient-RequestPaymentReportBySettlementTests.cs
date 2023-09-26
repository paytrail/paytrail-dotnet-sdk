using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_RequestPaymentReportBySettlementTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Request Payment Report By Settlement

        [Fact]
        public void RequestPaymentReportBySettlement_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentReportBySettlementRequest? request = null;
            PaymentReportResponse res = payTrail.RequestPaymentReportBySettlement(request, 375917);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestPaymentReportBySettlement_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentReportBySettlementRequest request = new PaymentReportBySettlementRequest();
            PaymentReportResponse res = payTrail.RequestPaymentReportBySettlement(request, 375917);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestPaymentReportBySettlement_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            PaymentReportBySettlementRequest request = new PaymentReportBySettlementRequest
            {
                RequestType = "json",
                CallbackUrl = "http://callback.example.com",
            };

            PaymentReportResponse res = payTrail.RequestPaymentReportBySettlement(request, 375917);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Request Payment Report By Settlement
}
