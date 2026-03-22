using AnalisardorCartao.Operacoes;
using System;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class VerificaPrecoCusto : Form
    {
        public VerificaPrecoCusto()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                LerArquivos lerArquivos = new LerArquivos(new Precos());

                lerArquivos.LerArquivoCartao(TxtArquivo.Text, ref dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void BtnLocalizarArquivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Relatório Linear";
            openFileDialog1.Filter = "Arquivos CSV|*.csv";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                TxtArquivo.Text = openFileDialog1.FileName;
            }
        }
    }
}
