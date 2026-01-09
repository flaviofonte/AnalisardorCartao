using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("ContasLinear")]
    public class ContasLinearEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ContaLinearID")]
        public int ContaLinearID { get; set; }
        [Column("Documento")]
        public string Documento {  get; set; }
        [Column("Cliente")]
        public string Cliente { get; set; }
        [Column("DataEmissao")]
        public DateTime DataEmissao { get; set; }
        [Column("Valor")]
        public double Valor {  get; set; }
    }
}
