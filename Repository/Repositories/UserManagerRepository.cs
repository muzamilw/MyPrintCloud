using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Linq;

namespace MPC.Repository.Repositories
{
    public class UserManagerRepository : BaseRepository<SystemUser>, IUserManagerRepository
    {
        public UserManagerRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SystemUser> DbSet
        {
            get
            {
                return db.SystemUsers;
            }
        }
        public SystemUser GetSalesManagerDataByID(int ManagerId)
        {
          
            SystemUser rec = db.SystemUsers.Where(u => u.SystemUserId == ManagerId).FirstOrDefault();
            return rec;
        }

        public string GetMarketingRoleIDByName()
        {
             //var rec = db.roles.Where(i => i.RoleName.Contains("Marketing") || i.RoleName.Contains("marketing")).FirstOrDefault();
             //if (rec != null)
             //{
             //     var Email = db.SystemUsers.Where(g => g.RoleId == rec.RoleId).Select(j => j.Email).FirstOrDefault();
             //     return Email;
             //}
             //else
             //{
                  return string.Empty;
            // }
           
        }
    }
}
