using System;

namespace AnalisardorCartao
{
    public class DadosAlinhados
    {
        public DateTime? DataVendaLinear { get; set; }
        public string NSULinear { get; set; }
        public string AutorizacaoLinear { get; set; }
        public string BandeiraLinear { get; set; }
        public string ModalidadeLinear { get; set; }
        public decimal? ValorVendaLinear { get; set; }
        public DateTime? DataVendaRede { get; set; }
        public decimal? ValorBrutoRede { get; set; }
        public decimal? Diferenca { get; set; }
        public decimal? ValorLiquido { get; set; }
        public string AutorizacaoRede { get; set; }
        public string NSUrede { get; set; }
        public string BandeiraRede { get; set; }
        public string ModalidadeRede { get; set; }
        public string Prazo { get; set; }
        public DateTime DataOrdem { get; set; }
        public string Status { get; set; }
    }
}
