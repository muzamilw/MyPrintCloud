using System;
using System.Net;
using System.Web;
using System.Web.SessionState;
using MPC.Webstore.SessionModels;



namespace MPC.Webstore.Common
{
    public sealed class UserCookieManager
    {
        public static bool isWritePresistentCookie = false;
        
        public static long WBStoreId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WBStoreId"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["WBStoreId"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WBStoreId"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WBStoreId");

                }

                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("WBStoreId", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }

        public static string WEBContactFirstName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBFirstName"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["WEBFirstName"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WEBFirstName"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WEBFirstName");

                }
                HttpCookie contactFirstNameCookie = null;
                contactFirstNameCookie = new HttpCookie("WEBFirstName", value.ToString());
                HttpContext.Current.Response.Cookies.Add(contactFirstNameCookie);
                HttpContext.Current.Request.Cookies["WEBFirstName"].Value = value;
            }
        }

        public static string WEBEmail
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBEmail"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["WEBEmail"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WEBEmail"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WEBEmail");

                }
                HttpCookie contactEmailCookie = null;
                contactEmailCookie = new HttpCookie("WEBEmail", value.ToString());
                HttpContext.Current.Response.Cookies.Add(contactEmailCookie);

            }
        }

        public static string WEBContactLastName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBLastName"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["WEBLastName"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Request.Cookies["WEBLastName"] != null)
                {
                    HttpContext.Current.Request.Cookies["WEBLastName"].Value = value.ToString();
                }
                else
                {
                    if (HttpContext.Current.Response.Cookies["WEBLastName"] != null)
                    {
                        HttpContext.Current.Response.Cookies.Remove("WEBLastName");

                    }
                    HttpCookie contactLastNameCookie = null;
                    contactLastNameCookie = new HttpCookie("WEBLastName", value.ToString());
                    HttpContext.Current.Response.Cookies.Add(contactLastNameCookie);
                }
            }
        }

        public static bool ContactCanEditProfile
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["CanEditProfile"] != null)
                {
                    return Convert.ToBoolean((HttpContext.Current.Request.Cookies["CanEditProfile"].Value));
                }
                else
                {
                    return false;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["CanEditProfile"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("CanEditProfile");

                }
                HttpCookie contactCanEditProfile = null;
                contactCanEditProfile = new HttpCookie("CanEditProfile", value.ToString());
                HttpContext.Current.Response.Cookies.Add(contactCanEditProfile);

            }
        }

        public static bool ShowPriceOnWebstore
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["ShowPrice"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Request.Cookies["ShowPrice"].Value);
                }
                else
                {
                    return true;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["ShowPrice"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("ShowPrice");

                }

                HttpCookie showPriceOnWebstore = null;
                showPriceOnWebstore = new HttpCookie("ShowPrice", value.ToString());
                HttpContext.Current.Response.Cookies.Add(showPriceOnWebstore);

            }
        }

        public static int WEBStoreMode
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBStoreMode"] != null)
                {
                    return Convert.ToInt32((HttpContext.Current.Request.Cookies["WEBStoreMode"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WEBStoreMode"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WEBStoreMode");

                }

                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("WEBStoreMode", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }

        public static long WEBOrganisationID    
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBOrganisationID"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["WEBOrganisationID"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WEBOrganisationID"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WEBOrganisationID");

                }
                HttpCookie organisationIdCookie = null;
                organisationIdCookie = new HttpCookie("WEBOrganisationID", value.ToString());
                HttpContext.Current.Response.Cookies.Add(organisationIdCookie);
            }
        }

        public static bool isIncludeTax  
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["isIncludeTax"] != null)
                {
                    return Convert.ToBoolean((HttpContext.Current.Request.Cookies["isIncludeTax"].Value));
                }
                else
                {
                    return false;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["isIncludeTax"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("isIncludeTax");

                }

                HttpCookie IncludeTaxCookie = null;
                IncludeTaxCookie = new HttpCookie("isIncludeTax", value.ToString());
                HttpContext.Current.Response.Cookies.Add(IncludeTaxCookie);
            }
        }

        public static double TaxRate
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["TaxPrice"] != null)
                {
                    return Convert.ToDouble((HttpContext.Current.Request.Cookies["TaxPrice"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["TaxPrice"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("TaxPrice");

                }

                HttpCookie TaxPriceCookie = null;
                TaxPriceCookie = new HttpCookie("TaxPrice", value.ToString());
                 
                HttpContext.Current.Response.Cookies.Add(TaxPriceCookie);
            }
        }

        public static long TemporaryCompanyId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["TemporaryCompanyId"] != null)
                {
                    return Convert.ToInt32((HttpContext.Current.Request.Cookies["TemporaryCompanyId"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["TemporaryCompanyId"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("TemporaryCompanyId");

                }

                HttpCookie TemporaryCompanyIdCookie = null;
                TemporaryCompanyIdCookie = new HttpCookie("TemporaryCompanyId", value.ToString());
                TemporaryCompanyIdCookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(TemporaryCompanyIdCookie);
            }
        }

        public static long WEBOrderId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["WEBOrderId"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["WEBOrderId"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["WEBOrderId"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("WEBOrderId");

                }

                HttpCookie OrderIdCookie = null;
                OrderIdCookie = new HttpCookie("WEBOrderId", value.ToString());
                HttpContext.Current.Response.Cookies.Add(OrderIdCookie);
            }
        }

        public static int isRegisterClaims
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["isRegisterClaims"] != null)
                {
                    return Convert.ToInt32( HttpContext.Current.Request.Cookies["isRegisterClaims"].Value);
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["isRegisterClaims"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("isRegisterClaims");

                }
                HttpCookie  RegisterClaimCookie = null;
                RegisterClaimCookie = new HttpCookie("isRegisterClaims", value.ToString());
                HttpContext.Current.Response.Cookies.Add(RegisterClaimCookie);
            }
        }

        public static bool PerformAutoLogin
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["isPerformAutoLogin"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Request.Cookies["isPerformAutoLogin"].Value);
                }
                else
                {
                    return false;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["isPerformAutoLogin"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("isPerformAutoLogin");

                }
                HttpCookie AutoLoginClaimCookie = null;
                AutoLoginClaimCookie = new HttpCookie("isPerformAutoLogin", value.ToString());
                HttpContext.Current.Response.Cookies.Add(AutoLoginClaimCookie);
            }
        }


        public static long FreeShippingVoucherId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FreeShippingVoucherId"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["FreeShippingVoucherId"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["FreeShippingVoucherId"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("FreeShippingVoucherId");

                }

                HttpCookie FreeShippingVoucherIdCookie = null;
                FreeShippingVoucherIdCookie = new HttpCookie("FreeShippingVoucherId", value.ToString());
                HttpContext.Current.Response.Cookies.Add(FreeShippingVoucherIdCookie);
            }
        }

        //public static bool removeAllCookies()
        //{
        //    if (HttpContext.Current.Response.Cookies["FirstName"] != null)
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("FirstName");

        //    }
        //    if (HttpContext.Current.Response.Cookies["Email"] != null)
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("Email");

        //    }
        //    if (HttpContext.Current.Request.Cookies["LastName"] != null)
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("LastName");
        //    }
        //    if (HttpContext.Current.Response.Cookies["OrderId"] != null)
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("OrderId");

        //    } 
        //    if (HttpContext.Current.Response.Cookies["TemporaryCompanyId"] != null)
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("TemporaryCompanyId");

        //    }
        //    return true;
        //}
    }
}