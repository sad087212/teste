using FIPECAFI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Data;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<Turma> Turmas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alunos__3214EC077C29ED62");

            entity.HasIndex(e => e.CodigoMatricula, "UQ__Alunos__4DF066147FFD2299").IsUnique();

            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Telefone).HasMaxLength(15);

            entity.HasOne(d => d.Curso).WithMany(p => p.Alunos)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alunos__CursoId__6477ECF3");

            entity.HasOne(d => d.Turma).WithMany(p => p.Alunos)
                .HasForeignKey(d => d.TurmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alunos__TurmaId__656C112C");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cursos__3214EC074101FB5C");

            entity.Property(e => e.Descricao).HasMaxLength(100);
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leads__3214EC07E579943C");
            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.Property(e => e.CursoInteresse).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Telefone).HasMaxLength(15);
        }); 

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Turmas__3214EC07FD2A1386");

            entity.Property(e => e.Descricao).HasMaxLength(100);

            entity.HasOne(d => d.Curso).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Turmas__CursoId__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}