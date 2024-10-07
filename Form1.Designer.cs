namespace hemasHospitalDrugInventory
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.transactionBtn = new System.Windows.Forms.Button();
            this.actorsBtn = new System.Windows.Forms.Button();
            this.inventoryBtn = new System.Windows.Forms.Button();
            this.homeBtn = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.notification = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.transactionBtn);
            this.panelMenu.Controls.Add(this.actorsBtn);
            this.panelMenu.Controls.Add(this.inventoryBtn);
            this.panelMenu.Controls.Add(this.homeBtn);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(158, 450);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // transactionBtn
            // 
            this.transactionBtn.FlatAppearance.BorderSize = 0;
            this.transactionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transactionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionBtn.Image = ((System.Drawing.Image)(resources.GetObject("transactionBtn.Image")));
            this.transactionBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.transactionBtn.Location = new System.Drawing.Point(0, 230);
            this.transactionBtn.Name = "transactionBtn";
            this.transactionBtn.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.transactionBtn.Size = new System.Drawing.Size(158, 39);
            this.transactionBtn.TabIndex = 2;
            this.transactionBtn.Text = "  Transactions";
            this.transactionBtn.UseVisualStyleBackColor = true;
            this.transactionBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // actorsBtn
            // 
            this.actorsBtn.FlatAppearance.BorderSize = 0;
            this.actorsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actorsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actorsBtn.Image = ((System.Drawing.Image)(resources.GetObject("actorsBtn.Image")));
            this.actorsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.actorsBtn.Location = new System.Drawing.Point(0, 185);
            this.actorsBtn.Name = "actorsBtn";
            this.actorsBtn.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.actorsBtn.Size = new System.Drawing.Size(158, 39);
            this.actorsBtn.TabIndex = 2;
            this.actorsBtn.Text = "      Actors";
            this.actorsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.actorsBtn.UseVisualStyleBackColor = true;
            this.actorsBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // inventoryBtn
            // 
            this.inventoryBtn.FlatAppearance.BorderSize = 0;
            this.inventoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inventoryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryBtn.Image = ((System.Drawing.Image)(resources.GetObject("inventoryBtn.Image")));
            this.inventoryBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inventoryBtn.Location = new System.Drawing.Point(0, 140);
            this.inventoryBtn.Name = "inventoryBtn";
            this.inventoryBtn.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.inventoryBtn.Size = new System.Drawing.Size(158, 39);
            this.inventoryBtn.TabIndex = 2;
            this.inventoryBtn.Text = "      Inventory";
            this.inventoryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inventoryBtn.UseVisualStyleBackColor = true;
            this.inventoryBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.FlatAppearance.BorderSize = 0;
            this.homeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeBtn.Image = ((System.Drawing.Image)(resources.GetObject("homeBtn.Image")));
            this.homeBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.homeBtn.Location = new System.Drawing.Point(0, 95);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.homeBtn.Size = new System.Drawing.Size(158, 39);
            this.homeBtn.TabIndex = 2;
            this.homeBtn.Text = "      Home";
            this.homeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(158, 60);
            this.panelLogo.TabIndex = 1;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelHeader.Controls.Add(this.notification);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(158, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(762, 60);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // notification
            // 
            this.notification.Dock = System.Windows.Forms.DockStyle.Right;
            this.notification.FlatAppearance.BorderSize = 0;
            this.notification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.notification.Image = ((System.Drawing.Image)(resources.GetObject("notification.Image")));
            this.notification.Location = new System.Drawing.Point(687, 0);
            this.notification.Name = "notification";
            this.notification.Size = new System.Drawing.Size(75, 60);
            this.notification.TabIndex = 0;
            this.notification.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 450);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.Button transactionBtn;
        private System.Windows.Forms.Button actorsBtn;
        private System.Windows.Forms.Button inventoryBtn;
        private System.Windows.Forms.Button notification;
    }
}

