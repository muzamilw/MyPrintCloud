using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class PaperSheetService : IPaperSheetService
    {
        private readonly IPaperSheetRepository paperSheetRepository;

        public PaperSheetService(IPaperSheetRepository paperSheetRepository)
        {
            this.paperSheetRepository = paperSheetRepository;
        }

        public IEnumerable<PaperSize> GetAll()
        {
            return paperSheetRepository.GetAll();
        }

        public PaperSize Add(PaperSize paperSize)
        {
            paperSheetRepository.Add(paperSize);
            return paperSize;
        }

        public PaperSize Update(PaperSize paperSize)
        {
            paperSheetRepository.Update(paperSize);
            return null;
        }

        public bool Delete(int paperSheetId)
        {

            paperSheetRepository.Delete(GetPaperSheetById(paperSheetId));
            return true;
        }

        public PaperSize GetPaperSheetById(int id)
        {
            return paperSheetRepository.Find(id);
        }
    }
}
