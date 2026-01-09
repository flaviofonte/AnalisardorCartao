using AnalisardorCartao.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using AnalisardorCartao.Dao;
using AnalisardorCartao.Repository;
using AnalisardorCartao.Entity;
using ExcelDataReader;

namespace AnalisardorCartao.Operacoes
{
    public class UpBrasil : ILerArquivo
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
                                    if (reader.GetValue(0).GetType() == typeof(string) && reader.GetString(0).Equals("Data transacao"))
                                    {
                                        dataGridView1.Columns.Add(reader.GetValue(0).ToString().Trim(), reader.GetValue(0).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(1).ToString().Trim(), reader.GetValue(1).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(7).ToString().Trim(), reader.GetValue(7).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(9).ToString().Trim(), reader.GetValue(9).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(10).ToString().Trim(), reader.GetValue(10).ToString());
                                        dataGridView1.Columns.Add(reader.GetValue(13).ToString().Trim(), reader.GetValue(13).ToString());
                                    }

                                    if (reader.FieldCount >= 13 && reader.GetValue(7) != null && reader.GetValue(10) != null && reader.GetValue(13) != null)
                                    {
                                        if (double.TryParse(reader.GetValue(13).ToString().Substring(3).Replace(".", ","), out double valor))
                                        {
                                            if (valor > 0.0)
                                            {
                                                string autorizacao = reader.GetValue(7).ToString().Trim();
                                                if (autorizacao.Length > 6) {
                                                    autorizacao = autorizacao.Substring(autorizacao.Length - 6);
                                                }
                                                string[] linha = new string[]
                                                {
                                                    reader.GetValue(0).ToString(),
                                                    reader.GetValue(1).ToString(),
                                                    autorizacao,
                                                    reader.GetValue(9) != null ? reader.GetValue(9).ToString() : "",
                                                    reader.GetValue(10).ToString(),
                                                    reader.GetValue(13).ToString()
                                                };
                                                dataGridView1.Rows.Add(linha);
                                            }
                                        }
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
                DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(0, 10));
                string nsu = "";
                string autorizacao = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL),
                    new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10,'0'), TipoOperacaoEnum.IGUAL)
                };

                RedeEntity rede = redeDao.GetByFilter(filtros);

                if (rede == null)
                {
                    if (decimal.TryParse(dataGridView1.Rows[i].Cells[5].Value.ToString().Substring(3).Replace(".", ","), out decimal valor))
                    {
                        rede = new RedeEntity()
                        {
                            Agencia = "",
                            Autorizacao = autorizacao.Trim().PadLeft(10, '0'),
                            Banco = "",
                            Bandeira = dataGridView1.Rows[i].Cells[4].Value.ToString(),
                            ContaCorrente = "",
                            DataVenda = data,
                            Modalidade = dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            NSU = nsu,
                            Parcela = 1,
                            Parcelas = 1,
                            ValorBruto = valor,
                            ValorLiquido = valor,
                            Status = "Aprovada"
                        };
                        redeDao.Adicionar(rede);
                    }
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
