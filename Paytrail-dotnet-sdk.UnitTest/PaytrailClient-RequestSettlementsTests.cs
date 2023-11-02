using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_RequestSettlementsTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Request Settlements

        [Fact]
        public void RequestSettlements_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            SettlementsRequest? request = null;
            SettlementsResponse res = payTrail.GetSettlements(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestSettlements_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            SettlementsRequest request = new SettlementsRequest
            {
                BankReference = "45667372",
                StartDate = "2021-07-16",
                EndDate = "2023-08-16",
                Limit = 10,
            };

            SettlementsResponse res = payTrail.GetSettlements(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestSettlements_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            SettlementsRequest request = new SettlementsRequest
            {
                BankReference = "45667372",
                StartDate = "2021-07-16",
                EndDate = "2023-08-16",
                Limit = 10,
            };

            SettlementsResponse res = payTrail.GetSettlements(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Request Settlements
}
