using System;
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
        private readonly ICostCentreVariableRepository _costCentreVariableRepository;
        #endregion

        #region Constructor

        public CostCenterService(ICostCentreRepository costCenterRepository, IChartOfAccountRepository chartOfAccountRepository, ISystemUserRepository systemUserRepository, ICostCenterTypeRepository costCenterTypeRepository,
            IMarkupRepository markupRepository, ICostCentreVariableRepository costCentreVariableRepository)
        {
            if (costCenterRepository == null)
            {
                throw new ArgumentNullException("costCenterRepository");
            }
            if (chartOfAccountRepository == null)
            {
                throw new ArgumentNullException("chartOfAccountRepository");
            }
            if (systemUserRepository == null)
            {
                throw new ArgumentNullException("systemUserRepository");
            }
            if (costCenterTypeRepository == null)
            {
                throw new ArgumentNullException("costCenterTypeRepository");
            }
            if (markupRepository == null)
            {
                throw new ArgumentNullException("markupRepository");
            }
            if (costCentreVariableRepository == null)
            {
                throw new ArgumentNullException("costCentreVariableRepository");
            }
            this._costCenterRepository = costCenterRepository;
            this._chartOfAccountRepository = chartOfAccountRepository;
            this._systemUserRepository = systemUserRepository;
            this._costcentreTypeRepository = costCenterTypeRepository;
            this._markupRepository = markupRepository;
            this._costCentreVariableRepository = costCentreVariableRepository;
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

        public CostCenterBaseResponse GetBaseData()
        {
            return new CostCenterBaseResponse
            {
                CostCenterCategories = _costcentreTypeRepository.GetAll(),
                CostCenterResources = _systemUserRepository.GetAll(),
                NominalCodes = _chartOfAccountRepository.GetAll(),
                Markups = _markupRepository.GetAll(),
                CostCentreVariables = _costCentreVariableRepository.returnLoadVariableList()
            };
        }
        #endregion

    }
}
