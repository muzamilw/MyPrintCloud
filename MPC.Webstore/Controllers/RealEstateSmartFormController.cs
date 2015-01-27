using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;

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

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateSmartFormController(IListingService myListingService)
        {
            if (myListingService == null)
            {
                throw new ArgumentNullException("myListingService");
            }

            this._myListingService = myListingService;
        }

        #endregion

        // GET: RealEstateSmartForm
        public ActionResult Index(long listingId, long itemId)
        {
            List<MPC.Models.Common.TemplateVariable> lstVariableAndValue = new List<MPC.Models.Common.TemplateVariable>();
            List<MPC.Models.Common.TemplateVariable> lstGeneralVariable = new List<MPC.Models.Common.TemplateVariable>();
            List<string> lstListingImages = new List<string>();
            List<VariableSection> lstSections = new List<VariableSection>();
            List<FieldVariable> lstVariablesData = _myListingService.GetVariablesListWithValues(listingId, itemId, out lstVariableAndValue, out lstGeneralVariable, out lstListingImages, out lstSections);

            ViewData["GeneralVariables"] = lstVariableAndValue;
            ViewData["ListingImages"] = lstListingImages;

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

            return View("PartialViews/RealEstateSmartForm");
        }

        [HttpPost]
        public ActionResult SmartFormSubmit()
        {
            foreach (var control in ViewData["ControlsList"] as List<SectionControls>)
            {
                
            }

            //View will be template designer - for now its empty
            return View();
        }
    }
}