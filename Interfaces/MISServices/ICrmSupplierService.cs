using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICrmSupplierService
    {
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
    }
}
