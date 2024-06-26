﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Models;

public partial class LibrarycContext : DbContext
{
    public LibrarycContext(DbContextOptions<LibrarycContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autor { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Emprestimo> Emprestimo { get; set; }

    public virtual DbSet<Livro> Livro { get; set; }

    public virtual DbSet<Multa> Multa { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__autor__5FC3872D8D84111E");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F5ADCBD31A");
        });

        modelBuilder.Entity<Emprestimo>(entity =>
        {
            entity.HasKey(e => e.IdEmprestimo).HasName("PK__empresti__45FD187EAB0C3519");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Emprestimo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__emprestim__id_cl__403A8C7D");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Emprestimo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__emprestim__id_li__412EB0B6");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.IdLivro).HasName("PK__livro__C252147D34AEE0DA");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Livro).HasConstraintName("FK__livro__id_autor__3D5E1FD2");
        });

        modelBuilder.Entity<Multa>(entity =>
        {
            entity.HasKey(e => e.IdMulta).HasName("PK__multa__295650BB02D87DB9");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Multa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__multa__id_client__440B1D61");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__4E3E04ADBB2183BB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}