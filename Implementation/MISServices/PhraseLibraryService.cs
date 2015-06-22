using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Phrase Library Service
    /// </summary>
    public class PhraseLibraryService : IPhraseLibraryService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISectionRepository sectionRepository;
        private readonly IPhraseRespository phraseRespository;
        private readonly IPhraseFieldRepository phraseFieldRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public PhraseLibraryService(ISectionRepository sectionRepository, IPhraseRespository phraseRespository, IPhraseFieldRepository phraseFieldRepository)
        {
            this.sectionRepository = sectionRepository;
            this.phraseRespository = phraseRespository;
            this.phraseFieldRepository = phraseFieldRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Section
        /// </summary>
        public IEnumerable<Section> GetSections()
        {
            return sectionRepository.GetSectionsForPhraseLibrary();
        }

       
        /// <summary>
        /// Get Phrases By Phrase Filed Id
        /// </summary>
        public IEnumerable<Phrase> GetPhrasesByPhraseFiledId(long phraseFieldId)
        {
            return phraseRespository.GetPhrasesByPhraseFiledId(phraseFieldId);
        }

        /// <summary>
        /// Save Phase Library
        /// </summary>
        public void SavePhaseLibrary(PhraseLibrarySaveModel phaseLibrary)
        {
            if (phaseLibrary.Sections != null)
            {
                IEnumerable<Section> sectionsDbVersion = sectionRepository.GetSectionsForPhraseLibrary();
                //find missing items
                List<Phrase> missingPhraseListItems = new List<Phrase>();
                foreach (var section in phaseLibrary.Sections)
                {
                    if (section.PhraseFields != null)
                    {
                        Section sectionDbVersion =
                            sectionsDbVersion.FirstOrDefault(s => s.SectionId == section.SectionId);
                        //update phase field text for job Production
                        if (section.SectionId == 4)
                        {
                            if (sectionDbVersion != null && sectionDbVersion.PhraseFields != null)
                            {
                                foreach (var phraseFieldDbItem in sectionDbVersion.PhraseFields)
                                {
                                    PhraseField phraseFieldItem =
                                        section.PhraseFields.FirstOrDefault(p => p.FieldId == phraseFieldDbItem.FieldId);
                                    if (phraseFieldItem != null)
                                    {
                                        phraseFieldDbItem.FieldName = phraseFieldItem.FieldName;
                                        phraseFieldDbItem.OrganisationId = sectionRepository.OrganisationId;
                                    }
                                }
                            }
                        }


                        //Update Phrases
                        foreach (var phaseField in section.PhraseFields)
                        {
                            if (phaseField.Phrases != null && sectionDbVersion != null &&
                               sectionDbVersion.PhraseFields != null)
                            {
                                PhraseField phraseFieldDbVersionItem =
                                    sectionDbVersion.PhraseFields.FirstOrDefault(p => p.FieldId == phaseField.FieldId);
                                foreach (var phraseItem in phaseField.Phrases)
                                {
                                    if (phraseFieldDbVersionItem != null)
                                    {
                                        Phrase phraseDbeVersionItem =
                                            phraseFieldDbVersionItem.Phrases.FirstOrDefault(
                                                p => p.PhraseId == phraseItem.PhraseId);
                                        //New Added Phrase
                                        if (phraseItem.PhraseId == 0)
                                        {
                                            phraseItem.OrganisationId = sectionRepository.OrganisationId;
                                            phraseFieldDbVersionItem.Phrases.Add(phraseItem);
                                        }
                                        else
                                        {
                                            //Update 
                                            if (phraseDbeVersionItem != null && !phraseItem.IsDeleted)
                                            {
                                                phraseDbeVersionItem.OrganisationId = sectionRepository.OrganisationId;
                                                phraseDbeVersionItem.Phrase1 = phraseItem.Phrase1;
                                            }
                                            else
                                            {
                                                //Delete Item
                                                missingPhraseListItems.Add(phraseDbeVersionItem);
                                            }
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
                
                foreach (var missingItem in missingPhraseListItems)
                {
                    Phrase phrase = phraseRespository.Find(missingItem.PhraseId);
                    phraseRespository.Delete(phrase);
                }

                sectionRepository.SaveChanges();
            }
        }


        /// <summary>
        /// Get Phrase Fields By Section Id
        /// </summary>
        public IEnumerable<PhraseField> GetPhraseFiledsBySectionId(long sectionId)
        {
            return phraseFieldRepository.GetPhraseFieldsBySectionId(sectionId).ToList();
        }


        #endregion
    }
}
