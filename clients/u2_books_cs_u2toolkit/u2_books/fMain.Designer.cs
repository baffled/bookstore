namespace u2_books
{
    partial class fMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.salesOrderEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrderEntryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatePurchaseOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promotionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.shippingCostsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplierDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 657);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1002, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.maintainToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.connectToolStripMenuItem.Text = "&Connect...";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookSearchToolStripMenuItem,
            this.clientSearchToolStripMenuItem,
            this.orderSearchToolStripMenuItem,
            this.toolStripMenuItem1,
            this.salesOrderEntryToolStripMenuItem,
            this.purchaseOrderEntryMenuItem,
            this.generatePurchaseOrdersToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // bookSearchToolStripMenuItem
            // 
            this.bookSearchToolStripMenuItem.Name = "bookSearchToolStripMenuItem";
            this.bookSearchToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.bookSearchToolStripMenuItem.Text = "Book Search";
            this.bookSearchToolStripMenuItem.Click += new System.EventHandler(this.bookSearchToolStripMenuItem_Click);
            // 
            // clientSearchToolStripMenuItem
            // 
            this.clientSearchToolStripMenuItem.Name = "clientSearchToolStripMenuItem";
            this.clientSearchToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.clientSearchToolStripMenuItem.Text = "Client Search";
            this.clientSearchToolStripMenuItem.Click += new System.EventHandler(this.clientSearchToolStripMenuItem_Click);
            // 
            // orderSearchToolStripMenuItem
            // 
            this.orderSearchToolStripMenuItem.Name = "orderSearchToolStripMenuItem";
            this.orderSearchToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.orderSearchToolStripMenuItem.Text = "Order Search";
            this.orderSearchToolStripMenuItem.Click += new System.EventHandler(this.orderSearchToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // salesOrderEntryToolStripMenuItem
            // 
            this.salesOrderEntryToolStripMenuItem.Name = "salesOrderEntryToolStripMenuItem";
            this.salesOrderEntryToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.salesOrderEntryToolStripMenuItem.Text = "Sales Order Entry";
            this.salesOrderEntryToolStripMenuItem.Click += new System.EventHandler(this.salesOrderEntryToolStripMenuItem_Click);
            // 
            // purchaseOrderEntryMenuItem
            // 
            this.purchaseOrderEntryMenuItem.Name = "purchaseOrderEntryMenuItem";
            this.purchaseOrderEntryMenuItem.Size = new System.Drawing.Size(210, 22);
            this.purchaseOrderEntryMenuItem.Text = "Purchase Order Entry";
            this.purchaseOrderEntryMenuItem.Click += new System.EventHandler(this.purchaseOrderEntryMenuItem_Click);
            // 
            // generatePurchaseOrdersToolStripMenuItem
            // 
            this.generatePurchaseOrdersToolStripMenuItem.Name = "generatePurchaseOrdersToolStripMenuItem";
            this.generatePurchaseOrdersToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.generatePurchaseOrdersToolStripMenuItem.Text = "Generate Purchase Orders";
            this.generatePurchaseOrdersToolStripMenuItem.Click += new System.EventHandler(this.generatePurchaseOrdersToolStripMenuItem_Click);
            // 
            // maintainToolStripMenuItem
            // 
            this.maintainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.promotionsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.shippingCostsToolStripMenuItem,
            this.supplierDetailsToolStripMenuItem});
            this.maintainToolStripMenuItem.Name = "maintainToolStripMenuItem";
            this.maintainToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.maintainToolStripMenuItem.Text = "Maintain";
            this.maintainToolStripMenuItem.Click += new System.EventHandler(this.maintainToolStripMenuItem_Click);
            // 
            // promotionsToolStripMenuItem
            // 
            this.promotionsToolStripMenuItem.Name = "promotionsToolStripMenuItem";
            this.promotionsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.promotionsToolStripMenuItem.Text = "Promotions";
            this.promotionsToolStripMenuItem.Click += new System.EventHandler(this.promotionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem2.Text = "Sales Taxes";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // shippingCostsToolStripMenuItem
            // 
            this.shippingCostsToolStripMenuItem.Name = "shippingCostsToolStripMenuItem";
            this.shippingCostsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.shippingCostsToolStripMenuItem.Text = "Shipping Costs";
            this.shippingCostsToolStripMenuItem.Click += new System.EventHandler(this.shippingCostsToolStripMenuItem_Click);
            // 
            // supplierDetailsToolStripMenuItem
            // 
            this.supplierDetailsToolStripMenuItem.Name = "supplierDetailsToolStripMenuItem";
            this.supplierDetailsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.supplierDetailsToolStripMenuItem.Text = "Supplier Details";
            this.supplierDetailsToolStripMenuItem.Click += new System.EventHandler(this.supplierDetailsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 679);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "U2 Bookstore Demonstration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salesOrderEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maintainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shippingCostsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supplierDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem promotionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatePurchaseOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseOrderEntryMenuItem;
    }
}

