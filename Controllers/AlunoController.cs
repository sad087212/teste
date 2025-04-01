using System.Diagnostics;
using FIPECAFI.Data;
using FIPECAFI.Models;
using FIPECAFI.Repository.Student;
using Microsoft.AspNetCore.Mvc;

namespace FIPECAFI.Controllers;

public class AlunoController : Controller
{
    
    readonly IStudentRepository _repository;
    readonly ApplicationDbContext _context;

    public AlunoController(IStudentRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }
    public async Task<IActionResult> Index(string? name, string? email, int? courseId)
    {
        var alunos = await _repository.GetAllAsync(name, email, courseId);
        ViewBag.Cursos = _context.Cursos.ToList();
        return View(alunos);
    }

    [HttpGet("Create/{leadId}")]
    public IActionResult Create(int leadId)
    {
        var lead = _context.Leads.Find(leadId);
        if (lead is null) return RedirectToAction("Index", "Leads");

        var curso = _context.Cursos.FirstOrDefault(c => c.Descricao == lead.CursoInteresse);
        if (curso == null) return RedirectToAction("Index", "Leads");

        var aluno = _context.Alunos.FirstOrDefault(a => a.Email == lead.Email);
        if (aluno == null)
        {
            aluno = new Aluno()
            {
                Nome = lead.Nome,
                Email = lead.Email,
                Telefone = lead.Telefone,
                CursoId = curso.Id
            };
        }

        var turmaDoAluno = _context.Turmas
            .FirstOrDefault(t => t.Alunos.Any(a => a.Email == aluno.Email));

        if (turmaDoAluno != null)
        {
            ViewBag.Turmas = new List<Turma> { turmaDoAluno };
        }
        else
        {
            var turmasDisponiveis = _context.Turmas
                .Where(t => t.CursoId == curso.Id)
                .ToList();

            ViewBag.Turmas = turmasDisponiveis;
        }

        ViewBag.Cursos = _context.Cursos.ToList();
        return View(aluno);
    }


    [HttpPost]
    public async Task<IActionResult> Store(Aluno aluno)
    {
        ModelState.Remove("Curso");
        ModelState.Remove("Turma");

        if (!ModelState.IsValid)
        {
            ViewBag.Cursos = _context.Cursos.ToList();
            ViewBag.Turmas = _context.Turmas.ToList();
            return View("Create", aluno);
        }

        var turma = _context.Turmas
            .FirstOrDefault(t => t.Alunos.Any(a => a.Email == aluno.Email));

        if (turma != null)
        {
            TempData["ErrorMessage"] = $"O aluno já está matriculado na turma {turma.Descricao}.";
            return RedirectToAction("Index", "Leads");
        }

        try
        {
            await _repository.CreateAsync(aluno);
            TempData["SuccessMessage"] = "Aluno matriculado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao salvar os dados: {ex.Message}");
            ViewBag.Cursos = _context.Cursos.ToList();
            ViewBag.Turmas = _context.Turmas.ToList();
            return View("Create", aluno);
        }
    }

    
    
    [HttpGet("Aluno/FilterStudent")]
    public async Task<IActionResult> FilterStudent(string name, string email, string course)
    {
        int? courseId = null;

        if (!string.IsNullOrEmpty(course))
        {
            courseId = _context.Cursos.FirstOrDefault(c => c.Descricao == course)?.Id;
        }

        var students = await _repository.GetAllAsync(name, email, courseId);
        return PartialView("_StudantTable", students);
    }


    
    
}