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
    class SectionService:ISectionService
    {

        private readonly ISectionRepository _SectionRepository;


        public SectionService(ISectionRepository SectionRepository)
        {

            this._SectionRepository = SectionRepository;

        }


        public IEnumerable<Section> GetSectionsForPhraseLibrary()
        {
            return _SectionRepository.GetSectionsForPhraseLibrary();
        }


    }
}
