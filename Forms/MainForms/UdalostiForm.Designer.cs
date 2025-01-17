﻿namespace BC_Futbal.Forms
{
    partial class UdalostiForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skoreLabel = new System.Windows.Forms.Label();
            this.GenerovatCsvBtn = new System.Windows.Forms.Button();
            this.ZapasUdalostiDataGrid = new System.Windows.Forms.DataGridView();
            this.ColumnCas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPolcas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMinuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNadstMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHrac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPoznamka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUdalost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polcas1CB = new System.Windows.Forms.CheckBox();
            this.polcas2CB = new System.Windows.Forms.CheckBox();
            this.rohovyKopCB = new System.Windows.Forms.CheckBox();
            this.priamyKopCB = new System.Windows.Forms.CheckBox();
            this.nepriamyKopCB = new System.Windows.Forms.CheckBox();
            this.AktivovatFilterBtn = new System.Windows.Forms.Button();
            this.pokutovyKopCB = new System.Windows.Forms.CheckBox();
            this.golCB = new System.Windows.Forms.CheckBox();
            this.offsideCB = new System.Windows.Forms.CheckBox();
            this.outCB = new System.Windows.Forms.CheckBox();
            this.striedanieCB = new System.Windows.Forms.CheckBox();
            this.zltaKartaCB = new System.Windows.Forms.CheckBox();
            this.cervenaKartaCB = new System.Windows.Forms.CheckBox();
            this.timCB = new System.Windows.Forms.CheckBox();
            this.tim2CB = new System.Windows.Forms.CheckBox();
            this.PridatDoDatabazyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ZapasUdalostiDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // skoreLabel
            // 
            this.skoreLabel.AutoSize = true;
            this.skoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.skoreLabel.Location = new System.Drawing.Point(12, 9);
            this.skoreLabel.Name = "skoreLabel";
            this.skoreLabel.Size = new System.Drawing.Size(93, 33);
            this.skoreLabel.TabIndex = 2;
            this.skoreLabel.Text = "label2";
            // 
            // GenerovatCsvBtn
            // 
            this.GenerovatCsvBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GenerovatCsvBtn.Location = new System.Drawing.Point(996, 9);
            this.GenerovatCsvBtn.Name = "GenerovatCsvBtn";
            this.GenerovatCsvBtn.Size = new System.Drawing.Size(173, 30);
            this.GenerovatCsvBtn.TabIndex = 3;
            this.GenerovatCsvBtn.Text = "Vygenerovať súbor";
            this.GenerovatCsvBtn.UseVisualStyleBackColor = true;
            this.GenerovatCsvBtn.Click += new System.EventHandler(this.GenerovatCsvBtn_Click);
            // 
            // ZapasUdalostiDataGrid
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ZapasUdalostiDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ZapasUdalostiDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ZapasUdalostiDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCas,
            this.ColumnPolcas,
            this.ColumnMinuta,
            this.ColumnNadstMin,
            this.ColumnHrac,
            this.ColumnPoznamka,
            this.ColumnUdalost,
            this.ColumnTim});
            this.ZapasUdalostiDataGrid.Location = new System.Drawing.Point(12, 45);
            this.ZapasUdalostiDataGrid.Name = "ZapasUdalostiDataGrid";
            this.ZapasUdalostiDataGrid.Size = new System.Drawing.Size(1157, 520);
            this.ZapasUdalostiDataGrid.TabIndex = 4;
            // 
            // ColumnCas
            // 
            this.ColumnCas.HeaderText = "Čas";
            this.ColumnCas.Name = "ColumnCas";
            this.ColumnCas.Width = 130;
            // 
            // ColumnPolcas
            // 
            this.ColumnPolcas.HeaderText = "Polčas";
            this.ColumnPolcas.Name = "ColumnPolcas";
            this.ColumnPolcas.Width = 70;
            // 
            // ColumnMinuta
            // 
            this.ColumnMinuta.HeaderText = "Minúta";
            this.ColumnMinuta.Name = "ColumnMinuta";
            this.ColumnMinuta.Width = 70;
            // 
            // ColumnNadstMin
            // 
            this.ColumnNadstMin.HeaderText = "Nadstavená minúta";
            this.ColumnNadstMin.Name = "ColumnNadstMin";
            this.ColumnNadstMin.Width = 120;
            // 
            // ColumnHrac
            // 
            this.ColumnHrac.HeaderText = "Hráč";
            this.ColumnHrac.Name = "ColumnHrac";
            this.ColumnHrac.Width = 200;
            // 
            // ColumnPoznamka
            // 
            this.ColumnPoznamka.HeaderText = "Poznámka";
            this.ColumnPoznamka.Name = "ColumnPoznamka";
            this.ColumnPoznamka.Width = 200;
            // 
            // ColumnUdalost
            // 
            this.ColumnUdalost.HeaderText = "Udalosť";
            this.ColumnUdalost.Name = "ColumnUdalost";
            this.ColumnUdalost.Width = 200;
            // 
            // ColumnTim
            // 
            this.ColumnTim.HeaderText = "Tím";
            this.ColumnTim.Name = "ColumnTim";
            this.ColumnTim.Width = 124;
            // 
            // polcas1CB
            // 
            this.polcas1CB.AutoSize = true;
            this.polcas1CB.Location = new System.Drawing.Point(1175, 193);
            this.polcas1CB.Name = "polcas1CB";
            this.polcas1CB.Size = new System.Drawing.Size(69, 17);
            this.polcas1CB.TabIndex = 5;
            this.polcas1CB.Text = "1. polčas";
            this.polcas1CB.UseVisualStyleBackColor = true;
            // 
            // polcas2CB
            // 
            this.polcas2CB.AutoSize = true;
            this.polcas2CB.Location = new System.Drawing.Point(1175, 217);
            this.polcas2CB.Name = "polcas2CB";
            this.polcas2CB.Size = new System.Drawing.Size(69, 17);
            this.polcas2CB.TabIndex = 6;
            this.polcas2CB.Text = "2. polčas";
            this.polcas2CB.UseVisualStyleBackColor = true;
            // 
            // rohovyKopCB
            // 
            this.rohovyKopCB.AutoSize = true;
            this.rohovyKopCB.Location = new System.Drawing.Point(1175, 335);
            this.rohovyKopCB.Name = "rohovyKopCB";
            this.rohovyKopCB.Size = new System.Drawing.Size(79, 17);
            this.rohovyKopCB.TabIndex = 8;
            this.rohovyKopCB.Text = "rohový kop";
            this.rohovyKopCB.UseVisualStyleBackColor = true;
            // 
            // priamyKopCB
            // 
            this.priamyKopCB.AutoSize = true;
            this.priamyKopCB.Location = new System.Drawing.Point(1175, 286);
            this.priamyKopCB.Name = "priamyKopCB";
            this.priamyKopCB.Size = new System.Drawing.Size(77, 17);
            this.priamyKopCB.TabIndex = 9;
            this.priamyKopCB.Text = "priamy kop";
            this.priamyKopCB.UseVisualStyleBackColor = true;
            // 
            // nepriamyKopCB
            // 
            this.nepriamyKopCB.AutoSize = true;
            this.nepriamyKopCB.Location = new System.Drawing.Point(1175, 312);
            this.nepriamyKopCB.Name = "nepriamyKopCB";
            this.nepriamyKopCB.Size = new System.Drawing.Size(89, 17);
            this.nepriamyKopCB.TabIndex = 10;
            this.nepriamyKopCB.Text = "nepriamy kop";
            this.nepriamyKopCB.UseVisualStyleBackColor = true;
            // 
            // AktivovatFilterBtn
            // 
            this.AktivovatFilterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AktivovatFilterBtn.Location = new System.Drawing.Point(1176, 155);
            this.AktivovatFilterBtn.Name = "AktivovatFilterBtn";
            this.AktivovatFilterBtn.Size = new System.Drawing.Size(104, 29);
            this.AktivovatFilterBtn.TabIndex = 11;
            this.AktivovatFilterBtn.Text = "Aktivovať filter";
            this.AktivovatFilterBtn.UseVisualStyleBackColor = true;
            this.AktivovatFilterBtn.Click += new System.EventHandler(this.AktivovatFilterBtn_Click);
            // 
            // pokutovyKopCB
            // 
            this.pokutovyKopCB.AutoSize = true;
            this.pokutovyKopCB.Location = new System.Drawing.Point(1175, 358);
            this.pokutovyKopCB.Name = "pokutovyKopCB";
            this.pokutovyKopCB.Size = new System.Drawing.Size(91, 17);
            this.pokutovyKopCB.TabIndex = 13;
            this.pokutovyKopCB.Text = "pokutový kop";
            this.pokutovyKopCB.UseVisualStyleBackColor = true;
            // 
            // golCB
            // 
            this.golCB.AutoSize = true;
            this.golCB.Location = new System.Drawing.Point(1175, 381);
            this.golCB.Name = "golCB";
            this.golCB.Size = new System.Drawing.Size(40, 17);
            this.golCB.TabIndex = 14;
            this.golCB.Text = "gól";
            this.golCB.UseVisualStyleBackColor = true;
            // 
            // offsideCB
            // 
            this.offsideCB.AutoSize = true;
            this.offsideCB.Location = new System.Drawing.Point(1175, 404);
            this.offsideCB.Name = "offsideCB";
            this.offsideCB.Size = new System.Drawing.Size(57, 17);
            this.offsideCB.TabIndex = 15;
            this.offsideCB.Text = "offside";
            this.offsideCB.UseVisualStyleBackColor = true;
            // 
            // outCB
            // 
            this.outCB.AutoSize = true;
            this.outCB.Location = new System.Drawing.Point(1175, 427);
            this.outCB.Name = "outCB";
            this.outCB.Size = new System.Drawing.Size(41, 17);
            this.outCB.TabIndex = 16;
            this.outCB.Text = "out";
            this.outCB.UseVisualStyleBackColor = true;
            // 
            // striedanieCB
            // 
            this.striedanieCB.AutoSize = true;
            this.striedanieCB.Location = new System.Drawing.Point(1175, 450);
            this.striedanieCB.Name = "striedanieCB";
            this.striedanieCB.Size = new System.Drawing.Size(71, 17);
            this.striedanieCB.TabIndex = 17;
            this.striedanieCB.Text = "striedanie";
            this.striedanieCB.UseVisualStyleBackColor = true;
            // 
            // zltaKartaCB
            // 
            this.zltaKartaCB.AutoSize = true;
            this.zltaKartaCB.Location = new System.Drawing.Point(1175, 473);
            this.zltaKartaCB.Name = "zltaKartaCB";
            this.zltaKartaCB.Size = new System.Drawing.Size(69, 17);
            this.zltaKartaCB.TabIndex = 18;
            this.zltaKartaCB.Text = "žltá karta";
            this.zltaKartaCB.UseVisualStyleBackColor = true;
            // 
            // cervenaKartaCB
            // 
            this.cervenaKartaCB.AutoSize = true;
            this.cervenaKartaCB.Location = new System.Drawing.Point(1175, 496);
            this.cervenaKartaCB.Name = "cervenaKartaCB";
            this.cervenaKartaCB.Size = new System.Drawing.Size(92, 17);
            this.cervenaKartaCB.TabIndex = 19;
            this.cervenaKartaCB.Text = "červená karta";
            this.cervenaKartaCB.UseVisualStyleBackColor = true;
            // 
            // timCB
            // 
            this.timCB.AutoSize = true;
            this.timCB.Location = new System.Drawing.Point(1175, 240);
            this.timCB.Name = "timCB";
            this.timCB.Size = new System.Drawing.Size(60, 17);
            this.timCB.TabIndex = 20;
            this.timCB.Text = "domáci";
            this.timCB.UseVisualStyleBackColor = true;
            // 
            // tim2CB
            // 
            this.tim2CB.AutoSize = true;
            this.tim2CB.Location = new System.Drawing.Point(1175, 263);
            this.tim2CB.Name = "tim2CB";
            this.tim2CB.Size = new System.Drawing.Size(54, 17);
            this.tim2CB.TabIndex = 21;
            this.tim2CB.Text = "hostia";
            this.tim2CB.UseVisualStyleBackColor = true;
            // 
            // PridatDoDatabazyBtn
            // 
            this.PridatDoDatabazyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PridatDoDatabazyBtn.Location = new System.Drawing.Point(817, 9);
            this.PridatDoDatabazyBtn.Name = "PridatDoDatabazyBtn";
            this.PridatDoDatabazyBtn.Size = new System.Drawing.Size(173, 30);
            this.PridatDoDatabazyBtn.TabIndex = 22;
            this.PridatDoDatabazyBtn.Text = "Pridať do databázy";
            this.PridatDoDatabazyBtn.UseVisualStyleBackColor = true;
            this.PridatDoDatabazyBtn.Click += new System.EventHandler(this.PridatDoDatabazyBtn_Click);
            // 
            // UdalostiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 577);
            this.Controls.Add(this.PridatDoDatabazyBtn);
            this.Controls.Add(this.tim2CB);
            this.Controls.Add(this.timCB);
            this.Controls.Add(this.cervenaKartaCB);
            this.Controls.Add(this.zltaKartaCB);
            this.Controls.Add(this.striedanieCB);
            this.Controls.Add(this.outCB);
            this.Controls.Add(this.offsideCB);
            this.Controls.Add(this.golCB);
            this.Controls.Add(this.pokutovyKopCB);
            this.Controls.Add(this.AktivovatFilterBtn);
            this.Controls.Add(this.nepriamyKopCB);
            this.Controls.Add(this.priamyKopCB);
            this.Controls.Add(this.rohovyKopCB);
            this.Controls.Add(this.polcas2CB);
            this.Controls.Add(this.polcas1CB);
            this.Controls.Add(this.ZapasUdalostiDataGrid);
            this.Controls.Add(this.GenerovatCsvBtn);
            this.Controls.Add(this.skoreLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UdalostiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Udalosti";
            this.Shown += new System.EventHandler(this.UdalostiForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ZapasUdalostiDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label skoreLabel;
        private System.Windows.Forms.Button GenerovatCsvBtn;
        private System.Windows.Forms.DataGridView ZapasUdalostiDataGrid;
        private System.Windows.Forms.CheckBox polcas1CB;
        private System.Windows.Forms.CheckBox polcas2CB;
        private System.Windows.Forms.CheckBox rohovyKopCB;
        private System.Windows.Forms.CheckBox priamyKopCB;
        private System.Windows.Forms.CheckBox nepriamyKopCB;
        private System.Windows.Forms.Button AktivovatFilterBtn;
        private System.Windows.Forms.CheckBox pokutovyKopCB;
        private System.Windows.Forms.CheckBox golCB;
        private System.Windows.Forms.CheckBox offsideCB;
        private System.Windows.Forms.CheckBox outCB;
        private System.Windows.Forms.CheckBox striedanieCB;
        private System.Windows.Forms.CheckBox zltaKartaCB;
        private System.Windows.Forms.CheckBox cervenaKartaCB;
        private System.Windows.Forms.CheckBox timCB;
        private System.Windows.Forms.CheckBox tim2CB;
        private System.Windows.Forms.Button PridatDoDatabazyBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPolcas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMinuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNadstMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHrac;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPoznamka;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUdalost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTim;
    }
}