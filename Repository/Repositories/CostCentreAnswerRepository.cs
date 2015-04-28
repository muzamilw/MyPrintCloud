using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class CostCentreAnswerRepository : BaseRepository<CostCentreAnswer>, ICostCentreAnswerRepository
    {
        public CostCentreAnswerRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<CostCentreAnswer> DbSet
        {
            get {
                return db.CostCentreAnswers;
                
            }
        }

        public IEnumerable<CostCentreAnswer> GetByQuestionId(int QuestionId)
        {
            return DbSet.Where(g => g.QuestionId == QuestionId).ToList();
        }

        public bool DeleteMCQsQuestionAnswerById(int MCQsQuestionAnswerId)
        {
            CostCentreAnswer MCQsQuestionAnswer = db.CostCentreAnswers.Where(g => g.Id == MCQsQuestionAnswerId).SingleOrDefault();
            db.CostCentreAnswers.Remove(MCQsQuestionAnswer);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
