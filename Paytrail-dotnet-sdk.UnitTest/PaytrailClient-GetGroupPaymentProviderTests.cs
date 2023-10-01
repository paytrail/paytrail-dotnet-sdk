using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_GetGroupPaymentProviderTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Get Group Payment Provider

        [Fact]
        public void GetGroupPaymentProvider_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            GetGroupedPaymentProvidersRequest? request = null;
            GetGroupedPaymentProvidersResponse res = payTrail.GetGroupedPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetGroupPaymentProvider_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            GetGroupedPaymentProvidersRequest request = new GetGroupedPaymentProvidersRequest();
            GetGroupedPaymentProvidersResponse res = payTrail.GetGroupedPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetGroupPaymentProvider_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            GetGroupedPaymentProvidersRequest request = new GetGroupedPaymentProvidersRequest
            {
                Amount = 100,
                Groups = new List<PaymentMethodGroup>()
                {
                    PaymentMethodGroup.creditcard,
                }
            };

            GetGroupedPaymentProvidersResponse res = payTrail.GetGroupedPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetGroupPaymentProvider_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            GetGroupedPaymentProvidersRequest request = new GetGroupedPaymentProvidersRequest
            {
                Amount = 100,
                Groups = new List<PaymentMethodGroup>()
                {
                    PaymentMethodGroup.creditcard,
                },
            };

            GetGroupedPaymentProvidersResponse res = payTrail.GetGroupedPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Get Group Payment Provider
}