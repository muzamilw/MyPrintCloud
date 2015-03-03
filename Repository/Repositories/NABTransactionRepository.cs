using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace MPC.Repository.Repositories
{
    public class NABTransactionRepository : BaseRepository<NABTransaction>, INABTransactionRepository
    {
        public NABTransactionRepository(IUnityContainer container)
            : base(container)
        {

        }

        protected override IDbSet<NABTransaction> DbSet
        {
            get
            {
                return db.NABTransactions;
            }
        }

        public long NabTransactionSaveRequest(int EstimateId, string Request)
        {
            try
            {


                NABTransaction oTransaction = new NABTransaction();
                oTransaction.EstimateId = EstimateId;
                oTransaction.Request = Request;
                oTransaction.datetime = DateTime.Now;

                db.NABTransactions.Add(oTransaction);

                db.SaveChanges();

                return oTransaction.Id;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public bool NabTransactionUpdateRequest(long TransdactionId, string Response)
        {
            try
            {

                NABTransaction oTransaction = db.NABTransactions.Where(g => g.Id == TransdactionId).SingleOrDefault();

                if (oTransaction != null)
                {
                    oTransaction.Response = Response;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
