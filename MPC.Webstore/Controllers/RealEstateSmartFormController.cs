using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using System.Runtime.Caching;
using MPC.Webstore.Common;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class RealEstateSmartFormController : Controller
    {

        public class SectionControls
        {
            private List<MPC.Models.Common.TemplateVariable> _controls;
            public string SectionName { get; set; }
            public List<MPC.Models.Common.TemplateVariable> Controls
            {
                get
                {
                    if (_controls == null)
                    {
                        _controls = new List<MPC.Models.Common.TemplateVariable>();

                    }

                    return _controls;
                }
                set
                {
                    _controls = value;
                }
            }
        }

        #region Private

        private readonly IListingService _myListingService;
        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;
        private readonly ICompanyService _myCompanyService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateSmartFormController(IListingService myListingService, IWebstoreClaimsHelperService webstoreClaimHelper
            , ICompanyService myCompanyService)
        {

            if (webstoreClaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
        
            if (myListingService == null)
            {
                throw new ArgumentNullException("myListingService");
            }

            this._myListingService = myListingService;
            this._webstoreclaimHelper = webstoreClaimHelper;
            this._myCompanyService = myCompanyService;
        }

        #endregion

        // GET: RealEstateSmartForm
        public ActionResult Index(long listingId, long itemId)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            //SessionParameters.CustomerContact.ContactID;
            //SessionParameters.ContactCompany.ContactCompanyID;
            //SessionParameters.CustomerContact.AddressID;
            //SessionParameters.ContactCompany.FlagID;
            //SessionParameters.CustomerContact.DepartmentID;
            long ContactID = _webstoreclaimHelper.loginContactID();
            long ContactCompanyID = _webstoreclaimHelper.loginContactCompanyID();
            long FlagID = StoreBaseResopnse.Company.FlagId;
            long AddressID = StoreBaseResopnse.StoreDetaultAddress.AddressId;

            List<MPC.Models.Common.TemplateVariable> lstVariableAndValue = new List<MPC.Models.Common.TemplateVariable>();
            List<MPC.Models.Common.TemplateVariable> lstGeneralVariable = new List<MPC.Models.Common.TemplateVariable>();
            List<MPC.Models.Common.TemplateVariable> lstListingImages = new List<MPC.Models.Common.TemplateVariable>();
            List<VariableSection> lstSections = new List<VariableSection>();
            List<FieldVariable> lstVariablesData = _myListingService.GetVariablesListWithValues(listingId, itemId, ContactID, ContactCompanyID, FlagID, AddressID, out lstVariableAndValue, out lstGeneralVariable, out lstListingImages, out lstSections);

            TempData["GeneralVariables"] = lstGeneralVariable;
            ViewData["ListingImages"] = lstListingImages;
            TempData["ListingImages"] = lstListingImages;

            List<SectionControls> lstControls = new List<SectionControls>();

            foreach (var item in lstSections)
            {
                SectionControls objSectionControl = new SectionControls();
                objSectionControl.SectionName = item.SectionName;

                List<FieldVariable> lstFieldVariables = lstVariablesData.Where(i => i.VariableSectionId == item.VariableSectionId).ToList();

                foreach (FieldVariable objFieldVariable in lstFieldVariables)
                {
                    MPC.Models.Common.TemplateVariable objTempVar = lstVariableAndValue.Where(i => i.Name == objFieldVariable.VariableName).FirstOrDefault();
                    objSectionControl.Controls.Add(objTempVar);
                }

                lstControls.Add(objSectionControl);
            }

            ViewData["ControlsList"] = lstControls;
            TempData["ControlsList"] = lstControls;
            return View("PartialViews/RealEstateSmartForm");
        }

        [HttpPost]
        public ActionResult SmartFormSubmit()
        {
            List<MPC.Models.Common.TemplateVariable> lstFieldVariables = new List<MPC.Models.Common.TemplateVariable>();
            MPC.Models.Common.TemplateVariable objTempVar;

            foreach (var control in TempData["ControlsList"] as List<SectionControls>)
            {
                if (control.SectionName != "LISTING IMAGES")
                {

                    foreach (MPC.Models.Common.TemplateVariable item in control.Controls)
                    {
                        if (item != null)
                        {
                            string controlValue = Request.Form[item.Name.Replace(" ", "").Trim()];

                            objTempVar = new MPC.Models.Common.TemplateVariable("{{" + item.Name.Replace(" ", "").Trim() + "}}", controlValue);
                            lstFieldVariables.Add(objTempVar);
                        }
                    }
                }
            }

            foreach (var image in TempData["ListingImages"] as List<MPC.Models.Common.TemplateVariable>) //images 
            {
                if (image != null)
                {
                    objTempVar = new MPC.Models.Common.TemplateVariable("{{" + image.Name.Replace(" ", "").Trim() + "}}", image.Value);
                    lstFieldVariables.Add(objTempVar);
                }
            }

            foreach (var genVar in TempData["GeneralVariables"] as List<MPC.Models.Common.TemplateVariable>) //images 
            {
                if (genVar != null)
                {
                    lstFieldVariables.Add(genVar);
                }
            }

            //View will be template designer - for now its empty
            return View();
        }
    }
}