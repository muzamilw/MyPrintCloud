using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace MPC.Common
{
    public class svgColorData
    {
        public string OriginalColor { get; set; }
        public int PathIndex { get; set; }
        public string ModifiedColor { get; set; }
    }
    public class DesignerSvgParser
    {
        /// <summary>
        /// The maximum image size supported.
        /// </summary>
        public static Size MaximumSize { get; set; }

        /// <summary>
        /// Converts an SVG file to a Bitmap image.
        /// </summary>
        /// <param name="filePath">The full path of the SVG image.</param>
        /// <returns>Returns the converted Bitmap image.</returns>
        public static Bitmap GetBitmapFromSVG(string filePath, string hexColor)
        {
            SvgDocument document = GetSvgDocument(filePath, hexColor);

            Bitmap bmp = document.Draw();
            return bmp;
        }
        public static byte[] ImageToByteArraybyImageConverter(Bitmap image)
        {
            ImageConverter imageConverter = new ImageConverter();
            
            byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            return imageByte;
        }

        /// <summary>
        /// Gets a SvgDocument for manipulation using the path provided.
        /// </summary>
        /// <param name="filePath">The path of the Bitmap image.</param>
        /// <returns>Returns the SVG Document.</returns>
        public static SvgDocument GetSvgDocument(string filePath, string hexColor)
        {
            SvgDocument document = SvgDocument.Open(filePath);
            //return AdjustSize(document);
            return AdjustColour(document, hexColor);
        }

        /// <summary>
        /// Makes sure that the image does not exceed the maximum size, while preserving aspect ratio.
        /// </summary>
        /// <param name="document">The SVG document to resize.</param>
        /// <returns>Returns a resized or the original document depending on the document.</returns>
        private static SvgDocument AdjustSize(SvgDocument document, string hexColor)
        {
            if (document.Height > MaximumSize.Height)
            {
                document.Width = (int)((document.Width / (double)document.Height) * MaximumSize.Height);
                document.Height = MaximumSize.Height;
            }
            return AdjustColour(document, hexColor);
        }

        private static SvgDocument AdjustColour(SvgDocument document, string hexColor)
        {
            if (hexColor != "")
            {
                SvgPaintServer firstColor = null;
                bool canColour = true;
                for (int i = 0; i < document.Children.Count; i++)
                {
                    if (firstColor == null)
                    {
                        if (document.Children[i] is SvgPath)
                        {

                            firstColor = (document.Children[i] as SvgPath).Fill;
                        }
                    }
                    if (document.Children[i] is SvgPath)
                    {
                        if ((document.Children[i] as SvgPath).Fill != firstColor)
                        {
                            canColour = false;
                        }
                    }
                }
                if (canColour)
                {
                    foreach (Svg.SvgElement item in document.Children)
                    {
                        Color color = HexToColor(hexColor);

                        ChangeFill(item, color);
                    }
                }
            }
            return document;
        }
        public static Color HexToColor(string hexColor)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            int red = 0;
            int green = 0;
            int blue = 0;

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }

            return Color.FromArgb(red, green, blue);
        }
        /// <summary>
        ///  Recursive fill function to change the color of a selected node and all of its children.
        /// </summary>
        /// <param name="element">The current element been resolved.</param>
        /// <param name="sourceColor">The source color to search for.</param>
        /// <param name="replaceColor">The color to be replaced the source color with.</param>
        private static void ChangeFill(SvgElement element, Color hexColor)
        {
            if (element is SvgPath)
            {

                (element as SvgPath).Fill = new SvgColourServer(hexColor);
            }
            if (element is SvgGroup)
            {
                var group = element as SvgGroup;
                
                (element as SvgGroup).Fill = new SvgColourServer(hexColor);
            }
            if (element.Children.Count > 0)
            {
                foreach (var item in element.Children)
                {
                    ChangeFill(item, hexColor);
                }
            }

        }
        private static void ChangeFill(SvgElement element, Color hexColor,Color orignalColor)
        {
            if (element is SvgPath)
            {
                SvgColourServer clr = new SvgColourServer(orignalColor);
                if ((element as SvgPath).Fill == clr)
                    (element as SvgPath).Fill = new SvgColourServer(hexColor);
            }
            if(element is SvgGroup)
            {
                SvgColourServer clr = new SvgColourServer(orignalColor);
                if ((element as SvgGroup).Fill == clr)
                    (element as SvgGroup).Fill = new SvgColourServer(hexColor);
            }
            if (element.Children.Count > 0)
            {
                foreach (var item in element.Children)
                {
                    ChangeFill(item, hexColor,orignalColor);
                }
            }

        }
        /// <summary>
        ///  Recursive fill function to change the color of a selected node and all of its children.
        /// </summary>
        /// <param name="element">The current element been resolved.</param>
        /// <param name="sourceColor">The source color to search for.</param>
        /// <param name="replaceColor">The color to be replaced the source color with.</param>
        public static string UpdateSvg(string srcUrl, float height, float width, List<svgColorData> colorStyles)
        {
            SvgDocument document = SvgDocument.Open(srcUrl);
           // double width = oObject.MaxWidth.Value, height = oObject.MaxHeight.Value;
            if (!document.Width.IsEmpty)
                width = document.Width;
            if (!document.Height.IsEmpty)
                height = document.Height;

            SvgUnit objUnit = new SvgUnit(SvgUnitType.Percentage, 100);
            document.Width = objUnit;
            document.Height = objUnit;
            //apply colors

            if(colorStyles != null && colorStyles.Count > 0 )
            {
                foreach (var obj in colorStyles)
                {
                    if(obj.PathIndex == -2)
                    {
                        if(obj.ModifiedColor != "")
                            AdjustColour(document, obj.ModifiedColor);
                    }
                    else
                    {
                        if (obj.ModifiedColor != "")
                        {
                            var index = 0;
                            for (int i = 0; i < document.Children.Count; i++)
                            {
                                if (document.Children[i] is SvgPath || document.Children[i] is SvgGroup)
                                {

                                    if (index == obj.PathIndex)
                                    {
                                        Color color = HexToColor(obj.ModifiedColor);
                                        //   Color orgClr = HexToColor(obj.OriginalColor);
                                        ChangeFill(document.Children[i], color);
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            SvgViewBox vb = new SvgViewBox(0, 0, width, height);
            document.ViewBox = vb;
            document.AspectRatio.Align = Svg.SvgPreserveAspectRatio.none;
            string ext = Path.GetExtension(srcUrl);
            string sourcePath = srcUrl;
           
            
            string[] results = sourcePath.Split(new string[] { ext }, StringSplitOptions.None);
            string destPath = results[0] + "_modified" + ext;
            document.Write(destPath);
            return destPath;

        }
        public static void GetSvgHieghtAndWidth(string srcUrl, out double width, out double height)
        {
            width = 0;
            height = 0;
            //SvgDocument document = SvgDocument.Open(srcUrl);
            //if (MaximumSize.Height != 0 && MaximumSize.Width != 0)
            //{

            //    if (document.Height > MaximumSize.Height)
            //    {
            //        width = (int)((document.Width / (double)document.Height) * MaximumSize.Height);
            //        height = MaximumSize.Height;
            //    }
            //}else
            //{
            //    if (!document.Width.IsEmpty)
            //        width = document.Width;
            //    if (!document.Height.IsEmpty)
            //        height = document.Height;
            //}
            var bitmap = GetBitmapFromSVG(srcUrl, "");
            width = bitmap.Width;
            height = bitmap.Height;
         
        }
        public static string UpdateSvgData(string srcUrl, float height, float width)
        {
            string data = "";
            SvgDocument document = SvgDocument.Open(srcUrl);
            // double width = oObject.MaxWidth.Value, height = oObject.MaxHeight.Value;
            if (!document.Width.IsEmpty)
                width = document.Width;
            if (!document.Height.IsEmpty)
                height = document.Height;

            SvgUnit objUnit = new SvgUnit(SvgUnitType.Percentage, 100);

            data = "width='100%' heigh='100%' viewBox='0 0 " + width + " " + height + "'  preserveAspectRatio='none' xmlns:dc='http://purl.org/dc/elements/1.1/' xmlns:cc='http://creativecommons.org/ns#' xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#' xmlns:svg='http://www.w3.org/2000/svg' xmlns='http://www.w3.org/2000/svg' xmlns:sodipodi='http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd' xmlns:inkscape='http://www.inkscape.org/namespaces/inkscape' version='1.1'  x='0px' y='0px'";




            return data;

        }
    }
}
