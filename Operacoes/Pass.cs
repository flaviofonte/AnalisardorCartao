using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Interface;
using AnalisardorCartao.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AnalisardorCartao.Operacoes
{
    public class Pass : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName);
                string[] linha;

                dataGridView1.Columns.Clear();
                linha = linhas[0].Split(';');
                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        if (s.StartsWith("CNPJ"))
                        {
                            linha = s.Split(';');
                            foreach (string str in linha)
                            {
                                dataGridView1.Columns.Add(str, str);
                            }
                        }
                    }
                }

                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        if (s.Contains("17.765"))
                        {
                            linha = s.Split(';');
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
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;

                DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                string autorizacao = dataGridView1.Rows[i].Cells[7].Value.ToString().Trim();
                string nsu = "";
                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL),
                    new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10,'0'), TipoOperacaoEnum.IGUAL)
                };

                RedeEntity rede = redeDao.GetByFilter(filtros);

                if (rede == null)
                {
                    rede = new RedeEntity()
                    {
                        Agencia = "",
                        Autorizacao = autorizacao.Trim().PadLeft(10, '0'),
                        Banco = "",
                        Bandeira = dataGridView1.Rows[i].Cells[4].Value.ToString(),
                        ContaCorrente = "",
                        DataVenda = data,
                        Modalidade = dataGridView1.Rows[i].Cells[5].Value.ToString(),
                        NSU = nsu,
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString().Substring(3)),
                        ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString().Substring(3)),
                        Status = "Aprovada"
                    };
                    redeDao.Adicionar(rede);
                    Application.DoEvents();
                }
            }
        }

        public void SalvaDados(DataGridView dataGridView1, string file)
        {
            throw new NotImplementedException();
        }
    }
}
