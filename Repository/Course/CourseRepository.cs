using FIPECAFI.Data;
using FIPECAFI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Repository.Course;

public class CourseRepository : ICourseRepository
{
    readonly ApplicationDbContext _context;
    
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Curso>> GetAllAsync(string? name, string? email, int? course)
    {
        var query = _context.Cursos.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            name = name.ToLower();
            query = query.Where(c => c.Descricao.ToLower().Contains(name));
        }

        return await query.ToListAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _context.Cursos.FindAsync(id);
    }

    public async Task CreateAsync(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Curso curso)
    {
        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var curso = _context.Cursos.Find(id);

        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }
    }
}