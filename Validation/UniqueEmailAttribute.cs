using System.ComponentModel.DataAnnotations;
using FIPECAFI.Data;
using FIPECAFI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Validation;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
        var email = value?.ToString();

        var lead = (Lead)validationContext.ObjectInstance;

        var existingLead = dbContext.Leads
            .AsNoTracking()
            .FirstOrDefault(l => l.Email == email && l.Id != lead.Id); 

        return existingLead == null 
            ? ValidationResult.Success 
            : new ValidationResult("Este e-mail já está cadastrado.");
    }
}