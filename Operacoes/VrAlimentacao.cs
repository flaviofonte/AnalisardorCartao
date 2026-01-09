using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Interface;
using AnalisardorCartao.Repository;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AnalisardorCartao.Operacoes
{
    public class VrAlimentacao : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                dataGridView1.Columns.Clear();
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(5) != null)
                                {
                                    if (reader.GetValue(5).GetType() == typeof(string))
                                    {
                                        string cabecalho = reader.GetValue(6).ToString().Trim();
                                        if (cabecalho.Equals("Valor"))
                                        {
                                            dataGridView1.Columns.Add(reader.GetValue(2).ToString().Trim(), reader.GetValue(2).ToString());
                                            dataGridView1.Columns.Add(reader.GetValue(1).ToString().Trim(), reader.GetValue(1).ToString());
                                            dataGridView1.Columns.Add(reader.GetValue(6).ToString().Trim(), reader.GetValue(6).ToString());
                                            dataGridView1.Columns.Add(reader.GetValue(5).ToString().Trim(), reader.GetValue(5).ToString());
                                        }
                                    }

                                    if (reader.GetValue(2) != null && reader.GetValue(6).GetType() == typeof(double))
                                    {
                                        string[] linha = new string[]
                                        {
                                            reader.GetValue(2).ToString(),
                                            reader.GetValue(1).ToString(),
                                            reader.GetValue(6).ToString(),
                                            reader.GetValue(5).ToString()
                                        };
                                        dataGridView1.Rows.Add(linha);
                                    }
                                }
                            }
                        } while (reader.NextResult());
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
                if (!DateTime.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString().Trim(), out DateTime data))
                {
                    continue;
                }

                string autorizacao = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                string nsu = "";
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
                        Bandeira = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(),
                        ContaCorrente = "",
                        DataVenda = data,
                        Modalidade = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(),
                        NSU = nsu,
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()),
                        ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()),
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
