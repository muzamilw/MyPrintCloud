﻿using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
namespace MPC.Webstore.Controllers
{
    public class PersonalDetailAndOrderPolicyController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly ICompanyContactRepository _companyContact;
        private readonly IOrderService _OrderService;
        // GET: PersonalDetailAndOrderPolicy

        public PersonalDetailAndOrderPolicyController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICompanyContactRepository _companyContact, IOrderService _OrderService)
        {
            this._myCompanyService = _myCompanyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._companyContact = _companyContact;
            this._OrderService = _OrderService;
        }
        public ActionResult Index()
        {

            CompanyContact Contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());      

            Estimate LastOrder = _OrderService.GetLastOrderByContactId(_webstoreAuthorizationChecker.loginContactID());
            Company CorpCompany = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());

            CompanyTerritory CompTerritory = _myCompanyService.GetCcompanyByTerritoryID(_webstoreAuthorizationChecker.loginContactCompanyID());

            ViewBag.CorpCompany = CorpCompany;
            ViewBag.CompTerritory = CompTerritory;
            ViewBag.Order = LastOrder;

            if (Contact != null)
            {
                return View("PartialViews/PersonalDetailAndOrderPolicy",Contact);
            }
            else
            {

                return View("PartialViews/PersonalDetailAndOrderPolicy",Contact);
            }
        }

        
        public ActionResult SaveOrderPolicy(string id)
        {
            Company CorpCompany = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
            CorpCompany.CorporateOrderingPolicy = id;
           _myCompanyService.UpdateCompanyOrderingPolicy(CorpCompany);
            return View();
        }
    }
}