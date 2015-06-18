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
        private readonly ISectionFlagRepository sectionFlagRepository;

        public SectionService(ISectionRepository SectionRepository, ISectionFlagRepository sectionFlagRepository)
        {
            this._SectionRepository = SectionRepository;
            this.sectionFlagRepository = sectionFlagRepository;
        }


        public IEnumerable<Section> GetSectionsForPhraseLibrary()
        {
            return _SectionRepository.GetSectionsForPhraseLibrary();
        }

        /// <summary>
        /// Get Section Flag By Section Id
        /// </summary>
        public IEnumerable<SectionFlag> GetSectionFlagBySectionId(long sectionId)
        {
           return sectionFlagRepository.GetSectionFlagBySectionId(sectionId);
        }

        public bool SaveSectionFlags(IEnumerable<SectionFlag> flags)
        {
            // getting existing flags for a section
            IEnumerable<SectionFlag> dBSectionFlags=sectionFlagRepository.GetSectionFlagBySectionId((long) 
                flags.FirstOrDefault().SectionId);

            foreach (SectionFlag flag in dBSectionFlags)
            {
                Boolean isFound = false;
                foreach (SectionFlag oldFlag in flags)
                {
                    if (oldFlag.SectionFlagId == flag.SectionFlagId) // update case
                    {
                        flag.FlagName = oldFlag.FlagName;
                        flag.flagDescription = oldFlag.flagDescription;
                        flag.FlagColor = oldFlag.FlagColor;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)            // deletion
                {
                    sectionFlagRepository.Delete(flag);
                }
                
            }
            // adding new flags 
            foreach (SectionFlag flag in flags)
            {
                if (flag.SectionFlagId < 0)
                {
                    SectionFlag obj= sectionFlagRepository.Find(0);
                    obj = new SectionFlag
                    {
                        OrganisationId = sectionFlagRepository.OrganisationId,
                        FlagName = flag.FlagName,
                        flagDescription = flag.flagDescription,
                        FlagColor = flag.FlagColor,
                        SectionId = flag.SectionId
                    };
                    sectionFlagRepository.Add(obj);
                }
            }
            sectionFlagRepository.SaveChanges();
            return true;
        }

    }
}
