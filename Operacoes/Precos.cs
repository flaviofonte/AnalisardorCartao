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
    public class Precos : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName);
                string[] linha;

                if (linhas.Length > 0)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string s = linhas[i];

                        if (!string.IsNullOrEmpty(s) && s.Length > 0)
                        {
                            s = s.Replace("\"", "");
                            linha = s.Split(',');
                            string[] campos = new string[7];
                            campos[0] = linha[9];
                            campos[1] = linha[10];
                            campos[2] = linha[11];
                            campos[3] = "0";
                            campos[4] = "0";
                            if (double.TryParse(linha[13], out double valor))
                            {
                                campos[3] = valor.ToString("N2");
                            }
                            if (double.TryParse(linha[14], out double tvalor))
                            {
                                campos[4] = tvalor.ToString("N2");
                            }
                            campos[5] = (((double.Parse(campos[4]) - double.Parse(campos[3])) / double.Parse(campos[4]) * 100)).ToString("N4");
                            campos[6] = (double.Parse(campos[4]) - double.Parse(campos[3])).ToString("N2");
                            dataGridView1.Rows.Add(campos);
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
            try
            {
                RedeDao redeDao = new RedeDao();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value == null)
                        continue;

                    DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    string nsu = Funcoes.AcertarNsu(dataGridView1.Rows[i].Cells[17].Value.ToString());
                    string autorizacao = Funcoes.AcertarAtuarizacao(dataGridView1.Rows[i].Cells[20].Value.ToString());
                    List<SearchField> filtros = new List<SearchField>()
                    {
                        new SearchField("Autorizacao", autorizacao.Trim().PadLeft(10,'0'), TipoOperacaoEnum.IGUAL),
                        new SearchField("NSU", nsu, TipoOperacaoEnum.IGUAL),
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
                            Bandeira = dataGridView1.Rows[i].Cells[8].Value.ToString(),
                            ContaCorrente = dataGridView1.Rows[i].Cells[17].Value.ToString(),
                            DataVenda = data,
                            Modalidade = dataGridView1.Rows[i].Cells[5].Value.ToString(),
                            NSU = nsu,
                            Parcela = 1,
                            Parcelas = 1,
                            ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(".", "")),
                            ValorLiquido = dataGridView1.Rows[i].Cells[16].Value.ToString() == "" ? 0 : decimal.Parse(dataGridView1.Rows[i].Cells[16].Value.ToString().Replace(".", ""))
                        };
                        redeDao.Adicionar(rede);
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                throw new Exception( string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message, ex);
            }
        }

        public void SalvaDados(DataGridView dataGridView1, string file)
        {
            throw new NotImplementedException();
        }
    }
}
