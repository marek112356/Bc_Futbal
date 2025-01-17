﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BC_Futbal.Model;
using BC_Futbal.Setup;

namespace BC_Futbal.Forms
{
    public partial class CervenaKartaForm : Form
    {
        private const string fotkyAdresar = "Files\\Fotky\\";
        private const string kartyAdresar = "Files\\Karty\\";

        private Hrac prezentovanyHrac = null;
        private int pocetZobrazenychPanelov;
        private List<Panel> zobrazovane = null;
        private int sirka;

        public CervenaKartaForm(int s, int cas, Hrac hrac, bool sDruhouZltouKartou, FontyTabule pisma,
            string animZ, string animC)
        {
            InitializeComponent();

            sirka = s;
            casovac.Interval = 1000 * cas;
            string adresar = Directory.GetCurrentDirectory();
            if (animZ.Equals(string.Empty))
                pictureBox1.Image = null;
            else
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(adresar + "\\" + kartyAdresar + animZ);
                }
                catch
                {
                    pictureBox1.Image = null;
                }
            }

            if (animC.Equals(string.Empty))
                pictureBox2.Image = null;
            else
            {
                try
                {
                    pictureBox2.Image = Image.FromFile(adresar + "\\" + kartyAdresar + animC);
                }
                catch
                {
                    pictureBox2.Image = null;
                }
            }

            prezentovanyHrac = hrac;
            pocetZobrazenychPanelov = 1;
            zobrazovane = new List<Panel>();

            // Nastavenie velkosti zobrazovacej plochy - zvacsenie na pozadovanu velkost
            float pomer = (float)sirka / Width;
            Scale(new SizeF(pomer, pomer));

            LayoutSetter.NastavVelkostiElementov(this, pomer);

            if (prezentovanyHrac != null)
            {
                try
                {
                    if (prezentovanyHrac.FotkaImage != null)
                    {
                        fotkaPictureBox.Image = prezentovanyHrac.FotkaImage;
                        fotkaPB.Image = prezentovanyHrac.FotkaImage;
                    }
                    else
                    {
                        fotkaPictureBox.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                        fotkaPB.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                    }
                    
                }
                catch
                {
                    fotkaPictureBox.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                    fotkaPB.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                }

                cisloHracaLabel.Text = prezentovanyHrac.CisloDresu.ToString();
                chLabel.Text = cisloHracaLabel.Text;
                string identifikacia = prezentovanyHrac.Meno + " " + prezentovanyHrac.Priezvisko.ToUpper();

                menoHracaLabel.Text = identifikacia;
                mhLabel.Text = menoHracaLabel.Text;
            }

            cisloHracaLabel.Font = pisma.CreateCisloMenoPrezentaciaFont();
            menoHracaLabel.Font = pisma.CreateCisloMenoPrezentaciaFont();
            menoHracaLabel.ForeColor = Color.Black;
            chLabel.Font = pisma.CreateCisloMenoPrezentaciaFont();
            mhLabel.Font = pisma.CreateCisloMenoPrezentaciaFont();

            uvodnyPanel1.Visible = false;
            uvodnyPanel2.Visible = false;
            prezentacnyPanel1.Visible = false;
            prezentacnyPanel2.Visible = false;

            if (sDruhouZltouKartou)
            {
                zobrazovane.Add(uvodnyPanel1);
                uvodnyPanel1.Visible = true;

                if (prezentovanyHrac != null)
                    zobrazovane.Add(prezentacnyPanel1);
            }
            else
                BackColor = Color.Red;

            zobrazovane.Add(uvodnyPanel2);
            uvodnyPanel2.Visible = !sDruhouZltouKartou;

            if (prezentovanyHrac != null)
                zobrazovane.Add(prezentacnyPanel2);
        }

        private void CervenaKartaForm_Load(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length == 1)
            {
                Location = Screen.PrimaryScreen.WorkingArea.Location;
                MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                Left += (Screen.PrimaryScreen.Bounds.Width - sirka) / 2;
            }
            else
            {
                LayoutSetter.ZobrazNaDruhejObrazovke(this);
            }

            LayoutSetter.ZobrazNaDruhejObrazovke(this);
            SpustiCas();
        }

        private void Casovac_Tick(object sender, EventArgs e)
        {
            if (pocetZobrazenychPanelov == zobrazovane.Count)
            {
                ZastavCas();
                Close();
            }
            else
            {
                zobrazovane[pocetZobrazenychPanelov - 1].Visible = false;
                zobrazovane[pocetZobrazenychPanelov].Visible = true;
                if (zobrazovane[pocetZobrazenychPanelov] == uvodnyPanel2)
                    BackColor = Color.Red;
                pocetZobrazenychPanelov++;
            }
        }

        private void SpustiCas()
        {
            casovac.Enabled = true;
        }

        private void ZastavCas()
        {
            casovac.Enabled = false;
        }
    }
}
