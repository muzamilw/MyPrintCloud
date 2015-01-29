using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CostCenterService : ICostCentersService
    {
        #region Private

        private readonly ICostCentreRepository _costCenterRepository;
        private readonly IChartOfAccountRepository _chartOfAccountRepository;
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICostCenterTypeRepository _costcentreTypeRepository;
        private readonly IMarkupRepository _markupRepository;
        #endregion

        #region Constructor

        public CostCenterService(ICostCentreRepository costCenterRepository)
        {
            this._costCenterRepository = costCenterRepository;
        }

        #endregion

        #region Public
        
        public IEnumerable<CostCentre> GetAll(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetAllNonSystemCostCentres();
        }

        public CostCentre Add(CostCentre costcenter)
        {
           // _costCenterRepository.Add(costcenter);
          //  _costCenterRepository.SaveChanges();
            return costcenter;
        }

        public CostCentre Update(CostCentre costcenter)
        {
           // _costCenterRepository.Update(costcenter);
           // _costCenterRepository.SaveChanges();
            return costcenter;
        }
        public bool Delete(long costcenterId)
        {
          //  _costCenterRepository.Delete(GetCostCentreById(costcenterId));
          //  _costCenterRepository.SaveChanges();
            return true;
        }

        public CostCentre GetCostCentreById(long id)
        {
             return _costCenterRepository.GetCostCentreByID(id);
        }
        public CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request)
        {
            return _costCenterRepository.GetUserDefinedCostCenters(request);
        }

        public CostCenterBaseResponse GetBaseData(CostCenterRequestModel request)
        {
            return new CostCenterBaseResponse
            {
                CostCenterCategories = _costcentreTypeRepository.GetAll(),
                CostCenterResources = _systemUserRepository.GetAll(),
                NominalCodes = _chartOfAccountRepository.GetAll(),
                Markups = _markupRepository.GetAll()
            };
        }
        #endregion

    }
}
