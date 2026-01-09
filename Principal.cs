using System;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            Funcoes.AtualizaODBC(Funcoes.LeConfiguracao("dados"));
            this.Text = "Conferência de cartão de crédito - " + Application.ProductVersion;
        }

        private void AbrirFilhos(Form filho)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == filho.GetType())
                {
                    frm.Focus();
                    return;
                }
            }
            filho.MdiParent = this;
            filho.Show();
        }

        private void ArquivoRedeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            AbrirFilhos(form);
        }

        private void ArquivoLinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Linear form = new Linear();
            AbrirFilhos(form);
        }

        private void AlinharCartoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alinhar form = new Alinhar();
            AbrirFilhos(form);
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ImportarLinearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContasLinear contas = new ContasLinear();
            AbrirFilhos(contas);
        }

        private void AlinharContasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharContas alinharContas = new AlinharContas();
            AbrirFilhos(alinharContas);
        }

        private void preçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFilhos(new VerificaPrecoCusto());
        }

        private void contasPagasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFilhos(new ContasPagas());
        }
    }
}
