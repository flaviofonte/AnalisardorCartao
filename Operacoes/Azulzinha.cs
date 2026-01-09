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
    internal class Azulzinha : ILerArquivo
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
                                if (reader.GetValue(0) != null)
                                {
                                    if (reader.GetValue(0).GetType() == typeof(string) && reader.GetString(0).Equals("Data da venda"))
                                    {
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            dataGridView1.Columns.Add(reader.GetValue(i).ToString().Trim(), reader.GetValue(i).ToString());
                                        }
                                    }

                                    if (reader.FieldCount >= 6 && reader.GetValue(0) != null && Funcoes.IsDate(reader.GetValue(0).ToString()))
                                    {
                                        string[] linha = new string[reader.FieldCount];
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            linha[i] = reader.GetValue(i).ToString();
                                        }
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
            try
            {
                RedeDao redeDao = new RedeDao();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value == null)
                        continue;

                    DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    string autorizacao = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
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
                            Bandeira = dataGridView1.Rows[i].Cells[12].Value.ToString(),
                            ContaCorrente = "",
                            DataVenda = data,
                            Modalidade = dataGridView1.Rows[i].Cells[9].Value.ToString(),
                            NSU = "",
                            Parcela = 1,
                            Parcelas = 1,
                            ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[14].Value.ToString().Replace(".", "")),
                            ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[14].Value.ToString().Replace(".", "")),
                            Status = dataGridView1.Rows[i].Cells[13].Value.ToString()
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
