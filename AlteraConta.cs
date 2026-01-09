using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using System;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class AlteraConta : Form
    {
        public AlteraConta()
        {
            InitializeComponent();
        }

        public int LancamentoID { get; set; }

        private readonly LancamentoDao lancamentoDao = new LancamentoDao();
        private readonly ClientesDao clientesDao = new ClientesDao();
        private LancamentoEntity lancamento;

        private void AlteraConta_Load(object sender, EventArgs e)
        {
            lancamento = lancamentoDao.GetById(LancamentoID);
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (lancamento != null)
            {
                ClientesEntity cliente = clientesDao.GetById(lancamento.Codigo);
                if (lancamento.Serie == "O")
                {
                    CboBox.SelectedIndex = 0;
                } else
                {
                    CboBox.SelectedIndex = 1;
                }
                TxtNota.Text = lancamento.Nota.ToString();
                TxtCaixa.Text = lancamento.Caixa;
                TxtCodigo.Text = lancamento.Codigo.ToString();
                TxtCupom.Text = lancamento.Cupom;
                if (cliente != null)
                {
                    TxtNome.Text = cliente.Nome;
                }
                TxtValor.Text = lancamento.Valor.ToString("N");
                MskData.Text = lancamento.Data.ToShortDateString();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                TxtCaixa.Focus();
            } else
            {
                Funcoes.AllowNumber(e);
            }
        }

        private void TxtCaixa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                TxtCupom.Focus();
            }
            else
            {
                Funcoes.AllowNumber(e);
            }
        }

        private void TxtCupom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                TxtCodigo.Focus();
            }
            else
            {
                Funcoes.AllowNumber(e);
            }
        }

        private void TxtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                MskData.Focus();
            }
        }

        private void MskData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                TxtValor.Focus();
            }
        }

        private void TxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                button2.Focus();
            }
            else
            {
                Funcoes.AllowNumber(e);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Salvar os dados da tela?", "Salvar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (CboBox.SelectedIndex == 0)
                {
                    lancamento.Serie = "O";
                }
                else
                {
                    lancamento.Serie = "S";
                }
                lancamento.Nota = Convert.ToInt32(TxtNota.Text);
                lancamento.Caixa=TxtCaixa.Text;
                lancamento.Codigo=Convert.ToInt32(TxtCodigo.Text);
                lancamento.Cupom=TxtCupom.Text;
                ClientesEntity cliente = clientesDao.GetById(Convert.ToInt32(TxtCodigo.Text));
                if (cliente != null)
                {
                    lancamento.Codigo = cliente.Codigo;
                }
                lancamento.Valor = Convert.ToDouble(TxtValor.Text);
                lancamento.Data = Convert.ToDateTime(MskData.Text);
                lancamentoDao.Atualizar(lancamento);
                Close();
            }
        }
    }
}
