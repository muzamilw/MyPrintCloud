using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using Newtonsoft.Json;
using PDFlib_dotnet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using WebSupergoo.ABCpdf8;

namespace MPC.Implementation.WebStoreServices
{
    public class objTextStyles
    {
        public string textColor { get; set; }
        public string fontName { get; set; }
        public string fontSize { get; set; }
        public string fontWeight { get; set; }
        public string fontStyle { get; set; }

        public string characterIndex { get; set; }
        public string textCMYK { get; set; }
        public string spotColorName { get; set; }
    }
    class TemplateService : ITemplateService
    {
        #region projection
        private Template returnLocalTemplate(GlobalTemplateDesigner.Templates objGlobal)
        {
            Template objTemplate = new Template();
            objTemplate.ApprovalDate = objGlobal.ApprovalDate;
            objTemplate.ApprovedBy = objGlobal.ApprovedBy;
            objTemplate.ApprovedByName = objGlobal.ApprovedByName;
            objTemplate.ApprovedDate = objGlobal.ApprovedDate;
            objTemplate.BaseColorID = objGlobal.BaseColorID;
            objTemplate.Code = objGlobal.Code;
            objTemplate.ColorHex = objGlobal.ColorHex;
            objTemplate.CuttingMargin = objGlobal.CuttingMargin;
            objTemplate.Description = objGlobal.Description;
            objTemplate.FullView = objGlobal.FullView;
            objTemplate.Image = objGlobal.Image;
            objTemplate.IsCorporateEditable = objGlobal.IsCorporateEditable;
            objTemplate.isCreatedManual = objGlobal.isCreatedManual;
            objTemplate.IsDisabled = objGlobal.IsDisabled;
            objTemplate.isEditorChoice = objGlobal.isEditorChoice;
            objTemplate.IsPrivate = objGlobal.IsPrivate;
            objTemplate.isSpotTemplate = objGlobal.isSpotTemplate;
            objTemplate.isWatermarkText = objGlobal.isWatermarkText;
            objTemplate.MatchingSetID = objGlobal.MatchingSetID;
            objTemplate.MatchingSetTheme = objGlobal.MatchingSetTheme;
            objTemplate.MPCRating = objGlobal.MPCRating;
            objTemplate.MultiPageCount = objGlobal.MultiPageCount;
            objTemplate.Orientation = objGlobal.Orientation;
            objTemplate.PDFTemplateHeight = objGlobal.PDFTemplateHeight;
            objTemplate.PDFTemplateWidth = objGlobal.PDFTemplateWidth;
            objTemplate.ProductCategoryId = objGlobal.ProductCategoryID; 
            objTemplate.ProductId = objGlobal.ProductID;
            objTemplate.ProductName = objGlobal.ProductName;
            objTemplate.RejectionReason = objGlobal.RejectionReason;
            objTemplate.TemplateType = objGlobal.TemplateType;
            objTemplate.TempString = objGlobal.TempString;
            return objTemplate;
        }
        private TemplatePage returnLocalPage(GlobalTemplateDesigner.TemplatePages objGlobalPage, Template objtemplate)
        {
           TemplatePage obj = new TemplatePage();
           obj.BackgroundFileName = objGlobalPage.BackgroundFileName;
           obj.BackGroundType = objGlobalPage.BackGroundType;
           obj.ColorC = objGlobalPage.ColorC;
           obj.ColorK = objGlobalPage.ColorK;
           obj.ColorM = objGlobalPage.ColorM;
           obj.ColorY = objGlobalPage.ColorY;
           obj.hasOverlayObjects = objGlobalPage.hasOverlayObjects;
         //  obj.Height = objGlobalPage.Height;
           obj.IsPrintable = objGlobalPage.IsPrintable;
           obj.Orientation = objGlobalPage.Orientation;
           obj.PageName = objGlobalPage.PageName;
           obj.PageNo = objGlobalPage.PageNo;
           obj.PageType = objGlobalPage.PageType;
           obj.ProductId = objGlobalPage.ProductID;
           obj.ProductPageId = objGlobalPage.ProductPageID;
      //     obj.Width = objGlobalPage.Width;

          if(objGlobalPage.Orientation == 2)
          {
              obj.Height = objtemplate.PDFTemplateWidth;
              obj.Width = objtemplate.PDFTemplateHeight;
          }
           
           return obj;
        }
        private TemplateObject returnLocalObject(GlobalTemplateDesigner.TemplateObjects tempObj)
        {
            TemplateObject obj = new TemplateObject();
            obj.Allignment = tempObj.Allignment;
            obj.AutoShrinkText = tempObj.AutoShrinkText;
            obj.BColor = tempObj.BColor;
            obj.CharSpacing = tempObj.CharSpacing;
            obj.CircleRadiusX = tempObj.CircleRadiusX;
            obj.CircleRadiusY = tempObj.CircleRadiusY;
            obj.ClippedInfo = tempObj.ClippedInfo;
            obj.ColorC = tempObj.ColorC;
            obj.ColorHex = tempObj.ColorHex;
            obj.ColorK = tempObj.ColorK;
            obj.ColorM = tempObj.ColorM;
            obj.ColorName = tempObj.ColorName;
            obj.ColorType = tempObj.ColorType;
            obj.ColorY = tempObj.ColorY;
            obj.ContentCaseType = tempObj.ContentCaseType;
            obj.ContentString = tempObj.ContentString;
            obj.DisplayOrderPdf = tempObj.DisplayOrderPdf;
            obj.DisplayOrderTxtControl = tempObj.DisplayOrderTxtControl;
            obj.FontName = tempObj.FontName;
            obj.FontSize = tempObj.FontSize;
            obj.GColor = tempObj.GColor;
            obj.Indent = tempObj.Indent;
            obj.IsBold = tempObj.IsBold;
            obj.IsEditable = tempObj.IsEditable;
            obj.IsFontCustom = tempObj.IsFontCustom;
            obj.IsFontNamePrivate = tempObj.IsFontNamePrivate;
            obj.IsHidden = tempObj.IsHidden;
            obj.IsItalic = tempObj.IsItalic;
            obj.IsMandatory = tempObj.IsMandatory;
            obj.IsOverlayObject = tempObj.IsOverlayObject;
            obj.IsPositionLocked = tempObj.IsPositionLocked;
            obj.IsQuickText = tempObj.IsQuickText;
            obj.IsSpotColor = tempObj.IsSpotColor;
            obj.IsTextEditable = tempObj.IsTextEditable;
            obj.IsUnderlinedText = tempObj.IsUnderlinedText;
            obj.LineSpacing = tempObj.LineSpacing;
            obj.MaxCharacters = tempObj.MaxCharacters;
            obj.MaxHeight = tempObj.MaxHeight;
            obj.MaxWidth = tempObj.MaxWidth;
            obj.Name = tempObj.Name;
            obj.ObjectId = tempObj.ObjectID;
            obj.ObjectType = tempObj.ObjectType;
            obj.Opacity = tempObj.Opacity;
            obj.ParentId = tempObj.ParentId;
            obj.PositionX = tempObj.PositionX;
            obj.PositionY = tempObj.PositionY;
            obj.ProductId = tempObj.ProductID;
            obj.ProductPageId = tempObj.ProductPageId;
            obj.QuickTextOrder = tempObj.QuickTextOrder;
            obj.RColor = tempObj.RColor;
            obj.RotationAngle = tempObj.RotationAngle;
            obj.SpotColorName = tempObj.SpotColorName;
            //obj.textCase = tempObj.textCase;
            obj.textStyles = tempObj.textStyles;
            obj.Tint = tempObj.Tint;
            obj.VAllignment = tempObj.VAllignment;
            obj.watermarkText = tempObj.watermarkText;
            obj.originalContentString = tempObj.ContentString;
            obj.originalTextStyles = tempObj.textStyles;
            return obj;

        }
        private TemplateBackgroundImage returnLocalImage(GlobalTemplateDesigner.TemplateBackgroundImages v2Img)
        {
            TemplateBackgroundImage objImage = new TemplateBackgroundImage();
            objImage.BackgroundImageAbsolutePath = v2Img.BackgroundImageAbsolutePath;
            objImage.BackgroundImageRelativePath = v2Img.BackgroundImageRelativePath;
            objImage.ContactCompanyId = v2Img.ContactCompanyID;
            objImage.ContactId = v2Img.ContactID;
            objImage.flgCover = v2Img.flgCover;
            objImage.flgPhotobook = v2Img.flgPhotobook;
            objImage.Id = v2Img.ID;
            objImage.ImageDescription = v2Img.ImageDescription;
            objImage.ImageHeight = v2Img.ImageHeight;
            objImage.ImageKeywords = v2Img.ImageKeywords;
            objImage.ImageName = v2Img.ImageName;
            objImage.ImageTitle = v2Img.ImageTitle;
            objImage.ImageType = v2Img.ImageType;
            objImage.ImageWidth = v2Img.ImageWidth;
            objImage.Name = v2Img.Name;
            objImage.ProductId = v2Img.ProductID;
            objImage.UploadedFrom = v2Img.UploadedFrom;
            return objImage;
        }
        private TemplateFont returnLocalFont(GlobalTemplateDesigner.TemplateFonts v2Font)
        {
            TemplateFont objFont = new TemplateFont();
            objFont.CustomerId = v2Font.CustomerID;
            objFont.DisplayIndex = v2Font.DisplayIndex;
            objFont.FontDisplayName = v2Font.FontDisplayName;
            objFont.FontFile = v2Font.FontFile;
            objFont.FontName = v2Font.FontName;
            objFont.FontPath = v2Font.FontPath;
            objFont.IsEnable = v2Font.IsEnable;
            objFont.IsPrivateFont = v2Font.IsPrivateFont;
            objFont.ProductFontId = v2Font.ProductFontId;
            objFont.ProductId = v2Font.ProductId;
            return objFont;
        }
        #endregion
        #region privateGeneratePdf
        // helper functions to generate pdf 
        private void LoadBackColor(ref Doc oPdf, TemplatePage oTemplate)
        {
            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

                oPdf.PageNumber = oTemplate.PageNo.Value;
                oPdf.Layer = oPdf.LayerCount + 1;
                oPdf.Color.Cyan = oTemplate.ColorC.Value;
                oPdf.Color.Magenta = oTemplate.ColorM.Value;
                oPdf.Color.Yellow = oTemplate.ColorY.Value;
                oPdf.Color.Black = oTemplate.ColorK.Value;
                oPdf.FillRect();
            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackColor", ex);
            }

        }
        private void LoadBackGroundImage(ref Doc oPdf, TemplatePage oTemplate, string imgPath)
        {

            try
            {
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                oPdf.PageNumber = oTemplate.PageNo.Value;
                oPdf.Layer = oPdf.LayerCount + 1;
                XImage oImg = new XImage();
                bool bFileExists = false;
                string FilePath = imgPath + oTemplate.BackgroundFileName;
                bFileExists = System.IO.File.Exists(FilePath);
                if (bFileExists)
                {
                    oImg.SetFile(FilePath);
                    oPdf.AddImageObject(oImg, true);
                    oPdf.Transform.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LoadBackGroundArtWork", ex);
            }

        }
        private objTextStyles GetStyleByCharIndex(int index, List<objTextStyles> objStyles)
        {
            foreach (var obj in objStyles)
            {
                if (obj.characterIndex == index.ToString())
                {
                    return obj;
                }
            }
            return null;
        }
        private void AddTextObject(TemplateObject ooBject, int PageNo, List<TemplateFont> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight, bool isTemplateSpot,long organisationID)
        {

            try
            {
                oPdf.TextStyle.Outline = 0;
                oPdf.TextStyle.Strike = false;
                oPdf.TextStyle.Bold = false;
                oPdf.TextStyle.Italic = false;
                oPdf.TextStyle.CharSpacing = 0;
                oPdf.PageNumber = PageNo;
                if (ooBject.CharSpacing != null)
                {
                    oPdf.TextStyle.CharSpacing = Convert.ToDouble(ooBject.CharSpacing.Value);
                }

                //    OPosY  =OPosY - (ooBject.FontSize.Value / 21);
                double yRPos = 0;
                if (oPdf.TopDown == true)
                    yRPos = oPdf.MediaBox.Height - ooBject.PositionY.Value;
                if (ooBject.ColorType.Value == 3)
                {
                    if (isTemplateSpot)
                    {
                        if (ooBject.IsSpotColor.HasValue == true && ooBject.IsSpotColor.Value == true)
                        {
                            if (ooBject.SpotColorName == "null" || ooBject.SpotColorName == null)
                            {
                                ooBject.SpotColorName = ooBject.ColorC.ToString() +  ooBject.ColorM.ToString() +  ooBject.ColorY.ToString() + ooBject.ColorK.ToString();
                            }
                            oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                            oPdf.Color.Gray = 255;
                        }else
                        {
                            oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
                        }
                    }
                    else
                    {
                        oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
                    }

                    //if (!ooBject.IsColumnNull("Tint"))
                    if (ooBject.Tint.HasValue)
                    {
                        oPdf.Color.Alpha = Convert.ToInt32((100 - ooBject.Tint) * 2.5);
                    }
                }
                else if (ooBject.ColorType.Value == 4) // For RGB Colors
                {
                    oPdf.Color.String = ooBject.RColor.ToString() + " " + ooBject.GColor.ToString() + " " + ooBject.BColor.ToString();
                }

                //for commented code see change book or commit before 3rd march
                int FontID = 0;
                var pFont = oFonts.Where(g => g.FontName == ooBject.FontName).FirstOrDefault();
                string path = "";
                if (pFont != null)
                {

                    if (pFont.FontPath == null)
                    {
                        // mpc designers fonts or system fonts 
                        path = "Organisation" + organisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
                    }
                    else
                    {  // customer fonts 
                        path = pFont.FontPath;
                    }
                    if (System.IO.File.Exists(Font + path + pFont.FontFile + ".ttf"))
                        FontID = oPdf.EmbedFont(Font + path + pFont.FontFile + ".ttf");
                }

                oPdf.Font = FontID;
                oPdf.TextStyle.Size = ooBject.FontSize.Value;
                if (ooBject.IsUnderlinedText.HasValue)
                    oPdf.TextStyle.Underline = ooBject.IsUnderlinedText.Value;
                if(ooBject.IsBold.HasValue)
                    oPdf.TextStyle.Bold = ooBject.IsBold.Value;
                if(ooBject.IsItalic.HasValue)
                    oPdf.TextStyle.Italic = ooBject.IsItalic.Value;
                double linespacing = 0;
                if (ooBject.LineSpacing.HasValue)
                    linespacing = ooBject.LineSpacing.Value - 1;
                linespacing = (linespacing * ooBject.FontSize.Value);
                oPdf.TextStyle.LineSpacing = linespacing;
                if (ooBject.Allignment == 1)
                {
                    oPdf.HPos = 0.0;
                }
                else if (ooBject.Allignment == 2)
                {
                    oPdf.HPos = 0.5;
                }
                else if (ooBject.Allignment == 3)
                {
                    oPdf.HPos = 1.0;
                }
                if (ooBject.VAllignment.HasValue)
                {
                    if (ooBject.VAllignment == 1)
                    {
                        oPdf.VPos = 0.0;
                    }
                    else if (ooBject.VAllignment == 2)
                    {
                        oPdf.VPos = 0.5;
                    }
                    else if (ooBject.VAllignment == 3)
                    {
                        
                      //  OPosY += DesignerUtils.PixelToPoint(Convert.ToDouble(ooBject.TextPaddingTop));
                        oPdf.VPos = 1.0;
                    }
                }
                if (ooBject.RotationAngle != 0)
                {

                    oPdf.Transform.Reset();
                    oPdf.Transform.Rotate(360 - ooBject.RotationAngle.Value, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY+ (OHeight/2));
                }
                List<objTextStyles> styles = new List<objTextStyles>();
                if (ooBject.textStyles != null)
                {
                    styles = JsonConvert.DeserializeObject<List<objTextStyles>>(ooBject.textStyles);
                }

                string StyledHtml = "<p>";

                if (styles.Count != 0)
                {
                    styles = styles.OrderBy(g => g.characterIndex).ToList();
                    for (int i = 0; i < ooBject.ContentString.Length; i++)
                    {
                        objTextStyles objStyle = GetStyleByCharIndex(i, styles);
                        if (objStyle != null && ooBject.ContentString[i] != '\n')
                        {
                            if (objStyle.fontName == null && objStyle.fontSize == null && objStyle.fontStyle == null && objStyle.fontWeight == null && objStyle.textColor == null)
                            {
                                string content = ooBject.ContentString[i].ToString();
                                content = content.Replace("<", "&#60;");
                                content = content.Replace(">", "&#62;");
                                StyledHtml += content;
                            }
                            else
                            {
                                string content = ooBject.ContentString[i].ToString();
                                content = content.Replace("<", "&#60;");
                                content = content.Replace(">", "&#62;");
                                string toApplyStyle = content;
                                string fontTag = "<font";
                                string fontSize = "";
                                string pid = "";
                                if (objStyle.fontName != null)
                                {
                                    int inlineFontId = 0;
                                    var oFont = oFonts.Where(g => g.FontName == objStyle.fontName).FirstOrDefault();
                                    if (oFont != null)
                                    {
                                        if (System.IO.File.Exists(Font + path + oFont.FontFile + ".ttf"))
                                            inlineFontId = oPdf.EmbedFont(Font + path + oFont.FontFile + ".ttf");
                                        // fontTag += " face='" + objStyle.fontName + "' embed= "+ FontID+" ";
                                    }
                                        pid = "pid ='" + inlineFontId.ToString() + "' ";
                                    
                                }
                                string lineSpacingString = "";
                                if (ooBject.LineSpacing != null)
                                {
                                    lineSpacingString = " linespacing= " + (ooBject.LineSpacing * ooBject.FontSize.Value) + " ";
                                }

                                if (objStyle.fontSize != null)
                                {
                                    lineSpacingString = " linespacing= " + (ooBject.LineSpacing * Convert.ToDouble(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize)))) + " ";
                                    fontSize += "<StyleRun fontsize='" + Convert.ToDouble(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + pid + lineSpacingString + ">";
                                    fontTag += " fontsize='" + Convert.ToDouble(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + lineSpacingString + " ";
                                } 
                                if (objStyle.fontStyle != null)
                                {
                                    fontTag += " font-style='" + objStyle.fontStyle + "'";
                                }
                                if (objStyle.fontWeight != null)
                                {
                                    fontTag += " font-weight='" + objStyle.fontWeight + "'";
                                }
                                if (objStyle.textColor != null)
                                {
                                    if (objStyle.textCMYK != null)
                                    {
                                        string[] vals = objStyle.textCMYK.Split(' ');
                                        string hexC = Convert.ToInt32(vals[0]).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(vals[1]).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(vals[2]).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(vals[3]).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;
                                        // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), objStyle.textCMYK);
                                        //oPdf.Color.Gray = 255;
                                        // fontTag += " color='#FF' csid=" + csInlineID;
                                        if(isTemplateSpot)
                                        {
                                            if(objStyle.spotColorName == "null" || objStyle.spotColorName == null)
                                            {
                                                objStyle.spotColorName = objStyle.textCMYK.Replace(" ","");
                                            }
                                            int csInlineID = oPdf.AddColorSpaceSpot(objStyle.spotColorName, objStyle.textCMYK);
                                            //oPdf.Color.Gray = 255;
                                            fontTag += " color='#FF' csid=" + csInlineID;
                                        }else
                                        {
                                            fontTag += " color='" + hex + "' ";
                                        }
                                       
                                    }
                                    else
                                    {
                                        fontTag += " color='" + objStyle.textColor + "'";
                                    }
                                }
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                        //   Utilities.CMYKtoRGBConverter objCData = new Utilities.CMYKtoRGBConverter();
                                        // string colorHex = objCData.getColorHex();
                                        // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                                        string hexC = Convert.ToInt32(ooBject.ColorC).ToString("X");
                                        if (hexC.Length == 1)
                                            hexC = "0" + hexC;
                                        string hexM = Convert.ToInt32(ooBject.ColorM).ToString("X");
                                        if (hexM.Length == 1)
                                            hexM = "0" + hexM;
                                        string hexY = Convert.ToInt32(ooBject.ColorY).ToString("X");
                                        if (hexY.Length == 1)
                                            hexY = "0" + hexY;
                                        string hexK = Convert.ToInt32(ooBject.ColorK).ToString("X");
                                        if (hexK.Length == 1)
                                            hexK = "0" + hexK;
                                        string hex = "#" + hexC + hexM + hexY + hexK;

                                        fontTag += " color='" + hex + "' ";
                                    }
                                    //fontTag += " color='" + objStyle.textColor + "'";
                                }
                                if (fontSize != "")
                                {
                                    toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                }
                                else
                                {
                                    if (objStyle.fontName != null)
                                    {
                                        fontSize += "<StyleRun " + pid + lineSpacingString + ">";
                                        toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
                                    }
                                    else
                                    {
                                        toApplyStyle = fontTag + " >" + toApplyStyle + "</font>";
                                    }

                                }
                                StyledHtml += toApplyStyle;
                            }
                        }
                        else
                        {
                            string content = ooBject.ContentString[i].ToString();
                            content = content.Replace("<", "&#60;");
                            content = content.Replace(">", "&#62;");
                            StyledHtml += content;
                        }
                    }

                }
                else
                {
                    ooBject.ContentString = ooBject.ContentString.Replace("<", "&#60;");
                    ooBject.ContentString = ooBject.ContentString.Replace(">", "&#62;");

                    StyledHtml += ooBject.ContentString;
                }
                StyledHtml += "</p>";

                string sNewLineNormalized = Regex.Replace(StyledHtml, @"\r(?!\n)|(?<!\r)\n", "<BR>");
                sNewLineNormalized = sNewLineNormalized.Replace("  ", "&nbsp;&nbsp;");

                if (ooBject.isBulletPoint.HasValue && ooBject.isBulletPoint.Value == true)
                {
                    string normalizedBulletPoints = "<ul>";

                    string[] textLines = sNewLineNormalized.Split(new string[] { "<BR>" }, StringSplitOptions.None);
                    foreach(var line in textLines)
                    {
                        string nline = line.Replace("<p>", "");  nline = nline.Replace("</p>", "");
                        normalizedBulletPoints += "<li>" + nline + "</li>";
                    }
                    normalizedBulletPoints += "</ul>";
                    double offset = DesignerUtils.PixelToPoint(4.5) + (ooBject.FontSize.Value * (ooBject.LineSpacing.Value));
                    OPosX -= offset;
                    OWidth += offset;
                    sNewLineNormalized = normalizedBulletPoints;

                }
                if (ooBject.AutoShrinkText == true)
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    int theID = oPdf.AddHtml(sNewLineNormalized);
                    while (oPdf.Chainable(theID))
                    {
                        oPdf.Delete(theID);
                        oPdf.FontSize--;
                        oPdf.Rect.String = oPdf.Rect.String; // reset Doc.Pos without having to save its initial value
                        theID = oPdf.AddHtml(sNewLineNormalized);
                    }
                }
                else
                {
                    oPdf.Rect.Position(OPosX, OPosY);
                    oPdf.Rect.Resize(OWidth, OHeight);
                    oPdf.AddHtml(sNewLineNormalized);
                }
                oPdf.Transform.Reset();

            }
            catch (Exception ex)
            {
                throw new Exception("ADDSingleLineText", ex);
            }

        }

        private void LoadImage(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {
            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)  
                {
                    logoPath = ""; //since path is already in filenm
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.ToLower().Contains("/mpc_content/"))
                    {
                        vals = oObject.ContentString.ToLower().Split(new string[] { "/mpc_content/" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
                        //FilePath = logoPath + oObject.ContentString;
                        bFileExists = System.IO.File.Exists((FilePath));
                    }else
                    {
                        if (oObject.ContentString.ToLower().Contains("assets-v2"))
                        {
                            // dont show any thing becuase path will contain dummy placeholder image
                        }
                        else
                        {
                            // replaced image 
                            if (oObject.ContentString != "")
                                FilePath = oObject.ContentString;
                            FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/" + FilePath;
                            bFileExists = System.IO.File.Exists(FilePath);
                        }
                    }
                   

                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                //  else
                //     filNm = oobject.LogoName;

                if (bFileExists)
                {
                   
                    //  oImg.SetFile(FilePath);

                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {


                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);


                        }


                    }
                    if (oObject.Opacity != null && oObject.Opacity != 1)
                    {
                        img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));

                        if (oObject.Opacity != null)
                        {
                            // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                            if (oObject.Opacity != 1)
                            {
                                img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                            }
                        }
                        oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
                        int id = oPdf.AddImageObject(oImg, true);
                    } else
                    {
                       // XImage oImgx = new XImage();
                        oImg.SetFile(FilePath);
                        oPdf.AddImageObject(oImg, true);
                    }
                    //oPdf.FrameRect();

                 
                    //if (oObject.Tint != null)
                    //{
                    //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                    //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                    //}

                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }

        private void LoadCroppedImg(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    logoPath = ""; //since path is already in filenm
                    string[] vals;
                    FilePath = "";
                    if (oObject.ContentString.ToLower().Contains("mpc_content"))
                    {
                        vals = oObject.ContentString.ToLower().Split(new string[] { "mpc_content" }, StringSplitOptions.None);
                        FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
                     //   FilePath = logoPath + oObject.ContentString;
                        bFileExists = System.IO.File.Exists((FilePath));
                    }
                    else
                    {
                        if (oObject.ContentString.ToLower().Contains("assets-v2"))
                        {
                            // dont show any thing becuase path will contain dummy placeholder image
                        } else
                        {
                            // replaced image 
                            if (oObject.ContentString != "")
                                FilePath = oObject.ContentString;
                            FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content") + "/" + FilePath;
                            bFileExists = System.IO.File.Exists(FilePath);
                        }
                    }
                   
                }
                else
                {
                    if (oObject.ContentString != "")
                        FilePath = oObject.ContentString;
                    FilePath = logoPath + "/" + FilePath;
                    bFileExists = System.IO.File.Exists(FilePath);
                }
                if (bFileExists)
                {
                    

                    var posY = oObject.PositionY + oObject.MaxHeight;
                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(oObject.ClippedInfo);
                    string xpath = "Cropped";
                    var nodes = xmlDoc.SelectNodes(xpath);
                    double sx, sy, swidth, sheight;
                    int isCroppped = 0;

                    if (oObject.Opacity != null && oObject.Opacity != 1)
                    {
                        img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
                        if (oObject.Opacity != null)
                        {
                            if (oObject.Opacity != 1)
                            {
                                img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                            }
                        }
                        oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
                        foreach (XmlNode childrenNode in nodes)
                        {
                            sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                            sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                            swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                            sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                            if (childrenNode.SelectSingleNode("isCropped") != null)
                                isCroppped = Convert.ToInt32(childrenNode.SelectSingleNode("isCropped").InnerText);

                            if(isCroppped == 0 )
                            {
                                swidth = DesignerUtils.PixelToPoint(swidth);
                                sheight = DesignerUtils.PixelToPoint(sheight);
                                posY = oObject.PositionY + sheight;
                                oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                                oPdf.Rect.Resize(swidth, sheight);
                            }
                            else
                            {
                                oImg.Selection.Inset(sx, sy);
                                oImg.Selection.Height = sheight;
                                oImg.Selection.Width = swidth;
                            }
                         
                        }
                        int id = oPdf.AddImageObject(oImg, true);
                    }
                    else
                    {
                        if (System.IO.Path.GetExtension(FilePath).ToLower().Contains(".tif"))
                        {
                            oPdf.AddImageFile(FilePath);
                        }
                        else
                        {
                            oImg.SetFile(FilePath);
                            foreach (XmlNode childrenNode in nodes)
                            {
                                sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                                sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                                swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                                sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                                if (childrenNode.SelectSingleNode("isCropped") != null)
                                    isCroppped = Convert.ToInt32(childrenNode.SelectSingleNode("isCropped").InnerText);

                                if (isCroppped == 0)
                                {
                                    swidth = DesignerUtils.PixelToPoint(swidth);
                                    sheight = DesignerUtils.PixelToPoint(sheight);
                                    posY = oObject.PositionY + sheight;
                                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                                    oPdf.Rect.Resize(swidth, sheight);
                                }
                                else
                                {
                                    oImg.Selection.Inset(sx, sy);
                                    oImg.Selection.Height = sheight;
                                    oImg.Selection.Width = swidth;
                                }
                               
                            }
                            oPdf.AddImageObject(oImg, true);
                        }
                    }
                    
                    oPdf.Transform.Reset();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("LoadImage", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
            }
        }
        //vector line drawing in PDF
        private void DrawVectorLine(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    //if (!ooBject.IsColumnNull("Tint"))
                    oPdf.Color.Alpha = Convert.ToInt32((oObject.Tint) * 2.55);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                oPdf.Width = oObject.MaxHeight.Value;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value);
                    }
                }

                // oPdf.AddImageObject(oImg,false);
                //oPdf.AddImage ((oImg);
                oPdf.AddLine(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2, oObject.PositionX.Value + oObject.MaxWidth.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorLine", ex);
            }

        }

        //vector rectangle drawing in PDF
        private void DrawVectorRectangle(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.55);
                    //if (!ooBject.IsColumnNull("Tint"))
                    //oPdf.Color.Alpha = 0;//Convert.ToInt32((100 - oObject.Tint) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }


                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                int id = oPdf.FillRect();
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorRectangle", ex);
            }

        }

        //vector Ellipse drawing in PDF
        private void DrawVectorEllipse(ref Doc oPdf, TemplateObject oObject, int PageNo)
        {

            try
            {
                oPdf.PageNumber = PageNo;

                if (oObject.ColorType == 3)
                {
                    if (oObject.IsSpotColor == true)
                    {
                        oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
                    }
                    oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();

                    if (oObject.Opacity != null)
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.5);
                }
                else if (oObject.ColorType == 4) // For RGB Colors
                {
                    oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
                }




                //oPdf.Width = oobject.MaxHeight;
                oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
                oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


                if (oObject.RotationAngle != null)
                {

                    if (oObject.RotationAngle != 0)
                    {
                        oPdf.Transform.Reset();
                        oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
                    }


                }

                int id = oPdf.FillRect(oObject.MaxWidth.Value / 2, oObject.MaxHeight.Value / 2);
                //if (oObject.Tint != null)
                //{
                //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
                //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
                //}
                //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
                oPdf.Transform.Reset();

            }

            catch (Exception ex)
            {
                throw new Exception("DrawVectorEllipse", ex);
            }

        }

        private void GetSVGAndDraw(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {
            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
            //XImage oImg = new XImage();
            //Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ContentString != "")
                    FilePath = oObject.ContentString;
                FilePath = logoPath + "/" + FilePath;
                bFileExists = System.IO.File.Exists(FilePath);
                if (bFileExists)
                {

                    var posY = oObject.PositionY + oObject.MaxHeight;

                    oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
                    oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

                    if (oObject.RotationAngle != null)
                    {
                        if (oObject.RotationAngle != 0)
                        {
                            oPdf.Transform.Reset();
                            oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
                        }
                    }
                    //oPdf.Transform.Reset();
                    oPdf.HtmlOptions.HideBackground = true;
                    oPdf.HtmlOptions.Engine = EngineType.Gecko;
             
                    float width =(float)oObject.MaxWidth.Value, height = (float)oObject.MaxHeight.Value;
                    string URl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/MPC_Content" + oObject.ContentString;
                    //if (oObject.originalContentString != null)
                    //{



                    //    string svgProp = DesignerSvgParser.UpdateSvgData(FilePath, height, width);//
                    //    string html = "<svg " + svgProp + "> " + oObject.originalContentString + " </svg>";
                    //    html = "<html><head><style>html, body { margin:0; padding:0; overflow:hidden } svg { position:fixed; top:0; left:0; height:100%; width:100% }</style></head><body  style='  padding: 0px 0px 0px 0px;margin: 0px 0px 0px 0px;'>" + html + "</body></html>";
                    //    oPdf.AddImageHtml(html);
                    //}
                    //else
                    //{

                        List<svgColorData> styles = new List<svgColorData>();
                        if (oObject.textStyles != null)
                        {
                            styles = JsonConvert.DeserializeObject<List<svgColorData>>(oObject.textStyles);
                        }

                        string file = DesignerSvgParser.UpdateSvgBasedOnClr(FilePath, height, width, styles);//
                        string html = File.ReadAllText(file);
                        html = "<html><head><style>html, body { margin:0; padding:0; overflow:hidden } svg { position:fixed; top:0; left:0; height:100%; width:100% }</style></head><body  style='  padding: 0px 0px 0px 0px;margin: 0px 0px 0px 0px;'>" + html + "</body></html>";
                        oPdf.AddImageHtml(html);
                    //}
                    oPdf.Transform.Reset();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LoadSvg", ex);
            }
            finally
            {
              //  oImg.Dispose();
               // if (img != null)
                  //  img.Dispose();
            }
        }
        private void DrawCuttingLines(ref Doc oPdf, double mrg, int PageNo, string pageName, string waterMarkTxt, bool drawCuttingMargins, bool drawWatermark, bool isWaterMarkText, double pdfTemplateHeight, double pdfTemplateWidth,double trimBoxSize, double bleedOffset)
        {
            try
            {
                oPdf.Color.String = "100 100 100 100";

                if (trimBoxSize != 0) // for digital central 
                {
                    mrg = DesignerUtils.MMToPoint(trimBoxSize);
                }
                double offset = 0;
                if (bleedOffset != 0 ) // for digital central 
                {
                    offset = DesignerUtils.MMToPoint(bleedOffset);
                }
                //mrg = 5.98110236159; // global change on request of digital central to make crop marks 2.11 mm
                oPdf.Layer = oPdf.LayerCount - 1;
                oPdf.PageNumber = PageNo;
                //oPdf.Width = 0.5;
                oPdf.Width = 0.25;
                oPdf.Rect.Left = oPdf.MediaBox.Left;
                oPdf.Rect.Top = oPdf.MediaBox.Top;
                oPdf.Rect.Right = oPdf.MediaBox.Right;
                oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
                double pgWidth = oPdf.MediaBox.Width;
                double pgHeight = oPdf.MediaBox.Height;
                for (int i = 1; i <= oPdf.PageCount; i++)
                {
                    oPdf.PageNumber = i;
                    if (drawCuttingMargins)
                    {
                        oPdf.Layer = 1;
                        oPdf.AddLine(mrg, 0, mrg, mrg - offset);
                        oPdf.AddLine(0, mrg, mrg - offset, mrg);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, 0, oPdf.MediaBox.Width - mrg, mrg - offset);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, mrg, oPdf.MediaBox.Width, mrg);
                        oPdf.AddLine(0, oPdf.MediaBox.Height - mrg, mrg - offset, oPdf.MediaBox.Height - mrg);
                        oPdf.AddLine(mrg, oPdf.MediaBox.Height - mrg + offset, mrg, oPdf.MediaBox.Height);
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg); //----
                        oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg + offset, oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height); //|

                        // adding date time stamp
                        oPdf.Layer = 1;
                        oPdf.TextStyle.Outline = 0;
                        oPdf.TextStyle.Strike = false;
                        // oPdf.TextStyle.Bold = true;
                        oPdf.TextStyle.Italic = false;
                        oPdf.TextStyle.CharSpacing = 0;
                        oPdf.TextStyle.Size = 6;
                        oPdf.Rect.Position(((pgWidth / 2) - 20), pgHeight + 5);
                        oPdf.Rect.Resize(200, 10);
                        oPdf.AddHtml("" + pageName + " " + DateTime.Now.ToString());
                        oPdf.Transform.Reset();
                    }
                    // water mark 
                    if (drawWatermark)
                    {
                        if (waterMarkTxt != null && waterMarkTxt != "")
                        {
                            if (isWaterMarkText)
                            {
                                oPdf.Color.String = "16 12 13 0";
                                oPdf.Color.Alpha = 120;
                                oPdf.TextStyle.Size = 30;
                                oPdf.Layer = 1;
                                oPdf.HPos = 0.5;
                                oPdf.VPos = 0.5;
                                oPdf.TextStyle.Outline = 2;
                                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                                // oPdf.FrameRect();
                                oPdf.Transform.Reset();
                                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                                oPdf.AddHtml(waterMarkTxt);
                                oPdf.Transform.Reset();
                            }
                            else
                            {
                                string FilePath = string.Empty;
                                XImage oImg = new XImage();
                                System.Drawing.Image objImage = null;
                                try
                                {
                                    oPdf.PageNumber = i;


                                    bool bFileExists = false;
                                    FilePath = waterMarkTxt;
                                    bFileExists = System.IO.File.Exists(FilePath);

                                    if (bFileExists)
                                    {
                                        objImage = System.Drawing.Image.FromFile(FilePath);
                                        oImg.SetFile(FilePath);
                                        double height = DesignerUtils.PixelToPoint(objImage.Height);
                                        double width = DesignerUtils.PixelToPoint(objImage.Width);
                                        if (height > pdfTemplateHeight)
                                        {
                                            height = pdfTemplateHeight;
                                        }
                                        if (width > pdfTemplateWidth)
                                        {
                                            width = pdfTemplateWidth;
                                        }

                                        double posX = (oPdf.MediaBox.Width - width) / 2;
                                        double posY = (oPdf.MediaBox.Height - height) / 2 + height;


                                        oPdf.Layer = 1;
                                        oPdf.Rect.Position(posX, posY);
                                        oPdf.Rect.Resize(width, height);
                                        oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);

                                        oPdf.AddImageObject(oImg, true);
                                        oPdf.Transform.Reset();
                                    }


                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("LoadWaterMarkImage", ex);
                                }
                                finally
                                {
                                    oImg.Dispose();
                                    if (objImage != null)
                                        objImage.Dispose();
                                }
                                // image
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DrawCuttingLine", ex);
            }
        }
        private void DrawBackgrounText(ref Doc oPdf)
        {
            int FontID = oPdf.AddFont("Arial");
            for (int i = 1; i <= oPdf.PageCount; i++)
            {
                oPdf.PageNumber = i;
                oPdf.Color.String = "211 211 211";
                //oPdf.Color.Alpha = 60;
                oPdf.Font = FontID;
                oPdf.TextStyle.Size = 40;
                //oPdf.TextStyle.CharSpacing = 2;
                //oPdf.TextStyle.Bold = true;
                //oPdf.TextStyle.Italic = false;
                oPdf.HPos = 0.5;
                oPdf.VPos = 0.5;
                oPdf.TextStyle.Outline = 2;
                oPdf.Rect.Position(0, oPdf.MediaBox.Height);
                oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
                // oPdf.FrameRect();
                oPdf.Transform.Reset();
                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
                oPdf.AddHtml("MPC Systems");
            }
            oPdf.HPos = 0;
            oPdf.VPos = 0;
            oPdf.TextStyle.Outline = 0;
            oPdf.TextStyle.Strike = false;
            oPdf.TextStyle.Bold = false;
            oPdf.TextStyle.Italic = false;
            oPdf.TextStyle.CharSpacing = 0;
            oPdf.Transform.Reset();
            oPdf.Transform.Rotate(0, 0, 0);
            oPdf.Transform.Reset();
        }
        //generate pdf function
        private byte[] generatePDF(Template objProduct, TemplatePage objProductPage, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        {
            Doc doc = new Doc();
            try
            {
                var FontsList = _templateFontService.GetFontListForTemplate(objProduct.ProductId);
                doc.TopDown = true;

                try
                {

                    if (!isoverLayMode)
                    {
                        if (objProductPage.BackGroundType == 1)  //PDF background
                        {
                            if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                            {
                                doc.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                               
                            }
                        }
                        else if (objProductPage.BackGroundType == 2) //background color
                        {
                          //  if (objProductPage.Orientation == 1) //standard 
                          //  {
                            if(objProductPage.Height.HasValue)
                            {
                                doc.MediaBox.Height = objProductPage.Height.Value;
                            }else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            }
                            if(objProductPage.Width.HasValue)
                            {
                                doc.MediaBox.Width = objProductPage.Width.Value;
                            }else
                            {
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                            }
                                
                                
                                
                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            LoadBackColor(ref doc, objProductPage);
                        }
                        else if (objProductPage.BackGroundType == 3) //background Image
                        {

                          //  if (objProductPage.Orientation == 1) //standard 
                          //  {
                            if (objProductPage.Height.HasValue)
                            {
                                doc.MediaBox.Height = objProductPage.Height.Value;
                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            }
                            if (objProductPage.Width.HasValue)
                            {
                                doc.MediaBox.Width = objProductPage.Width.Value;
                            }
                            else
                            {
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                            }

                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                        }
                    }
                    else
                    {
                        //if (objProductPage.Orientation == 1) //standard 
                        //{
                        if (objProductPage.Height.HasValue)
                        {
                            doc.MediaBox.Height = objProductPage.Height.Value;
                        }
                        else
                        {
                            doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                        }
                        if (objProductPage.Width.HasValue)
                        {
                            doc.MediaBox.Width = objProductPage.Width.Value;
                        }
                        else
                        {
                            doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
                        }

                        //}
                        //else
                        //{
                        //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                        //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                        //}
                        doc.AddPage();
                    }
                }
                catch (Exception ex)
                {
                    throw new MPCException(ex.ToString(), OrganisationID);
                }


                double YFactor = 0;
                double XFactor = 0;
                // int RowCount = 0;




                List<TemplateObject> oParentObjects = null;

                if (IsDrawHiddenObjects)
                {
                    if (isoverLayMode == true)
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                    else
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                }
                else
                {
                    if (isoverLayMode == true)
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                    else
                    {
                        oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null) && (g.hasClippingPath == false || g.hasClippingPath == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                    }
                }
                int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();
                hasOverlayObject = false;
                if (count > 0)
                {
                    hasOverlayObject = true;
                }
                foreach (var objObjects in oParentObjects)
                {
                    if (objObjects.PositionY == null)
                        objObjects.PositionY = 0;
                    if (objObjects.PositionX == null)
                        objObjects.PositionX = 0;
                    if (XFactor != objObjects.PositionX)
                    {
                        if (objObjects.ContentString == ""){
                            if(objObjects.PositionY.HasValue)
                                YFactor = objObjects.PositionY.Value - 7;
                            else
                                YFactor = 0;
                        }
                        else
                            YFactor = 0;
                        if(objObjects.PositionX.HasValue)
                          XFactor = objObjects.PositionX.Value;
                    }



                    if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                    {


                        int VAlign = 1, HAlign = 1;

                        HAlign = objObjects.Allignment.Value;

                        VAlign = objObjects.VAllignment.Value;

                        double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                        if (VAlign == 1 || VAlign == 2 || VAlign == 3)
                            currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                        bool isTemplateSpot = false;
                        if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                            isTemplateSpot = true;

                        AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot,OrganisationID);



                    }
                    // object type 13 real state property image 

                    else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                    {
                        //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                        // {
                        if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                        {
                            if (objObjects.ClippedInfo == null)
                            {
                                if (objObjects.ContentString.Contains(".svg") && !objObjects.ContentString.Contains("{{"))
                                {
                                    GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                }
                                else
                                {
                                    LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                }
                            }
                            else
                            {
                                LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            }
                        }
                        //  }
                        //  else
                        // {
                        //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        // }
                    }
                    else if (objObjects.ObjectType == 5)    //line vector
                    {
                        DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 6)    //line vector
                    {
                        DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 7)    //line vector
                    {
                        DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                    }
                    else if (objObjects.ObjectType == 9)    //svg Path
                    {
                        GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                    }

                }
                double TrimBoxSize = 5;
                double BleedBoxSize = 0;
                //crop marks or margins
                if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
                {
                    //doc.CropBox.Height = doc.MediaBox.Height;
                    //doc.CropBox.Width = doc.MediaBox.Width;


                    bool isWaterMarkText = objProduct.isWatermarkText ?? true;
                    if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
                    {
                        TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                    }
                    doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
                    if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                    {
                        double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                        doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

                    }
                    if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                    {
                        BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                        doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
                    }
                    if(bleedareaSize != 0 ){
                         
                        doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + (bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + (bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - (bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - (bleedareaSize)).ToString());
                    }
                    int FontID = 0;
                    var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
                    //if (pFont != null)
                    //{
                    //    string path = "";
                    //    if (pFont.FontPath == null)
                    //    {
                    //        path = "";
                    //    }
                    //    else
                    //    {  // customer fonts 

                    //        path = pFont.FontPath;
                    //    }
                    //    if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                    //        FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");


                    //}
                    if (pFont != null)
                    {
                        string path = "";
                        if (pFont.FontPath == null)
                        {
                            // mpc designers fonts or system fonts 
                            path = "Organisation" + OrganisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
                        }
                        else
                        {  // customer fonts 
                            path = pFont.FontPath;
                        }
                        if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                            FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                    }
                    doc.Font = FontID;
                    double trimboxSizeCuttingLines = 0;
                    if (TrimBoxSize != 5)
                        trimboxSizeCuttingLines = TrimBoxSize;
                    DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
                }

                if (IsDrawBGText == true)
                {
                    DrawBackgrounText(ref doc);
                }
                int res = 300;
                if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                {
                    res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                }
                doc.Rendering.DotsPerInch = res;

                //if (ShowHighResPDF == false)
                //    opage.Session["PDFFile"] = doc.GetData();
                //OpenPage(opage, "Admin/Products/ViewPdf.aspx");

                return doc.GetData();
            }
            catch (Exception ex)
            {
                throw new Exception("ShowPDF", ex);
            }
            finally
            {
                doc.Dispose();
            }
        }
        // generating multipage pdf 
        private byte[] generatePDF(Template objProduct, List<TemplatePage> productPages, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        {
            hasOverlayObject = false;
            Doc doc = new Doc();
            try
            {
                var FontsList = _templateFontService.GetFontListForTemplate(objProduct.ProductId);
                doc.TopDown = true;
                foreach (var objProductPage in productPages)
                {
                    try
                    {

                        if (!isoverLayMode)
                        {
                            if (objProductPage.BackGroundType == 1)  //PDF background
                            {
                                if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                                {
                                    using (var cPage = new Doc())
                                    {
                                        try
                                        {
                                            cPage.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                                            doc.Append(cPage);
                                            doc.PageNumber = objProductPage.PageNo.Value;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new Exception("Appedning", ex);
                                        }
                                        finally
                                        {
                                            cPage.Dispose();
                                        }

                                    }

                                }
                            }
                            else if (objProductPage.BackGroundType == 2) //background color
                            {
                                //if (objProductPage.Orientation == 1) //standard 
                                //{
                                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                //}
                                //else
                                //{
                                //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                //}
                                doc.AddPage();
                                doc.PageNumber = objProductPage.PageNo.Value;
                                LoadBackColor(ref doc, objProductPage);
                            }
                            else if (objProductPage.BackGroundType == 3) //background Image
                            {

                                //if (objProductPage.Orientation == 1) //standard 
                                //{
                                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                                //}
                                //else
                                //{
                                //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                                //}
                                doc.AddPage();
                                doc.PageNumber = objProductPage.PageNo.Value;
                                LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                            }
                        }
                        else
                        {
                            //if (objProductPage.Orientation == 1) //standard 
                            //{
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                            //}
                            //else
                            //{
                            //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            //}
                            doc.AddPage();
                            doc.PageNumber = objProductPage.PageNo.Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new MPCException(ex.ToString(), OrganisationID);
                    }


                    double YFactor = 0;
                    double XFactor = 0;
                    // int RowCount = 0;




                    List<TemplateObject> oParentObjects = null;

                    if (IsDrawHiddenObjects)
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    else
                    {
                        if (isoverLayMode == true)
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                        else
                        {
                            oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
                        }
                    }
                    int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();

                    if (count > 0)
                    {
                        hasOverlayObject = true;
                    }
                    foreach (var objObjects in oParentObjects)
                    {
                        if (objObjects.PositionX == null)
                            objObjects.PositionX = 0;
                        if (objObjects.PositionY == null)
                            objObjects.PositionY = 0;
                        if (objObjects.MaxHeight == null)
                            objObjects.MaxHeight = 0;
                        if (objObjects.MaxWidth == null)
                            objObjects.MaxWidth = 0;
                        if (XFactor != objObjects.PositionX)
                        {
                            if (objObjects.ContentString == ""){
                                  if(objObjects.PositionY.HasValue)
                                      YFactor = objObjects.PositionY.Value - 7;
                            else
                                YFactor = 0;
                            }
                            else
                                YFactor = 0;
                            if (objObjects.PositionX.HasValue)
                                XFactor = objObjects.PositionX.Value;
                            else
                                XFactor = 0;
                        }



                        if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                        {


                            int VAlign = 1, HAlign = 1;

                            HAlign = objObjects.Allignment.Value;

                            VAlign = objObjects.VAllignment.Value;

                            double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                            if (VAlign == 1 || VAlign == 2 || VAlign == 3)
                                currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                            bool isTemplateSpot = false;
                            if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                                isTemplateSpot = true;

                            AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot,OrganisationID);



                        }
                        // object type 13 real state property image 

                        else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
                        {
                            //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
                            // {
                            if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
                            {
                                if (objObjects.ClippedInfo == null)
                                {
                                    if (objObjects.ContentString.Contains(".svg") && !objObjects.ContentString.Contains("{{"))
                                    {
                                        GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                    else
                                    {
                                        LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                    }
                                }
                                else
                                {
                                    LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                                }
                            }
                            //  }
                            //  else
                            // {
                            //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            // }
                        }
                        else if (objObjects.ObjectType == 5)    //line vector
                        {
                            DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 6)    //line vector
                        {
                            DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 7)    //line vector
                        {
                            DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
                        }
                        else if (objObjects.ObjectType == 9)    //svg Path
                        {
                            GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                            //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
                        }

                    }
                    double TrimBoxSize = 5;
                    double BleedBoxSize = 0;
                    if (drawBleedArea)
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
                        {
                            TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
                        }
                        doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
                        if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
                        {
                            double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
                            doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

                        }
                       
                        if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
                        {
                            BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
                            doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
                        }
                        if (bleedareaSize != 0)
                        {

                            doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + (bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + (bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - (bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - (bleedareaSize)).ToString());
                        }
                    }
                    //crop marks or margins
                    if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0 )
                    {
                        //doc.CropBox.Height = doc.MediaBox.Height;
                        //doc.CropBox.Width = doc.MediaBox.Width;


                        bool isWaterMarkText = objProduct.isWatermarkText ?? true;
                       
                        int FontID = 0;
                        var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
                        if (pFont != null)
                        {
                            string path = "";
                            if (pFont.FontPath == null)
                            {
                                // mpc designers fonts or system fonts 
                                path = "Organisation" + OrganisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
                            }
                            else
                            {  // customer fonts 
                                path = pFont.FontPath;
                            }
                            if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
                                FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
                        }

                        doc.Font = FontID;
                        double trimboxSizeCuttingLines = 0;
                        if (TrimBoxSize != 5)
                            trimboxSizeCuttingLines = TrimBoxSize;
                        DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
                    }

                    if (IsDrawBGText == true)
                    {
                        DrawBackgrounText(ref doc);
                    }
                    int res = 300;
                    if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
                    {
                        res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
                    }
                    doc.Rendering.DotsPerInch = res;

                    //if (ShowHighResPDF == false)
                    //    opage.Session["PDFFile"] = doc.GetData();
                    //OpenPage(opage, "Admin/Products/ViewPdf.aspx");
                }
                return doc.GetData();
            }
            catch (Exception ex)
            {
                throw new Exception("ShowPDF", ex);
            }
            finally
            {
                doc.Dispose();
            }

        }

        // generate low res proof image from pdf file 
        private string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {
            CuttingMargin = DesignerUtils.PixelToPoint(CuttingMargin);
            using (Doc theDoc = new Doc())
            {
                Stream str = null;
                try
                {
                    theDoc.Read(PDFDoc);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(savePath) == false)
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }
                    //string filePath = savePath + PreviewFileName + ".png";
                    string filePath2 = savePath + PreviewFileName + ".jpg";
                    theDoc.Rendering.DotsPerInch = DPI;
                    //theDoc.Rendering.Save(filePath);
                    theDoc.Rendering.Save(filePath2);
                    theDoc.Dispose();
                    //if (RoundCorners)
                    //{
                    //    generateRoundCorners(filePath, filePath,str);
                    //}



                    return PreviewFileName + ".jpg";



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                    if(str != null)
                       str.Dispose();
                }
            }

        }
        private string generatePagePreview(string PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {
            CuttingMargin = DesignerUtils.PixelToPoint(CuttingMargin);
            using (Doc theDoc = new Doc())
            {
                Stream str = null;
                try
                {
                    theDoc.Read(PDFDoc);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;
                    theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                    if (System.IO.Directory.Exists(savePath) == false)
                    {
                        System.IO.Directory.CreateDirectory(savePath);
                    }
                    //string filePath = savePath + PreviewFileName + ".png";
                    string filePath2 = savePath + PreviewFileName + ".jpg";
                    theDoc.Rendering.DotsPerInch = DPI;
                    //theDoc.Rendering.Save(filePath); 
                    theDoc.Rendering.Save(filePath2);
                    theDoc.Dispose();
                    //if (RoundCorners)
                    //{
                    //    generateRoundCorners(filePath, filePath,str);
                    //}



                    return PreviewFileName + ".jpg";



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                    if (str != null)
                        str.Dispose();
                }
            }

        }
        public bool generatePagePreviewMultiplage(byte[] PDFDoc, string savePath, double CuttingMargin, int DPI, bool RoundCorners)
        {

            CuttingMargin = DesignerUtils.PixelToPoint(CuttingMargin); // as when we get template back from Designer it contains cutting margin in pixels
            //XSettings.License = "810-031-225-276-0715-601";
            using (Doc theDoc = new Doc())
            {

                try
                {
                    theDoc.Read(PDFDoc);
                    for (int i = 1; i <= theDoc.PageCount; i++)
                    {


                        theDoc.PageNumber = i;
                        theDoc.Rect.String = theDoc.CropBox.String;
                        theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

                        if (System.IO.Directory.Exists(savePath) == false)
                        {
                            System.IO.Directory.CreateDirectory(savePath);
                        }

                        theDoc.Rendering.DotsPerInch = DPI;
                        //string fileName = "p" + i + ".png";
                        string fileName2 = "p" + i + ".jpg";
                        //if (RoundCorners)
                        //{
                        //    Stream str = new MemoryStream();

                        //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName), str);
                        //    generateRoundCorners(System.IO.Path.Combine(savePath, fileName), System.IO.Path.Combine(savePath, fileName), str);

                        //}
                        //else
                        //{
                        //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
                        //}
                        //theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName2));
                    }

                    theDoc.Dispose();

                    return true;



                }
                catch (Exception ex)
                {
                    throw new Exception("generatePagePreview", ex);
                }
                finally
                {
                    if (theDoc != null)
                        theDoc.Dispose();
                }
            }
        }
        private void generateRoundCorners(string physicalPath, string pathToSave,Stream str)
        {
            string path = physicalPath;
            int roundedDia = 30;
            Bitmap bitmap = null;
            using (Image imgin = Image.FromStream(str))//FromFile(path)
            {
                bitmap = new Bitmap(imgin.Width, imgin.Height);
                Graphics g = Graphics.FromImage(bitmap);
                try
                {
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = (System.Drawing.Drawing2D.SmoothingMode.AntiAlias);
                    Brush brush = new System.Drawing.TextureBrush(imgin);
                    FillRoundedRectangle(g, new Rectangle(0, 0, imgin.Width, imgin.Height), roundedDia, brush);
                    if (File.Exists(pathToSave))
                    {
                        File.Delete(pathToSave);
                    }
                    bitmap.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Png);

                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    if(bitmap != null)
                       bitmap.Dispose();
                    if(imgin != null)
                        imgin.Dispose();
                    if(g != null)
                        g.Dispose();
                }
            }
        }
        private static void FillRoundedRectangle(Graphics g, Rectangle r, int d, Brush b)
        {
            
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);

                g.FillPath(b, gp);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(gp != null)
                  gp.Dispose();
            }
           
        }
        #endregion
        #region private
        public readonly ITemplateRepository _templateRepository;
        public readonly IProductCategoryRepository _ProductCategoryRepository;
        public readonly ITemplateBackgroundImagesService _templateBackgroundImagesService;
        public readonly ITemplateFontsService _templateFontService;
        public readonly ICompanyRepository _companyRepository;
        public readonly ICompanyContactRepository _contactRepository;
        public readonly IOrganisationRepository _organisationRepository;
        public readonly ITemplatePageService _templatePageService;
        public readonly IItemRepository _itemRepository;
        // it will convert pdf to template pages and will preserve template objects and images 
        private bool CovertPdfToBackground(string physicalPath, long ProductID, long OrganisationID)
        {
            bool result = false;
            double pdfHeight, pdfWidth = 0;
            try
            {
                using (Doc theDoc = new Doc())
                {
                    try
                    {
                            List<TemplateObject> listTobjs = new List<TemplateObject>();
                            List<TemplatePage> listTpages = new List<TemplatePage>();
                            List<TemplatePage> listNewTemplatePages = new List<TemplatePage>();
                            double cuttingMargins = 0;
                            _templateRepository.DeleteTemplatePagesAndObjects(ProductID, out listTobjs,out listTpages);
                            theDoc.Read(physicalPath);
                            pdfWidth = theDoc.MediaBox.Width;
                            pdfHeight = theDoc.MediaBox.Height;
                            int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                            int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                            // create template pages
                            for (int i = 1; i <= theDoc.PageCount; i++)
                            {
                                theDoc.PageNumber = i;
                                theDoc.Rect.String = theDoc.CropBox.String;
                                theDoc.Rect.Inset(cuttingMargins, cuttingMargins);
                                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
                                //check if folder exist
                                string tempFolder = drURL + ProductID.ToString();
                                if (!System.IO.Directory.Exists(tempFolder))
                                {
                                    System.IO.Directory.CreateDirectory(tempFolder);
                                }
                                // generate image 
                                string ThumbnailFileName = "/templatImgBk";
                                theDoc.Rendering.DotsPerInch = 150;
                                string renderingPath = tempFolder + ThumbnailFileName + i.ToString() + ".jpg";
                                theDoc.Rendering.Save(renderingPath);
                                // save template page 
                                TemplatePage objPage = new TemplatePage();
                                objPage.PageNo = i;
                                objPage.ProductId = ProductID;
                                objPage.PageName = "Page " + i;
                                if (theDoc.PageCount == 2 && i == 1)
                                {
                                    objPage.PageName = "Front";
                                }
                                else if (theDoc.PageCount == 2 && i == 2)
                                {
                                    objPage.PageName = "Back";
                                }
                                else if (theDoc.PageCount == 4 && i == 1)
                                {
                                    objPage.PageName = "Front";
                                }
                                else if (theDoc.PageCount == 4 && i == 2)
                                {
                                    objPage.PageName = "Inside Front";
                                }
                                else if (theDoc.PageCount == 4 && i == 3)
                                {
                                    objPage.PageName = "Inside Back";
                                }
                                else if (theDoc.PageCount == 4 && i == 2)
                                {
                                    objPage.PageName = "Back";
                                }
                                objPage.BackgroundFileName = ProductID + "/Side" + (i).ToString() + ".pdf";
                                listNewTemplatePages.Add(objPage);
                               
                                // save pdf 
                                Doc singlePagePdf = new Doc();
                                try
                                {
                                    singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                    singlePagePdf.AddPage();
                                    singlePagePdf.AddImageDoc(theDoc, i, null);
                                    singlePagePdf.FrameRect();

                                    int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                    if (srcDocRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                    }
                                    if (srcPageRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                    }
                                    string targetFolder = drURL;
                                    if (File.Exists(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf"))
                                    {
                                        File.Delete(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    }
                                    singlePagePdf.Save(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    singlePagePdf.Clear();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("GenerateTemplateBackground", e);
                                }
                                finally
                                {
                                    if (singlePagePdf != null)
                                        singlePagePdf.Dispose();
                                }
                            }
                        /// save the pages
                            result = _templateRepository.updateTemplate(ProductID, pdfWidth, pdfHeight,listNewTemplatePages,listTpages,listTobjs);
                        theDoc.Dispose();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("GeneratePDfPreservingObjects", ex);
                    }
                    finally
                    {
                        if (theDoc != null)
                            theDoc.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // it willl convert pdf to template pages and will delete all existing objects and images 
        private bool CovertPdfToBackgroundWithoutObjects(string physicalPath, long ProductID, long OrganisationID)
        {
            bool result = false;
            try
            {
                int CuttingMargin = 0;
                double pdfHeight, pdfWidth = 0;
                _templateRepository.DeleteTemplatePagesAndObjects(ProductID);
                _templateBackgroundImagesService.DeleteTemplateBackgroundImages(ProductID,OrganisationID);
                using (Doc theDoc = new Doc())
                {

                    try
                    {
                        theDoc.Read(physicalPath);
                        int tID = 0;
                        pdfWidth = theDoc.MediaBox.Width;
                        pdfHeight = theDoc.MediaBox.Height;
                        List<TemplatePage> listPages = new List<TemplatePage>();
                        int srcPagesID = theDoc.GetInfoInt(theDoc.Root, "Pages");
                        int srcDocRot = theDoc.GetInfoInt(srcPagesID, "/Rotate");
                        string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" );
                        for (int i = 1; i <= theDoc.PageCount; i++)
                        {
                            theDoc.PageNumber = i;
                            theDoc.Rect.String = theDoc.CropBox.String;
                            theDoc.Rect.Inset(CuttingMargin, CuttingMargin);
                            //check if folder exist
                            string tempFolder = drURL + ProductID.ToString();
                            if (!System.IO.Directory.Exists(tempFolder))
                            {
                                System.IO.Directory.CreateDirectory(tempFolder);
                            }
                            // generate image 
                            string ThumbnailFileName = "/templatImgBk";
                            theDoc.Rendering.DotsPerInch = 150;
                            string renderingPath = tempFolder + ThumbnailFileName + i.ToString() + ".jpg";
                            theDoc.Rendering.Save(renderingPath);
                            // save template page 
                            TemplatePage objPage = new TemplatePage();
                            objPage.PageNo = i;
                            objPage.ProductId = ProductID;
                            objPage.PageName = "Page " + i;
                            if (theDoc.PageCount == 2 && i == 1)
                            {
                                objPage.PageName = "Front";
                            }
                            else if (theDoc.PageCount == 2 && i == 2)
                            {
                                objPage.PageName = "Back";
                            }
                            else if (theDoc.PageCount == 4 && i == 1)
                            {
                                objPage.PageName = "Front";
                            }
                            else if (theDoc.PageCount == 4 && i == 2)
                            {
                                objPage.PageName = "Inside Front";
                            }
                            else if (theDoc.PageCount == 4 && i == 3)
                            {
                                objPage.PageName = "Inside Back";
                            }
                            else if (theDoc.PageCount == 4 && i == 2)
                            {
                                objPage.PageName = "Back";
                            }
                            objPage.BackgroundFileName = ProductID + "/Side" + (i).ToString() + ".pdf";
                            listPages.Add(objPage);
                            //int templatePage = SaveTemplatePage(i, TemplateID, "Front", TemplateID + "/Side" + (i).ToString() + ".pdf");
                            // save pdf 
                            using (Doc singlePagePdf = new Doc())
                            {
                                try
                                {
                                    singlePagePdf.Rect.String = singlePagePdf.MediaBox.String = theDoc.MediaBox.String;
                                    singlePagePdf.AddPage();
                                    singlePagePdf.AddImageDoc(theDoc, i, null);
                                    singlePagePdf.FrameRect();

                                    int srcPageRot = theDoc.GetInfoInt(theDoc.Page, "/Rotate");
                                    if (srcDocRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcDocRot);
                                    }
                                    if (srcPageRot != 0)
                                    {
                                        singlePagePdf.SetInfo(singlePagePdf.Page, "/Rotate", srcPageRot);
                                    }
                                    string targetFolder = drURL;
                                    if (File.Exists(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf"))
                                    {
                                        File.Delete(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    }
                                    singlePagePdf.Save(targetFolder + ProductID + "/Side" + i.ToString() + ".pdf");
                                    singlePagePdf.Clear();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("GenerateTemplateBackground", e);
                                }
                                finally
                                {
                                    if (singlePagePdf != null)
                                        singlePagePdf.Dispose();
                                }
                            }
                            result = _templateRepository.updateTemplate(ProductID, pdfWidth, pdfHeight, listPages);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("GenerateTemplateThumbnail", ex);
                    }
                    finally
                    {
                        if (theDoc != null)
                            theDoc.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        // generate template pdf file called from MIS and webstore 
        private bool GenerateTemplatePdf(long productID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners, bool isDrawHiddenObjs,double bleedareaSize,bool isMultipageProduct)
        {
            bool result = false;
            try
            {

                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
                string fontsUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/");//"~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/"
                if(!Directory.Exists(drURL + productID ))
                {
                    Directory.CreateDirectory(drURL + productID);
                }
                List<TemplatePage> oTemplatePages = new List<TemplatePage>();
                List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
                Template objProduct = _templateRepository.GetTemplate(productID, out oTemplatePages, out oTemplateObjects);
                if (isMultipageProduct)
                {
                    bool hasOverlayObject = false;
                    byte[] PDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize,true);
                    //writing the PDF to FS
                    System.IO.File.WriteAllBytes(drURL + productID + "/pages.pdf" , PDFFile);
                    //gernating 
                    generatePagePreviewMultiplage(PDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
                    if (hasOverlayObject)
                    {
                        byte[] overlayPDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true); ;// generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, false, out hasOverlayObject, true, true);
                        System.IO.File.WriteAllBytes(drURL + productID + "/pagesoverlay.pdf", PDFFile);
                        generatePagePreviewMultiplage(overlayPDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
                    }
                    result = true;
                }
                else
                {


                    foreach (TemplatePage objPage in oTemplatePages)
                    {
                        bool hasOverlayObject = false;
                        byte[] PDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize, true);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                        //generate and write overlay image to FS 
                        generatePagePreview(PDFFile, drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        List<TemplateObject> clippingPaths = oTemplateObjects.Where(g => g.ProductPageId == objPage.ProductPageId && g.hasClippingPath == true && g.IsOverlayObject != true).ToList();
                        if (clippingPaths.Count > 0)
                        {
                            // ClippingPathService objService = new ClippingPathService();
                            double height, width = 0;
                            if (objPage.Height.HasValue)
                            {
                                height = objPage.Height.Value;
                            }
                            else
                            {
                                height = objProduct.PDFTemplateHeight.Value;
                            }
                            if (objPage.Width.HasValue)
                            {
                                width = objPage.Width.Value;
                            }
                            else
                            {
                                width = objProduct.PDFTemplateWidth.Value;
                            }
                            generateClippingPaths(drURL + productID + "/p" + objPage.PageNo + ".pdf", clippingPaths, drURL + productID + "/p" + objPage.PageNo + "clip.pdf", width, height);
                            File.Copy(drURL + productID + "/p" + objPage.PageNo + "clip.pdf", drURL + productID + "/p" + objPage.PageNo + ".pdf", true);
                            File.Delete(drURL + productID + "/p" + objPage.PageNo + "clip.pdf");
                            generatePagePreview(drURL + productID + "/p" + objPage.PageNo + ".pdf", drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        }
                        if (hasOverlayObject)
                        {
                            // generate overlay PDF 
                            byte[] overlayPDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true);
                            // writing overlay pdf to FS 
                            System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                            // generate and write overlay image to FS 
                            generatePagePreview(overlayPDFFile, drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                            List<TemplateObject> overlayClippingPaths = oTemplateObjects.Where(g => g.ProductPageId == objPage.ProductPageId && g.hasClippingPath == true && g.IsOverlayObject == true).ToList();
                            if (clippingPaths.Count > 0)
                            {
                                double height, width = 0;
                                if (objPage.Height.HasValue)
                                {
                                    height = objPage.Height.Value;
                                }
                                else
                                {
                                    height = objProduct.PDFTemplateHeight.Value;
                                }
                                if (objPage.Width.HasValue)
                                {
                                    width = objPage.Width.Value;
                                }
                                else
                                {
                                    width = objProduct.PDFTemplateWidth.Value;
                                }
                                generateClippingPaths(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayClippingPaths, drURL + productID + "/p" + objPage.PageNo + "clipoverlay.pdf", width, height);
                                File.Copy(drURL + productID + "/p" + objPage.PageNo + "clipoverlay.pdf", drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", true);
                                File.Delete(drURL + productID + "/p" + objPage.PageNo + "clipoverlay.pdf");
                                generatePagePreview(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                            }
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }
        public void generateClippingPaths(string path, List<TemplateObject> lstObjs, string outputPath, double width, double height)
        {
            PDFlib p;
            int image;


            p = new PDFlib();

            try
            {
                p.set_option("errorpolicy=return");
                p.set_parameter("license", "W900202-010530-800852-ZTBG52-RBSR22");

                if (p.begin_document(outputPath, "") == -1)
                {
                    Console.WriteLine("Error: {0}\n", p.get_errmsg());
                    return;
                }
                var oldDoc = p.open_pdi_document(path, "");
                var oldPage = p.open_pdi_page(oldDoc, 1, "");

                p.begin_page_ext(width, height, "");
                p.fit_pdi_page(oldPage, 0, 0, "");
                p.close_pdi_page(oldPage);

                foreach (var obj in lstObjs)
                {
                    string imgName = obj.ContentString.Replace("__clip_mpc.png", ".jpg");
                    string imagefile = System.Web.Hosting.HostingEnvironment.MapPath("~/Mpc_Content") + "/" + imgName;
                    image = p.load_image("auto", imagefile, "");

                    if (image == -1)
                    {
                        Console.WriteLine("Error: {0}\n", p.get_errmsg());
                        return;
                    }
                    var posY = height - obj.PositionY - obj.MaxHeight;
                    p.fit_image(image, (float)obj.PositionX, (float)posY, "boxsize={" + obj.MaxWidth + " " + obj.MaxHeight + "} " +"fitmethod=entire");

                    p.close_image(image);
                }


                p.end_page_ext("");

                p.end_document("");
                p.close_pdi_document(oldDoc);
            }

            catch (PDFlibException e)
            {
                // caught exception thrown by PDFlib
                Console.WriteLine("PDFlib exception occurred in image sample:");
                Console.WriteLine("[{0}] {1}: {2}\n", e.get_errnum(),
                        e.get_apiname(), e.get_errmsg());
            }
            finally
            {
                if (p != null)
                {
                    p.Dispose();
                }
            }
        }
 
        
        #endregion
        #region constructor
        public TemplateService(ITemplateRepository templateRepository, IProductCategoryRepository ProductCategoryRepository,ITemplateBackgroundImagesService templateBackgroundImages,ITemplateFontsService templateFontSvc,ICompanyRepository companyRepository, ICompanyContactRepository contactRepostiory,IOrganisationRepository organisationRepository,ITemplatePageService templatePageService,IItemRepository itemRepository)
        {
            this._templateRepository = templateRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._templateBackgroundImagesService = templateBackgroundImages;
            this._templateFontService = templateFontSvc;
            this._companyRepository = companyRepository;
            this._contactRepository = contactRepostiory;
            this._organisationRepository = organisationRepository;
            this._templatePageService = templatePageService;
            this._itemRepository = itemRepository;
        }
        #endregion

        #region public

        /// <summary>
        /// called from webstore usually for coping template  // added by saqib ali
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Template GetTemplate(long productID)
        {
            var template = _templateRepository.GetTemplate(productID, true);
            // add default cutting margin if not available 
            if (template.CuttingMargin.HasValue)
                template.CuttingMargin = DesignerUtils.PointToPixel(template.CuttingMargin.Value);
            else
                template.CuttingMargin = DesignerUtils.PointToPixel(14.173228345);
            //if (product.Orientation == 2) //rotating the canvas in case of vert orientation
            //{
            //    double tmp = product.PDFTemplateHeight.Value;
            //    product.PDFTemplateHeight = product.PDFTemplateWidth;
            //    product.PDFTemplateWidth = tmp;
            //}
            return template;
        }

        // called from designer, all the units are converted to pixel before sending  // added by saqib ali
        public Template GetTemplateInDesigner(long productID,long categoryIdv2,double  height, double width,long organisationId,long itemId)
        {
            Template product = null;
            if (productID == 0)
            {
                product = _templateRepository.CreateTemplate(productID, categoryIdv2, height, width,itemId,organisationId);
               // _templatePageService.CreateBlankBackgroundPDFs(product.ProductId,Convert.ToDouble( product.PDFTemplateHeight),Convert.ToDouble( product.PDFTemplateWidth), 1, organisationId);
            }
            else
            {
                product = _templateRepository.GetTemplate(productID, true);
            }
            product.PDFTemplateHeight = DesignerUtils.PointToPixel(product.PDFTemplateHeight.Value);
            product.PDFTemplateWidth = DesignerUtils.PointToPixel(product.PDFTemplateWidth.Value);
            if(product.CuttingMargin.HasValue)
                product.CuttingMargin = DesignerUtils.PointToPixel(product.CuttingMargin.Value);
            else
                product.CuttingMargin = DesignerUtils.PointToPixel(14.173228345);  // in case of coorporate this value is not set 
            string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationId.ToString() + "/Templates/" + productID.ToString() + "/");
                       
            foreach (var objPage in product.TemplatePages)
            {
                if(objPage.Width.HasValue)
                    objPage.Width = DesignerUtils.PointToPixel(objPage.Width.Value);
                if (objPage.Height.HasValue)
                    objPage.Height = DesignerUtils.PointToPixel(objPage.Height.Value);
               // targetFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/Products/");
                if (objPage.BackGroundType != 3)
                {
                    if (File.Exists(drURL + "templatImgBk" + objPage.PageNo.ToString() + ".jpg"))
                    {
                        objPage.BackgroundFileName =  productID + "/templatImgBk" + objPage.PageNo.ToString() + ".jpg";
                    }
                    else
                    {
                        objPage.BackgroundFileName = "";
                    }
                }
            }

            return product;
        }
        // delete template and all references   // added by saqib ali
        public bool DeleteTemplate(long ProductID, out long CategoryID, long OrganisationID)
        {
            var result = false;
            try
            {
                result=  _templateRepository.DeleteTemplate(ProductID, out CategoryID);
                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                      foreach (string item in System.IO.Directory.GetFiles(drURL))
                        {
                            System.IO.File.Delete(item);
                        }

                    Directory.Delete(drURL);
                }
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }

        // called from MIS to delete the template folder // added by saqib ali
        public bool DeleteTemplateFiles(long ProductID, long OrganisationID)
        {
            try
            {

                bool result = false;

                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                    foreach (string item in System.IO.Directory.GetFiles(drURL))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(drURL);
                }

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
        }
        //copy template and all physical files  // added by saqib ali
        public long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName,long OrganisationID)
        {
            long result = 0;
            try 
            { 
                List<TemplatePage> objPages;
                List<TemplateBackgroundImage> objImages;
                 result = _templateRepository.CopyTemplate(ProductID, SubmittedBy, SubmittedByName, out objPages, OrganisationID,out objImages);
                 if (result != 0)
                 {
                     string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
                     string targetFolder = drURL + result.ToString();
                     //create template directory
                     if (!System.IO.Directory.Exists(targetFolder))
                     {
                         System.IO.Directory.CreateDirectory(targetFolder);
                     }
                     foreach (TemplatePage oTemplatePage in objPages)
                     {
                         //copy background pdfs and images
                         if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                         {
                             string filename = oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));
                             string destinationPath = Path.Combine(drURL + result.ToString() + "/" + filename);
                             string sourcePath = Path.Combine(drURL, ProductID.ToString() + "/" + filename);
                             if (!File.Exists(destinationPath) && File.Exists(sourcePath))
                             {
                                 //copy side 1
                                 File.Copy(sourcePath, destinationPath);
                             }
                             // copy side 1 image file if exist in case of pdf template
                             if (File.Exists(drURL + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg"))
                             {
                                 File.Copy(drURL + ProductID.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", drURL + result.ToString() + "/templatImgBk" + oTemplatePage.PageNo.ToString() + ".jpg", true);
                             }
                         }

                     }
                     //copy the template images

                     foreach (TemplateBackgroundImage item in objImages)
                     {
                         string ext = Path.GetExtension(item.ImageName);
                         string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                         string[] names = results[0].Split('/');
                         string filePath = drURL + "/" + ProductID.ToString() + "/" + names[names.Length - 1] + ext;
                         string filename;



                         // copy thumbnail 
                         if (!ext.Contains("svg"))
                         {

                             string imgName = names[names.Length - 1] + "_thumb" + ext;

                             string ThumbPath = drURL + "/" + ProductID.ToString() + "/" + imgName;
                             FileInfo oFileThumb = new FileInfo(ThumbPath);
                             if (oFileThumb.Exists)
                             {
                                 string oThumbName = oFileThumb.Name;
                                 oFileThumb.CopyTo((drURL + result.ToString() + "/" + oThumbName), true);
                             }
                         }
                         FileInfo oFile = new FileInfo(filePath);

                         if (oFile.Exists)
                         {
                             filename = oFile.Name;
                             oFile.CopyTo((drURL + result.ToString() + "/" + filename), true);
                         }
                     }
                 } else
                 {
                     throw new MPCException("Clone template failed due to store procedure. 'sp_cloneTemplate'", OrganisationID);
                 }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }
        // copy list of templates called from MIS returns list of copied ids if id is null template is not copied  // added by saqib ali
        public List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName,long OrganisationID)
        {
            List<long?> newTemplateList = new List<long?>();
            try
            {
                foreach (long? ProductID in productIDList)
                {
                    if (ProductID != null && ProductID.HasValue)
                    {
                        long result = CopyTemplate(ProductID.Value, SubmittedBy, SubmittedByName, OrganisationID);
                        if (result != 0)
                        {
                            newTemplateList.Add(result);
                        }
                        else
                        {
                            newTemplateList.Add(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return newTemplateList;
        }
        // generate template from the given pdf file,called from MIS // added by saqib ali
        // filePhysicalPath =server.mappath( 'MPC_Content/Products/Organisation1/Templates/random__CorporateTemplateUpload.pdf')  // can be changed but it should be in mpcContent 
        //F:\\Development\\Github\\MyPrintCloud-dev\\MPC.web\\MPC_Content\\Products\\Organisation1\\Templates\\random__CorporateTemplateUpload.pdf
        //mode = 1 for creating template and removing all the existing objects  and images
        // mode = 2 for creating template and preserving template objects and images
        public bool generateTemplateFromPDF(string filePhysicalPath, int mode, long templateID, long OrganisationID)
        {
            bool result = false;
            try
            {
                if (mode == 2)
                {
                   result =  CovertPdfToBackground(filePhysicalPath, templateID, OrganisationID);
                }
                else
                {
                   result =  CovertPdfToBackgroundWithoutObjects(filePhysicalPath, templateID,OrganisationID);
                }
                if (File.Exists(filePhysicalPath))
                {
                    File.Delete(filePhysicalPath);
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }

        // called from webstore and MIS to generate pdf file of the template.// added by saqib ali
        public void processTemplatePDF(long TemplateID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners,bool isMultipageProduct)
        {
            GenerateTemplatePdf(TemplateID, OrganisationID, printCropMarks, printWaterMarks, isroundCorners,true,0,isMultipageProduct);
        }
        // called from MIS and webstore to regenerate template PDF files  // added by saqib ali
        //draw bleed area is flag iin item 
        // bleed area size is present in organisation 
        public void regeneratePDFs(long productID, long OrganisationID, bool printCuttingMargins, bool isMultipageProduct,bool drawBleedArea, double bleedAreaSize)
        {
           if(drawBleedArea == false)
           {
               bleedAreaSize = 0;
           }
           GenerateTemplatePdf(productID, OrganisationID, printCuttingMargins, false, false, false, bleedAreaSize, isMultipageProduct);

        }
        // called from webstore to save template locally // added by saqib ali =
        // base path = F:\Development\Github\MyprintCloud-dev\MPC.Web\MPC_Content\Designer\Organisation2\
        // mode = 1 => create a new template from v2 objects
        // mode =2 => update an existing template  from v2 objects
        public long SaveTemplateLocally(Template oTemplate, List<TemplatePage> oTemplatePages, List<TemplateObject> oTemplateObjects, List<TemplateBackgroundImage> oTemplateImages, List<TemplateFont> oTemplateFonts, string RemoteUrlBasePath, string BasePath,long organisationID, int mode, long localTemplateID)
        {

            long newProductID = 0;
            List<TemplateFont> fontsToDownload = new List<TemplateFont>();
            //string BasePath = System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/");
            if (mode == 1)
            {
                newProductID = _templateRepository.SaveTemplateLocally(oTemplate, oTemplatePages, oTemplateObjects, oTemplateImages, oTemplateFonts, organisationID, out fontsToDownload, mode, localTemplateID);
            } else
            {
                newProductID = localTemplateID;
            }
            string targetFolder = BasePath + "/Templates/" + newProductID.ToString(); //System.Web.HttpContext.Current.Server.MapPath("../Designer/Products/" + newProductID.ToString());
            if (!System.IO.Directory.Exists(targetFolder))
            {
                System.IO.Directory.CreateDirectory(targetFolder);
            }
            foreach (var oPage in oTemplatePages)
            {
                if (oPage.BackGroundType == 1 || oPage.BackGroundType == 3)
                {
                    string remoteUrl = RemoteUrlBasePath + "products/" + oPage.BackgroundFileName;
                    string destinationUrl = BasePath + "Templates/" + newProductID.ToString() + "/" + oPage.BackgroundFileName.Substring(oPage.BackgroundFileName.IndexOf("/"), oPage.BackgroundFileName.Length - oPage.BackgroundFileName.IndexOf("/"));
                    DesignerUtils.DownloadFile(remoteUrl, destinationUrl);
                }
            }
            foreach (TemplateBackgroundImage item in oTemplateImages)
            {
                string ext = Path.GetExtension(item.ImageName);
                // generate thumbnail 
                if (!ext.Contains("svg"))
                {
                    string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                    string destPath = results[0] + "_thumb" + ext;
                    string localThumbnail = newProductID.ToString() + "/" + Path.GetFileName(destPath);
                    string localPath = BasePath + "/Templates/" + localThumbnail;
                    DesignerUtils.DownloadFile(RemoteUrlBasePath + "products/" + destPath, localPath);
                }
                item.ProductId = newProductID;
                string NewLocalFileName = newProductID.ToString() + "/" + Path.GetFileName(item.ImageName);
                string localFilePath = BasePath + "/Templates/" + NewLocalFileName;
                DesignerUtils.DownloadFile(RemoteUrlBasePath + "products/" + item.ImageName, localFilePath);
            }
            if (mode == 2)
            {
                _templateBackgroundImagesService.DeleteTemplateBackgroundImages(localTemplateID, organisationID);
                _templateRepository.SaveTemplateLocally(oTemplate, oTemplatePages, oTemplateObjects, oTemplateImages, oTemplateFonts, organisationID, out fontsToDownload, mode, localTemplateID);
            }
            foreach (var objFont in fontsToDownload)
            {
                try
                {
                    string path = "WebFonts/";
                    string remotePath = "";
                    if (objFont.FontPath == null)
                    {
                        // mpc designers fonts or system fonts 
                        remotePath = "PrivateFonts/FontFace/";//+ objFont.FontFile;
                    }
                    else
                    {  // customer fonts 

                        path += objFont.FontPath;
                        remotePath = objFont.FontPath;
                    }

                    if (!Directory.Exists(BasePath + path))
                    {
                        Directory.CreateDirectory((BasePath + path));
                    }
                    // downloading ttf file 
                    string RemoteURl = RemoteUrlBasePath + remotePath + objFont.FontFile + ".ttf";
                    string DestURL = BasePath + path + objFont.FontFile + ".ttf";
                    DesignerUtils.DownloadFile(RemoteURl, DestURL);
                    // downloading woff file 
                    RemoteURl = RemoteUrlBasePath + remotePath + objFont.FontFile + ".woff";
                    DestURL = BasePath + path + objFont.FontFile + ".woff";
                    DesignerUtils.DownloadFile(RemoteURl, DestURL);
                    // downloading eot file 
                    RemoteURl = RemoteUrlBasePath + remotePath + objFont.FontFile + ".eot";
                    DestURL = BasePath + path + objFont.FontFile + ".eot";
                    DesignerUtils.DownloadFile(RemoteURl, DestURL);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
           
            return newProductID;
        }

        // called from designer while generating proof  // added by saqib ali // not tested yet
        public string SaveTemplate(List<TemplateObject> lstTemplatesObjects,List<TemplatePage> lstTemplatePages,long organisationID,bool printCropMarks,bool printWaterMarks,bool isRoundCorners, double bleedAreaSize,bool isMultipageProduct)
        {
            try
            {

                long productID = 0;
                List<TemplateObject> objsToRemove = new List<TemplateObject>();
               // List<TemplateObject> objsToAdd = new List<TemplateObject>();
                if (lstTemplatesObjects.Count > 0)
                {

                    productID = lstTemplatesObjects[0].ProductId.Value;
                    foreach (var oObject in lstTemplatesObjects)
                    {
                        if (oObject.ObjectId != -999 )
                        {
                            if (oObject.PositionX == null && oObject.PositionY == null && oObject.MaxHeight == null && oObject.MaxWidth == null)
                                objsToRemove.Add(oObject);

                            if (oObject.PositionX == null)
                                oObject.PositionX = 0;
                            if (oObject.PositionY == null)
                                oObject.PositionY = 0;
                            if (oObject.MaxHeight == null)
                                oObject.MaxHeight = 0;
                            if (oObject.MaxWidth == null)
                                oObject.MaxWidth = 0;

                           
                            if(oObject.PositionX.HasValue)
                                oObject.PositionX = Math.Round(DesignerUtils.PixelToPoint(oObject.PositionX.Value), 6);
                            if(oObject.PositionY.HasValue)
                                oObject.PositionY = Math.Round(DesignerUtils.PixelToPoint(oObject.PositionY.Value), 6);
                            if(oObject.FontSize.HasValue)
                              oObject.FontSize = Math.Round(DesignerUtils.PixelToPoint(oObject.FontSize.Value), 6);
                            if(oObject.MaxWidth.HasValue)
                                 oObject.MaxWidth = Math.Round(DesignerUtils.PixelToPoint(oObject.MaxWidth.Value), 6);
                            if(oObject.MaxHeight.HasValue)
                                oObject.MaxHeight = Math.Round(DesignerUtils.PixelToPoint(oObject.MaxHeight.Value), 6);
                            if (oObject.CharSpacing != null)
                            {
                                oObject.CharSpacing = Convert.ToDouble(DesignerUtils.PixelToPoint(Convert.ToDouble(oObject.CharSpacing.Value)));
                            }
                            if (oObject.ObjectType == 3)
                            {
                                if (oObject.ContentString.Contains("__clip_mpc"))
                                {
                                    oObject.hasClippingPath = true;
                                }
                                else
                                {
                                    oObject.hasClippingPath = false;
                                }
                            }

                            oObject.ProductId = productID;

                        }
                    }
                    foreach(var objR in objsToRemove)
                    {
                        lstTemplatesObjects.Remove(objR);
                    }

                }
                foreach (TemplatePage obj in lstTemplatePages)
                {

                     if (obj.BackgroundFileName != "")
                     {
                         string ext = System.IO.Path.GetExtension(obj.BackgroundFileName);
                         if (obj.BackGroundType == 1 && ext.Contains("jpg"))
                         {
                             obj.BackgroundFileName = productID + "/" + "Side" + obj.PageNo + ".pdf";
                         }
                         else
                         {
                             obj.BackgroundFileName = obj.BackgroundFileName;
                         }

                     }
                }
               bleedAreaSize =  _templateRepository.SaveTemplate(productID, lstTemplatePages, lstTemplatesObjects);
               bool result=  GenerateTemplatePdf(productID, organisationID, printCropMarks, printWaterMarks, isRoundCorners, true,bleedAreaSize,isMultipageProduct);
               return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        // download tempate from v2 and merge it locally 
        public long MergeRetailTemplate(int RemoteTemplateID, long LocalTempalteID, long organisationId, bool ChangeQuickText,long CompanyID,long ContactID,long ItemID)
        {
            try
            {

                long LocalProductID = 0;
                using (GlobalTemplateDesigner.TemplateSvcSPClient pSc = new GlobalTemplateDesigner.TemplateSvcSPClient())
                {
                    GlobalTemplateDesigner.Templates oTemplateV2 = pSc.GetTemplateWebStore(RemoteTemplateID);

                    oTemplateV2.TemplateOwner = oTemplateV2.ProductCategoryID;

                    GlobalTemplateDesigner.TemplatePages[] oTemplatePagesV2 = pSc.GetTemplatePages(RemoteTemplateID);

                    GlobalTemplateDesigner.TemplateObjects[] oTemplateObjectsV2 = pSc.GetTemplateObjects(RemoteTemplateID);

                    GlobalTemplateDesigner.TemplateBackgroundImages[] oTemplateImagesV2 = pSc.GettemplateImages(RemoteTemplateID);

                    GlobalTemplateDesigner.TemplateFonts[] oTemplateFontV2 = pSc.GetTemplateFonts(RemoteTemplateID);
                    // added for 2 sided business cards 
                    if (oTemplateV2.TemplateType == 2)
                    {
                        if (oTemplatePagesV2.Length >= 2)
                        {
                            GlobalTemplateDesigner.TemplatePages[] oTemp = new GlobalTemplateDesigner.TemplatePages[2];
                            oTemp[0] = oTemplatePagesV2[0];
                            oTemp[1] = oTemplatePagesV2[1];
                            oTemplatePagesV2 = oTemp;
                        }
                    }
                    //template type 3 for multiback business cards 
                    Template oTemplate = returnLocalTemplate(oTemplateV2);
                    List<TemplatePage> oTemplatePages = new List<TemplatePage>();
                    foreach(var obj in oTemplatePagesV2)
                    {
                        oTemplatePages.Add(returnLocalPage(obj,oTemplate));
                    }
                    List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
                    foreach(var oObj in oTemplateObjectsV2)
                    {
                        oTemplateObjects.Add(returnLocalObject(oObj));
                    }
                    List<TemplateBackgroundImage> oTemplateImages = new List<TemplateBackgroundImage>();
                    foreach (var bkImg in oTemplateImagesV2)
                    {
                        oTemplateImages.Add(returnLocalImage(bkImg));
                    }
                    List<TemplateFont> oTemplateFont = new List<TemplateFont>();
                    foreach (var objFont in oTemplateFontV2)
                    {
                        oTemplateFont.Add(returnLocalFont(objFont));
                    }
                    Company objCompany = _companyRepository.GetStoreById(CompanyID);
                    if (objCompany != null)
                    {
                        oTemplate.TempString = objCompany.WatermarkText;
                        oTemplate.isWatermarkText = objCompany.isTextWatermark;
                        if (objCompany.isTextWatermark == false)
                        {
                            oTemplate.TempString = System.Web.HttpContext.Current.Server.MapPath(objCompany.WatermarkText);
                        }

                    }

                    // commented by zohaib will implement later because have not implement 5 page business card in designer
                    //LocalTemplateDesigner.TemplateSvcSPClient oLocSvc = new LocalTemplateDesigner.TemplateSvcSPClient();
                    //LocalProductID = oLocSvc.SaveTemplateLocally(oTemplate, oTemplatePages, oTemplateObjects, oTemplateImages, oTemplateFont, TemplateDesignerUrl + "designer/", Server.MapPath("~/designengine/designer/")); //products/
                    //if (PageParameters.CategoryId == bizCardCategory)
                    //{
                    //    foreach (var oTemp in oTemplatePages)
                    //    {
                    //        DownloadFile(TemplateDesignerUrl + "designer/" + "products/" + RemoteTemplateID + "/p" + oTemp.PageNo.ToString() + ".png", Server.MapPath("~/designengine/designer/") + "products\\" + LocalProductID.ToString() + "/p" + oTemp.PageNo.ToString() + ".png");
                    //    }
                    //}
                    //ProductManager productManager = new ProductManager();
                    string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + organisationId.ToString() + "/");
                   // LocalTemplateDesigner.TemplateSvcSPClient oLocSvc = new LocalTemplateDesigner.TemplateSvcSPClient();
                    if (LocalTempalteID == 0)
                    {
                        LocalProductID = SaveTemplateLocally(oTemplate, oTemplatePages, oTemplateObjects, oTemplateImages, oTemplateFont, "http://designerv2.myprintcloud.com/designer/", drURL, organisationId,1, LocalTempalteID); //products/
                    }
                    else
                    {
                        LocalProductID = SaveTemplateLocally(oTemplate, oTemplatePages, oTemplateObjects, oTemplateImages, oTemplateFont, "http://designerv2.myprintcloud.com/designer/", drURL, organisationId, 2, LocalTempalteID); //products/
                    }

                    //_itemRepository.ResolveTemplateVariables()
                    //productManager.ResolveTemplateVariables(LocalProductID, SessionParameters.ContactCompany, SessionParameters.CustomerContact, StoreMode.Retail);
                    //if(ItemID > 0)
                    //     _itemRepository.VariablesResolve(ItemID, LocalProductID, ContactID);
                 
                 
                    return LocalProductID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID)
        {
            List<ProductCategoriesView> PCview = _ProductCategoryRepository.GetMappedCategoryNames(false, CompanyID);
            return _templateRepository.BindTemplatesList(TemplateName, pageNumber, CustomerID, CompanyID, PCview);
        }
        
        public string GetTemplateNameByTemplateID(long tempID)
        {
            return _templateRepository.GetTemplateNameByTemplateID(tempID);
        }

      
        public long CloneTemplateByTemplateID(long TempID)
        {
            return _templateRepository.CloneTemplateByTemplateID(TempID);
        }

        /// <summary>
        /// To populate the template information base on template id and item rec by zohaib 10/1/2015
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="ItemRecc"></param>
        /// <param name="template"></param>
        /// <param name="tempPages"></param>
        public void populateTemplateInfo(long templateID, Item ItemRecc, out Template template, out List<TemplatePage> tempPages)
        {
            try
            {
                _templateRepository.populateTemplateInfo(templateID, ItemRecc, out template, out tempPages);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateProof(DesignerPostSettings objSettings, double bleedAreaSize)
        {
         //   bleedAreaSize = _templateRepository.getOrganisationBleedArea(objSettings.organisationId); // always use template bleed area size
         //   bleedAreaSize = DesignerUtils.PixelToPoint(objSettings.);
            List<TemplateObject> lstTemplatesObjects = objSettings.objects;
            return SaveTemplate(lstTemplatesObjects, objSettings.objPages, objSettings.organisationId, objSettings.printCropMarks, objSettings.printWaterMarks, objSettings.isRoundCornerrs,bleedAreaSize,objSettings.isMultiPageProduct);
        }

        public QuickText GetContactQuickTextFields(long CustomerID, long ContactID)
        {

            QuickText oQuickText = null;
            CompanyContact  oContact = _contactRepository.GetContactByID(ContactID);
            if (oContact != null)
            {

                oQuickText = new QuickText();
                oQuickText.Address1 = oContact.quickAddress1 ?? string.Empty;

                oQuickText.Company = oContact.quickCompanyName ?? string.Empty;
                oQuickText.CompanyMessage = oContact.quickCompMessage ?? string.Empty;
                oQuickText.Email = oContact.quickEmail ?? string.Empty;
                oQuickText.Fax = oContact.quickFax ?? string.Empty;
                oQuickText.Name = oContact.quickFullName ?? string.Empty;
                oQuickText.Telephone = oContact.quickPhone ?? string.Empty;
                oQuickText.Title = oContact.quickTitle ?? string.Empty;
                oQuickText.Website = oContact.quickWebsite ?? string.Empty;
                //  oQuickText.LogoPath = curCustomer.Image ?? string.Empty;
                oQuickText.CustomerID = CustomerID;
                oQuickText.ContactID = ContactID;

                oQuickText.MobileNumber = oContact.quickMobileNumber ?? string.Empty;
                oQuickText.FacebookID = oContact.quickFacebookId ?? string.Empty;
                oQuickText.TwitterID = oContact.quickTwitterId ?? string.Empty;
                oQuickText.LinkedInID = oContact.quickLinkedInId ?? string.Empty;
                oQuickText.OtherId = oContact.quickOtherId ?? string.Empty;

            }
            else
                oQuickText = new QuickText();


           return oQuickText;

        }
        public bool UpdateQuickTextTemplateSelection( QuickText objQText)
        {
            return _contactRepository.updateQuikcTextInfo(objQText.ContactID, objQText);

        }

        // get conversion ratio of mm to current system unit and unit name (1.0__inch)
        public string GetConvertedSizeWithUnits(long productId, long organisationID, long itemID)
        {

            double h = Math.Round(Convert.ToDouble(1), 0);
            string unit = "mm";
            double height = h;
            double scaledHeight = h;
            double scaleFactor = 1;
            double resultDimentions = h; // current height or width 
            var organisation = _organisationRepository.GetOrganizatiobByID(organisationID);
            var item = _itemRepository.GetItemByIdDesigner(itemID);
            if (item != null)
            {
                scaledHeight = Convert.ToDouble(item.Scalar);
                if (scaledHeight == 0)
                {
                    scaledHeight = 1;
                }
                scaleFactor = scaledHeight;
            }


            if (organisation.SystemLengthUnit == 1)
            {
                scaledHeight *= height;
                resultDimentions =  scaledHeight;
            }
            else if (organisation.SystemLengthUnit == 2)
            {
                height = _templateRepository.ConvertLength(Convert.ToDouble(1), MPC.Models.Common.LengthUnit.Mm, MPC.Models.Common.LengthUnit.Cm);
                height = Math.Round(height, 3);
                scaledHeight *= height;
                resultDimentions =scaledHeight;
                unit = "cm";
            }
            else if (organisation.SystemLengthUnit == 3)
            {
                height = _templateRepository.ConvertLength(Convert.ToDouble(1),  MPC.Models.Common.LengthUnit.Mm,  MPC.Models.Common.LengthUnit.Inch);
                height = Math.Round(height, 3);
                scaledHeight *= height;
                resultDimentions =  scaledHeight ;
                unit = "inch";
            }


            return resultDimentions + "__" + unit+ "__" + (resultDimentions/scaleFactor) ;
        }

        public string OrderConfirmationPDF(long OrderId, long StoreId)
        {
            Doc theDoc = new Doc();
            try
            {


//                string URl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/ReceiptPlain?OrderId=" + OrderId + "&StoreId=" + StoreId + "&IsPrintReceipt=0";
                string URl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + "/OrderReceipt/" + OrderId + "/" + StoreId + "/0";
                string FileName = OrderId + "_OrderReceipt.pdf";
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/" + FileName);
                string AttachmentPath = "/mpc_content/EmailAttachments/" + FileName;

                string AddGeckoKey = ConfigurationManager.AppSettings["AddEngineTypeGecko"];
                if (AddGeckoKey == "1")
                {
                    theDoc.HtmlOptions.Engine = EngineType.Gecko;
                }

                theDoc.FontSize = 22;
                int objid = theDoc.AddImageUrl(URl);


                while (true)
                {
                    theDoc.FrameRect();
                    if (!theDoc.Chainable(objid))
                        break;
                    theDoc.Page = theDoc.AddPage();
                    objid = theDoc.AddImageToChain(objid);
                }
                string physicalFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/EmailAttachments/");
                if (!Directory.Exists(physicalFolderPath))
                    Directory.CreateDirectory(physicalFolderPath);
                theDoc.Save(FilePath);
                theDoc.Clear();

                if (System.IO.File.Exists(FilePath))
                    return AttachmentPath;
                else
                    return null;
            }
            catch (Exception e)
            {
                theDoc.Clear();
                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("Message :" + e.Message + "<br/>" + Environment.NewLine + "StackTrace :" + e.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                throw e;
                return null;
            }
            finally
            {
                theDoc.Dispose();
            }
        }

        public bool updatecontactId(long templateId, long contactId)
        {
           return _templateRepository.updatecontactId(templateId, contactId);
        }
        #endregion
    }

}
