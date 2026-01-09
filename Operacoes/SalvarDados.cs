using System.Windows.Forms;
using AnalisardorCartao.Interface;

namespace AnalisardorCartao.Operacoes
{
    public class SalvarDados
    {

        private ILerArquivo _salvarDados;

        public SalvarDados(ILerArquivo salvarDados)
        {
            _salvarDados = salvarDados;
        }

        public void DefineSalvador(ILerArquivo salvarDados)
        {
            _salvarDados = salvarDados;
        }

        public void SalvaDadosBase(DataGridView dataGridView1)
        {
            _salvarDados.SalvaDados(dataGridView1);
        }
    }
}
