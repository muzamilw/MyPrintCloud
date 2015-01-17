using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Areas.Designer.Controllers
{
    public class DesignerController : Controller
    {
        // GET: Designer/Designer
        //Designer/productName/CategoryIDv2/TemplateID/ItemID/companyID/cotnactID/isCalledFrom/organisationid/printCropMarks/printWaterMarks//IsEmbedded;
        //c=350&cv2=18&t=5747&cm=false&wm=false&CustomerID=12566&ContactID=172348&ItemId=18477
        public ActionResult Index(string designName, int categoryIDV2, long templateID, long itemID, long customerID, long contactID, int isCalledFrom, long organisationId, bool printCropMarks, bool printWaterMarks, bool isEmbedded)
        {
            return View();
        }
    }
}