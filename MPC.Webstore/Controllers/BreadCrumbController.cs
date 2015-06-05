using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class BreadCrumbController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private List<BreadCrumbModel> _filteredCats = null;
        private int _sequence = 0;
        private long categoryID = 0;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BreadCrumbController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: BreadCrumb
        public ActionResult Index()
        {
            BreadCrumbFactory(BreadCrumbMode.CategoryBrowsing);

            return PartialView("PartialViews/BreadCrumb", _filteredCats);
        }

        private void BreadCrumbFactory(BreadCrumbMode WorkMode)
        {
            switch (WorkMode)
            {

                case BreadCrumbMode.CategoryBrowsing:
                    this.CategoryBrowsingMode();
                    break;
            }


        }



        private void MyAccountWorkingMode()
        {
            //BuildMyAccountBreadCrumbMenu();        
        }

        private void CategoryBrowsingMode()
        {


            try
            {
                //Get the Category from QueryString
                string url = "";
                string id = RouteData.Values["id"].ToString();
                if (RouteData.Values["id"].ToString() != null || RouteData.Values["id"].ToString() != "")
                {
                    categoryID = Convert.ToInt64(id);

                    if (categoryID > 0)
                    {

                        List<ProductCategory> _productCatList = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                        this.BuildBreadCrumbMenu(categoryID, _productCatList);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region PageControl Events

        //protected void dlBreadCrumbMenu_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    HtmlAnchor ancher = null;
        //    Model.Category category = null;

        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        ancher = (HtmlAnchor)e.Item.FindControl("aLinkItem");
        //        if (e.Item.ItemIndex != this._filteredCats.Count - 1)
        //        {
        //            category = this._filteredCats[e.Item.ItemIndex];
        //            ancher.HRef = category.Url;
        //        }
        //        else
        //        {
        //            ancher.Visible = false;
        //            ((Label)e.Item.FindControl("lblTextItem")).Visible = true;
        //            ((Label)e.Item.FindControl("lblVerticalLine")).Visible = false;
        //        }

        //    }
        //}

        #endregion

        #region Private Methods



        //private void BuildMyAccountBreadCrumbMenu()
        //{
        //    //this._filteredCats  = new List<Model.Category>();


        //    XmlDocument xmldoc = this.MyAccountXmlDoc;
        //    this._filteredCats = this.ParseDocumentBuildMenu(xmldoc, this.MyAccountCurrentPageUrl);


        //    this.dlBreadCrumbMenu.DataSource = this._filteredCats;
        //    this.dlBreadCrumbMenu.DataBind();
        //}


        //private List<Model.Category> ParseDocumentBuildMenu(XmlDocument xmldoc, string currentPageUrl)
        //{

        //    XmlNode xRootNode = xmldoc.SelectSingleNode("MenuItem[@IsRoot='true']");

        //    List<Model.Category> catList = new List<Model.Category>();

        //    if (xRootNode != null)
        //    {
        //        ParseNodes(xRootNode, catList, currentPageUrl);
        //        //Add the home page node at the end
        //        this.CreateCatNodeItem(catList, xRootNode.Attributes[TITLE].InnerText, xRootNode.Attributes["url"].InnerText);

        //        catList = catList.OrderByDescending(item => item.Sequence).ToList();
        //    }


        //    return catList;
        //}

        //private void ParseNodes(XmlNode xRootNode, List<Model.Category> catList, string currentPageUrl)
        //{

        //    foreach (XmlNode xmNode in xRootNode.ChildNodes)
        //    {
        //        if (xmNode.HasChildNodes)
        //        {
        //            if (this.WorkMode == WorkingMode.MyAccount)
        //            {
        //                this.ParseNodes(xmNode, catList, currentPageUrl);
        //                this.CreateCatNodeItem(catList, xmNode.Attributes[TITLE].InnerText, xmNode.Attributes[URL].InnerText);
        //            }
        //        }
        //        else
        //        {
        //            string nodeUrl = xmNode.Attributes[URL].InnerText;
        //            if (nodeUrl.EndsWith(currentPageUrl, true, null))
        //                this.CreateCatNodeItem(catList, xmNode.Attributes[TITLE].InnerText, nodeUrl);
        //        }
        //    }
        //}

        //private void CreateCatNodeItem(List<Model.Category> catList, string catName, string url)
        //{
        //    this._seqNumb += 1;
        //    catList.Add(new Model.Category()
        //            {
        //                CategoryName = catName,
        //                Url = "~/" + url,
        //                Sequence = this._seqNumb,

        //            });
        //}



        private void BuildBreadCrumbMenu(long curCategoryID, List<ProductCategory> productCatList)
        {

            ProductCategory curCategory = productCatList.Find(cat => cat.ProductCategoryId == curCategoryID);

            this._filteredCats = new List<BreadCrumbModel>();
            //Trivese to its parent Category
            this.TriverseCategoriesUptoParent(_filteredCats, curCategoryID, productCatList);

            if (this._filteredCats.Count > 0)
            {
                //sort in decending order
                this._filteredCats = this._filteredCats.OrderByDescending(est => est.Sequence).ToList();
            }

            //if (this.WorkMode == WorkingMode.CategoryBrowsing)
            //{
            //    this.dlBreadCrumbMenu.DataSource = this._filteredCats;
            //    this.dlBreadCrumbMenu.DataBind();
            //}

        }

        private void TriverseCategoriesUptoParent(List<BreadCrumbModel> filteredCats, long curCategoryID, List<ProductCategory> productCatList)
        {
            ProductCategory curCategory = null;

            this._sequence += 1;
            curCategory = productCatList.Find(cat => cat.ProductCategoryId == curCategoryID);

            if (curCategory != null && curCategory.ParentCategoryId.HasValue)
            {

                filteredCats.Add(this.FillCategoryDto(curCategory, Utils.BuildCategoryUrl("Category", curCategory.CategoryName, curCategory.ProductCategoryId.ToString())));
                this.TriverseCategoriesUptoParent(filteredCats, (int)curCategory.ParentCategoryId.Value, productCatList);
            }
            else
            {
                if (curCategory != null)
                    filteredCats.Add(this.FillCategoryDto(curCategory, Utils.BuildCategoryUrl("Category", curCategory.CategoryName, curCategory.ProductCategoryId.ToString())));

                return;
            }
        }

        private BreadCrumbModel FillCategoryDto(ProductCategory curCategory, string url)
        {
            BreadCrumbModel category = null;

            if (categoryID == curCategory.ProductCategoryId)
            {
                category = new BreadCrumbModel()
                {
                    Name = curCategory.CategoryName,
                    CrumbId = curCategory.ProductCategoryId,
                    CrumbParentId = curCategory.ParentCategoryId ?? 0,
                    Url = url,
                    Sequence = _sequence,
                    IsEnable = false
                };

            }
            else
            {
                category = new BreadCrumbModel()
                {
                    Name = curCategory.CategoryName,
                    CrumbId = curCategory.ProductCategoryId,
                    CrumbParentId = curCategory.ParentCategoryId ?? 0,
                    Url = url,
                    Sequence = _sequence,
                    IsEnable = true
                };

            }

            return category;
        }


        #endregion

        //protected void dlBreadCrumbMenu_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HtmlAnchor ancher = null;
        //        Model.Category category = null;

        //        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //        {
        //            ancher = (HtmlAnchor)e.Item.FindControl("aLinkItem");
        //            if (e.Item.ItemIndex != this._filteredCats.Count - 1)
        //            {
        //                category = this._filteredCats[e.Item.ItemIndex];
        //                ancher.HRef = category.Url;
        //            }
        //            else
        //            {
        //                category = this._filteredCats[e.Item.ItemIndex];
        //                ancher.HRef = category.Url;
        //                //ancher.Visible = false;
        //                //((Label)e.Item.FindControl("lblTextItem")).Visible = true;
        //                ((Label)e.Item.FindControl("lblVerticalLine")).Visible = false;
        //            }

        //        }
        //    }
        //}
    }
}