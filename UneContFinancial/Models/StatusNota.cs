using System.ComponentModel;

namespace UneContFinancial.Models;

public enum StatusNota
{
    [Description("Emitida")]
    Emitida = 1,
    [Description("Cobrança Realizada")]
    CobrancaRealizada = 2,
    [Description("Pagamento em Atraso")]
    PagamentoEmAtraso = 3,
    [Description("Pagamento Realizado")]
    PagamentoRealizado = 4
}