using AnalisardorCartao.Dao;
using AnalisardorCartao.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public partial class BuscaCliente : Form
    {
        public int CodigoCliente { get; set; }
        private readonly ClientesDao clientesDao = new ClientesDao();

        public BuscaCliente()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            CodigoCliente = Convert.ToInt32(ListaClientes.Rows[ListaClientes.SelectedCells[0].RowIndex].Cells[0].Value);
            Hide();
        }

        private void TxtBucar_TextChanged(object sender, EventArgs e)
        {
            if (TxtBucar.Text.Length > 0)
            {
                foreach (DataGridViewRow item in ListaClientes.Rows)
                {
                    if (item.Cells[1].Value.ToString().StartsWith(TxtBucar.Text))
                    {
                        item.Selected = true;
                        ListaClientes.FirstDisplayedCell = item.Cells[0];
                        break;
                    }
                }
            }
        }

        private void BuscaCliente_Shown(object sender, EventArgs e)
        {
            List<ClientesEntity> clientes = clientesDao.GetTodos("Nome");
            clientes?.ForEach(c =>
            {
                string[] linha = new string[2];
                linha[0] = c.Codigo.ToString();
                linha[1] = c.Nome;
                ListaClientes.Rows.Add(linha);
            });
        }
    }
}
