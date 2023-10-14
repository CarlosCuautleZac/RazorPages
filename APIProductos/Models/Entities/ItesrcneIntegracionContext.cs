using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIProductos.Models.Entities;

public partial class ItesrcneIntegracionContext : DbContext
{
    public ItesrcneIntegracionContext()
    {
    }

    public ItesrcneIntegracionContext(DbContextOptions<ItesrcneIntegracionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorias> Categorias { get; set; }

    public virtual DbSet<Encuesta> Encuesta { get; set; }

    public virtual DbSet<Listacompras> Listacompras { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Productos2> Productos2 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_integracion;user=itesrcne_integra;password=integra1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Encuesta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("encuesta");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Bueno).HasColumnType("int(11)");
            entity.Property(e => e.Criterio).HasMaxLength(45);
            entity.Property(e => e.Excelente).HasColumnType("int(11)");
            entity.Property(e => e.Malo).HasColumnType("int(11)");
            entity.Property(e => e.MuyMalo)
                .HasColumnType("int(11)")
                .HasColumnName("Muy malo");
            entity.Property(e => e.Pregunta).HasMaxLength(500);
            entity.Property(e => e.Regular).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Listacompras>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("listacompras");

            entity.HasIndex(e => e.IdProducto, "fkprolista_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Cantidad).HasColumnType("int(11)");
            entity.Property(e => e.IdProducto).HasColumnType("int(11)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Listacompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkprolista");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Imagen).HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("double(10,2)");
        });

        modelBuilder.Entity<Productos2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productos2");

            entity.HasIndex(e => e.IdCategoria, "fkcatprod_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("double(12,2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos2)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkcatprod");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
