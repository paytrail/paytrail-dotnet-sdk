using System;
using System.ComponentModel;

namespace Paytrail_dotnet_sdk.Util
{
    public enum ResponseMessage
    {
        [Description("Success")]                    
        Success = 200,
        [Description("Exception")]            
        Exception = 503,
        [Description("Paytrail Server Return Null")]
        ResponseNull = 404,
        [Description("Paytrail Server Error")]          
        ResponseError = 500,
        [Description("Request Body Is Null")]       
        RequestNull = 400,
        [Description("Validation Failed")]
        ValidateFail = 403,
        [Description("Unauthorized")]
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
