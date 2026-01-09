using System.Windows.Forms;

namespace AnalisardorCartao.Interface
{
    public interface ILerArquivo
    {
        void LerArquivo(string fileName, ref DataGridView dataGridView1);
        void SalvaDados(DataGridView dataGridView1);
    }
}
