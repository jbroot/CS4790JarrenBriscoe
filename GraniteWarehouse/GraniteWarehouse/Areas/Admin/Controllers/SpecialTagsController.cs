﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GraniteWarehouse.Areas.Admin.Controllers
{
    public class SpecialTagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}