using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.APIControllers
{
    public class SignUpController : ApiController
    {
        [HttpGet]
        public string Login(string email, string Password, string keepLoggedIn)
        {
           
            return "fail";
        }
    }
}
