namespace u2_books.Forms
{
    partial class fBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fBook));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSalePrice = new System.Windows.Forms.MaskedTextBox();
            this.cmdSearchTaxCode = new System.Windows.Forms.Button();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.txtPurchPrice = new System.Windows.Forms.TextBox();
            this.txtMinQty = new System.Windows.Forms.TextBox();
            this.txtStockLevel = new System.Windows.Forms.TextBox();
            this.txtSalePriceOld = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSearchSupplier = new System.Windows.Forms.Button();
            this.cmdSearchPublisher = new System.Windows.Forms.Button();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.txtMedia = new System.Windows.Forms.TextBox();
            this.cboGenre = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboDept = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.txtSynopsis = new System.Windows.Forms.TextBox();
            this.txtBookId = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdSearchAuthor = new System.Windows.Forms.Button();
            this.cmdOrderHistory = new System.Windows.Forms.Button();
            this.cmdPurchaseHistory = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Book Id:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Visible = false;
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Enabled = false;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Title:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Author : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "ISBN:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdSearchSupplier);
            this.groupBox2.Controls.Add(this.txtSalePrice);
            this.groupBox2.Controls.Add(this.cmdSearchTaxCode);
            this.groupBox2.Controls.Add(this.txtSupplier);
            this.groupBox2.Controls.Add(this.txtTaxCode);
            this.groupBox2.Controls.Add(this.txtPurchPrice);
            this.groupBox2.Controls.Add(this.txtMinQty);
            this.groupBox2.Controls.Add(this.txtStockLevel);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSalePriceOld);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(15, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 207);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Details";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(121, 24);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(100, 20);
            this.txtSalePrice.SkipLiterals = false;
            this.txtSalePrice.TabIndex = 6;
            this.txtSalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdSearchTaxCode
            // 
            this.cmdSearchTaxCode.Location = new System.Drawing.Point(200, 171);
            this.cmdSearchTaxCode.Name = "cmdSearchTaxCode";
            this.cmdSearchTaxCode.Size = new System.Drawing.Size(21, 21);
            this.cmdSearchTaxCode.TabIndex = 5;
            this.cmdSearchTaxCode.Text = "..";
            this.cmdSearchTaxCode.UseVisualStyleBackColor = true;
            this.cmdSearchTaxCode.Click += new System.EventHandler(this.cmdSearchTaxCode_Click);
            // 
            // txtTaxCode
            // 
            this.txtTaxCode.Location = new System.Drawing.Point(121, 171);
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.ReadOnly = true;
            this.txtTaxCode.Size = new System.Drawing.Size(66, 20);
            this.txtTaxCode.TabIndex = 4;
            this.txtTaxCode.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtPurchPrice
            // 
            this.txtPurchPrice.Location = new System.Drawing.Point(121, 139);
            this.txtPurchPrice.Name = "txtPurchPrice";
            this.txtPurchPrice.Size = new System.Drawing.Size(100, 20);
            this.txtPurchPrice.TabIndex = 3;
            this.txtPurchPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPurchPrice.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtMinQty
            // 
            this.txtMinQty.Location = new System.Drawing.Point(121, 109);
            this.txtMinQty.Name = "txtMinQty";
            this.txtMinQty.Size = new System.Drawing.Size(100, 20);
            this.txtMinQty.TabIndex = 2;
            this.txtMinQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinQty.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtStockLevel
            // 
            this.txtStockLevel.Location = new System.Drawing.Point(121, 56);
            this.txtStockLevel.Name = "txtStockLevel";
            this.txtStockLevel.Size = new System.Drawing.Size(100, 20);
            this.txtStockLevel.TabIndex = 1;
            this.txtStockLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStockLevel.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtSalePriceOld
            // 
            this.txtSalePriceOld.Location = new System.Drawing.Point(121, 24);
            this.txtSalePriceOld.Name = "txtSalePriceOld";
            this.txtSalePriceOld.Size = new System.Drawing.Size(100, 20);
            this.txtSalePriceOld.TabIndex = 0;
            this.txtSalePriceOld.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 139);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Purchase Price :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Sale Price";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 174);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Tax Code :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Min Quantity :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Stock Level : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSearchPublisher);
            this.groupBox1.Controls.Add(this.txtPublisher);
            this.groupBox1.Controls.Add(this.txtMedia);
            this.groupBox1.Controls.Add(this.cboGenre);
            this.groupBox1.Controls.Add(this.cboDept);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtISBN);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Catalog Information";
            // 
            // cmdSearchSupplier
            // 
            this.cmdSearchSupplier.Location = new System.Drawing.Point(248, 82);
            this.cmdSearchSupplier.Name = "cmdSearchSupplier";
            this.cmdSearchSupplier.Size = new System.Drawing.Size(21, 21);
            this.cmdSearchSupplier.TabIndex = 6;
            this.cmdSearchSupplier.Text = "..";
            this.cmdSearchSupplier.UseVisualStyleBackColor = true;
            this.cmdSearchSupplier.Click += new System.EventHandler(this.cmdSearchSupplier_Click);
            // 
            // cmdSearchPublisher
            // 
            this.cmdSearchPublisher.Location = new System.Drawing.Point(259, 122);
            this.cmdSearchPublisher.Name = "cmdSearchPublisher";
            this.cmdSearchPublisher.Size = new System.Drawing.Size(21, 21);
            this.cmdSearchPublisher.TabIndex = 4;
            this.cmdSearchPublisher.Text = "..";
            this.cmdSearchPublisher.UseVisualStyleBackColor = true;
            this.cmdSearchPublisher.Click += new System.EventHandler(this.cmdSearchPublisher_Click);
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(121, 82);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.ReadOnly = true;
            this.txtSupplier.Size = new System.Drawing.Size(121, 20);
            this.txtSupplier.TabIndex = 5;
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(108, 123);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.ReadOnly = true;
            this.txtPublisher.Size = new System.Drawing.Size(145, 20);
            this.txtPublisher.TabIndex = 3;
            // 
            // txtMedia
            // 
            this.txtMedia.Location = new System.Drawing.Point(108, 86);
            this.txtMedia.Name = "txtMedia";
            this.txtMedia.Size = new System.Drawing.Size(172, 20);
            this.txtMedia.TabIndex = 2;
            // 
            // cboGenre
            // 
            this.cboGenre.FormattingEnabled = true;
            this.cboGenre.Items.AddRange(new object[] {
            "BIOGRAPHY",
            "BUSINESS",
            "CLASSICS",
            "CRIME",
            "DARK_ROMANCE",
            "DRAMA",
            "FANTASY",
            "FICTION",
            "HISTORY",
            "HUMOUR",
            "LANGUAGE"});
            this.cboGenre.Location = new System.Drawing.Point(108, 55);
            this.cboGenre.Name = "cboGenre";
            this.cboGenre.Size = new System.Drawing.Size(172, 21);
            this.cboGenre.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Supplier : ";
            // 
            // cboDept
            // 
            this.cboDept.FormattingEnabled = true;
            this.cboDept.Items.AddRange(new object[] {
            "ADULT",
            "JUNIOR"});
            this.cboDept.Location = new System.Drawing.Point(108, 24);
            this.cboDept.Name = "cboDept";
            this.cboDept.Size = new System.Drawing.Size(172, 21);
            this.cboDept.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Publisher : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Department:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Media :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Genre:";
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(460, 37);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(119, 147);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImage.TabIndex = 1;
            this.picImage.TabStop = false;
            // 
            // txtSynopsis
            // 
            this.txtSynopsis.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSynopsis.Location = new System.Drawing.Point(19, 24);
            this.txtSynopsis.Multiline = true;
            this.txtSynopsis.Name = "txtSynopsis";
            this.txtSynopsis.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSynopsis.Size = new System.Drawing.Size(365, 386);
            this.txtSynopsis.TabIndex = 0;
            // 
            // txtBookId
            // 
            this.txtBookId.Location = new System.Drawing.Point(66, 34);
            this.txtBookId.Name = "txtBookId";
            this.txtBookId.ReadOnly = true;
            this.txtBookId.Size = new System.Drawing.Size(69, 20);
            this.txtBookId.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(66, 74);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(358, 52);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(108, 165);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(172, 20);
            this.txtISBN.TabIndex = 2;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(142, 32);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(66, 23);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "Find..";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Visible = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(66, 138);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(315, 20);
            this.txtAuthor.TabIndex = 3;
            this.txtAuthor.TextChanged += new System.EventHandler(this.txtAuthor_TextChanged);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(619, 150);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(109, 34);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(619, 112);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(109, 32);
            this.cmdUpdate.TabIndex = 7;
            this.cmdUpdate.Text = "&Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdSearchAuthor
            // 
            this.cmdSearchAuthor.Location = new System.Drawing.Point(387, 137);
            this.cmdSearchAuthor.Name = "cmdSearchAuthor";
            this.cmdSearchAuthor.Size = new System.Drawing.Size(21, 21);
            this.cmdSearchAuthor.TabIndex = 4;
            this.cmdSearchAuthor.Text = "..";
            this.cmdSearchAuthor.UseVisualStyleBackColor = true;
            this.cmdSearchAuthor.Click += new System.EventHandler(this.cmdSearchAuthor_Click);
            // 
            // cmdOrderHistory
            // 
            this.cmdOrderHistory.Location = new System.Drawing.Point(619, 37);
            this.cmdOrderHistory.Name = "cmdOrderHistory";
            this.cmdOrderHistory.Size = new System.Drawing.Size(109, 32);
            this.cmdOrderHistory.TabIndex = 8;
            this.cmdOrderHistory.Text = "Order History";
            this.cmdOrderHistory.UseVisualStyleBackColor = true;
            this.cmdOrderHistory.Click += new System.EventHandler(this.cmdOrderHistory_Click);
            // 
            // cmdPurchaseHistory
            // 
            this.cmdPurchaseHistory.Location = new System.Drawing.Point(619, 74);
            this.cmdPurchaseHistory.Name = "cmdPurchaseHistory";
            this.cmdPurchaseHistory.Size = new System.Drawing.Size(109, 32);
            this.cmdPurchaseHistory.TabIndex = 8;
            this.cmdPurchaseHistory.Text = "Purchase History";
            this.cmdPurchaseHistory.UseVisualStyleBackColor = true;
            this.cmdPurchaseHistory.Click += new System.EventHandler(this.cmdPurchaseHistory_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSynopsis);
            this.groupBox3.Location = new System.Drawing.Point(328, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 416);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Publisher\'s Description";
            // 
            // fBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 621);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.cmdPurchaseHistory);
            this.Controls.Add(this.cmdOrderHistory);
            this.Controls.Add(this.cmdSearchAuthor);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtBookId);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fBook";
            this.Text = "Book Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fBook_FormClosing);
            this.Load += new System.EventHandler(this.fBook_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBookId;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMedia;
        private System.Windows.Forms.ComboBox cboGenre;
        private System.Windows.Forms.ComboBox cboDept;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSynopsis;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdSearchAuthor;
        private System.Windows.Forms.Button cmdSearchSupplier;
        private System.Windows.Forms.Button cmdSearchPublisher;
        private System.Windows.Forms.TextBox txtStockLevel;
        private System.Windows.Forms.TextBox txtSalePriceOld;
        private System.Windows.Forms.TextBox txtMinQty;
        private System.Windows.Forms.TextBox txtPurchPrice;
        private System.Windows.Forms.Button cmdSearchTaxCode;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.Button cmdOrderHistory;
        private System.Windows.Forms.Button cmdPurchaseHistory;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.MaskedTextBox txtSalePrice;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}