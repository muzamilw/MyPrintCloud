using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateVariableRepository : IBaseRepository<TemplateVariable, long>
    {
        List<TemplateVariable> getVariablesList(long templateID);
        void InsertTemplateVariables(List<TemplateVariable> lstTemplateVariables,List<TemplateVariableExtension> lstVariableExtensions);
    }
}
