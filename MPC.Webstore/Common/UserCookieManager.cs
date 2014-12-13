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
                HttpCookie storeIdCookie = null;
                storeIdCookie = new HttpCookie("StorId", value.ToString());
                HttpContext.Current.Response.Cookies.Add(storeIdCookie);
            }
        }
    }
}