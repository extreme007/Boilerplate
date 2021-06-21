﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController :  Controller
    {
        [Route("404.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
