﻿namespace BC_Futbal.Forms
{
    partial class CervenaKartaSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CervenaKartaSettingsForm));
            this.PotvrditBtn = new System.Windows.Forms.Button();
            this.HraciLB = new System.Windows.Forms.ListBox();
            this.SpatBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PotvrditBtn
            // 
            this.PotvrditBtn.BackColor = System.Drawing.Color.Red;
            this.PotvrditBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PotvrditBtn.Location = new System.Drawing.Point(291, 12);
            this.PotvrditBtn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PotvrditBtn.Name = "PotvrditBtn";
            this.PotvrditBtn.Size = new System.Drawing.Size(174, 78);
            this.PotvrditBtn.TabIndex = 552;
            this.PotvrditBtn.Text = "Potvrdiť";
            this.PotvrditBtn.UseVisualStyleBackColor = false;
            this.PotvrditBtn.Click += new System.EventHandler(this.PotvrditBtn_Click);
            // 
            // HraciLB
            // 
            this.HraciLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HraciLB.FormattingEnabled = true;
            this.HraciLB.ItemHeight = 26;
            this.HraciLB.Location = new System.Drawing.Point(10, 12);
            this.HraciLB.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.HraciLB.Name = "HraciLB";
            this.HraciLB.Size = new System.Drawing.Size(278, 394);
            this.HraciLB.TabIndex = 551;
            this.HraciLB.DoubleClick += new System.EventHandler(this.HraciLB_DoubleClick);
            // 
            // SpatBtn
            // 
            this.SpatBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SpatBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SpatBtn.Location = new System.Drawing.Point(291, 92);
            this.SpatBtn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SpatBtn.Name = "SpatBtn";
            this.SpatBtn.Size = new System.Drawing.Size(174, 78);
            this.SpatBtn.TabIndex = 550;
            this.SpatBtn.Text = "Návrat späť";
            this.SpatBtn.UseVisualStyleBackColor = false;
            this.SpatBtn.Click += new System.EventHandler(this.SpatBtn_Click);
            // 
            // CervenaKartaSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 430);
            this.Controls.Add(this.PotvrditBtn);
            this.Controls.Add(this.HraciLB);
            this.Controls.Add(this.SpatBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CervenaKartaSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Červená karta - nastavenia";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CervenaKartaSettingsForm_FormClosed);
            this.Click += new System.EventHandler(this.CervenaKartaSettingsForm_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PotvrditBtn;
        private System.Windows.Forms.ListBox HraciLB;
        private System.Windows.Forms.Button SpatBtn;
    }
}