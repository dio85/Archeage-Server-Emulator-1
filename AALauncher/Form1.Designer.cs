﻿namespace AALauncher
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.emailBox = new System.Windows.Forms.TextBox();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btAuth = new System.Windows.Forms.Button();
            this.gamePathBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.loginServerIpBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // emailBox
            // 
            this.emailBox.Location = new System.Drawing.Point(15, 25);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(203, 20);
            this.emailBox.TabIndex = 0;
            this.emailBox.Text = "123@yanlongli.com";
            this.emailBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // passwordText
            // 
            this.passwordText.Location = new System.Drawing.Point(15, 78);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(203, 20);
            this.passwordText.TabIndex = 1;
            this.passwordText.Text = "123456";
            this.passwordText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // btAuth
            // 
            this.btAuth.Location = new System.Drawing.Point(462, 43);
            this.btAuth.Name = "btAuth";
            this.btAuth.Size = new System.Drawing.Size(133, 33);
            this.btAuth.TabIndex = 4;
            this.btAuth.Text = "Auth";
            this.btAuth.UseVisualStyleBackColor = true;
            this.btAuth.Click += new System.EventHandler(this.btAuth_Click);
            // 
            // gamePathBox
            // 
            this.gamePathBox.Location = new System.Drawing.Point(268, 78);
            this.gamePathBox.Name = "gamePathBox";
            this.gamePathBox.Size = new System.Drawing.Size(179, 20);
            this.gamePathBox.TabIndex = 6;
            this.gamePathBox.Text = "D:\\Program Files (x86)\\Glyph\\Games\\ArcheAge\\bin32\\archeage.exe";
            this.gamePathBox.Click += new System.EventHandler(this.gamePathBox_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(159, 141);
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(303, 13);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // loginServerIpBox
            // 
            this.loginServerIpBox.Location = new System.Drawing.Point(268, 25);
            this.loginServerIpBox.Name = "loginServerIpBox";
            this.loginServerIpBox.Size = new System.Drawing.Size(179, 20);
            this.loginServerIpBox.TabIndex = 8;
            this.loginServerIpBox.Text = "127.0.0.1:1237";
            this.loginServerIpBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Game Login Server IP:PORT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Path to game";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(14, 129);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(580, 40);
            this.textBox1.TabIndex = 11;
            this.textBox1.Visible = false;
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Location = new System.Drawing.Point(179, 211);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(248, 35);
            this.btStart.TabIndex = 12;
            this.btStart.Text = "Start Game";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Author: m0nax3@yandex.ru //汉化：admin@yanlongli.com";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 259);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loginServerIpBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gamePathBox);
            this.Controls.Add(this.btAuth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AALauncher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAuth;
        private System.Windows.Forms.TextBox gamePathBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox loginServerIpBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label label5;
    }
}

