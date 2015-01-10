using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MPC.Webstore.Common
{
    public class Utils
    {
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
            // return string.Format("{0:n}", Math.Round(Convert.ToDouble(valueToFormat.Replace(".",",")), 2));


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

      
    }
    public static class CommonHtmlExtensions
    {
        static Assembly FindGlobalResAssembly()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.FullName.StartsWith("App_GlobalResources."))
                    return asm;
            }
            return null;
        }

        public static string GetResource(this HtmlHelper htmlHelper, string name)
        {
            string languageIdentifier = "Resources." + UserCookieManager.OrganisationLanguageIdentifier;
            Assembly asm =  FindGlobalResAssembly();
            if (asm == null)
                return null;
            return new ResourceManager(languageIdentifier, asm).GetObject(name).ToString();
        }

        public static string GetResource(string name)
        {
            string languageIdentifier = "Resources." + UserCookieManager.OrganisationLanguageIdentifier;
            Assembly asm = FindGlobalResAssembly();
            if (asm == null)
                return null;
            return new ResourceManager(languageIdentifier, asm).GetObject(name).ToString();
        }
    }
}