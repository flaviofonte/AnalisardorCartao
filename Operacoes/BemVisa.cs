using AnalisardorCartao.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using AnalisardorCartao.Dao;
using AnalisardorCartao.Repository;
using AnalisardorCartao.Entity;

namespace AnalisardorCartao.Operacoes
{
    public class BemVisa : ILerArquivo
    {
        public void LerArquivo(string fileName, ref DataGridView dataGridView1)
        {
            try
            {
                string[] linhas = File.ReadAllLines(fileName);
                string[] linha;

                dataGridView1.Columns.Clear();
                linha = linhas[0].Split(';');
                foreach (string s in linha)
                {
                    dataGridView1.Columns.Add(s, s);
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
            RedeDao redeDao = new RedeDao();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null)
                    continue;

                DateTime data = DateTime.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(0, 10));
                string nsu = dataGridView1.Rows[i].Cells[2].Value.ToString();
                nsu = nsu.Substring(nsu.Length - 6);
                nsu = Funcoes.AcertarNsu(nsu);
                string autorizacao = Funcoes.AcertarAtuarizacao("0");
                List<SearchField> filtros = new List<SearchField>()
                {
                    new SearchField("NSU", nsu, TipoOperacaoEnum.IGUAL),
                    new SearchField("DataVenda", data, TipoOperacaoEnum.IGUAL)
                };

                RedeEntity rede = redeDao.GetByFilter(filtros);

                if (rede == null)
                {
                    rede = new RedeEntity()
                    {
                        Agencia = "",
                        Autorizacao = autorizacao,
                        Banco = "",
                        Bandeira = dataGridView1.Rows[i].Cells[11].Value.ToString(),
                        ContaCorrente = "",
                        DataVenda = data,
                        Modalidade = dataGridView1.Rows[i].Cells[3].Value.ToString(),
                        NSU = nsu,
                        Parcela = 1,
                        Parcelas = 1,
                        ValorBruto = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString().Replace(".", ",")),
                        ValorLiquido = decimal.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString().Replace(".", ","))
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
