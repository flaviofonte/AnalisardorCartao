using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Interface;
using AnalisardorCartao.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AnalisardorCartao.Operacoes
{
    public class Cielo : ILerArquivo
    {
        private string file;

        public Cielo() { }

        public Cielo(string file) { this.file = file; }

        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                int iLinha = 0;
                string[] linhas = File.ReadAllLines(fileName, Encoding.UTF8);
                string[] linha;

                for (int i = 0; i < linhas.Length; i++)
                {
                    if (linhas[i].Contains("Data da venda;"))
                    {
                        iLinha = i;
                        break;
                    }
                }

                dataGridView1.Columns.Clear();
                linha = linhas[iLinha].Split(';');
                foreach (string s in linha)
                {
                    dataGridView1.Columns.Add(s, s);
                }
                iLinha++;
                if (linhas.Length > 0)
                {
                    for (int i = iLinha; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        linha = s.Split(';');
                        if (Funcoes.IsDate(linha[0]))
                        {
                            dataGridView1.Rows.Add(linha);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message, ex);
            }
        }

        public void SalvaDados(DataGridView dataGridView1)
        {
            RedeDao redeDao = new RedeDao();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null || !DateTime.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString().Trim(), out DateTime data))
                {
                    continue;
                }

                string autorizacao = dataGridView1.Rows[i].Cells[13].Value.ToString().Trim();
                if (file.Contains("parceiros"))
                {
                    autorizacao = dataGridView1.Rows[i].Cells[11].Value.ToString().Trim();
                }
                string nsu = "";
                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10, '0'), TipoOperacaoEnum.IGUAL),
                    new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL)
                };

                var rede = redeDao.GetByFilter(filtros);

                if (rede == null)
                {
                    string mod = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                    if (file.Contains("parceiros"))
                    {
                        mod = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                    }
                    if (mod.Length > 25)
                    {
                        mod = mod.Substring(1, 25);
                    }
                    string bandeira = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                    decimal valor;
                    string status = dataGridView1.Rows[i].Cells[8].Value.ToString().Trim();
                    if (file.Contains("parceiros"))
                    {
                        bandeira = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim();
                        valor = decimal.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString().Substring(3).Replace(".",""));
                        status = dataGridView1.Rows[i].Cells[9].Value.ToString().Trim();
                    } else
                    {
                        valor = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString().Replace(".", ""));
                    }
                    rede = new RedeEntity()
                        {
                            Agencia = "",
                            Autorizacao = autorizacao.Trim().PadLeft(10, '0'),
                            Banco = "",
                            Bandeira = bandeira,
                            ContaCorrente = "",
                            DataVenda = data,
                            Modalidade = mod,
                            NSU = nsu,
                            Parcela = 1,
                            Parcelas = 1,
                            ValorBruto = valor,
                            ValorLiquido = valor,
                            Status = status
                        }
                    ;
                    redeDao.Adicionar(rede);
                    Application.DoEvents();
                }
            }
        }
    }
}
