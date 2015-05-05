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
    public class SectionFlagsService:ISectionFlagsService
    {
        private readonly ISectionFlagRepository _SectionFlagRepository;

        public SectionFlagsService(ISectionFlagRepository SectionFlagRepository)
        {
            this._SectionFlagRepository = SectionFlagRepository;
        }

        public IEnumerable<SectionFlag>  GetAllSectionFlag()
        {
            return _SectionFlagRepository.GetAllSectionFlagName();
        }
    }
}
