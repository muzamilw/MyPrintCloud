using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    class LookupMethodService : ILookupMethodService
    {
        private readonly ILookupMethodRepository _LookupMethodRepository;

        public LookupMethodService(ILookupMethodRepository _LookupMethodRepository)
        {
            this._LookupMethodRepository = _LookupMethodRepository;
        }


        public IEnumerable<LookupMethod> GetAll()
        {
            return _LookupMethodRepository.GetAll();
        }

        public LookupMethodResponse GetlookupById(long MethodId)
        {
            return _LookupMethodRepository.GetlookupById(MethodId);
        }

        public bool UpdateLookup(LookupMethodResponse response)
        {
            return _LookupMethodRepository.UpdateLookup(response);
        }
        public LookupMethod AddLookup(LookupMethodResponse response)
        {
            return _LookupMethodRepository.AddLookup(response);
        }
        public bool DeleteGuillotinePTVId(long id)
        {
            return _LookupMethodRepository.DeleteGuillotinePTVId(id);
        }
        public bool DeleteMachineLookup(long id)
        {
            return _LookupMethodRepository.DeleteMachineLookup(id);
        }
    }
}
