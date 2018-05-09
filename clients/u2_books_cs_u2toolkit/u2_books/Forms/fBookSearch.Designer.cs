namespace u2_books.Forms
{
    partial class fBookSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fBookSearch));
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.cboPublishers = new System.Windows.Forms.ComboBox();
            this.cboAuthors = new System.Windows.Forms.ComboBox();
            this.rbKeyword = new System.Windows.Forms.RadioButton();
            this.rbPublisher = new System.Windows.Forms.RadioButton();
            this.rbAuthor = new System.Windows.Forms.RadioButton();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.cmdSearch);
            this.gbSearch.Controls.Add(this.txtKeywords);
            this.gbSearch.Controls.Add(this.cboPublishers);
            this.gbSearch.Controls.Add(this.cboAuthors);
            this.gbSearch.Controls.Add(this.rbKeyword);
            this.gbSearch.Controls.Add(this.rbPublisher);
            this.gbSearch.Controls.Add(this.rbAuthor);
            this.gbSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSearch.Location = new System.Drawing.Point(0, 0);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(736, 107);
            this.gbSearch.TabIndex = 0;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Search Books";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(421, 75);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(165, 78);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(238, 20);
            this.txtKeywords.TabIndex = 2;
            this.txtKeywords.TextChanged += new System.EventHandler(this.txtKeywords_TextChanged);
            // 
            // cboPublishers
            // 
            this.cboPublishers.FormattingEnabled = true;
            this.cboPublishers.Location = new System.Drawing.Point(165, 50);
            this.cboPublishers.Name = "cboPublishers";
            this.cboPublishers.Size = new System.Drawing.Size(238, 21);
            this.cboPublishers.TabIndex = 1;
            // 
            // cboAuthors
            // 
            this.cboAuthors.FormattingEnabled = true;
            this.cboAuthors.Location = new System.Drawing.Point(165, 24);
            this.cboAuthors.Name = "cboAuthors";
            this.cboAuthors.Size = new System.Drawing.Size(238, 21);
            this.cboAuthors.TabIndex = 1;
            // 
            // rbKeyword
            // 
            this.rbKeyword.AutoSize = true;
            this.rbKeyword.Location = new System.Drawing.Point(12, 79);
            this.rbKeyword.Name = "rbKeyword";
            this.rbKeyword.Size = new System.Drawing.Size(112, 17);
            this.rbKeyword.TabIndex = 0;
            this.rbKeyword.Text = "Keyword Search : ";
            this.rbKeyword.UseVisualStyleBackColor = true;
            // 
            // rbPublisher
            // 
            this.rbPublisher.AutoSize = true;
            this.rbPublisher.Location = new System.Drawing.Point(12, 51);
            this.rbPublisher.Name = "rbPublisher";
            this.rbPublisher.Size = new System.Drawing.Size(133, 17);
            this.rbPublisher.TabIndex = 0;
            this.rbPublisher.Text = "Books from Publisher : ";
            this.rbPublisher.UseVisualStyleBackColor = true;
            // 
            // rbAuthor
            // 
            this.rbAuthor.AutoSize = true;
            this.rbAuthor.Checked = true;
            this.rbAuthor.Location = new System.Drawing.Point(12, 25);
            this.rbAuthor.Name = "rbAuthor";
            this.rbAuthor.Size = new System.Drawing.Size(112, 17);
            this.rbAuthor.TabIndex = 0;
            this.rbAuthor.TabStop = true;
            this.rbAuthor.Text = "Books by Author : ";
            this.rbAuthor.UseVisualStyleBackColor = true;
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(650, 5);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(72, 26);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.dgvBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5,
            this.Column8,
            this.Column7});
            this.dgvBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBooks.Location = new System.Drawing.Point(0, 107);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowHeadersVisible = false;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(736, 309);
            this.dgvBooks.TabIndex = 0;
            this.dgvBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellContentClick);
            this.dgvBooks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellDoubleClick);
            this.dgvBooks.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvBooks_DataError);
            this.dgvBooks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvBooks_KeyDown);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BookId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Book Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Title";
            this.Column2.HeaderText = "Title";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "AuthorName";
            this.Column3.HeaderText = "Author";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ISBN";
            this.Column4.HeaderText = "ISBN";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Media";
            this.Column6.HeaderText = "Media";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "SalesPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column5.HeaderText = "Price";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "View";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Text = "View...";
            this.Column8.UseColumnTextForButtonValue = true;
            this.Column8.Width = 50;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Edit";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Text = "Edit...";
            this.Column7.UseColumnTextForButtonValue = true;
            this.Column7.Width = 50;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdOk);
            this.panel1.Controls.Add(this.cmdClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 35);
            this.panel1.TabIndex = 1;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(562, 5);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(77, 25);
            this.cmdOk.TabIndex = 5;
            this.cmdOk.Text = "&Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Visible = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // fBookSearch
            // 
            this.AcceptButton = this.cmdSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(736, 451);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fBookSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Book Search";
            this.Load += new System.EventHandler(this.fBookSearch_Load);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.RadioButton rbAuthor;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.ComboBox cboPublishers;
        private System.Windows.Forms.ComboBox cboAuthors;
        private System.Windows.Forms.RadioButton rbKeyword;
        private System.Windows.Forms.RadioButton rbPublisher;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdOk;
    }
}