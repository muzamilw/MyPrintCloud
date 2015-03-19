using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
