using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("Rede")]
    public class RedeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Codigo")]
        public virtual int Codigo { get; set; }
        [Column("DataVenda")]
        public virtual DateTime DataVenda { get; set; }
        [Column("ValorBruto")]
        public virtual decimal? ValorBruto { get; set; }
        [Column("ValorLiquido")]
        public virtual decimal? ValorLiquido { get; set; }
        [Column("NSU")]
        public virtual string NSU { get; set; }
        [Column("Autorizacao")]
        public virtual string Autorizacao { get; set; }
        [Column("Modalidade")]
        public virtual string Modalidade { get; set; }
        [Column("Bandeira")]
        public virtual string Bandeira { get; set; }
        [Column("Parcelas")]
        public virtual int Parcelas { get; set; }
        [Column("Parcela")]
        public virtual int Parcela { get; set; }
        [Column("Banco")]
        public virtual string Banco { get; set; }
        [Column("Agencia")]
        public virtual string Agencia { get; set; }
        [Column("ContaCorrente")]
        public virtual string ContaCorrente { get; set; }
        [Column("Status")]
        public virtual string Status { get; set; }
    }
}
