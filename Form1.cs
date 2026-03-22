using AnalisardorCartao.Operacoes;
using System;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void CarregarTiposArquivo()
        {
            CboTipoArquivos.Items.Add("Alelo");
            CboTipoArquivos.Items.Add("Azulzinha");
            CboTipoArquivos.Items.Add("Sodexo/Alimentação Pass");
            CboTipoArquivos.Items.Add("Ben Visa");
            CboTipoArquivos.Items.Add("Cabal");
            CboTipoArquivos.Items.Add("Cielo");
            CboTipoArquivos.Items.Add("Rede");
            CboTipoArquivos.Items.Add("Ticket Relatório Transações");
            CboTipoArquivos.Items.Add("Ticket Extrato de Reembolsos Detalhado");
            CboTipoArquivos.Items.Add("Up Brasil");
            CboTipoArquivos.Items.Add("VR alimentação");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CboTipoArquivos.Text))
            {
                MessageBox.Show(this, "Escolha o tipo do arquivo a esportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string filtro;
            string tipo = CboTipoArquivos.Text;

            switch (tipo)
            {
                case "Alelo":
                case "Azulzinha":
                case "Ticket Relatório Transações":
                case "VR alimentação":
                case "Up Brasil":
                    filtro = "Arquivos válidos|*.xlsx;*.xls;*.csv";
                    break;
                default:
                    filtro = "Arquivos válidos|*.csv";
                    break;
            }


            dataGridView1.Rows.Clear();
            openFileDialog1.Title = "Arquivo da rede";
            openFileDialog1.Filter = filtro;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                TxtArquivo.Text = openFileDialog1.FileName;
            }
        }

        public void BloquearBotoes()
        {
            this.Cursor = Cursors.WaitCursor;
            dataGridView1.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            CboTipoArquivos.Enabled = false;
        }

        public void DesBloquearBotoes()
        {
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            CboTipoArquivos.Enabled = true;
            this.Cursor = Cursors.Default;
            dataGridView1.Cursor = Cursors.Default;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CboTipoArquivos.Text))
            {
                MessageBox.Show(this, "Selecione o tipo de arquivo a ser importado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TxtArquivo.Text))
            {
                MessageBox.Show(this, "Selecione o arquivo a ser importado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            BloquearBotoes();
            try
            {
                LerArquivos lerArquivos = new LerArquivos(new Rede());
                string tipo = CboTipoArquivos.Text;
                switch (tipo)
                {
                    case "Alelo":
                        lerArquivos.DefineLeitor(new Alelo());
                        break;
                    case "Azulzinha":
                        lerArquivos.DefineLeitor(new Azulzinha());
                        break;
                    case "Sodexo/Alimentação Pass":
                        lerArquivos.DefineLeitor(new Pass());
                        break;
                    case "Cabal":
                        lerArquivos.DefineLeitor(new Cabal());
                        break;
                    case "Ticket Relatório Transações":
                        lerArquivos.DefineLeitor(new Ticket());
                        break;
                    case "Ticket Extrato de Reembolsos Detalhado":
                        lerArquivos.DefineLeitor(new TicketReembolso());
                        break;
                    case "VR alimentação":
                        lerArquivos.DefineLeitor(new VrAlimentacao());
                        break;
                    case "Cielo":
                        lerArquivos.DefineLeitor(new Cielo());
                        break;
                    case "Ben Visa":
                        lerArquivos.DefineLeitor(new BemVisa());
                        break;
                    case "Rede":
                        lerArquivos.DefineLeitor(new Rede());
                        break;
                    case "Up Brasil":
                        lerArquivos.DefineLeitor(new UpBrasil());
                        break;
                }
                lerArquivos.LerArquivoCartao(TxtArquivo.Text, ref dataGridView1);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.InnerException == null ? ex.Message : ex.InnerException.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        DesBloquearBotoes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            BloquearBotoes();
            //try
            //{
                SalvarDados salvarDados = new SalvarDados(new Rede());
                string tipo = CboTipoArquivos.Text;
                switch (tipo)
                {
                    case "Alelo":
                        salvarDados.DefineSalvador(new Alelo());
                        break;
                    case "Azulzinha":
                        salvarDados.DefineSalvador(new Azulzinha());
                        break;
                    case "Sodexo/Alimentação Pass":
                        salvarDados.DefineSalvador(new Pass());
                        break;
                    case "Ben Visa":
                        salvarDados.DefineSalvador(new BemVisa());
                        break;
                    case "Cabal":
                        salvarDados.DefineSalvador(new Cabal());
                        break;
                    case "Rede":
                        salvarDados.DefineSalvador(new Rede());
                        break;
                    case "Ticket Relatório Transações":
                        salvarDados.DefineSalvador(new Ticket());
                        break;
                    case "Up Brasil":
                        salvarDados.DefineSalvador(new UpBrasil());
                        break;
                    case "VR alimentação":
                        salvarDados.DefineSalvador(new VrAlimentacao());
                        break;
                    case "Cielo":
                        salvarDados.DefineSalvador(new Cielo(TxtArquivo.Text));
                        break;
                    case "Ticket Extrato de Reembolsos Detalhado":
                        salvarDados.DefineSalvador(new TicketReembolso());
                        break;
                }
                salvarDados.SalvaDadosBase(dataGridView1);
                MessageBox.Show(this, "Importação dos dados da rede teminada com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.InnerException == null ? ex.Message : ex.InnerException.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            DesBloquearBotoes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarTiposArquivo();
        }
    }
}