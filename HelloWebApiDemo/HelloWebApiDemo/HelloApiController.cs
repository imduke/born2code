﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HelloWebApiDemo
{
    public class HelloApiController : ApiController
    {
        public string Get()
        {
            return "From Hello API Controller";
        }
    }
}