using FIPECAFI.Data;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Models.Seeds;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<ApplicationDbContext>>()))
        {
            // Seed Cursos
            if (!context.Cursos.Any())
            {
                context.Cursos.AddRange(
                    new Curso { Descricao = "Graduação em Ciências Contábeis" },
                    new Curso { Descricao = "Pós-Graduação em Controladoria" },
                    new Curso { Descricao = "Pós-Graduação em Finanças" },
                    new Curso { Descricao = "Pós-Graduação em Gestão de Negócios" },
                    new Curso { Descricao = "MBA em Controladoria e Finanças" },
                    new Curso { Descricao = "MBA em Gestão Tributária" },
                    new Curso { Descricao = "MBA em Mercado de Capitais" },
                    new Curso { Descricao = "Mestrado Profissional em Controladoria e Finanças" },
                    new Curso { Descricao = "Curso de Extensão - Temas Específicos em Contabilidade" }
                );
                context.SaveChanges();
            }

            // Seed Turmas
            if (!context.Turmas.Any())
            {
                var cursos = context.Cursos.ToList();
                var random = new Random();

                foreach (var curso in cursos)
                {
                    // Gera um número aleatório de turmas entre 2 e 5
                    var numTurmas = random.Next(2, 6);
                    var turmas = new List<Turma>();

                    for (int i = 1; i <= numTurmas; i++)
                    {
                        turmas.Add(new Turma
                        {
                            Descricao = $"Turma {i} - {curso.Descricao} - 2024",
                            CursoId = curso.Id
                        });
                    }

                    context.Turmas.AddRange(turmas);
                }

                context.SaveChanges();
            }
        }
    }
}
