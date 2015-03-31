using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.Areas.Api.ModelMappers;
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
        private readonly ILookupMethodService _LookupMethodService;
        public LookupMethodController(ILookupMethodService _LookupMethodService)
        {
            this._LookupMethodService = _LookupMethodService;
        }
        // GET: Api/LookupMethod
        public LookupMethodResponse Get(long MethodId)
        {

            return _LookupMethodService.GetlookupById(MethodId).CreateFrom();

        }

        public LookupMethodListResponse Get()
        {

            return _LookupMethodService.GetAll().CreateFrom();
            
        }
        public bool Post(LookupMethodResponse response)
        {
            
                return _LookupMethodService.UpdateLookup(response.CreateFrom());
           
            
          
            
        }
        public bool Delete(LookupDeleteRequestModel req)
        {
            if (req.GuillotinePTVId > 0)
            {
                return _LookupMethodService.DeleteGuillotinePTVId(req.GuillotinePTVId);
            }
            else
            {
                return _LookupMethodService.DeleteMachineLookup(req.LookupMethodId);
            }
           
           
        }
        
        public LookupMethod Put(LookupMethodResponse response)
        {


            return _LookupMethodService.AddLookup(response.CreateFrom()).CreateFrom();

        }

    }
}