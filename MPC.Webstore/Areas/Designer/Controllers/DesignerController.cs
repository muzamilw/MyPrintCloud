﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Areas.Designer.Controllers
{
    public class DesignerController : Controller
    {
        // GET: Designer/Designer
        //Designer/productName/CategoryIDv2/TemplateID/ItemID/companyID/cotnactID/printCropMarks/printWaterMarks/isCalledFrom/IsEmbedded;
        //c=350&cv2=18&t=5747&cm=false&wm=false&CustomerID=12566&ContactID=172348&ItemId=18477
        public ActionResult Index(string designName, int categoryIDV2, int templateID, int itemID, int customerID, int contactID, bool printCropMarks, bool printWaterMarks,int isCalledFrom ,bool isEmbedded)
        {
            return View();
        }
    }
}