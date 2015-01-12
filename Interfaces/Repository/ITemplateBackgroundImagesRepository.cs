using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateBackgroundImagesRepository : IBaseRepository<TemplateBackgroundImage, int>
    {
        void DeleteTemplateBackgroundImages(long productID);
    }
}
