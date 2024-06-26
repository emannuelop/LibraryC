﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Models;

[Table("multa")]
public partial class Multa
{
    [Key]
    [Column("id_multa")]
    public int IdMulta { get; set; }

    [Column("data")]
    public DateOnly? Data { get; set; }

    [Required]
    [Column("motivo")]
    [StringLength(255)]
    [Unicode(false)]
    public string Motivo { get; set; }

    [Column("valor", TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }

    [Required]
    [Column("status")]
    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Multa")]
    public virtual Cliente IdClienteNavigation { get; set; }
}