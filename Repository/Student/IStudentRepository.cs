using FIPECAFI.Models;

namespace FIPECAFI.Repository.Student;

public interface IStudentRepository : IRepository<Aluno>
{
    Task<int> GenerateRegistrationAsync();
}