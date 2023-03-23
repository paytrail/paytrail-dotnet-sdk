using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Paytrail_dotnet_sdk.Util
{
    public enum ErrorMessage
    {
        [Description("Success")]
        Success = 1,
        [Description("Exception")]
        Exception = 100,
        [Description("Call service return null")]
        ResponseNull = 300,
        [Description("Call service return error")]
        ResponseError = 301,
        [Description("Request is null")]
        RequestNull = 200,
        [Description("Validation return fail")]
        ValidateFail = 201,
        [Description("Signature is null")]
        SignatureNull = 202
        
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
