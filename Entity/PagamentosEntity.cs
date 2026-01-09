using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("pagamentos")]
    public class PagamentosEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PagamentoID")]
        public int PagamnetoId { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("Cancelado")]
        public DateTime? Cancelado { get; set; }
        [Column("Por")]
        public string Por { get; set; }
        [Column("Quem")]
        public string Quem { get; set; }
        [Column("TipoPagamentoID")]
        public int TipoPagamentoID { get; set; }
        [Column("Valor")]
        public double Valor { get; set; }
    }
}
