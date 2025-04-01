using FIPECAFI.Data;
using FIPECAFI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Repository.Student;

public class StudentRepository: IStudentRepository
{

    readonly ApplicationDbContext _context;
    
    public StudentRepository( ApplicationDbContext context )
    {
        _context = context;
    }


    public async Task<List<Aluno>> GetAllAsync(string? name, string? email, int? courseId)
    {
        var query = _context.Alunos
            .Include(a => a.Curso)
            .Include(a => a.Turma)
            .AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            name = name.ToLower();
            query = query.Where(a => a.Nome.ToLower().Contains(name));
        }

        if (!string.IsNullOrEmpty(email))
        {
            email = email.ToLower();
            query = query.Where(a => a.Email.ToLower().Contains(email));
        }

        if (courseId.HasValue)
        {
            query = query.Where(a => a.CursoId == courseId);
        }

        return await query.ToListAsync();
    }


    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _context.Alunos.FindAsync(id);
    }

    public async Task CreateAsync(Aluno aluno)
    {
        aluno.CodigoMatricula = await GenerateRegistrationAsync();
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno != null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GenerateRegistrationAsync()
    {
        var currentYear = DateTime.Now.Year;

        var lastCode = await _context.Alunos
            .Where(a => a.CodigoMatricula.ToString().StartsWith(currentYear.ToString()))
            .OrderByDescending(a => a.CodigoMatricula)
            .Select(a => a.CodigoMatricula)
            .FirstOrDefaultAsync();

        int sequence = lastCode == 0 ? 1 : int.Parse(lastCode.ToString().Substring(4)) + 1;

        return int.Parse($"{currentYear}{sequence.ToString("D6")}");
    }

}