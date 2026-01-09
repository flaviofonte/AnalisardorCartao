using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalisardorCartao.Entity
{
    [Table("Clientes")]
    public class ClientesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Codigo")]
        public int Codigo {  get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
    }
}
