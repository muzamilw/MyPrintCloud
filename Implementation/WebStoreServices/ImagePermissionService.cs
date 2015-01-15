using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class ImagePermissionService : IImagePermissionsService
    {
        #region private
        public readonly IImagePermissionsRepository _imagePermissionRepository;
        #endregion

        #region constructor
        public ImagePermissionService(IImagePermissionsRepository imagePermissions)
        {
            this._imagePermissionRepository = imagePermissions;

        }
        #endregion

        #region public
        public bool UpdateImagTerritories(long imgID, string territory)
        {
            string[] territories = territory.Split('_');
            return _imagePermissionRepository.UpdateImagTerritories(imgID, territories);

        }
        public List<ImagePermission> getTerritories(long imgID)
        {
            return _imagePermissionRepository.getTerritories(imgID);
        }
        #endregion
    }
}
