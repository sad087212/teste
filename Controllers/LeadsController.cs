using System.Diagnostics;
using FIPECAFI.Data;
using FIPECAFI.Models;
using FIPECAFI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Controllers;

public class LeadsController : Controller
{
    readonly ApplicationDbContext _context;
    readonly ILeadsRepository _repository;

    public LeadsController(ApplicationDbContext context, ILeadsRepository repository)
    {
        _context = context;
        _repository = repository;
    }
    
    public async Task<IActionResult> Index(string name, string email, string course)
    {
        var cursos = _context.Cursos.ToList();
        var leads = await _repository.GetAllLeadsAsync(name, email, course);
        
        ViewBag.Cursos = cursos;
        return View(leads);
    }
    
  

    public IActionResult Create()
    {
        
        ViewBag.Cursos = GetCursos();
        return View();
    }

    [HttpPost]
    public IActionResult Store(Lead lead)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cursos = GetCursos();
            return View("Create", lead); 
        }

        try
        {
            _repository.Create(lead);
            TempData["SuccessMessage"] = "Lead Criado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Erro ao criar lead!";
            throw;
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var lead = await _repository.GetByIdAsync(id);
        if (lead == null)
        {
            return NotFound();
        }
        
        ViewBag.Cursos = GetCursos();
        return View(lead);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Lead lead)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.cursos = GetCursos();
            return View("Edit", lead);
        }

        try
        {
            await _repository.UpdateAsync(lead);
            TempData["SuccessMessage"] = "Lead Alterado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["SuccessMessage"] = "Erro ao Altera o Lead!";
            throw;
        }
        
       
    }
    
    [HttpPost]
    [Route("Leads/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var lead = await _repository.GetByIdAsync(id);
        if (lead == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        TempData["SuccessMessage"] = "Lead excluído com sucesso!";
        return RedirectToAction(nameof(Index));
    }


    public List<Curso> GetCursos()
    {
        return _context.Cursos.ToList();
    }
    
    public async Task<IActionResult> Filter(string name, string email, string course)
    {
        var leads = await _repository.GetAllLeadsAsync(name, email, course);
        return PartialView("_LeadsTable", leads);
    }

}