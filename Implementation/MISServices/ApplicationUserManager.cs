using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using MPC.Models.Common;
using MPC.Repository.BaseRepository;

namespace MPC.Implementation.MISServices
{

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public static class UnityConfig
    {
        public static IUnityContainer UnityContainer { get; set; }
    }

    /// <summary>
    /// Application User Manager
    /// </summary>
    public class ApplicationUserManager : UserManager<MisUser, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationUserManager(IUserStore<MisUser, string> store)
            : base(store)
        {
        }

        /// <summary>
        /// Create User
        /// </summary>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;


            BaseDbContext db = (BaseDbContext)UnityConfig.UnityContainer.Resolve(typeof(BaseDbContext),
                new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

            var manager = new ApplicationUserManager(new UserStore<MisUser>(db));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MisUser, string>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<MisUser, string>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<MisUser, string>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<MisUser, string>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

}
