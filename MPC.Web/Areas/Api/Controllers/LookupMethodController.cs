using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class LookupMethodController : ApiController
    {
        // GET: Api/LookupMethod
        public LookupMethodResponse Get(long id)
        {


            return new LookupMethodResponse { };
        }
    }
}