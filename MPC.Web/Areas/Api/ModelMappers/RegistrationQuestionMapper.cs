using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Registration Question Mapper
    /// </summary>
    public static class RegistrationQuestionMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static RegistrationQuestion CreateFromDropDown(this DomainModels.RegistrationQuestion source)
        {
            return new RegistrationQuestion
            {
                QuestionId = source.QuestionId,
                Question = source.Question,
            };
        }

        #endregion
    }
}