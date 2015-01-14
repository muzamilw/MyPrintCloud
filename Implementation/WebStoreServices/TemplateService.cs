using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    }
    class TemplateService : ITemplateService
    {
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

                oPdf.PageNumber = 0;
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
                oPdf.PageNumber = 1;
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
        private void AddTextObject(TemplateObject ooBject, int PageNo, List<TemplateFont> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight, bool isTemplateSpot)
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
                            oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
                            oPdf.Color.Gray = 255;
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
                        path = "";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
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
                oPdf.TextStyle.Bold = ooBject.IsBold.Value;

                oPdf.TextStyle.Italic = ooBject.IsItalic.Value;
                double linespacing = ooBject.LineSpacing.Value - 1;
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

                if (ooBject.RotationAngle != 0)
                {

                    oPdf.Transform.Reset();
                    oPdf.Transform.Rotate(360 - ooBject.RotationAngle.Value, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY);
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
                                StyledHtml += ooBject.ContentString[i];
                            }
                            else
                            {
                                string toApplyStyle = ooBject.ContentString[i].ToString();
                                string fontTag = "<font";
                                string fontSize = "";
                                string pid = "";
                                if (objStyle.fontName != null)
                                {
                                    var oFont = oFonts.Where(g => g.FontName == objStyle.fontName).FirstOrDefault();
                                    if (System.IO.File.Exists(Font + path + oFont.FontFile + ".ttf"))
                                        FontID = oPdf.EmbedFont(Font + path + oFont.FontFile + ".ttf");
                                    // fontTag += " face='" + objStyle.fontName + "' embed= "+ FontID+" ";
                                    pid = "pid ='" + FontID.ToString() + "' ";
                                }
                                if (objStyle.fontSize != null)
                                {
                                    fontSize += "<StyleRun fontsize='" + Convert.ToInt32(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + pid + ">";
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
                                        fontTag += " color='" + hex + "' ";
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
                                        fontSize += "<StyleRun " + pid + ">";
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
                            StyledHtml += ooBject.ContentString[i];
                        }
                    }

                }
                else
                {
                    StyledHtml += ooBject.ContentString;
                }
                StyledHtml += "</p>";
                string sNewLineNormalized = Regex.Replace(StyledHtml, @"\r(?!\n)|(?<!\r)\n", "<BR>");
                sNewLineNormalized = sNewLineNormalized.Replace("  ", "&nbsp;&nbsp;");

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
                // for commented old styles algo see commit of 13 april 2014



            }
            catch (Exception ex)
            {
                throw new Exception("ADDSingleLineText", ex);
            }

        }

        private void LoadImage(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        {

            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;


                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    //logoPath = ""; //since path is already in filenm
                   /// string[] vals;
                    //FilePath = "";
                    //if (oObject.ContentString.Contains("MPC_Content/"))
                    //{
                    //    vals = oObject.ContentString.Split(new string[] { "StoredImages/" }, StringSplitOptions.None);
                    //    FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/../StoredImages/" + vals[vals.Length - 1]);
                    //}
                    FilePath = oObject.ContentString;
                    bFileExists = System.IO.File.Exists(FilePath);

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

                    //oPdf.FrameRect();

                    int id = oPdf.AddImageObject(oImg, true);
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


            logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            XImage oImg = new XImage();
            Bitmap img = null;
            try
            {
                oPdf.PageNumber = PageNo;
                bool bFileExists = false;
                string FilePath = string.Empty;
                if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
                {
                    //logoPath = "";
                    //string[] vals;
                    //FilePath = "";
                    //if (oObject.ContentString.Contains("StoredImages/"))
                    //{
                    //    vals = oObject.ContentString.Split(new string[] { "StoredImages/" }, StringSplitOptions.None);
                    //    FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/../StoredImages/" + vals[vals.Length - 1]);
                    //}
                    FilePath = oObject.ContentString;
                    bFileExists = System.IO.File.Exists(FilePath);
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
                    img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
                    if (oObject.Opacity != null)
                    {
                        if (oObject.Opacity != 1)
                        {
                            img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));

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
                    foreach (XmlNode childrenNode in nodes)
                    {
                        sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
                        sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
                        swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
                        sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
                        oImg.Selection.Inset(sx, sy);
                        oImg.Selection.Height = sheight;
                        oImg.Selection.Width = swidth;
                    }
                    int id = oPdf.AddImageObject(oImg, true);
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
                    oPdf.Color.Alpha = Convert.ToInt32((oObject.Tint) * 2.5);
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
                        oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.5);
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

            XImage oImg = new XImage();
            Bitmap img = null;
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
                    DesignerSvgParser.MaximumSize = new Size(Convert.ToInt32(oObject.MaxWidth), Convert.ToInt32(oObject.MaxHeight));
                    img = DesignerSvgParser.GetBitmapFromSVG(FilePath, oObject.ColorHex);
                    if (oObject.Opacity != null)
                    {
                        // float opacity =float.Parse( oObject.Tint.ToString()) /100;
                        if (oObject.Opacity != 1)
                        {
                            img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
                        }
                    }
                    oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));

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
                    int id = oPdf.AddImageObject(oImg, true);
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
                throw new Exception("LoadSvg", ex);
            }
            finally
            {
                oImg.Dispose();
                if (img != null)
                    img.Dispose();
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
                                oPdf.Color.Alpha = 220;
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
                            }
                            else
                            {
                                string FilePath = string.Empty;
                                XImage oImg = new XImage();
                                System.Drawing.Image objImage = null;
                                try
                                {
                                    oPdf.PageNumber = PageNo;


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
        private byte[] generatePDF(Template objProduct, TemplatePage objProductPage, List<TemplateObject> listTemplateObjects ,string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID)
        {
            Doc doc = new Doc();
            try
            {
                var FontsList = _templateFontService.GetFontList();
                doc.TopDown = true;

                try
                {

                    if (!isoverLayMode)
                    {
                        if (objProductPage.BackGroundType == 1)  //PDF background
                        {
                            if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
                                doc.Read(ProductFolderPath + objProductPage.BackgroundFileName);
                        }
                        else if (objProductPage.BackGroundType == 2) //background color
                        {
                            if (objProductPage.Orientation == 1) //standard 
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            }
                            doc.AddPage();
                            LoadBackColor(ref doc, objProductPage);
                        }
                        else if (objProductPage.BackGroundType == 3) //background Image
                        {

                            if (objProductPage.Orientation == 1) //standard 
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                            }
                            else
                            {
                                doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                                doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                            }
                            doc.AddPage();
                            LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
                        }
                    }
                    else
                    {
                        if (objProductPage.Orientation == 1) //standard 
                        {
                            doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
                            doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

                        }
                        else
                        {
                            doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
                            doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

                        }
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
                hasOverlayObject = false;
                if (count > 0)
                {
                    hasOverlayObject = true;
                }
                foreach (var objObjects in oParentObjects)
                {

                    if (XFactor != objObjects.PositionX)
                    {
                        if (objObjects.ContentString == "")
                            YFactor = objObjects.PositionY.Value - 7;
                        else
                            YFactor = 0;
                        XFactor = objObjects.PositionX.Value;
                    }



                    if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
                    {


                        int VAlign = 1, HAlign = 1;

                        HAlign = objObjects.Allignment.Value;

                        VAlign = objObjects.VAllignment.Value;

                        double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


                        if (VAlign == 1 || VAlign == 2)
                            currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
                        bool isTemplateSpot = false;
                        if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
                            isTemplateSpot = true;

                        AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot);



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
                                LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
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
                    int FontID = 0;
                    var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
                    if (pFont != null)
                    {
                        string path = "";
                        if (pFont.FontPath == null)
                        {
                            path = "";
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
        // generate low res proof image from pdf file 
        private string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        {
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

                    theDoc.Rendering.DotsPerInch = DPI;
                    if (RoundCorners)
                    {
                        using (str = new MemoryStream())
                        {
                            theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                            generateRoundCorners(System.IO.Path.Combine(savePath, PreviewFileName) + ".png", System.IO.Path.Combine(savePath, PreviewFileName) + ".png", str);
                        }
                    }
                    else
                    {
                        theDoc.Rendering.Save(System.IO.Path.Combine(savePath, PreviewFileName) + ".png");
                    }

                    theDoc.Dispose();

                    return PreviewFileName + ".png";



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
        private void generateRoundCorners(string physicalPath, string pathToSave, Stream str)
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
                            pdfWidth = theDoc.MediaBox.Width;
                            pdfHeight = theDoc.MediaBox.Height;
                            _templateRepository.DeleteTemplatePagesAndObjects(ProductID, out listTobjs,out listTpages);
                            theDoc.Read(physicalPath);
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
                                objPage.PageName = "Front";
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
                            objPage.PageName = "Front";
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
        private bool GenerateTemplatePdf(long productID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners)
        {
            bool result = false;
            try
            {

                string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
                string fontsUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/");
                List<TemplatePage> oTemplatePages = new List<TemplatePage>();
                List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
                Template objProduct = _templateRepository.GetTemplate(productID, out oTemplatePages, out oTemplateObjects);
                    foreach (TemplatePage objPage in oTemplatePages)
                    {
                        bool hasOverlayObject = false;
                        byte[] PDFFile = generatePDF(objProduct, objPage,oTemplateObjects, drURL, fontsUrl, false, true, printCropMarks, printWaterMarks, out hasOverlayObject, false,OrganisationID);
                        //writing the PDF to FS
                        System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + ".pdf", PDFFile);
                        //generate and write overlay image to FS 
                        generatePagePreview(PDFFile, drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
                        if (hasOverlayObject)
                        {
                            // generate overlay PDF 
                            byte[] overlayPDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, true, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID);
                            // writing overlay pdf to FS 
                            System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
                            // generate and write overlay image to FS 
                            generatePagePreview(overlayPDFFile, drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return result;
        }
        #endregion
        #region constructor
        public TemplateService(ITemplateRepository templateRepository, IProductCategoryRepository ProductCategoryRepository,ITemplateBackgroundImagesService templateBackgroundImages,ITemplateFontsService templateFontSvc)
        {
            this._templateRepository = templateRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._templateBackgroundImagesService = templateBackgroundImages;
            this._templateFontService = templateFontSvc;

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
            var product= _templateRepository.GetTemplate(productID);
            if (product.Orientation == 2) //rotating the canvas in case of vert orientation
            {
                double tmp = product.PDFTemplateHeight.Value;
                product.PDFTemplateHeight = product.PDFTemplateWidth;
                product.PDFTemplateWidth = tmp;
            }
            return product;
        }

        // called from designer, all the units are converted to pixel before sending  // added by saqib ali
        public Template GetTemplateInDesigner(long productID)
        {
            var product = _templateRepository.GetTemplate(productID);

            product.PDFTemplateHeight = DesignerUtils.PointToPixel(product.PDFTemplateHeight.Value);
            product.PDFTemplateWidth = DesignerUtils.PointToPixel(product.PDFTemplateWidth.Value);
            product.CuttingMargin = DesignerUtils.PointToPixel(product.CuttingMargin.Value);


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
        public void processTemplatePDF(long TemplateID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners)
        {
            GenerateTemplatePdf(TemplateID, OrganisationID, printCropMarks, printWaterMarks, isroundCorners);
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

       
        #endregion
    }
}
