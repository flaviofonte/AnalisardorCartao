using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using AnalisardorCartao.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class Alinhar : Form
    {
        public Alinhar()
        {
            InitializeComponent();
        }

        private readonly List<DadosAlinhados> lista = new List<DadosAlinhados>();
        private List<DadosAlinhados> listaFiltrada = new List<DadosAlinhados>();
        private readonly AlinhadosDao alinhadosDao = new AlinhadosDao();
        private string FiltroSecundario = "";

        private void BloquearBotoes()
        {
            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void DesbloquearBotoes()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
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
                AlinhadosDao alinhadosDao = new AlinhadosDao();
                List<SearchField> filtroConta = new List<SearchField>()
                    {
                        new SearchField("DataOrdem", DateTime.Parse(TextBox1.Text), TipoOperacaoEnum.MAIORIGUAL),
                        new SearchField("DataOrdem", DateTime.Parse(TextBox2.Text), TipoOperacaoEnum.MENORIGUAL),
                    };

                List<AlinhadosEntity> resultado = alinhadosDao.GetAllByFilter(filtroConta);
                resultado = resultado.OrderBy(r => r.DataOrdem).ThenBy(r => r.ValorOrdem).ToList();
                resultado?.ForEach(r =>
                {
                    DadosAlinhados row = new DadosAlinhados()
                    {
                        DataVendaLinear = r.DataOrdem,
                        NSULinear = r.NSU,
                        BandeiraLinear = r.BandeiraLinear,
                        ValorVendaLinear = r.ValorLinear,
                        DataVendaRede = r.DataVendaRede,
                        NSUrede = r.NSUREDE,
                        BandeiraRede = r.BandeiraRede,
                        ModalidadeRede = r.ModalidadeRede,
                        ValorBrutoRede = r.ValorBrutoRede,
                        Diferenca = CalculeDiferenca(r.ValorBrutoRede, r.ValorLinear),
                        AutorizacaoRede = r.AutorizacaoRede,
                        Prazo = "",
                        DataOrdem = r.DataOrdem,
                        AutorizacaoLinear = r.AutorizacaoLinear,
                        ModalidadeLinear = r.ModalidadeLinear,
                        ValorLiquido = 0,
                        Status = r.Status
                    };
                    lista.Add(row);
                });
                PreencherGrid(lista, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FiltroSecundario = "";

            DesbloquearBotoes();
            Cursor = Cursors.Default;
        }

        private decimal CalculeDiferenca(decimal? valorRede, decimal? valorLinear)
        {
            if (valorLinear == null)
            {
                return valorRede.Value;
            }
            if (valorRede == null)
            {
                return valorLinear.Value * -1;
            }
            return valorRede.Value - valorLinear.Value;
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

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                if (string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    double valor = Convert.ToDouble((string)e.Value);
                    if (valor != 0)
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                BloquearBotoes();
                dataGridView1.Rows.Clear();
                listaFiltrada = lista.Where(l => l.Diferenca != 0 || l.NSULinear == null).ToList();
                PreencherGrid(listaFiltrada, false);
                FiltroSecundario = "{CONCILIACAO.Diferenca} <> 0";
                DesbloquearBotoes();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                BloquearBotoes();
                dataGridView1.Rows.Clear();
                listaFiltrada = lista.Where(l => l.Diferenca == 0).ToList();
                PreencherGrid(listaFiltrada, false);
                FiltroSecundario = "{CONCILIACAO.Diferenca} = 0";
                DesbloquearBotoes();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            PreencherGrid(lista, true);
            FiltroSecundario = "";
        }

        private void PreencherGrid(List<DadosAlinhados> listaDados, bool comTotal)
        {
            dataGridView1.Rows.Clear();
            listaDados?.ForEach(r =>
            {
                string[] row = new string[14];
                row[0] = r.DataOrdem.ToString("dd/MM/yyyy");
                row[1] = r.NSULinear ?? "";
                row[2] = r.AutorizacaoLinear ?? "";
                row[3] = r.BandeiraLinear ?? "";
                row[4] = r.ModalidadeLinear ?? "";
                row[5] = r.ValorVendaLinear == null ? "" : r.ValorVendaLinear.Value.ToString("N2");
                row[6] = r.NSUrede ?? "";
                row[7] = r.AutorizacaoRede ?? "";
                row[8] = r.BandeiraRede ?? "";
                row[9] = r.ModalidadeRede ?? "";
                row[10] = r.ValorBrutoRede == null ? "" : r.ValorBrutoRede.Value.ToString("N2");
                row[11] = r.Diferenca == null ? "" : r.Diferenca.Value.ToString("N2");
                row[12] = r.Prazo ?? "";
                row[13] = r.Status ?? "";
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

            BloquearBotoes();
            try
            {
                PrintDGV.Print_DataGridView(dataGridView1, $"Relatório de análise de contas entre {TextBox1.Text} e {TextBox2.Text}");
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

        private void Button6_Click(object sender, EventArgs e)
        {
            BloquearBotoes();
            saveFileDialog1.Title = "Planilha excel";
            saveFileDialog1.Filter = "Planilhas Excel|*.xlsx";
            saveFileDialog1.InitialDirectory = Funcoes.SFormataCaminho(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            saveFileDialog1.FileName = "cartões checados " + TextBox1.Text.Replace("/", "_") + " até " + TextBox2.Text.Replace("/", "_") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                ExportacaoExcel expex = new ExportacaoExcel();

                expex.ExportacaoListaExcel(dataGridView1, saveFileDialog1.FileName);
                Cursor = Cursors.Default;
                MessageBox.Show(this, "Dados exportados com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DesbloquearBotoes();
        }

        private void Alinhar_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
