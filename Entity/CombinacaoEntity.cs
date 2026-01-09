using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("COMBINACAO")]
    public class CombinacaoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CombinacaoID")]
        public int CombinacaoID { get; set; }
        [Column("CodigoLinear")]
        public int CodigoLinear { get; set; }
        [Column("CodigoContas")]
        public int CodigoContas { get; set; }
        [Column("DataCombinacao")]
        public DateTime DataCombinacao { get; set; }
    }
}
