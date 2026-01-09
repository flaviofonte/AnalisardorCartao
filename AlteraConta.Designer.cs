namespace AnalisardorCartao
{
    partial class AlteraConta
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
            this.CboBox = new System.Windows.Forms.ComboBox();
            this.TxtNota = new System.Windows.Forms.TextBox();
            this.TxtCaixa = new System.Windows.Forms.TextBox();
            this.TxtCupom = new System.Windows.Forms.TextBox();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.TxtNome = new System.Windows.Forms.TextBox();
            this.MskData = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TxtValor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CboBox
            // 
            this.CboBox.FormattingEnabled = true;
            this.CboBox.Items.AddRange(new object[] {
            "Outros",
            "Sinsuv"});
            this.CboBox.Location = new System.Drawing.Point(66, 39);
            this.CboBox.Name = "CboBox";
            this.CboBox.Size = new System.Drawing.Size(127, 21);
            this.CboBox.TabIndex = 0;
            // 
            // TxtNota
            // 
            this.TxtNota.Location = new System.Drawing.Point(96, 66);
            this.TxtNota.MaxLength = 9;
            this.TxtNota.Name = "TxtNota";
            this.TxtNota.Size = new System.Drawing.Size(77, 20);
            this.TxtNota.TabIndex = 1;
            this.TxtNota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNota_KeyPress);
            // 
            // TxtCaixa
            // 
            this.TxtCaixa.Location = new System.Drawing.Point(66, 101);
            this.TxtCaixa.Name = "TxtCaixa";
            this.TxtCaixa.Size = new System.Drawing.Size(73, 20);
            this.TxtCaixa.TabIndex = 2;
            this.TxtCaixa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCaixa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCaixa_KeyPress);
            // 
            // TxtCupom
            // 
            this.TxtCupom.Location = new System.Drawing.Point(169, 132);
            this.TxtCupom.Name = "TxtCupom";
            this.TxtCupom.Size = new System.Drawing.Size(100, 20);
            this.TxtCupom.TabIndex = 3;
            this.TxtCupom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCupom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCupom_KeyPress);
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.Location = new System.Drawing.Point(73, 163);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(78, 20);
            this.TxtCodigo.TabIndex = 4;
            this.TxtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtNome
            // 
            this.TxtNome.Location = new System.Drawing.Point(73, 194);
            this.TxtNome.Name = "TxtNome";
            this.TxtNome.Size = new System.Drawing.Size(452, 20);
            this.TxtNome.TabIndex = 5;
            this.TxtNome.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNome_KeyPress);
            // 
            // MskData
            // 
            this.MskData.Location = new System.Drawing.Point(68, 225);
            this.MskData.Mask = "00/00/0000";
            this.MskData.Name = "MskData";
            this.MskData.Size = new System.Drawing.Size(83, 20);
            this.MskData.TabIndex = 6;
            this.MskData.ValidatingType = typeof(System.DateTime);
            this.MskData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MskData_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Núm. Nota";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Caixa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cupom/Documento Linear";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Código";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nome";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Valor";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(381, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(462, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Salvar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // TxtValor
            // 
            this.TxtValor.Location = new System.Drawing.Point(66, 256);
            this.TxtValor.MaxLength = 15;
            this.TxtValor.Name = "TxtValor";
            this.TxtValor.Size = new System.Drawing.Size(98, 20);
            this.TxtValor.TabIndex = 18;
            this.TxtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtValor_KeyPress);
            // 
            // AlteraConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(561, 306);
            this.Controls.Add(this.TxtValor);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MskData);
            this.Controls.Add(this.TxtNome);
            this.Controls.Add(this.TxtCodigo);
            this.Controls.Add(this.TxtCupom);
            this.Controls.Add(this.TxtCaixa);
            this.Controls.Add(this.TxtNota);
            this.Controls.Add(this.CboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AlteraConta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlteraConta";
            this.Load += new System.EventHandler(this.AlteraConta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboBox;
        private System.Windows.Forms.TextBox TxtNota;
        private System.Windows.Forms.TextBox TxtCaixa;
        private System.Windows.Forms.TextBox TxtCupom;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.TextBox TxtNome;
        private System.Windows.Forms.MaskedTextBox MskData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TxtValor;
    }
}