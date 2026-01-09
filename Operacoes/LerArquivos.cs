using System.Windows.Forms;
using AnalisardorCartao.Interface;


namespace AnalisardorCartao.Operacoes
{
    public class LerArquivos
    {

        private ILerArquivo _lerArquivo;

        public LerArquivos(ILerArquivo lerArquivo)
        {
            _lerArquivo = lerArquivo;
        }

        public void DefineLeitor(ILerArquivo lerArquivo)
        {
            _lerArquivo = lerArquivo;
        }

        public void LerArquivoCartao(string fileName, ref DataGridView dataGridView1)
        {
            _lerArquivo.LerArquivo(fileName, ref dataGridView1);
        }
    }
}
