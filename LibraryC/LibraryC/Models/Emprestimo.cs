﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Models;

[Table("emprestimo")]
public partial class Emprestimo
{
    [Key]
    [Column("id_emprestimo")]
    public int IdEmprestimo { get; set; }

    [Column("data_emprestimo")]
    public DateOnly? DataEmprestimo { get; set; }

    [Column("data_prevista_devolucao")]
    public DateOnly? DataPrevistaDevolucao { get; set; }

    [Column("data_devolucao")]
    public DateOnly? DataDevolucao { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_livro")]
    public int IdLivro { get; set; }

    [Required]
    [Column("status")]
    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Emprestimo")]
    public virtual Cliente IdClienteNavigation { get; set; }

    [ForeignKey("IdLivro")]
    [InverseProperty("Emprestimo")]
    public virtual Livro IdLivroNavigation { get; set; }
}