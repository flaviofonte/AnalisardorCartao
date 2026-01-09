using System;

namespace AnalisardorCartao.Objetos
{
    internal class AlinharContasDTO
    {
        public int Lancamento { get; set; }
        public int Tipo { get; set; }
        public string Dbcr { get; set; }
        public int Codigo { get; set; }
        public string Historico { get; set; }
        public string Nota { get; set; }
        public string Cupon { get; set; }
        public string Caixa { get; set; }
        public string Serie { get; set; }
        public int ContaLinearID { get; set; }
        public string Documento { get; set; }
        public string Cliente { get; set; }
        public DateTime DataEmissao { get; set; }
        public double ValorLinear { get; set; }
        public string Nome { get; set; }
        public int CodigoLinear { get; set; }
    }
}
