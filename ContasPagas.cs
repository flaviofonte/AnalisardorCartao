using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class ContasPagas : Form
    {
        public ContasPagas()
        {
            InitializeComponent();
        }

        private readonly PagamentoDao pagamentoDao = new PagamentoDao();
        private readonly TipoPagamentoDao tipoPagamentoDao = new TipoPagamentoDao();
        private readonly LancPgtoDao lancPgtoDao = new LancPgtoDao();
        private readonly ClientesDao clientesDao = new ClientesDao();

        private void BloquearBotoes()
        {
            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            button5.Enabled = false;
        }

        private void DesbloquearBotoes()
        {
            button1.Enabled = true;
            button5.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            TextBox1.SelectAll();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                TextBox2.Focus();
            }
        }

        private void TextBox2_Enter(object sender, EventArgs e)
        {
            TextBox2.SelectAll();
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                button1.Focus();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!Funcoes.IsDate(TextBox1.Text))
            {
                MessageBox.Show(this, "Informe datas válidas para o período.");
                TextBox1.Focus();
                return;
            }

            if (!Funcoes.IsDate(TextBox2.Text))
            {
                MessageBox.Show(this, "Informe datas válidas para o período.");
                TextBox2.Focus();
                return;
            }

            if (DateTime.Parse(TextBox1.Text) > DateTime.Parse(TextBox2.Text))
            {
                MessageBox.Show(this, "A data inicial deve ser menor ou igual a data final.");
                TextBox1.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            BloquearBotoes();

            ContasPagasView.Rows.Clear();

            List<TipoPagamentoEntity> tipos = tipoPagamentoDao.GetTodos("Descricao");
            tipos?.ForEach(t => t.Soma = 0.0);
            tipos.Add(new TipoPagamentoEntity { Descricao = "Sem classificação", Soma = 0.0, TipoPagamentoID = -1 });

            List<SearchField> filtroConta = new List<SearchField>()
                    {
                        new SearchField("Tipo", 1, TipoOperacaoEnum.IGUAL),
                        new SearchField("Data", DateTime.Parse(TextBox1.Text), TipoOperacaoEnum.MAIORIGUAL),
                        new SearchField("Data", DateTime.Parse(TextBox2.Text), TipoOperacaoEnum.MENORIGUAL),
                    };

            List<LancPgtoEntity> contas = lancPgtoDao.GetAllByFilter(filtroConta);
            
            contas?.ForEach(c =>
            {
                ClientesEntity cliente = clientesDao.GetById(c.Codigo);
                PagamentosEntity pagamento = null;
                TipoPagamentoEntity tipoPagamento = null;
                if (c.PagamentoID != null)
                {
                    pagamento = pagamentoDao.GetById(c.PagamentoID.Value);
                    tipoPagamento = tipoPagamentoDao.GetById(pagamento.TipoPagamentoID);
                    tipos.Where(t => t.TipoPagamentoID == pagamento.TipoPagamentoID).First().Soma += c.Valor;
                }
                else
                {
                    tipos.Where(t => t.TipoPagamentoID == -1).First().Soma += c.Valor;
                }
                string[] row = new string[5];
                row[0] = cliente.Nome;
                row[1] = c.Cupom;
                row[2] = c.DataPagamento.Value.ToString("d");
                row[3] = c.Valor.ToString("N");
                row[4] = "";
                if (pagamento != null)
                {
                    row[4] = tipoPagamento.Descricao;
                }
                ContasPagasView.Rows.Add(row);
            });

            ContasPagasView.Sort(ContasPagasView.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

            string[] rowb = new string[5];
            rowb[0] = "";
            rowb[1] = "";
            rowb[2] = "";
            rowb[3] = "";
            rowb[4] = "";
            rowb[4] = "";
            ContasPagasView.Rows.Add(rowb);

            tipos?.ForEach(t =>
            {
                string[] row = new string[5];
                row[0] = t.Descricao;
                row[1] = "";
                row[2] = "";
                row[3] = t.Soma.ToString("N");
                row[4] = "";
                row[4] = "";
                ContasPagasView.Rows.Add(row);
            });

            DesbloquearBotoes();
            Cursor = Cursors.Default;

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!Funcoes.IsDate(TextBox1.Text))
            {
                MessageBox.Show(this, "Informe datas válidas para o período.");
                TextBox1.Focus();
                return;
            }

            if (!Funcoes.IsDate(TextBox2.Text))
            {
                MessageBox.Show(this, "Informe datas válidas para o período.");
                TextBox2.Focus();
                return;
            }

            if (DateTime.Parse(TextBox1.Text) > DateTime.Parse(TextBox2.Text))
            {
                MessageBox.Show(this, "A data inicial deve ser menor ou igual a data final.");
                TextBox1.Focus();
                return;
            }

            BloquearBotoes();
            try
            {
                PrintDGV.Print_DataGridView(ContasPagasView, $"Relatório de análise de contas entre {TextBox1.Text} e {TextBox2.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DesbloquearBotoes();
            }
        }
    }
}
