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
    public class TicketReembolso : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName, Encoding.UTF8);
                string[] linha;
                int inicio = 0;
                dataGridView1.Columns.Clear();
                for (int i = 0; i < linhas.Length; i++)
                {
                    linha = linhas[i].Split(';');
                    if (!string.IsNullOrEmpty(linha[0]) && linha[0].Contains("mero"))
                    {
                        dataGridView1.Columns.Add(linha[14], linha[14]);
                        dataGridView1.Columns.Add(linha[18], linha[18]);
                        dataGridView1.Columns.Add(linha[23], linha[23]);
                        dataGridView1.Columns.Add(linha[30], linha[30]);
                        inicio = i;
                        break;
                    }
                }

                if (linhas.Length > 0)
                {
                    inicio++;
                    for (int i = inicio; i < linhas.Length; i++)
                    {
                        string s = linhas[i];
                        linha = s.Split(';');
                        if (DateTime.TryParse(linha[14], out DateTime _))
                        {
                            string[] registro = new string[4];
                            registro[0] = linha[14];
                            registro[1] = linha[18];
                            registro[2] = linha[23];
                            registro[3] = linha[30];
                            dataGridView1.Rows.Add(registro);
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
                DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string nsu = "";
                string autorizacao = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().PadLeft(6,'0');
                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10,'0'), TipoOperacaoEnum.IGUAL),
                    new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL)
                };

                RedeEntity rede = redeDao.GetByFilter(filtros);

                if (rede == null)
                {
                    rede = new RedeEntity()
                    {
                        Agencia = "",
                        Autorizacao = autorizacao.Trim().PadLeft(10, '0'),
                        Banco = "",
                        Bandeira = "Ticket",
                        ContaCorrente = "",
                        DataVenda = data,
                        Modalidade = dataGridView1.Rows[i].Cells[2].Value.ToString(),
                        NSU = nsu,
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(3)),
                        ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(3)),
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
