using EastAdvising.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace EastAdvising.Controllers
{
    [Authorize]
    public class AccountController
    {

        private UserManager<Advisor> userManager;
        private SignInManager<Advisor> signInManager;


        public AccountController(UserManager<Advisor> userMgr,
        SignInManager<Advisor> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        
        //Google login stuff goes here

    }
}
