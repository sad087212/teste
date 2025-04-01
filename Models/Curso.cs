using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FIPECAFI.Models;

public partial class Curso
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Descrição é obrigatoria")]
    public string Descricao { get; set; } = null!;

    public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
