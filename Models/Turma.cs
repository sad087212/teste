using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FIPECAFI.Models;

public partial class Turma
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Descrição é nescessaria")]
    public string Descricao { get; set; } = null!;

    [Required(ErrorMessage = "Curso é nescessaria")]
    public int CursoId { get; set; }

    public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

    public virtual Curso Curso { get; set; } = null!;
}
