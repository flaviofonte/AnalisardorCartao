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
    public class Ticket : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                dataGridView1.Columns.Clear();
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    ExcelReaderConfiguration config = new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = System.Text.Encoding.UTF8,
                        AutodetectSeparators = new char[] { ';' }
                    };
                    using (var reader = ExcelReaderFactory.CreateReader(stream, config))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) != null && reader.GetValue(4) != null)
                                {
                                    if (reader.GetValue(0).GetType() == typeof(string) && reader.GetString(0).Equals("Data da Transação", StringComparison.OrdinalIgnoreCase))
                                    {
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (reader.GetValue(i) != null)
                                            {
                                                string s = reader.GetValue(i).ToString();
                                                if (!string.IsNullOrWhiteSpace(s))
                                                {
                                                    dataGridView1.Columns.Add(s, s);
                                                }
                                            }
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()) && Funcoes.IsDate(reader.GetValue(0).ToString()))
                                    {
                                        List<string> linha = new List<string>();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (reader.GetValue(i) != null)
                                            {
                                                string s = reader.GetValue(i).ToString();
                                                if (!string.IsNullOrWhiteSpace(s))
                                                {
                                                    linha.Add(s);
                                                }
                                            }
                                        }
                                        string[] ar = linha.ToArray();
                                        dataGridView1.Rows.Add(ar);
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
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                    continue;

                DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(0, 10));
                string nsu = "";
                string autorizacao = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().PadLeft(6, '0');
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
