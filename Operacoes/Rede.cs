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
    public class Rede : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName);
                string[] linha;

                dataGridView1.Columns.Clear();
                linha = linhas[0].Split(';');
                int l = 1;
                foreach (string s in linha)
                {
                    string cab = $"{l} - " + s;
                    dataGridView1.Columns.Add(cab, cab);
                    l++;
                }

                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        linha = s.Split(';');
                        dataGridView1.Rows.Add(linha);
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
            try
            {
                RedeDao redeDao = new RedeDao();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value == null || !DateTime.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString(), out DateTime data))
                        continue;

                    string nsu = dataGridView1.Rows[i].Cells[17].Value.ToString().Trim();
                    string autorizacao = dataGridView1.Rows[i].Cells[20].Value.ToString().Trim();
                    List<SearchField> filtros = new List<SearchField>()
                    {
                        new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10,'0'), TipoOperacaoEnum.IGUAL),
                        new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL)
                    };

                    RedeEntity rede = redeDao.GetByFilter(filtros);

                    if (!decimal.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(".", ""), out decimal valor))
                    {
                        valor = 0;
                    }

                    if (rede == null)
                    {
                        rede = new RedeEntity()
                        {
                            Agencia = "",
                            Autorizacao = autorizacao.Trim().PadLeft(10, '0'),
                            Banco = "",
                            Bandeira = dataGridView1.Rows[i].Cells[9].Value.ToString(),
                            ContaCorrente = "",
                            DataVenda = data,
                            Modalidade = dataGridView1.Rows[i].Cells[5].Value.ToString(),
                            NSU = nsu,
                            Parcela = 1,
                            Parcelas = 1,
                            ValorBruto = valor,
                            ValorLiquido = valor,
                            Status = dataGridView1.Rows[i].Cells[2].Value.ToString()
                        };
                        redeDao.Adicionar(rede);
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                throw new Exception(string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message, ex);
            }
        }
    }
}
