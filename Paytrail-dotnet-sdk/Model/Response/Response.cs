﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public abstract class Response
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }
}
