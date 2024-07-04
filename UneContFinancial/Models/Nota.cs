using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UneContFinancial.Models;

public class Nota
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string NomePagador { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string NumeroIdentificacao { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? DataEmissaoNota { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? DataCobranca { get; set; } = null;

    [DataType(DataType.Date)]
    public DateTime? DataPagamento { get; set; } = null;

    [Required]
    [Precision(14, 2)]
    public decimal Valor { get; set; }

    [Required]
    public string NotaFiscal { get; set; } = string.Empty;

    [Required]
    public string BoletoBancario { get; set; } = string.Empty;

    [Required]
    public StatusNota Status { get; set; }
}