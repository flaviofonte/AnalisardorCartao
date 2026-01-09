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
    public class Alelo : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                dataGridView1.Columns.Clear();
                int tipo = 0;
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null)
                                {
                                    if (reader.GetValue(0).GetType() == typeof(string) && reader.GetString(0).Equals("Nome"))
                                    {
                                        dataGridView1.Columns.Add(reader.GetValue(4).ToString().Trim(), "1 - " + reader.GetValue(4).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(8).ToString().Trim(), "2 - " + reader.GetValue(8).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(6).ToString().Trim(), "3 - " + reader.GetValue(6).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(3).ToString().Trim(), "4 - " + reader.GetValue(3).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(9).ToString().Trim(), "5 - " + reader.GetValue(9).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(10).ToString().Trim(), "6 - " + reader.GetValue(10).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(11).ToString().Trim(), "7 - " + reader.GetValue(11).ToString());
                                    }

                                    if (reader.GetValue(0).GetType() == typeof(string) && reader.GetString(0).Equals("CNPJ"))
                                    {
                                        tipo = 1;
                                        dataGridView1.Columns.Add(reader.GetValue(5).ToString().Trim(), "1 - " + reader.GetValue(5).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(8).ToString().Trim(), "2 - " + reader.GetValue(8).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(6).ToString().Trim(), "3 - " + reader.GetValue(6).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(3).ToString().Trim(), "4 - " + reader.GetValue(3).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(9).ToString().Trim(), "5 - " + reader.GetValue(9).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(10).ToString().Trim(), "6 - " + reader.GetValue(10).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(11).ToString().Trim(), "7 - " + reader.GetValue(11).ToString());
                                    }

                                    if (reader.FieldCount >= 10 && reader.GetValue(10) != null && reader.GetValue(10).GetType() == typeof(double))
                                    {
                                        string[] linha = new string[]
                                        {
                                            tipo == 0 ? reader.GetValue(4).ToString() : reader.GetValue(5).ToString().Substring(0, 10),
                                            reader.GetValue(8).ToString(),
                                            reader.GetValue(6).ToString(),
                                            reader.GetValue(3).ToString(),
                                            reader.GetValue(9).ToString(),
                                            reader.GetValue(10).ToString(),
                                            reader.GetValue(11).ToString()
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
                if (!DateTime.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Substring(0, 10), out DateTime data))
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
                        Modalidade = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim(),
                        NSU = nsu,
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                        ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()),
                        Status = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim()
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
