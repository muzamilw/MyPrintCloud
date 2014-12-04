using System;
using System.Web;
using System.Web.SessionState;
using MPC.Models.DomainModels;


namespace MPC.Webstore.Common
{
    public sealed class SessionParameters
    {
        /// <summary>
        /// Gets the current session.
        /// </summary>
        private static HttpSessionState CurrentSession
        {
            get { return HttpContext.Current.Session; }
        }


        public static CompanyContact LoginContact
        {
            get
            {
                if (CurrentSession != null && CurrentSession["CompanyContact"] != null)
                {
                    return CurrentSession["CompanyContact"] as CompanyContact;
                }
                else
                {
                    return null;
                }

            }
            set { CurrentSession["CompanyContact"] = value; }
        }

        public static Company LoginCompany
        {
            get
            {
                if (CurrentSession != null && CurrentSession["LoginCompany"] != null)
                {
                    return CurrentSession["LoginCompany"] as Company;
                }
                else
                {
                    return null;
                }

            }
            set { CurrentSession["LoginCompany"] = value; }
        }
    }
}