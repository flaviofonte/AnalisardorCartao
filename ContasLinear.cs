using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class ContasLinear : Form
    {
        public ContasLinear()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            openFileDialog1.Title = "Arquivo csv";
            openFileDialog1.Filter = "Arquivo compactado|*.csv";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TxtArquivo.Text = openFileDialog1.FileName;
            }
        }

        private void LerArquivos(string fileName)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName, Encoding.Default);
                string[] linha;

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                dataGridView1.Columns.Add("Documento", "Documento");
                dataGridView1.Columns.Add("Cliente", "Cliente");
                dataGridView1.Columns.Add("Emissão", "Emissão");
                dataGridView1.Columns.Add("Valor Venda", "Valor Venda");

                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        if (!s.Contains("Filial"))
                        {
                            continue;
                        }
                        linha = s.Split(',');
                        string[] dados = new string[4];
                        dados[0] = linha[16].Replace("\"", "");
                        dados[1] = linha[19].Replace("\"", "");
                        dados[2] = linha[21].Replace("\"", "");
                        dados[3] = linha[25].Replace("\"", "") + "," + linha[26].Replace("\"", "");
                        dataGridView1.Rows.Add(dados);
                    }
                }
                dataGridView1.Columns[1].Width = 500;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Houve um erro ao ler os dados do arquivo, verifique se é o arquivo certo da linear e tente novamente.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BloquearBotoes()
        {
            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void DesBloquearBotoes()
        {
            this.Cursor = Cursors.Default;
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            BloquearBotoes();
            string sfile = TxtArquivo.Text;
            LerArquivos(sfile);
            DesBloquearBotoes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            BloquearBotoes();
            ContasLinearDao contasLinearDao = new ContasLinearDao();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;

                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("Documento", dataGridView1.Rows[i].Cells[0].Value.ToString(), TipoOperacaoEnum.IGUAL),
                    new SearchField("DataEmissao", DateTime.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()), TipoOperacaoEnum.IGUAL),
                };

                ContasLinearEntity linear = contasLinearDao.GetByFilter(filtros);

                if (linear == null)
                {
                    ContasLinearEntity linearEntity = new ContasLinearEntity
                    {
                        Cliente = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                        DataEmissao = DateTime.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                        Documento = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        Valor = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString())
                    };
                    contasLinearDao.Adicionar(linearEntity);
                    Application.DoEvents();
                }
            }
            DesBloquearBotoes();
            MessageBox.Show(this, "Importação dos dados do linear teminada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}