using System;
using System.Net;
using System.Web;
using System.Web.SessionState;
using MPC.Webstore.SessionModels;


namespace MPC.Webstore.Common
{
    public sealed class UserCookieManager
    {


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
                if (HttpContext.Current.Request.Cookies["FirstName"] != null)
                {
                    HttpContext.Current.Request.Cookies["FirstName"].Value = value.ToString();
                }
                else
                {
                    HttpCookie contactFirstNameCookie = null;
                    contactFirstNameCookie = new HttpCookie("FirstName", value.ToString());
                    HttpContext.Current.Response.Cookies.Add(contactFirstNameCookie);
                }
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
                    HttpCookie contactLastNameCookie = null;
                    contactLastNameCookie = new HttpCookie("LastName", value.ToString());
                    HttpContext.Current.Response.Cookies.Add(contactLastNameCookie);
                }
            }
        }
        public static string ContactCanEditProfile
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["CanEditProfile"] != null)
                {
                    return (HttpContext.Current.Request.Cookies["CanEditProfile"].Value);
                }
                else
                {
                    return "";
                }

            }
            set
            {
                if (HttpContext.Current.Request.Cookies["CanEditProfile"] != null)
                {
                    HttpContext.Current.Request.Cookies["CanEditProfile"].Value = value.ToString();
                }
                else
                {
                    HttpCookie contactCanEditProfile = null;
                    contactCanEditProfile = new HttpCookie("CanEditProfile", value.ToString());
                    HttpContext.Current.Response.Cookies.Add(contactCanEditProfile);
                }
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
                    return false;
                }

            }
            set
            {
                if (HttpContext.Current.Request.Cookies["ShowPrice"] != null)
                {
                    HttpContext.Current.Request.Cookies["ShowPrice"].Value = value.ToString();
                }
                else
                {
                    HttpCookie showPriceOnWebstore = null;
                    showPriceOnWebstore = new HttpCookie("ShowPrice", value.ToString());
                    HttpContext.Current.Response.Cookies.Add(showPriceOnWebstore);
                }
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
                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("StoreMode", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }
    }
}