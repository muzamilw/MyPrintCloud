using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading;
using Microsoft.Practices.Unity;
using System.IO;
using Newtonsoft.Json;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
 
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        //private readonly IUserStatusService _IUserStatusService;
        //private readonly IUserFlagService _IUserFlagService;
        //private readonly IEmailManagerService _IEmailManagerService;
        //private readonly ISystemUserService _ISystemUserService;

        //private readonly IClaimsHelperService _IClaimHelperService;
        //[Dependency]
        //public IClaimsSecurityService _IClaimsSecurityService { get; set; }




        //private IAuthenticationManager AuthenticationManager
        //{
        //    get { return HttpContext.GetOwinContext().Authentication; }
        //}
        public AccountController()
        {
            //if (IEmailManagerService == null)
            //{
            //    throw new ArgumentNullException("IEmailManagerService");
            //}

            //this._IEmailManagerService = IEmailManagerService;
            //if (IClaimsSecurityService == null)
            //{
            //    throw new ArgumentNullException("IClaimsSecurityService");
            //}

            //this._IClaimsSecurityService = IClaimsSecurityService;
            //if (IUserStatusService == null)
            //{
            //    throw new ArgumentNullException("IUserStatusService");
            //}

            //this._IUserStatusService = IUserStatusService;
            //if (IUserFlagService == null)
            //{
            //    throw new ArgumentNullException("IUserFlagService");
            //}

            //this._IUserFlagService = IUserFlagService;
            //  if (_ISystemUserService == null)
            //{
            //    throw new ArgumentNullException("IUserFlagService");
            //}

            //this._ISystemUserService = _ISystemUserService;
            //this._IClaimHelperService = IClaimHelperService;
            
        }
        //public ApplicationUserManager UserManager
        //{
        //    get { return _userManager ??  HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        //    private set { _userManager = value; }
        //}

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //
        // GET: /Account/Login
        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}


        //private ApplicationSignInManager _signInManager;

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set { _signInManager = value; }
        //}

        //
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
          
        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true

        //    if (!_ISystemUserService.IsUserEmailConfirmed(model.Email))
        //    {
        //        ModelState.AddModelError("", "Please verify email to activate your account.");
        //        return View(model);
        //    }

        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            var currentUser = await UserManager.FindByEmailAsync(model.Email);
        //            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

        //            _IClaimsSecurityService.AddSignInClaimsToIdentity(currentUser.Id, currentUser.Email, currentUser.FullName, identity);

        //            var claimsPriciple = new ClaimsPrincipal(identity);
        //            // Make sure the Principal's are in sync
        //            HttpContext.User = claimsPriciple;

        //            Thread.CurrentPrincipal = HttpContext.User;

        //            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

        //             if (!string.IsNullOrEmpty(currentUser.profilePictureUrl))
        //          {
        //              string profileName = currentUser.profilePictureUrl;
        //              string extOfFile = Path.GetExtension(profileName);
        //              string FileName = Path.GetFileNameWithoutExtension(profileName);
        //              UserCookieManager.UserProfileImage = "/Clydy_Content/Profile/" + FileName + "_thumb" + extOfFile;
        //          }
        //             return RedirectToAction("CreateDesign", "Home", new {ContentId = 0, @area = "templates" });
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}

        //
        // GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
        //    if (user != null)
        //    {
        //        var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}
       
        // POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}
        //[HttpPost]
        //public JsonResult UpdatePassword(string userId,string txtOldPassword, string txtNewPassword)
        //{

        //    var result = SignInManager.UserManager.ChangePassword(userId, txtOldPassword, txtNewPassword);
        //    string response = "done";
        //    if (!result.Succeeded)
        //    {
        //        response = "";
        //        foreach(var str in result.Errors)
        //        {
        //            response += str +"\n";
        //        }
        //    }
        //    return Json(response, JsonRequestBehavior.DenyGet);
        //}
        //
        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}


        //[AllowAnonymous]
        //public ActionResult Success(string type)
        //{
        //    ViewBag.Type = type;
        //    return View();
        //}



        //
        // GET: /Account/ForgotPassword
        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
        //            protocol: Request.Url.Scheme);
        //        //await
        //        //    UserManager.SendEmailAsync(user.Id, "Reset Password",
        //        //        "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");

        //        await _IEmailManagerService.SendPasswordResetLinkEmail(user, callbackUrl);
        //        ViewBag.Link = "Please check your email.";

        //        return RedirectToAction("Success", "Account", new { type = "2" });
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ForgotPasswordConfirmation(string userid)
        //{
        //    return View();
        //}

        //
        // GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Success", "Account", new { type = "3" });
        //       // return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //// Sign in the user with this external login provider if the user already has a login
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        // If the user does not have an account, then prompt the user to create an account
            //        ViewBag.ReturnUrl = returnUrl;
            //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            //}
            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = "" });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //         return RedirectToAction("CreateDesign", "Home", new {ContentId = 0, @area = "templates" });
            //}

            //if (ModelState.IsValid)
            //{
            //    // Get the information about the user from the external login provider
            //    var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        return View("ExternalLoginFailure");
            //    }
            //    var user = new SystemUser { UserName = model.Email, Email = model.Email, FullName = model.Email };
            //    bool doLogin = false;
            //    if(!_ISystemUserService.isUserExist(model.Email))
            //    {
            //          var  res = await UserManager.CreateAsync(user);
            //          if (res.Succeeded)
            //          {
            //              bool result = _ISystemUserService.insertExternalLonginKey(user.Id, info.Login.LoginProvider, info.Login.ProviderKey);
            //          //    res = await UserManager.AddLoginAsync(user.Id, info.Login);
            //              if (result)
            //              {
            //                  doLogin = true;
            //              }
            //          }
            //          AddErrors(res);
            //    }
            //    else
            //    {
            //         user = _ISystemUserService.GetUserProfileByEmail(model.Email);
            //         bool result = _ISystemUserService.insertExternalLonginKey(user.Id, info.Login.LoginProvider, info.Login.ProviderKey);
            //         if (result)
            //         {
            //             doLogin = true;
            //         }
            //    }
            //    if(doLogin)
            //    {
            //        ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

            //        _IClaimsSecurityService.AddSignInClaimsToIdentity(user.Id, user.Email, user.FullName, identity);

            //        var claimsPriciple = new ClaimsPrincipal(identity);
            //        // Make sure the Principal's are in sync
            //        HttpContext.User = claimsPriciple;

            //        Thread.CurrentPrincipal = HttpContext.User;

            //        AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

            //        if (!string.IsNullOrEmpty(user.profilePictureUrl))
            //        {
            //            string profileName = user.profilePictureUrl;
            //            string extOfFile = Path.GetExtension(profileName);
            //            string FileName = Path.GetFileNameWithoutExtension(profileName);
            //            UserCookieManager.UserProfileImage = "/Clydy_Content/Profile/" + FileName + "_thumb" + extOfFile;
            //        }
            //        return RedirectToAction("CreateDesign", "Home", new { ContentId = 0, @area = "templates" });
            //    }

              
            //}

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(SigninViewModel model)
        //{
        //    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = "1234", code = "aaaa" },
        //    //          protocol: Request.Url.Scheme);
               
        //    if (ModelState.IsValid)
        //    {
        //        //EmailManager oEmailManager = new EmailManager();
        //        var user = new SystemUser { UserName = model.Email, Email = model.Email, FullName = model.FName + " " + model.LName, isDefault = true, CreatedDateTime = DateTime.Now, IsSubscribed = true };
        //        var result = await UserManager.CreateAsync(user, model.Password);


        //        if (result.Succeeded)
        //        {
        //            user.LastLoginTime = DateTime.Now;

        //            UserManager.Update(user);
        //            _IUserStatusService.AddDefaultUserStatus(user.Id);
        //            _IUserFlagService.AddDefaultUserFlag(user.Id);
        //            var addUserToRoleResult = await UserManager.AddToRoleAsync(user.Id, "Admin");
        //            if (!addUserToRoleResult.Succeeded)
        //            {
        //                throw new InvalidOperationException(string.Format("Failed to add user to role {0}", "Admin"));
        //            }


        //            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

        //            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = user.Id, code = code },
        //                      protocol: Request.Url.Scheme);


        //            await _IEmailManagerService.SendAccountVerificationEmail(user, callbackUrl);
        //            ViewBag.Link = callbackUrl;


        //            return RedirectToAction("Success", "Account", new { type="1"});
        //           // return View("DisplayEmail");
        //        }
        //        else 
        //        {
        //            ViewBag.hasErrors = "An account already exists with this email address.";
                    
        //        }
        //        SystemUser userCurrent = _ISystemUserService.GetUserProfileByEmail(model.Email);
        //        if (userCurrent != null && userCurrent.Status == 3 && userCurrent.EmailConfirmed == false)
        //        {
        //            var UpdatePassword = SignInManager.UserManager.ChangePassword(userCurrent.Id, "UserRegistrationPending", model.Password);
                   
        //            if (UpdatePassword.Succeeded)
        //            {
        //                userCurrent.FullName = model.FName + " " + model.LName;
        //                userCurrent.Status = 2;
        //                _ISystemUserService.updateSystemUser(userCurrent);
        //                //response = "";
        //                //foreach (var str in result.Errors)
        //                //{
        //                //    response += str + "\n";
        //                //}
        //            }


        //            // UserRegistrationPending
        //            var code = await UserManager.GenerateEmailConfirmationTokenAsync(userCurrent.Id);
        //            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = userCurrent.Id, code = code },
        //                   protocol: Request.Url.Scheme);


        //            await _IEmailManagerService.SendAccountVerificationEmail(userCurrent, callbackUrl);
        //            ViewBag.Link = callbackUrl;
        //            return View("DisplayEmail");
        //        }


                
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return RedirectToAction("Index", "Web");
        //}
        
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public string TempRegister(string email)
        //{
          
        //        var user = new SystemUser { UserName = email, Email = email, FullName = "ABC " , isDefault = true, CreatedDateTime = DateTime.Now, IsSubscribed = true };
        //        var result = UserManager.Create(user, "UserRegistrationPending");


        //        if (result.Succeeded)
        //        {
        //            UserManager.Update(user);
        //            _IUserStatusService.AddDefaultUserStatus(user.Id);
        //            _IUserFlagService.AddDefaultUserFlag(user.Id);
        //            _ISystemUserService.AddPendingstatus(user.Id);

        //            var addUserToRoleResult = UserManager.AddToRole(user.Id, "Admin");
        //            if (!addUserToRoleResult.Succeeded)
        //            {
        //                throw new InvalidOperationException(string.Format("Failed to add user to role {0}", "Admin"));
        //            }

                  
        //        }
               

        //    // If we got this far, something failed, redisplay form
        //    return user.Id;
        //}


        //public bool CreateChargebeeCustomer(string UserId)
        //{

        //    try
        //    {

        //        //chargebee accout creation.
        //        ApiConfig.Configure(System.Configuration.ConfigurationManager.AppSettings["chargebeeSiteName"], System.Configuration.ConfigurationManager.AppSettings["chargebeeAPIKey"]);

        //        SystemUser currentUser = UserManager.FindById(UserId);

        //            EntityResult resultChargeBee = Subscription.Create()
        //                              .PlanId(System.Configuration.ConfigurationManager.AppSettings["DefaultChargebeePlanId"])
        //                              .CustomerEmail(currentUser.Email)
        //                              .CustomerFirstName(currentUser.FullName)
        //                              .Request();
        //            Subscription subscription = resultChargeBee.Subscription;
        //            Customer chargebeeCustomer = resultChargeBee.Customer;

        //            // string username = User.Identity.Name;

                
        //            currentUser.ChargeBeeSubscriptionId = subscription.Id;
        //            currentUser.ChargeBeeCustomerId = chargebeeCustomer.Id;
        //            currentUser.ChargeBeePlanId = subscription.PlanId;
        //            currentUser.ChargeBeePlanName = subscription.PlanId;


        //            UserManager.Update(currentUser);


        //            Response.Cookies["ChargebeeCustomerId"].Value = chargebeeCustomer.Id.ToString();

                   

        //            return true;
                

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public static string AddAgileCrmContact(string email, string fullname)
        //{

        //    string firstname = "";
        //    string lastname = "";

        //    string[] namearray = fullname.Split(' ');
        //    if (namearray.Length > 0)
        //    {
        //        firstname = namearray[0];
        //    }
        //    else
        //    {
        //        firstname = fullname;
        //    }

        //    if (fullname.IndexOf(' ') > 0)
        //    {
        //        lastname = fullname.Substring(fullname.IndexOf(' '), fullname.Length - fullname.IndexOf(' '));
        //    }
        //    else
        //    {
        //        lastname = "";
        //    }

        //    JsonSerializerSettings oSetting = new JsonSerializerSettings();
        //    oSetting.NullValueHandling = NullValueHandling.Ignore;
        //    oSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        //    Property oProperty = null;

        //    Contact oContact = new Contact();
        //    //oContact.contact_company_id = CompanyResult.contact_company_id;
        //    oContact.type = "PERSON";
        //    oProperty = new Property("SYSTEM", "email", "NULL", email);
        //    oContact.properties.Add(oProperty);
        //    oProperty = new Property("SYSTEM", "first_name", "NULL", firstname);
        //    oContact.properties.Add(oProperty);
        //    oProperty = new Property("SYSTEM", "last_name", "NULL", lastname);
        //    oContact.properties.Add(oProperty);
        //    oProperty = new Property("SYSTEM", "company", "NULL", fullname);
        //    oContact.properties.Add(oProperty);
        //    //oProperty = new Property("SYSTEM", "phone", "work", phone);
        //    //oContact.properties.Add(oProperty);
        //    //oProperty = new Property("CUSTOM", "domain", "NULL", domain + ".saleflow.com");
        //    //oContact.properties.Add(oProperty);
        //    oContact.lead_score = "1";
        //    oContact.tags.Add("Clydy Web Registration");
        //    //oContact.tags.Add(region);

        //    string contactDetail = JsonConvert.SerializeObject(oContact, Formatting.Indented, oSetting);

        //    string result = AgileCRMService.CreateContact(contactDetail);

        //    //AgileCRMService.AddNote(email, "Region : " + region + " , Domain : " + domain + ".saleflow.com");

        //    return result;
        //}



        //public string AddAgileCrmContactTag(string email, string tag)
        //{

        //    var result = AgileCRMService.AddTags(email, tag);

        //    return result;
        //}
        
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    //ViewBag.userId = userId;
        //    //return View("ConfirmEmail");

        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }

        //    var user = UserManager.FindById(userId);
        //    if (user != null)
        //    {
        //        if (user.EmailConfirmed == true)
        //        {
        //            ViewBag.errormessage = "Email address is already confirmed. Please login to access account.";
        //            return View("ConfirmError");
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.errormessage = "Invalid or incorrect parameters";
        //        return View("ConfirmError");
        //    }

        //    var result = await UserManager.ConfirmEmailAsync(userId, code);

        //    if (result.Succeeded)
        //    {
        //        //send registration successful email;
                
        //        await _IEmailManagerService.SendRegisrationSuccessEmail(userId);
        //        var currentUser = UserManager.FindById(userId);
        //        //create user in agile CRM
        //        AddAgileCrmContact(currentUser.Email, currentUser.FullName);


        //        // ControllerContext.HttpContext.Response.Cookies.Add(new HttpCookie("mode", "new"));

        //        // var currentUser = await UserManager.FindByIdAsync(userId);
        //        //     HttpCookie userinfoCookie = new HttpCookie("UserInfo");
        //        // userinfoCookie["CustomerId"] = currentUser.CustomerID.ToString();
        //        //  userinfoCookie["Email"] = currentUser.Email;
        //        //   Response.Cookies["MISAddress"].Value = currentUser.tbl_Customer.MISAddress;
        //        //     Response.Cookies["FullName"].Value = currentUser.FullName;
        //        //      userinfoCookie.Expires = DateTime.Now.AddDays(2);
        //        //    Response.Cookies.Add(userinfoCookie);






        //        ViewBag.userId = userId;
        //        return View("Login");


        //    }
        //    else
        //    {
        //        ViewBag.errormessage = "Invalid or incorrect parameters. Please contact support@clydy.com";
        //        return View("ConfirmError");
        //    }

        //}
        ////
        //// POST: /Account/LogOff
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    _ISystemUserService.RemoveExternalLonginKey(_IClaimHelperService.SystemUserID());
        //    AuthenticationManager.SignOut();

        //    return RedirectToAction("Index", "Home");
        //}

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //#region Helpers
        //// Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        ////private IAuthenticationManager AuthenticationManager
        ////{
        ////    get
        ////    {
        ////        return HttpContext.GetOwinContext().Authentication;
        ////    }
        ////}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        //#endregion
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}