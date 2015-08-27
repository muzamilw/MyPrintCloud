using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class UMController : ApiController
    {
        private readonly ICompanyService _mycompanyservice;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public UMController(ICompanyService _mycompanyservice, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._mycompanyservice = _mycompanyservice;
            this._myClaimHelper = _myClaimHelper;
        }

        [HttpGet]
        public HttpResponseMessage UserProfileData(long ContactID)
        {
            jsonResponse obj = new jsonResponse();
            CompanyContact LoginContact = _mycompanyservice.GetContactByID(ContactID);

            obj.CompanyTerritory = _mycompanyservice.GetAllCompanyTerritories(UserCookieManager.WBStoreId).ToList();
            obj.RegistrationQuestions = _mycompanyservice.GetAllQuestions().ToList();
            obj.Addresses = _mycompanyservice.GetAddressesByTerritoryID(LoginContact.TerritoryId??0);
            List<CompanyContactRole> roles = null;
          //  if (LoginContact.ContactRoleId == (int)Roles.Manager)
           // {
           //     roles = _mycompanyservice.GetContactRolesExceptAdmin((int)Roles.Adminstrator);
           // }
           // else
           // {
                roles = _mycompanyservice.GetAllContactRoles();
           // }
            obj.CompanyContactRoles = roles;
            Address LoginContactAddress = _mycompanyservice.GetAddressByID(LoginContact.AddressId);
            obj.LoginContact = LoginContact;
            obj.SelectedShippingAddress = _mycompanyservice.GetAddressByID(LoginContact.ShippingAddressId ?? 0);
            obj.SelectedBillingAddress = _mycompanyservice.GetAddressByID(LoginContact.AddressId);
            obj.SelectedQuestion = _mycompanyservice.GetSecretQuestionByID(LoginContact.QuestionId ?? 0);
            obj.setSelectedTerritory = _mycompanyservice.GetTerritoryById(LoginContact.TerritoryId??0);
            obj.SelectedRole = _mycompanyservice.GetRoleByID(LoginContact.ContactRoleId ?? 0);
            obj.SelectedShipppingState = _mycompanyservice.GetStateNameById(obj.SelectedShippingAddress.StateId??0);
            obj.SelectedBillingState = _mycompanyservice.GetStateNameById(obj.SelectedBillingAddress.StateId ?? 0);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }
        [HttpGet]
        public HttpResponseMessage LoadDDData()
        {
            jsonResponse obj = new jsonResponse();
            obj.CompanyTerritory = _mycompanyservice.GetAllCompanyTerritories(UserCookieManager.WBStoreId).ToList();
            obj.RegistrationQuestions = _mycompanyservice.GetAllQuestions().ToList();
            List<CompanyContactRole> roles = null;
            roles = _mycompanyservice.GetAllContactRoles();
            obj.CompanyContactRoles = roles;
            obj.Addresses = _mycompanyservice.GetAddressesByTerritoryID(obj.CompanyTerritory.FirstOrDefault().TerritoryId);
            obj.setSelectedTerritory = _mycompanyservice.GetTerritoryById(obj.CompanyTerritory.FirstOrDefault().TerritoryId);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, obj, formatter);
        }
        
    }
    public class jsonResponse
    {
        public List<CompanyTerritory> CompanyTerritory;
        public List<RegistrationQuestion> RegistrationQuestions;
        public List<Address> Addresses;
        public List<CompanyContactRole> CompanyContactRoles;
        public CompanyContact LoginContact;
        public Address SelectedShippingAddress;
        public Address SelectedBillingAddress;
        public RegistrationQuestion SelectedQuestion;
        public CompanyTerritory setSelectedTerritory;
        public CompanyContactRole SelectedRole;
        public string SelectedShipppingState;
        public string SelectedBillingState;
        public Address GetAddress;
    }
}
