using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    public class AlinhadosEntity
    {
        [Column("dataordem")]
        public DateTime DataOrdem { get; set; }
        [Column("valorordem")]
        public decimal ValorOrdem { get; set; }
        [Column("datavendalinear")]
        public DateTime? DataVendaLinear { get; set; }
        [Column("valorlinear")]
        public decimal? ValorLinear { get; set; }
        [Column("nsulinear")]
        public string NSU { get; set; }
        [Column("NSU")]
        public string NSUREDE { get; set; }
        [Column("bandeiralinear")]
        public string BandeiraLinear { get; set; }
        [Column("DataVenda")]
        public DateTime? DataVendaRede { get; set; }
        [Column("ValorBruto")]
        public decimal? ValorBrutoRede { get; set; }
        [Column("Autorizacao")]
        public string AutorizacaoRede { get; set; }
        [Column("Modalidade")]
        public string ModalidadeRede { get; set; }
        [Column("Bandeira")]
        public string BandeiraRede { get; set; }
        [Column("diferenca")]
        public decimal? DiferencaRede { get; set; }
        [Column("modalidadelinear")]
        public string ModalidadeLinear { get; set; }
        [Column("autorizacaolinear")]
        public string AutorizacaoLinear { get; set; }
        [Column("Status")]
        public string Status { get; set; }
    }
}
