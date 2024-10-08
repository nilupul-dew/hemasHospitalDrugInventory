namespace hemasHospitalDrugInventory
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.loginBtn = new System.Windows.Forms.Button();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.LoginPanel.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 287);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LoginPanel.Controls.Add(this.loginBtn);
            this.LoginPanel.Controls.Add(this.passwordText);
            this.LoginPanel.Controls.Add(this.usernameText);
            this.LoginPanel.Controls.Add(this.label1);
            this.LoginPanel.Controls.Add(this.pictureBox1);
            this.LoginPanel.Location = new System.Drawing.Point(101, 135);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(603, 290);
            this.LoginPanel.TabIndex = 2;
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.loginBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.loginBtn.Location = new System.Drawing.Point(397, 206);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(101, 29);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = false;
            // 
            // passwordText
            // 
            this.passwordText.BackColor = System.Drawing.SystemColors.Control;
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.passwordText.Location = new System.Drawing.Point(348, 137);
            this.passwordText.Multiline = true;
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(195, 30);
            this.passwordText.TabIndex = 2;
            this.passwordText.Text = "Password";
            this.passwordText.UseSystemPasswordChar = true;
            // 
            // usernameText
            // 
            this.usernameText.BackColor = System.Drawing.SystemColors.Control;
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.usernameText.Location = new System.Drawing.Point(348, 88);
            this.usernameText.Multiline = true;
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(195, 28);
            this.usernameText.TabIndex = 2;
            this.usernameText.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Small", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(400, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Login";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelHeader.Controls.Add(this.pictureBox2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 60);
            this.panelHeader.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(115, 57);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoginPanel);
            this.Controls.Add(this.panelHeader);
            this.Name = "Login";
            this.Text = "LogIn";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}