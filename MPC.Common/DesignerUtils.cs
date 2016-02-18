using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace MPC.Common
{
    public class DesignerUtils
    {

        public static double MMToPoint(double val)
        {
            return val * 2.834645669;
        }
         
        public static double PointToMM(double val)
        {
            return val / 2.834645669;
        }


        public static double PointToPixel(double Val)
        {
            return Val * 96 / 72;
        }
        public static double PixelToPoint(double Val)
        {
            return Val / 96 * 72;
        }
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }
        public static double InchtoPoint(double val)
        {
            return val * 25.4 * 2.834645669;

        }

        public static double PointToInch(double val)
        {
            return val / (25.4 * 2.834645669);

        }
        // download file from remote server 
        public static bool DownloadFile(string SourceURL, string DestinationBasePath)
        {
            Stream stream = null;
            MemoryStream memStream = new MemoryStream();
            try
            {
                WebRequest req = WebRequest.Create(SourceURL);
                WebResponse response = req.GetResponse();
                if (response != null)
                {
                    stream = response.GetResponseStream();


                    byte[] downloadedData = new byte[0];

                    byte[] buffer = new byte[1024];

                    //Get Total Size
                    int dataLength = (int)response.ContentLength;


                    while (true)
                    {
                        //Try to read the data
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;
                        else
                            memStream.Write(buffer, 0, bytesRead);
                    }

                    File.WriteAllBytes(DestinationBasePath, memStream.ToArray());
                }
                else
                    return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Clean up
                if (stream != null)
                    stream.Close();

                if (memStream != null)
                    memStream.Close();
            }
            return true;
        }

    }

}
