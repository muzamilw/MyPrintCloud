using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyDomain
    {
        public long CompanyDomainId { get; set; }

        public string Domain { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        #region Public

        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        public void Clone(CompanyDomain target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }

            target.Domain = Domain;
          
        }

        #endregion

    }
}
