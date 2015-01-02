﻿using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateObjectService : ITemplateObjectService
    {
        #region private
        public readonly ITemplateObjectRepository _templateObjectRepository;
        #endregion

        #region constructor
        public TemplateObjectService(ITemplateObjectRepository templateObjectRepository)
        {
            this._templateObjectRepository = templateObjectRepository;
        }
        #endregion

        #region public 
        public List<TemplateObject> GetProductObjects(int productId)
        {
            // values used to change the display order
            int productPageId = -1;
            int DisplayOrderCounter = 0;


            CMYKtoRGBConverter oColorConv = new CMYKtoRGBConverter();

            var list = _templateObjectRepository.GetProductObjects(productId);
            foreach (var item in list)
            {
                item.PositionX = DesignerUtils.PointToPixel(item.PositionX.Value);
                item.PositionY = DesignerUtils.PointToPixel(item.PositionY.Value);
                item.FontSize = DesignerUtils.PointToPixel(item.FontSize.Value);
                item.MaxWidth = DesignerUtils.PointToPixel(item.MaxWidth.Value);
                item.MaxHeight = DesignerUtils.PointToPixel(item.MaxHeight.Value);
                if (item.CharSpacing != null)
                {
                    item.CharSpacing = Convert.ToDouble(DesignerUtils.PointToPixel(Convert.ToDouble(item.CharSpacing.Value)));
                }
                item.ColorHex = oColorConv.getColorHex(item.ColorC.Value, item.ColorM.Value, item.ColorY.Value, item.ColorK.Value);

                // used to create page objects display order so that it can be used in objects display order
                if (productPageId == -1)
                {
                    productPageId = Convert.ToInt32(item.ProductPageId);
                    item.DisplayOrderPdf = DisplayOrderCounter;
                    DisplayOrderCounter++;

                }
                else if (productPageId == Convert.ToInt32(item.ProductPageId))
                {
                    item.DisplayOrderPdf = DisplayOrderCounter;
                    DisplayOrderCounter++;
                }
                else
                {
                    productPageId = Convert.ToInt32(item.ProductPageId);
                    item.DisplayOrderPdf = 0;
                    DisplayOrderCounter = 1;
                }


                if (item.IsQuickText == null)
                {
                    item.IsQuickText = false;
                }
            }



            TemplateObject oTempItem = new TemplateObject();
            oTempItem.ProductId = productId;
            oTempItem.ObjectId = -999;
            oTempItem.ObjectType = 2;
            oTempItem.Name = "Dummy Object";
            oTempItem.ContentString = "Dummy Object";
            oTempItem.MaxWidth = 50;
            oTempItem.MaxHeight = 100;
            oTempItem.PositionX = 10;
            oTempItem.PositionY = 10;
            oTempItem.FontName = "Arial";
            oTempItem.FontSize = 10;
            oTempItem.DisplayOrderPdf = 100;
            oTempItem.ColorC = 0;
            oTempItem.ColorM = 100;
            oTempItem.ColorY = 100;
            oTempItem.ColorK = 20;
            oTempItem.IsEditable = true;
            oTempItem.IsHidden = false;
            oTempItem.IsMandatory = false;
            oTempItem.IsPositionLocked = false;
            oTempItem.AutoShrinkText = false;
            oTempItem.RotationAngle = 0;
            oTempItem.ColorType = 3;
            oTempItem.IsSpotColor = false;
            oTempItem.VAllignment = 1;
            oTempItem.Allignment = 1;
            oTempItem.ColorName = "";
            oTempItem.SpotColorName = "";
            oTempItem.TCtlName = "";
            oTempItem.ExField1 = "";
            oTempItem.ExField2 = "";
            oTempItem.Opacity = 1;
            oTempItem.IsQuickText = false;
            oTempItem.QuickTextOrder = 0;
            oTempItem.CharSpacing = 0;
            oTempItem.ColorHex = oColorConv.getColorHex(0, 100, 100, 20);

            list.Insert(0, oTempItem);
            return list;
        }
        #endregion
    }
}
