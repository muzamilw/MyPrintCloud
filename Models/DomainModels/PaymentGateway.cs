using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class PaymentGateway
    {
        public int PaymentGatewayId { get; set; }
        public string BusinessEmail { get; set; }
        public string IdentityToken { get; set; }
        public bool isActive { get; set; }
        public long? CompanyId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string SecureHash { get; set; }
        public string CancelPurchaseUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string LiveApiUrl { get; set; }
        public string TestApiUrl { get; set; }
        public bool? SendToReturnURL { get; set; }
        public bool? UseSandbox { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual Company Company { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(PaymentGateway target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }



            target.BusinessEmail = BusinessEmail;
            target.IdentityToken = IdentityToken;
            target.isActive = isActive;
            target.PaymentMethodId = PaymentMethodId;
            target.SecureHash = SecureHash;
            target.CancelPurchaseUrl = CancelPurchaseUrl;
            target.ReturnUrl = ReturnUrl;
            target.NotifyUrl = NotifyUrl;
            target.LiveApiUrl = LiveApiUrl;
            target.TestApiUrl = TestApiUrl;
            target.UseSandbox = UseSandbox;
           

        }

        #endregion
    }
}
