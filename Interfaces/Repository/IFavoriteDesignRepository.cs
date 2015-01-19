using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IFavoriteDesignRepository
    {
        FavoriteDesign GetFavContactDesign(long templateID, long contactID);
        /// <summary>
        /// Gets favorite design count Of a login user to display on dashboard
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        int GetFavDesignCountByContactId(long contactId);
    }
}
