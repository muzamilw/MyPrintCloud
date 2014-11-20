using System.Collections.Generic;
using MPC.Interfaces.IServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.Services
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
            paperSheetRepository.SaveChanges();
            return paperSize;
        }

        public PaperSize Update(PaperSize paperSize)
        {
            paperSheetRepository.Update(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }

        public bool Delete(int paperSheetId)
        {

            paperSheetRepository.Delete(GetPaperSheetById(paperSheetId));
            paperSheetRepository.SaveChanges();
            return true;
        }

        public PaperSize GetPaperSheetById(int id)
        {
            return paperSheetRepository.Find(id);
        }
    }
}
