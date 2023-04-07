﻿using Newtonsoft.Json;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PayTrailClient_GetPaymentTests
    {
        #region Get Payment Info

        const string MERCHANTIDN = "375917";
        const string SECRETKEYN = "SAIPPUAKAUPPIAS";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        [Fact]
        public void GetPayment_RequestNull_ReturnCode200()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            string request = null;
            GetPaymentResponse res = payTrail.GetPaymentInfo(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            //Assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void GetPayment_Success_ReturnCode1()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PayTrailClient ptrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            GetPaymentResponse res = ptrail.GetPaymentInfo("4bd78702-c9ea-11ed-beb7-ab93e0fdf0aa");
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            string data = JsonConvert.SerializeObject(res.data);

            //Assert
            Assert.Equal(expected, actual);

        }



        [Fact]
        public void GetPayment_CallPayTrailReturnNull_ReturnCode300()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");

            GetPaymentResponse res = payTrail.GetPaymentInfo("");
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GetPayment_CallPayTrailReturnFail_ReturnCode301()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PayTrailClient ptrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            GetPaymentResponse res = ptrail.GetPaymentInfo("2b67605c-c948-11ed-94a5-43738adc3579");
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            string data = JsonConvert.SerializeObject(res.data);

            //Assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void GetPayment_CallPayException_ReturnCode100()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Exception;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");

            GetPaymentResponse res = payTrail.GetPaymentInfo("");
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);

        }
        #endregion Get Payment Info
    }
}
