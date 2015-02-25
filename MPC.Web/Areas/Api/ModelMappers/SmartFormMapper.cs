using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Smart Form Mapper
    /// </summary>
    public static class SmartFormMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.SmartForm CreateFrom(this SmartForm source)
        {
            return new DomainModels.SmartForm
            {
                SmartFormId = source.SmartFormId,
                CompanyId = source.CompanyId,
                Heading = source.Heading,
                Name = source.Name,
                SmartFormDetails = source.SmartFormDetails != null ? source.SmartFormDetails.Select(smd => smd.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SmartForm CreateFrom(this DomainModels.SmartForm source)
        {
            return new SmartForm
            {
            };
        }

        #endregion
    }
}