using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ErrorController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _myCampaignService;
        private readonly IUserManagerService _myUserManagerService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorController(ICompanyService myCompanyService, ICampaignService myCampaignService, IUserManagerService myUserManagerService)
            
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myCampaignService = myCampaignService;
            this._myUserManagerService = myUserManagerService;
        }

        #endregion
        // GET: Error
        public ActionResult Index(Exception exception, int errorType, string url)
        {
            if(exception != null)
            {
                Response.TrySkipIisCustomErrors = true;

                string EmailIds = ConfigurationManager.AppSettings["ErrorReportEmailIDs"];
                string MesgBody = "";
                if (!string.IsNullOrEmpty(EmailIds))
                {
                    if (UserCookieManager.WBStoreId > 0)
                    {
                        Company company = _myCompanyService.GetCompanyByCompanyID(UserCookieManager.WBStoreId);

                        if (company != null)
                        {
                            Organisation organisation = _myCompanyService.GetOrganisatonById(Convert.ToInt64(company.OrganisationId));
                            if (organisation != null)
                            {
                                string[] idsListToSendEmail = EmailIds.Split(',');
                                foreach (string id in idsListToSendEmail)
                                {
                                    MesgBody += "Dear ,<br>";
                                    MesgBody += "An error has been occurred:<br>";
                                    MesgBody += "Store Name: " + company.Name + "<br>";
                                    MesgBody += "Organisation Name: " + organisation.OrganisationName + "<br>";
                                    MesgBody += "Store Id: " + company.CompanyId + "<br>";
                                    MesgBody += "Organisation Id: " + organisation.OrganisationId + "<br>";
                                    MesgBody += "Url: " + url + "<br>";
                                    MesgBody += "Stack Trace: " + exception.StackTrace + "<br>";
                                    MesgBody += "Inner Exception: " + exception.InnerException + "<br>";

                                    SystemUser EmailOFSM = _myUserManagerService.GetSalesManagerDataByID(company.SalesAndOrderManagerId1.Value);
                                    if (EmailOFSM != null)
                                    {
                                        _myCampaignService.AddMsgToTblQueue(id, "", id, MesgBody, EmailOFSM.FullName, EmailOFSM.Email, organisation.SmtpUserName, organisation.SmtpPassword, organisation.SmtpServer, "Error Report on " + company.Name, null, 0);
                                    }
                                    else
                                    {
                                        _myCampaignService.AddMsgToTblQueue(id, "", id, MesgBody, "", "info@myprintcloud.com", organisation.SmtpUserName, organisation.SmtpPassword, organisation.SmtpServer, "Error Report on " + company.Name, null, 0);
                                    }
                                }
                            }
                        }
                    }
                }
                ViewBag.Exception = exception.Message;
                ViewBag.StackTrace = exception.StackTrace;
                ViewBag.InnerException = exception.InnerException;
                //MPCLogger olog = new MPCLogger();
                //olog.Write();
            }
           
            return View();
        }

    }
}