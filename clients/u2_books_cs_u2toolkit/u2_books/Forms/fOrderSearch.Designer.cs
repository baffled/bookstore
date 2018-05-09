namespace u2_books.Forms
{
    partial class fOrderSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fOrderSearch));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSelAfter = new System.Windows.Forms.Button();
            this.cmdSelBefore = new System.Windows.Forms.Button();
            this.rbOneClient = new System.Windows.Forms.RadioButton();
            this.rbAllClients = new System.Windows.Forms.RadioButton();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearchBefore = new System.Windows.Forms.TextBox();
            this.txtSearchAfter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvLines = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.cmdView = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSelAfter);
            this.panel1.Controls.Add(this.cmdSelBefore);
            this.panel1.Controls.Add(this.rbOneClient);
            this.panel1.Controls.Add(this.rbAllClients);
            this.panel1.Controls.Add(this.cboClients);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtSearchBefore);
            this.panel1.Controls.Add(this.txtSearchAfter);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 74);
            this.panel1.TabIndex = 0;
            // 
            // cmdSelAfter
            // 
            this.cmdSelAfter.Location = new System.Drawing.Point(654, 8);
            this.cmdSelAfter.Name = "cmdSelAfter";
            this.cmdSelAfter.Size = new System.Drawing.Size(29, 23);
            this.cmdSelAfter.TabIndex = 8;
            this.cmdSelAfter.TabStop = false;
            this.cmdSelAfter.Text = "...";
            this.cmdSelAfter.UseVisualStyleBackColor = true;
            this.cmdSelAfter.Click += new System.EventHandler(this.cmdSelAfter_Click);
            // 
            // cmdSelBefore
            // 
            this.cmdSelBefore.Location = new System.Drawing.Point(654, 39);
            this.cmdSelBefore.Name = "cmdSelBefore";
            this.cmdSelBefore.Size = new System.Drawing.Size(29, 23);
            this.cmdSelBefore.TabIndex = 8;
            this.cmdSelBefore.TabStop = false;
            this.cmdSelBefore.Text = "...";
            this.cmdSelBefore.UseVisualStyleBackColor = true;
            this.cmdSelBefore.Click += new System.EventHandler(this.cmdSelBefore_Click);
            // 
            // rbOneClient
            // 
            this.rbOneClient.AutoSize = true;
            this.rbOneClient.Location = new System.Drawing.Point(29, 42);
            this.rbOneClient.Name = "rbOneClient";
            this.rbOneClient.Size = new System.Drawing.Size(57, 17);
            this.rbOneClient.TabIndex = 1;
            this.rbOneClient.Text = "Client :";
            this.rbOneClient.UseVisualStyleBackColor = true;
            // 
            // rbAllClients
            // 
            this.rbAllClients.AutoSize = true;
            this.rbAllClients.Checked = true;
            this.rbAllClients.Location = new System.Drawing.Point(29, 13);
            this.rbAllClients.Name = "rbAllClients";
            this.rbAllClients.Size = new System.Drawing.Size(70, 17);
            this.rbAllClients.TabIndex = 0;
            this.rbAllClients.TabStop = true;
            this.rbAllClients.Text = "All Clients";
            this.rbAllClients.UseVisualStyleBackColor = true;
            // 
            // cboClients
            // 
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(103, 41);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(274, 21);
            this.cboClients.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(699, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearchBefore
            // 
            this.txtSearchBefore.Location = new System.Drawing.Point(547, 39);
            this.txtSearchBefore.Name = "txtSearchBefore";
            this.txtSearchBefore.Size = new System.Drawing.Size(100, 20);
            this.txtSearchBefore.TabIndex = 3;
            // 
            // txtSearchAfter
            // 
            this.txtSearchAfter.Location = new System.Drawing.Point(547, 10);
            this.txtSearchAfter.Name = "txtSearchAfter";
            this.txtSearchAfter.Size = new System.Drawing.Size(100, 20);
            this.txtSearchAfter.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Date Before : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(447, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Date After : ";
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colOrderDate,
            this.Column2,
            this.Column3});
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(323, 408);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "OrderId";
            this.Column1.HeaderText = "OrderId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // colOrderDate
            // 
            this.colOrderDate.DataPropertyName = "OrderDate";
            this.colOrderDate.HeaderText = "Order Date";
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ClientId";
            this.Column2.HeaderText = "Client";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Fullname";
            this.Column3.HeaderText = "Client Name";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 74);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvOrders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLines);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(897, 408);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgvLines
            // 
            this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AllowUserToDeleteRows = false;
            this.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.dgvLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLines.Location = new System.Drawing.Point(0, 26);
            this.dgvLines.Name = "dgvLines";
            this.dgvLines.ReadOnly = true;
            this.dgvLines.RowHeadersVisible = false;
            this.dgvLines.Size = new System.Drawing.Size(570, 382);
            this.dgvLines.TabIndex = 0;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "BookId";
            this.Column4.HeaderText = "Code";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 50;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Title";
            this.Column5.HeaderText = "Title";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "AuthorName";
            this.Column6.HeaderText = "Author";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Price";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column7.HeaderText = "Price";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Qty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column8.HeaderText = "Qty";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 50;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "TaxCode";
            this.Column9.HeaderText = "Tax";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 20;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "GoodsAmt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column10.HeaderText = "GoodsAmt";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "TaxAmt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column11.HeaderText = "TaxAmt";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.cmdEdit);
            this.panel2.Controls.Add(this.cmdView);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 26);
            this.panel2.TabIndex = 1;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(490, 2);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(68, 21);
            this.cmdEdit.TabIndex = 1;
            this.cmdEdit.Text = "&Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdView
            // 
            this.cmdView.Location = new System.Drawing.Point(418, 2);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(68, 21);
            this.cmdView.TabIndex = 1;
            this.cmdView.Text = "View";
            this.cmdView.UseVisualStyleBackColor = true;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(9, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Details for selected order ";
            // 
            // fOrderSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 482);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fOrderSearch";
            this.Text = "Order Search";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearchBefore;
        private System.Windows.Forms.TextBox txtSearchAfter;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.RadioButton rbOneClient;
        private System.Windows.Forms.RadioButton rbAllClients;
        private System.Windows.Forms.Button cmdSelAfter;
        private System.Windows.Forms.Button cmdSelBefore;
    }
}