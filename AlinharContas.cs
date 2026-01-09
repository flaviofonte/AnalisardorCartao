using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Objetos;
using AnalisardorCartao.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class AlinharContas : Form
    {
        public AlinharContas()
        {
            InitializeComponent();
        }

        private readonly List<AlinharContasDTO> lista = new List<AlinharContasDTO>();
        private readonly ContasLinearDao contasLinearDao = new ContasLinearDao();
        private readonly LancamentoDao lancamentoDao = new LancamentoDao();
        private readonly LancPgtoDao lancPgtoDao = new LancPgtoDao();
        private readonly ClientesDao clientesDao = new ClientesDao();
        private readonly CombinacaoDao combinacaoDao = new CombinacaoDao();

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

            BloquearBotoes();

            Cursor = Cursors.WaitCursor;
            dataGridView1.Rows.Clear();
            lista.Clear();

            try
            {
                List<SearchField> filtroConta = new List<SearchField>()
                    {
                        new SearchField("Tipo", 1, TipoOperacaoEnum.IGUAL),
                        new SearchField("Data", DateTime.Parse(TextBox1.Text), TipoOperacaoEnum.MAIORIGUAL),
                        new SearchField("Data", DateTime.Parse(TextBox2.Text), TipoOperacaoEnum.MENORIGUAL),
                    };
                List<LancamentoEntity> contas = lancamentoDao.GetAllByFilter(filtroConta);
                List<LancPgtoEntity> contasp = lancPgtoDao.GetAllByFilter(filtroConta);

                List<SearchField> filtro = new List<SearchField>()
                {
                    new SearchField("DataEmissao", DateTime.Parse(TextBox1.Text), TipoOperacaoEnum.MAIORIGUAL),
                    new SearchField("DataEmissao", DateTime.Parse(TextBox2.Text), TipoOperacaoEnum.MENORIGUAL),
                };

                var resultado = contasLinearDao.GetAllByFilter("DataEmissao", SortDirectionEnum.Ascending, filtro);

                resultado?.ToList()?.ForEach(r =>
                {
                    string[] cliente = r.Cliente.Split('-');
                    AlinharContasDTO row = new AlinharContasDTO
                    {
                        ContaLinearID = r.ContaLinearID,
                        ValorLinear = r.Valor,
                        Cliente = cliente[1],
                        DataEmissao = r.DataEmissao,
                        Documento = r.Documento.Trim(),
                        CodigoLinear = int.Parse(cliente[0])
                    };

                    List<SearchField> filtroComb = new List<SearchField>()
                    {
                        new SearchField("CodigoLinear", row.CodigoLinear, TipoOperacaoEnum.IGUAL)
                    };
                    CombinacaoEntity combina = combinacaoDao.GetByFilter(filtroComb);

                    LancamentoEntity lac = contas.Where(c => c.Cupom.Trim() == r.Documento.Trim() & c.Valor == r.Valor).FirstOr(null);
                    if (lac != null)
                    {
                        ClientesEntity clientesEntity = clientesDao.GetById(lac.Codigo);
                        row.Caixa = lac.Caixa;
                        row.Codigo = lac.Codigo;
                        row.Cupon = lac.Cupom;
                        row.Dbcr = lac.Dbcr;
                        row.Historico = lac.Historico;
                        row.Lancamento = lac.Lancamento;
                        row.Nota = lac.Nota.ToString();
                        row.Serie = lac.Serie;
                        row.Tipo = 1;
                        row.Nome = clientesEntity?.Nome;
                    }
                    else
                    {
                        bool lancou = false;
                        if (combina != null)
                        {
                            lac = contas.Where(c => c.Codigo == combina.CodigoContas & c.Valor == r.Valor).FirstOr(null);
                            if (lac != null)
                            {
                                ClientesEntity clientesEntity = clientesDao.GetById(lac.Codigo);
                                row.Caixa = lac.Caixa;
                                row.Codigo = lac.Codigo;
                                row.Cupon = r.Documento.Trim();
                                row.Dbcr = lac.Dbcr;
                                row.Historico = lac.Historico;
                                row.Lancamento = lac.Lancamento;
                                row.Nota = lac.Nota.ToString();
                                row.Serie = lac.Serie;
                                row.Tipo = 1;
                                row.Nome = clientesEntity?.Nome;
                                lancou = true;
                            }
                        }
                        if (!lancou)
                        {
                            LancPgtoEntity lacp = contasp.Where(c => c.Cupom.Trim() == r.Documento.Trim() & c.Valor == r.Valor).FirstOr(null);
                            if (lacp != null)
                            {
                                ClientesEntity clientesEntity = clientesDao.GetById(lacp.Codigo);
                                row.Caixa = lacp.Caixa;
                                row.Codigo = lacp.Codigo;
                                row.Cupon = lacp.Cupom;
                                row.Dbcr = lacp.Dbcr;
                                row.Historico = lacp.Historico;
                                row.Lancamento = lacp.Lancamento;
                                row.Nota = lacp.Nota.ToString();
                                row.Serie = lacp.Serie;
                                row.Tipo = 2;
                                row.Nome = clientesEntity?.Nome;
                            }
                            else
                            {
                                if (combina != null)
                                {
                                    lacp = contasp.Where(c => c.Codigo == combina.CodigoContas & c.Valor == r.Valor).FirstOr(null);
                                    if (lacp != null)
                                    {
                                        ClientesEntity clientesEntity = clientesDao.GetById(lacp.Codigo);
                                        row.Caixa = lacp.Caixa;
                                        row.Codigo = lacp.Codigo;
                                        row.Cupon = r.Documento.Trim();
                                        row.Dbcr = lacp.Dbcr;
                                        row.Historico = lacp.Historico;
                                        row.Lancamento = lacp.Lancamento;
                                        row.Nota = lacp.Nota.ToString();
                                        row.Serie = lacp.Serie;
                                        row.Tipo = 2;
                                        row.Nome = clientesEntity?.Nome;
                                    }
                                }
                            }
                        }
                    }
                    lista.Add(row);
                    Application.DoEvents();
                });

                contas?.ForEach(c =>
                {
                    if (lista.Count(l => l.Nota == c.Nota.ToString() && l.ValorLinear == c.Valor) <= 0)
                    {
                        ClientesEntity cliente = clientesDao.GetById(c.Codigo);
                        AlinharContasDTO row = new AlinharContasDTO
                        {
                            DataEmissao = c.Data,
                            ValorLinear = c.Valor,
                            Caixa = c.Caixa,
                            Codigo = c.Codigo,
                            Cupon = c.Cupom,
                            Dbcr = c.Dbcr,
                            Historico = c.Historico,
                            Lancamento = c.Lancamento,
                            Nota = c.Nota.ToString(),
                            Serie = c.Serie,
                            Tipo = 1,
                            Nome = cliente?.Nome
                        };
                        lista.Add(row);
                    }
                    Application.DoEvents();
                });

                contasp?.ForEach(c =>
                {
                    if (lista.Count(l => l.Nota == c.Nota.ToString() && l.ValorLinear == c.Valor) <= 0)
                    {
                        ClientesEntity cliente = clientesDao.GetById(c.Codigo);
                        AlinharContasDTO row = new AlinharContasDTO
                        {
                            DataEmissao = c.Data,
                            ValorLinear = c.Valor,
                            Caixa = c.Caixa,
                            Codigo = c.Codigo,
                            Cupon = c.Cupom,
                            Dbcr = c.Dbcr,
                            Historico = c.Historico,
                            Lancamento = c.Lancamento,
                            Nota = c.Nota.ToString(),
                            Serie = c.Serie,
                            Tipo = 2,
                            Nome = cliente?.Nome
                        };
                        lista.Add(row);
                    }
                    Application.DoEvents();
                });

                PreencherGrid(lista.OrderBy(l => l.DataEmissao).OrderBy(l => l.ValorLinear).ToList(), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DesbloquearBotoes();
            Cursor = Cursors.Default;
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

        private void PreencherGrid(List<AlinharContasDTO> listaDados, bool comTotal)
        {
            dataGridView1.Rows.Clear();
            listaDados?.ForEach(r =>
            {
                string[] row = new string[16];
                row[0] = r.DataEmissao.ToString("dd/MM/yyyy");
                row[1] = r.Documento ?? "";
                row[2] = r.Cliente == null ? "" : r.CodigoLinear.ToString() + "-" + r.Cliente;
                row[3] = r.ValorLinear.ToString("N2");
                row[4] = r.Codigo == 0 ? "" : r.Codigo.ToString();
                row[5] = r.Nome ?? "";
                row[6] = r.Nota ?? "";
                row[7] = r.Serie == null ? "Outros" : r.Cupon == "S" ? "Sinsuve" : "Outros";
                row[8] = r.Cupon ?? "";
                row[9] = r.Tipo == 0 ? "" : r.Tipo == 1 ? "A" : "P";
                row[10] = r.Historico ?? "";
                row[11] = r.Lancamento.ToString();
                dataGridView1.Rows.Add(row);
            });

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(this, "Nenhum dado encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            PrintDGV.Print_DataGridView(dataGridView1, $"Relatório de análise de contas entre {TextBox1.Text} e {TextBox2.Text}");
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(ColumnInt_KeyPress);
        }

        private void ColumnInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funcoes.AllowNumber(e);
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                if (id != 0)
                {
                    ClientesEntity cliente = clientesDao.GetById(id);
                    if (cliente != null)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = cliente.Nome;
                        dataGridView1.Rows[e.RowIndex].Cells[10].Value = "COMPRAS";
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Outros";
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Cliente não encontrado";
                    }
                }
                else
                {
                    BuscaCliente form = new BuscaCliente
                    {
                        CodigoCliente = 0
                    };
                    form.TxtBucar.Text = "";
                    DialogResult resul = form.ShowDialog(this);
                    if (resul == DialogResult.OK)
                    {
                        id = form.CodigoCliente;
                        ClientesEntity cliente = clientesDao.GetById(id);
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = id.ToString();
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = cliente.Nome;
                        dataGridView1.Rows[e.RowIndex].Cells[10].Value = "COMPRAS";
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Outros";
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = "";
                    }
                }
            }
        }

        private void AlinharContas_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Exportar para o Contas as contas com o campo Nota/Cupom informado?", "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                BarraProgresso.Visible = true;
                BarraProgresso.Value = 0;
                BarraProgresso.Maximum = dataGridView1.Rows.Count;
                BarraProgresso.Visible = true;
                BarraProgresso.Refresh();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (!string.IsNullOrEmpty(item.Cells[1].Value.ToString()) && !string.IsNullOrEmpty(item.Cells[4].Value.ToString())
                        && !string.IsNullOrEmpty(item.Cells[5].Value.ToString())
                         && !string.IsNullOrEmpty(item.Cells[6].Value.ToString()) && !string.IsNullOrEmpty(item.Cells[8].Value.ToString()))
                    {
                        //incluir e verificar se está em combinação
                        if (!JaExiste(Convert.ToDateTime(item.Cells[0].Value.ToString()), item.Cells[8].Value.ToString().Trim(), Convert.ToInt32(item.Cells[6].Value.ToString()), Convert.ToInt32(item.Cells[4].Value.ToString())))
                        {
                            LancamentoEntity lancamento = new LancamentoEntity
                            {
                                Nota = Convert.ToInt32(item.Cells[6].Value.ToString()),
                                Sequencia = 0,
                                Data = Convert.ToDateTime(item.Cells[0].Value.ToString()),
                                Serie = item.Cells[7].Value.ToString().ToArray()[0].ToString(),
                                Caixa = "",
                                Codigo = Convert.ToInt32(item.Cells[4].Value.ToString()),
                                Cupom = item.Cells[8].Value.ToString().Trim(),
                                Dbcr = "D",
                                Historico = item.Cells[10].Value.ToString().Trim(),
                                Tipo = 1,
                                Valor = double.Parse(item.Cells[3].Value.ToString())
                            };
                            lancamentoDao.Adicionar(lancamento);
                        }
                    }
                    else if (string.IsNullOrEmpty(item.Cells[1].Value.ToString()) && string.IsNullOrEmpty(item.Cells[2].Value.ToString())
                        && !string.IsNullOrEmpty(item.Cells[6].Value.ToString())
                         && !string.IsNullOrEmpty(item.Cells[8].Value.ToString()) && !string.IsNullOrEmpty(item.Cells[9].Value.ToString()))
                    {
                        int codigoConta = 0;
                        if (item.Cells[9].Value.ToString() == "A" && Convert.ToInt32(item.Cells[8].Value.ToString()) > 0)
                        {
                            LancamentoEntity lancamento = lancamentoDao.GetById(Convert.ToInt32(item.Cells[11].Value.ToString()));
                            lancamento.Cupom = item.Cells[8].Value.ToString().Trim();
                            lancamentoDao.Atualizar(lancamento);
                            codigoConta = lancamento.Codigo;
                        }
                        else if (item.Cells[9].Value.ToString() == "P" && Convert.ToInt32(item.Cells[8].Value.ToString()) > 0)
                        {
                            LancPgtoEntity lancPgto = lancPgtoDao.GetById(Convert.ToInt32(item.Cells[11].Value.ToString()));
                            lancPgto.Cupom = item.Cells[8].Value.ToString().Trim();
                            lancPgtoDao.Atualizar(lancPgto);
                            codigoConta = lancPgto.Codigo;
                        }
                        if (codigoConta > 0)
                        {
                            int rowLinha = dataGridView1.Rows.Cast<DataGridViewRow>()
                                .Select(row => row.Cells[1].Value?.ToString())
                                .ToList().FindIndex(cv => cv == item.Cells[8].Value.ToString().Trim());
                            if (rowLinha != -1)
                            {
                                String dado = dataGridView1.Rows[rowLinha].Cells[2].Value.ToString();
                                string codigoLinear = dado.Split('-')[0];
                                CombinaCodigos(codigoConta, Convert.ToInt32(codigoLinear));
                            }
                        }

                    }

                    BarraProgresso.Increment(1);
                }
                BarraProgresso.Visible = false;
                Cursor = Cursors.Default;
            }
        }

        private void CombinaCodigos(int codigoContas, int codigoLinear)
        {
            List<SearchField> filtroComb = new List<SearchField>()
                    {
                        new SearchField("CodigoContas", codigoContas, TipoOperacaoEnum.IGUAL)
                    };
            CombinacaoEntity combinacao = combinacaoDao.GetByFilter(filtroComb);
            if (combinacao == null)
            {
                combinacao = new CombinacaoEntity()
                {
                    CodigoContas = codigoContas,
                    CodigoLinear = codigoLinear,
                    DataCombinacao = DateTime.Now
                };
                combinacaoDao.Adicionar(combinacao);
            }
            else
            {
                if (combinacao.CodigoLinear != codigoLinear)
                {
                    combinacao.CodigoLinear = codigoLinear;
                    combinacaoDao.Atualizar(combinacao);
                }
            }
        }

        private bool JaExiste(DateTime data, string cupom, int nota, int codigo)
        {
            List<SearchField> filtroConta = new List<SearchField>()
                    {
                        new SearchField("Tipo", 1, TipoOperacaoEnum.IGUAL),
                        new SearchField("Data", data.Date, TipoOperacaoEnum.IGUAL),
                        new SearchField("Cupom", cupom, TipoOperacaoEnum.IGUAL),
                        new SearchField("Nota", nota, TipoOperacaoEnum.IGUAL),
                        new SearchField("Codigo", codigo, TipoOperacaoEnum.IGUAL)
                    };

            LancamentoEntity lancamento = lancamentoDao.GetByFilter(filtroConta);
            return lancamento != null;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value) > 0)
            {
                AlteraConta form = new AlteraConta
                {
                    LancamentoID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value)
                };
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    button1.PerformClick();
                    dataGridView1.Rows[e.RowIndex].Cells[11].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = e.RowIndex;
                }
            }
        }
    }
}
