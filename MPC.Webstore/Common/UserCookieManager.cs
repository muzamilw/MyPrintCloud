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

        public static long StoreId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["StorId"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["StorId"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["StorId"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("StorId");

                }

                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("StorId", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }

        public static string ContactFirstName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["FirstName"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["FirstName"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["FirstName"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("FirstName");

                }
                HttpCookie contactFirstNameCookie = null;
                contactFirstNameCookie = new HttpCookie("FirstName", value.ToString());
                HttpContext.Current.Response.Cookies.Add(contactFirstNameCookie);

            }
        }

        public static string ContactLastName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["LastName"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["LastName"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Request.Cookies["LastName"] != null)
                {
                    HttpContext.Current.Request.Cookies["LastName"].Value = value.ToString();
                }
                else
                {
                    if (HttpContext.Current.Response.Cookies["LastName"] != null)
                    {
                        HttpContext.Current.Response.Cookies.Remove("LastName");

                    }
                    HttpCookie contactLastNameCookie = null;
                    contactLastNameCookie = new HttpCookie("LastName", value.ToString());
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

        public static int StoreMode
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["StoreMode"] != null)
                {
                    return Convert.ToInt32((HttpContext.Current.Request.Cookies["StoreMode"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["StoreMode"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("StoreMode");

                }

                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("StoreMode", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }

        public static long OrganisationID    
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["OrganisationID"] != null)
                {
                    return Convert.ToInt64((HttpContext.Current.Request.Cookies["OrganisationID"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["OrganisationID"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("OrganisationID");

                }
                HttpCookie organisationIdCookie = null;
                organisationIdCookie = new HttpCookie("OrganisationID", value.ToString());
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

        public static double StoreTax
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

        public static int RememberMeCompanyID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["RememberMeCompanyID"] != null)
                {
                    return Convert.ToInt32((HttpContext.Current.Request.Cookies["RememberMeCompanyID"].Value));
                }
                else
                {
                    return 0;
                }

            }
            set
            {
                if (HttpContext.Current.Response.Cookies["RememberMeCompanyID"] != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("RememberMeCompanyID");

                }

                HttpCookie RememberMeCompanyIDCookie = null;
                RememberMeCompanyIDCookie = new HttpCookie("RememberMeCompanyID", value.ToString());
                RememberMeCompanyIDCookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(RememberMeCompanyIDCookie);
            }
        }
    }
}