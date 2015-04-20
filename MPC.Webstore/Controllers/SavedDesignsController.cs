using MPC.ExceptionHandling;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class SavedDesignsController : Controller
    {
        private readonly IItemService _ItemService;
        private readonly IOrderService _IOrderService;
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly ITemplateService _TemplateService;
        public SavedDesignsController(IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService ItemService,ITemplateService templateService,IOrderService IOrderService)
        {

            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._ItemService = ItemService;
            this._TemplateService = templateService;
            this._IOrderService = IOrderService;
        }

        // GET: SavedDesigns
        public ActionResult Index()
        {
            long OrganisationID = 0;
            try
            {
                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;

                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

                OrganisationID = StoreBaseResopnse.Organisation.OrganisationId;

                BindSavedDesigns();

                return View("PartialViews/SavedDesigns");
            
            }
            catch (Exception ex)
            {

                throw new MPCException(ex.ToString(), OrganisationID);
            }
           

           
        }
        public void BindSavedDesigns()
        {
            if (_myClaimHelper.loginContactID() > 0)
            {
                List<SaveDesignView> designs = _ItemService.GetSavedDesigns(_myClaimHelper.loginContactID());
                if (designs != null && designs.Count > 0)
                {
                    ViewData["SaveDesignView"] = designs;

                }
                else
                {
                    ViewData["SaveDesignView"] = null;
                }


            }
        }
        public ActionResult RemoveSaveDesign(long ItemID)
        {
            long OrganisationID = 0;
            try
            {
                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;


                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

                OrganisationID = StoreBaseResopnse.Organisation.OrganisationId;
                List<ArtWorkAttatchment> itemAttatchments = null;
                Template clonedTempldateFiles = null;


                bool res = _ItemService.RemoveCloneItem(ItemID, out itemAttatchments, out clonedTempldateFiles);
                if (res)
                {
                    // file removing physicslly
                    if (itemAttatchments != null && itemAttatchments.Count > 0)
                        _ItemService.RemoveItemAttacmentPhysically(itemAttatchments);

                    if (clonedTempldateFiles != null)
                        _TemplateService.DeleteTemplateFiles(clonedTempldateFiles.ProductId, StoreBaseResopnse.Organisation.OrganisationId);



                    BindSavedDesigns();

                  
                }
                Response.Redirect("/SavedDesigns");
                return null;
            }
            catch (Exception ex)
            {

                throw new MPCException(ex.ToString(), OrganisationID);
            }
           

        }

        public ActionResult ReOrder(long ItemID)
        {
             long OrganisationID = 0;
             try
             {
                 string CacheKeyName = "CompanyBaseResponse";
                 ObjectCache cache = MemoryCache.Default;


                 MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                 OrganisationID = StoreBaseResopnse.Organisation.OrganisationId;

                 List<SaveDesignView> productList = _ItemService.GetSavedDesigns(_myClaimHelper.loginContactID());
                 SaveDesignView ExistingProduct = productList.Where(p => p.ItemID == ItemID).FirstOrDefault();

                if (ExistingProduct.StatusID == 3 && ExistingProduct.IsOrderedItem == true)
                {
                    //In Cart - Added to Cart but not ordered/Check out
                    //(Go Landing Page and Edit/Update)

                    string URL = "/ProductOptions/0/" + ExistingProduct.ItemID + "/Modify/" + ExistingProduct.TemplateID;
                        //
                   
                    Response.Redirect(URL);
                    
                   
                }
                else if (ExistingProduct.IsOrderedItem == false)
                {
                    //In Progress - Template Selected designed and saved template but not added to the cart.
                    //(Go Landing Page and Add it to Cart)


                    string URL =  "/ProductOptions/0/" + ExistingProduct.ItemID + "/" + ExistingProduct.TemplateID;
                       

                    Response.Redirect(URL);
                    
                 //   Response.Redirect("ProductConfirmSelect.aspx?itemid=" + ExistingProduct.ItemID + "&templateid=" + ExistingProduct.TemplateID + "&CategoryId=" + ExistingProduct.ProductCategoryID);
                }
                else if (ExistingProduct.StatusID != 3 && ExistingProduct.IsOrderedItem == true)
                {
                    //Confirmed Order - Template from the Confirmed Order
                    //(Go Landing page and re order for the selected template and associate this templateID to new ordered item.

                    long OrderID = 0;

                    //new added
                    
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        OrderID = _IOrderService.GetOrderID(_myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), string.Empty, OrganisationID);
                    }
                    


                    if (UserCookieManager.WEBOrderId == null || UserCookieManager.WEBOrderId == 0)
                    {
                       UserCookieManager.WEBOrderId = _IOrderService.CreateNewOrder(_myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(),OrganisationID,string.Empty);
                       
                    }


                    Item clonedItem = null;
     
                    clonedItem = _ItemService.CloneItem(ExistingProduct.ItemID,ExistingProduct.RefItemID ?? 0,UserCookieManager.WEBOrderId,_myClaimHelper.loginContactCompanyID(),ExistingProduct.TemplateID ?? 0,0,null,false,false,_myClaimHelper.loginContactID(),StoreBaseResopnse.Organisation.OrganisationId);

                    // Code to copy item attachments ..
                    Estimate objOrder = _IOrderService.GetOrderByID(UserCookieManager.WEBOrderId);
             
                   // _ItemService.CopyAttachments(ExistingProduct.ItemID, clonedItem, objOrder.Order_Code, false, objOrder.CreationDate ?? DateTime.Now);

                    _ItemService.CopyAttachments((int)ExistingProduct.ItemID, clonedItem, objOrder.Order_Code, false, objOrder.CreationDate ?? DateTime.Now);

                    string URL = "/ProductOptions/0/" + ExistingProduct.ItemID + "/SaveOrder/" + ExistingProduct.TemplateID;
        
                    Response.Redirect(URL);
                    
                }
                return null;

             
             }
             catch (Exception ex)
             {
                 throw new MPCException(ex.ToString(), OrganisationID);
             }


        }

     

    }
}