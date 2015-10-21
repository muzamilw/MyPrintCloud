using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Registration Question Repository Interface
    /// </summary>
    public interface IRegistrationQuestionRepository : IBaseRepository<RegistrationQuestion, long>
    {
        RegistrationQuestion GetSecretQuestionByID(int QuestionID);
    }
}
