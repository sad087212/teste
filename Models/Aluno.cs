using System;
using System.ComponentModel.DataAnnotations;
using FIPECAFI.Validation;

namespace FIPECAFI.Models;

public partial class Aluno
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O código de matrícula é obrigatório.")]
    public int CodigoMatricula { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "O telefone deve ser válido.")]
    public string Telefone { get; set; } = null!;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "O curso é obrigatório.")]
    public int CursoId { get; set; }

    [Required(ErrorMessage = "A turma é obrigatória.")]
    public int TurmaId { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public virtual Curso? Curso { get; set; }

    public virtual Turma? Turma { get; set; }
}