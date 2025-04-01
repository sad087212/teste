using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FIPECAFI.Validation;

namespace FIPECAFI.Models;

public partial class Lead
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "O telefone deve ser válido.")]
    public string Telefone { get; set; } = null!;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
    [UniqueEmail(ErrorMessage = "O e-mail já está cadastrado. ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Você precisa escolher um curso")]
    public string CursoInteresse { get; set; } = null!;
}
