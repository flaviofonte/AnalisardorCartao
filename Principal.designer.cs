using System.Drawing;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.importaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arquivoRedeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arquivoLinearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alinharCartõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarLinearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alinharContasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preçosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contasPagasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importaçõesToolStripMenuItem,
            this.validaçãoToolStripMenuItem,
            this.contasToolStripMenuItem,
            this.preçosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // importaçõesToolStripMenuItem
            // 
            this.importaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoRedeToolStripMenuItem,
            this.arquivoLinearToolStripMenuItem,
            this.toolStripSeparator1,
            this.sairToolStripMenuItem});
            this.importaçõesToolStripMenuItem.Name = "importaçõesToolStripMenuItem";
            this.importaçõesToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.importaçõesToolStripMenuItem.Text = "Importações";
            // 
            // arquivoRedeToolStripMenuItem
            // 
            this.arquivoRedeToolStripMenuItem.Name = "arquivoRedeToolStripMenuItem";
            this.arquivoRedeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.arquivoRedeToolStripMenuItem.Text = "Arquivo rede";
            this.arquivoRedeToolStripMenuItem.Click += new System.EventHandler(this.ArquivoRedeToolStripMenuItem_Click);
            // 
            // arquivoLinearToolStripMenuItem
            // 
            this.arquivoLinearToolStripMenuItem.Name = "arquivoLinearToolStripMenuItem";
            this.arquivoLinearToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.arquivoLinearToolStripMenuItem.Text = "Arquivo linear";
            this.arquivoLinearToolStripMenuItem.Click += new System.EventHandler(this.ArquivoLinearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.SairToolStripMenuItem_Click);
            // 
            // validaçãoToolStripMenuItem
            // 
            this.validaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alinharCartõesToolStripMenuItem});
            this.validaçãoToolStripMenuItem.Name = "validaçãoToolStripMenuItem";
            this.validaçãoToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.validaçãoToolStripMenuItem.Text = "Validação";
            // 
            // alinharCartõesToolStripMenuItem
            // 
            this.alinharCartõesToolStripMenuItem.Name = "alinharCartõesToolStripMenuItem";
            this.alinharCartõesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.alinharCartõesToolStripMenuItem.Text = "Alinhar cartões";
            this.alinharCartõesToolStripMenuItem.Click += new System.EventHandler(this.AlinharCartoesToolStripMenuItem_Click);
            // 
            // contasToolStripMenuItem
            // 
            this.contasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarLinearToolStripMenuItem,
            this.alinharContasToolStripMenuItem,
            this.relatóriosToolStripMenuItem});
            this.contasToolStripMenuItem.Name = "contasToolStripMenuItem";
            this.contasToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.contasToolStripMenuItem.Text = "Contas";
            // 
            // importarLinearToolStripMenuItem
            // 
            this.importarLinearToolStripMenuItem.Name = "importarLinearToolStripMenuItem";
            this.importarLinearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importarLinearToolStripMenuItem.Text = "Importar Linear";
            this.importarLinearToolStripMenuItem.Click += new System.EventHandler(this.ImportarLinearToolStripMenuItem_Click);
            // 
            // alinharContasToolStripMenuItem
            // 
            this.alinharContasToolStripMenuItem.Name = "alinharContasToolStripMenuItem";
            this.alinharContasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.alinharContasToolStripMenuItem.Text = "Alinhar Contas";
            this.alinharContasToolStripMenuItem.Click += new System.EventHandler(this.AlinharContasToolStripMenuItem_Click);
            // 
            // preçosToolStripMenuItem
            // 
            this.preçosToolStripMenuItem.Name = "preçosToolStripMenuItem";
            this.preçosToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.preçosToolStripMenuItem.Text = "Preços";
            this.preçosToolStripMenuItem.Click += new System.EventHandler(this.preçosToolStripMenuItem_Click);
            // 
            // relatóriosToolStripMenuItem
            // 
            this.relatóriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contasPagasToolStripMenuItem});
            this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.relatóriosToolStripMenuItem.Text = "Relatórios";
            // 
            // contasPagasToolStripMenuItem
            // 
            this.contasPagasToolStripMenuItem.Name = "contasPagasToolStripMenuItem";
            this.contasPagasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.contasPagasToolStripMenuItem.Text = "Contas Pagas";
            this.contasPagasToolStripMenuItem.Click += new System.EventHandler(this.contasPagasToolStripMenuItem_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 330);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.Text = "Conferência de cartão de crédito";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem importaçõesToolStripMenuItem;
        private ToolStripMenuItem arquivoRedeToolStripMenuItem;
        private ToolStripMenuItem arquivoLinearToolStripMenuItem;
        private ToolStripMenuItem validaçãoToolStripMenuItem;
        private ToolStripMenuItem alinharCartõesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem contasToolStripMenuItem;
        private ToolStripMenuItem importarLinearToolStripMenuItem;
        private ToolStripMenuItem alinharContasToolStripMenuItem;
        private ToolStripMenuItem preçosToolStripMenuItem;
        private ToolStripMenuItem relatóriosToolStripMenuItem;
        private ToolStripMenuItem contasPagasToolStripMenuItem;
    }
}