using MPC.Interfaces.WebStoreServices;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using MPC.Webstore.ModelMappers;
using MPC.Models.ResponseModels;
using System.Runtime.Caching;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MPC.Webstore.Common
{
    public class Utils
    {
        private readonly ICompanyService _myCompanyService;
        private static XmlDocument rexcFiel = null;
        public Utils(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            _myCompanyService = myCompanyService;
           // this._myCompanyService = myCompanyService;
        }

        public static double CalculateTaxOnPrice(double ActualPrice, double TaxValue)
        {
            double Price = ActualPrice + ((ActualPrice * TaxValue) / 100);
            return Price;
        }
        public static string FormatValueToTwoDecimal(string valueToFormat)
        {
            if (!string.IsNullOrEmpty(valueToFormat))
            {
                return string.Format("{0:n}", Math.Round(Convert.ToDouble(valueToFormat, CultureInfo.CurrentCulture), 2));
            }
            else
            {
                return "";
            }
        }
        public static string GetAppBasePath()
        {
            return WebConfigurationManager.AppSettings["AppBasePath"];
        }

        public static string FormatDateValue(DateTime? dateTimeValue, string formatString = null)
        {
            const string defaultFormat = "MMMM d, yyyy";

            if (dateTimeValue.HasValue)
                return dateTimeValue.Value.ToString(string.IsNullOrWhiteSpace(formatString) ? defaultFormat : formatString);
            else
                return string.Empty;
        }


        public static string FormatDecimalValueToTwoDecimal(string valueToFormat, string currenctySymbol)
        {
            return string.Format("{0}{1}", currenctySymbol, Utils.FormatDecimalValueToTwoDecimal(valueToFormat));
        }
        public static string FormatDecimalValueToTwoDecimal(string valueToFormat)
        {
            if (!string.IsNullOrEmpty(valueToFormat))
            {
                return string.Format("{0:n}", Math.Round(Convert.ToDouble(valueToFormat, CultureInfo.CurrentCulture), 2));
            }
            else
            {
                return "";
            }
        }

        public static double FormatDecimalValueToTwoDecimal(double? valueToFormat)
        {
           
                return Math.Round(Convert.ToDouble(valueToFormat, CultureInfo.CurrentCulture), 2);
           
        }

        public static DateTime AddBusinessdays(decimal ProductionDays, DateTime StartingDay)
        {
            var sign = ProductionDays < 0 ? -1 : 1;

            var unsignedDays = Math.Abs(ProductionDays);

            var weekdaysAdded = 0;

            DateTime Estimateddate = StartingDay;

            while (weekdaysAdded < unsignedDays)
            {
                Estimateddate = Estimateddate.AddDays(sign);

                if (Estimateddate.DayOfWeek != DayOfWeek.Saturday && Estimateddate.DayOfWeek != DayOfWeek.Sunday)

                    weekdaysAdded++;

            }
            return Estimateddate;
        }
        public static double CalculateVATOnPrice(double ActualPrice, double TaxValue)
        {
            double Price = ActualPrice + ((ActualPrice * TaxValue) / 100);
            return Price;
        }

        public static string FormatDecimalValueToZeroDecimal(string valueToFormat, string currenctySymbol)
        {
            return string.Format("{0}{1}", currenctySymbol, Utils.FormatDecimalValueToZeroDecimal(valueToFormat));
        }
        public static string FormatDecimalValueToZeroDecimal(string valueToFormat)
        {

            return string.Format("{0:0}", Math.Round(Convert.ToDouble(valueToFormat), 2));

        }

        public static string GetImagePath(string Folder, long OrganisationId, long CompanyId, string ImageURl)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}", Folder, "Organisation" + OrganisationId, "/", CompanyId, "/", ImageURl);
        }
        public static string GetKeyValueFromResourceFile(string key, long StoreId)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            if (StoreId > 0)
            {
                MyCompanyDomainBaseReponse stores = (cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>)[StoreId];

                XmlDocument resxFile = null;

                if (stores != null)
                {
                    resxFile = stores.ResourceFile;
                }

                if (resxFile != null)
                {
                    XmlNode loRoot = resxFile.SelectSingleNode("root/data[@name='" + key + "']/value");

                    if (loRoot != null)
                    {
                        return (loRoot).InnerXml;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else 
            { 
                return ""; 
            }
            
        }

        public static void DeleteFile(string completePath)
        {
            try
            {
                completePath = HttpContext.Current.Server.MapPath(completePath);
                if (System.IO.File.Exists(completePath))
                {
                    System.IO.File.Delete(completePath);
                }
            }
            catch (Exception)
            { }
        }

        public static void DeleteAllFilesInFolder(string folderPath)
        {
            DirectoryInfo dirInfo = null;
            try
            {

                dirInfo = new DirectoryInfo(folderPath);
                if (dirInfo.Exists)
                {
                    // string[] files = Directory.GetFiles(folderPath, "*.png");
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string file in files)
                    {
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                }

            }
            catch (Exception)
            { }
            finally
            {
                dirInfo = null;
            }
        }

        public static string specialCharactersEncoder(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                value = value.Replace("/", "-");
                value = value.Replace(" ", "-");
                value = value.Replace(";", "-");
                value = value.Replace("&#34;", "");
                value = value.Replace("&", "");
                value = value.Replace("+", "");
            }
            
            return value;
        }

        public static string specialCharactersEncoderCostCentre(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("/", "");
                value = value.Replace(" ", "");
                value = value.Replace(";", "");
                value = value.Replace("&#34;", "");
                value = value.Replace("&", "");
                value = value.Replace("+", "");
            }

            return value;
        }
        public static string BuildCategoryUrl(string pageName, string CategoryName, string CategoryId)
        {
            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(pageName))
                queryString = string.Format("{0}{1}{2}", "/", pageName, "/");

            CategoryName = specialCharactersEncoder(CategoryName);
            queryString += string.Format("{0}{1}{2}", CategoryName, "/", CategoryId);
            return queryString;
        }
      
    }

    public static class CloneList
    {
        public static T DeepClone<T>(this T o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
    public static class CommonHtmlExtensions
    {
        public static string ResolveString(this HtmlHelper htmlHelper, string categoryname)
        {
            Regex reg = new Regex("[\\s;\\/:*?\"<>|&']", RegexOptions.IgnoreCase);
            categoryname = reg.Replace(categoryname, "-");
            return categoryname;
        }

        public static string GetAppBasePath(this HtmlHelper htmlHelper)
        {
            return WebConfigurationManager.AppSettings["AppBasePath"];
        }

        public static string GetKeyValueFromResourceFile(this HtmlHelper htmlHelper, string Key, long StoreId)
        {
            return Utils.GetKeyValueFromResourceFile(Key, StoreId);
        }

        public static string GetAttachmentFileName(this HtmlHelper htmlHelper, string ProductCode, string OrderCode, string ItemCode, string SideCode, string extension, DateTime OrderCreationDate)
        {
            string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") + OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;

            return FileName;
        }
        public static string GetFileExtension(this HtmlHelper htmlHelper, string fileName)
        {
            return System.IO.Path.GetExtension(fileName);
        }
    //    static Assembly FindGlobalResAssembly()
    //    {
    //        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    //        {
    //            if (asm.FullName.StartsWith("App_GlobalResources."))
    //                return asm;
    //        }
    //        return null;
    //    }

    //    public static string GetResource(this HtmlHelper htmlHelper, string name)
    //    {
    //        string languageIdentifier = "Resources." + UserCookieManager.OrganisationLanguageIdentifier;
    //        Assembly asm =  FindGlobalResAssembly();
    //        if (asm == null)
    //            return null;
    //        return new ResourceManager(languageIdentifier, asm).GetObject(name).ToString();
    //    }

    //    public static string GetResource(string name)
    //    {
    //        string languageIdentifier = "Resources." + UserCookieManager.OrganisationLanguageIdentifier;
    //        Assembly asm = FindGlobalResAssembly();
    //        if (asm == null)
    //            return null;
    //        return new ResourceManager(languageIdentifier, asm).GetObject(name).ToString();
    //    }


     
    }
}