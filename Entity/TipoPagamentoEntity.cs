using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("TipoPagamento")]
    public class TipoPagamentoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoPagamentoID {  get; set; }
        [Column("Descricao")]
        public string Descricao { get; set; }

        [NotMapped]
        public double Soma { get; set; }
    }
}
