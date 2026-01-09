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
    public partial class Linear : Form
    {
        public Linear()
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

                dataGridView1.Columns.Add("Data Venda", "Data Venda");
                dataGridView1.Columns.Add("NSU", "NSU");
                dataGridView1.Columns.Add("Bandeira", "Bandeira");
                dataGridView1.Columns.Add("Autorizadora", "Autorizadora");
                dataGridView1.Columns.Add("Modalidade", "Modalidade");
                dataGridView1.Columns.Add("Autorização", "Autorização");
                dataGridView1.Columns.Add("Cod. Estab.", "Cod. Estab.");
                dataGridView1.Columns.Add("Valor Venda", "Valor Venda");

                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        if (!s.Substring(0, 12).Contains("Filial: 001"))
                        {
                            continue;
                        }
                        linha = s.Replace("\",\"", "|").Split('|');
                        string[] dados = new string[8];
                        dados[0] = linha[1].Split(':')[1];
                        dados[1] = linha[25];
                        dados[2] = linha[26];
                        if (String.IsNullOrEmpty(dados[2]))
                        {
                            dados[2] = linha[8].Split('-')[2].Trim();
                        }
                        dados[3] = linha[27];
                        if (String.IsNullOrEmpty(dados[3]))
                        {
                            dados[3] = linha[8].Split('-')[3].Trim();
                        }
                        dados[4] = linha[8].Split('-')[2].Trim();
                        dados[5] = linha[28];
                        dados[6] = linha[29];
                        dados[7] = linha[30];

                        dataGridView1.Rows.Add(dados);
                    }
                }
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
            LinearDao linearDao = new LinearDao();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;

                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("DataVenda", DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), TipoOperacaoEnum.IGUAL),
                    new SearchField("Autorizacao", dataGridView1.Rows[i].Cells[5].Value.ToString().Trim(), TipoOperacaoEnum.IGUAL)
                };

                LinearEntity linear = linearDao.GetByFilter(filtros);

                if (linear == null)
                {
                    linear = new LinearEntity()
                    {
                        Agencia = "",
                        Autorizacao = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim().PadLeft(10, '0'),
                        Banco = "",
                        Bandeira = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim(),
                        ContaCorrente = "",
                        DataVenda = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()),
                        Modalidade = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim(),
                        NSU = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(),
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString().Replace(".", "")),
                        ValorLiquido = double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString().Replace(".", ""))
                    };
                    linearDao.Adicionar(linear);
                    Application.DoEvents();
                }
            }
            DesBloquearBotoes();
            MessageBox.Show(this, "Importação dos dados do linear teminada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}