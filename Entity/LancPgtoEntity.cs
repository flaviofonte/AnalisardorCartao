using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnalisardorCartao.Entity
{
    [Table("LANCPGTO")]
    public class LancPgtoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Lancamento")]
        public int Lancamento { get; set; }
        [Column("DATA")]
        public DateTime Data { get; set; }
        [Column("TIPO")]
        public int Tipo { get; set; }
        [Column("VALOR")]
        public double Valor { get; set; }
        [Column("DBCR")]
        public string Dbcr { get; set; }
        [Column("CODIGO")]
        public int Codigo { get; set; }
        [Column("HISTORICO")]
        public string Historico { get; set; }
        [Column("NOTA")]
        public int Nota { get; set; }
        [Column("CUPON")]
        public string Cupom { get; set; }
        [Column("CAIXA")]
        public string Caixa { get; set; }
        [Column("SERIE")]
        public string Serie { get; set; }
        [Column("PagamentoID")]
        public int? PagamentoID { get; set; }
        [Column("DATAPGTO")]
        public DateTime? DataPagamento { get; set; }
    }
}
