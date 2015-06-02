using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// State Mapper
    /// </summary>
    public static class StateMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static State CreateFrom(this DomainModels.State source)
        {
            return new State
            {
                StateId = source.StateId,
                StateName = source.StateName,
                StateCode = source.StateCode,
                CountryId = source.CountryId
            };
        }


        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static StateDropDown CreateFromDropDown(this DomainModels.State source)
        {
            return new StateDropDown
            {
                StateId = source.StateId,
                StateName = source.StateName + " ( " + source.StateCode + " )" ,
                CountryId = source.CountryId
            };
        }


    }
}