using System;
using System.ComponentModel;

namespace Paytrail_dotnet_sdk.Util
{
    public enum ResponseMessage
    {
        [Description("Success")]
        Success = 200,
        [Description("Exception")]  // Exception from sdk
        Exception = 100,
        [Description("Call service return null")] // Error from paytrail API
        ResponseNull = 300,
        [Description("Call service return error")] // Exception from paytrail API
        ResponseError = 301,
        [Description("Request body is null")] // Validate
        RequestNull = 400,
        [Description("Validation failed")] // Validate
        ValidateFail = 403,
        [Description("Unauthorize")] // Authorize
        SignatureNull = 401
    }

    public static class Extension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }
    }
}
