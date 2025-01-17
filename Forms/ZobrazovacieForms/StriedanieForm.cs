﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BC_Futbal.Model;
using BC_Futbal.Setup;

namespace BC_Futbal.Forms
{
    public partial class StriedanieForm : Form
    {
        private const string fotkyAdresar = "Pripojenie\\Fotky\\";

        private Hrac striedanyHrac = null;
        private Hrac striedajuciHrac = null;
        private bool prezentaciaSkoncila;
        private int sirka;

        public StriedanieForm(int s, int cas, string nazovMuzstva, Hrac striedany, Hrac striedajuci, FarbyPrezentacie farby, FontyTabule pisma)
        {
            InitializeComponent();

            string adresar = Directory.GetCurrentDirectory();
            casovac.Interval = 1000 * cas;
            sirka = s;
            striedanyHrac = striedany;
            striedajuciHrac = striedajuci;
            prezentaciaSkoncila = false;

            nazovLabel.Text = nazovMuzstva;

            float pomer = (float)sirka / Width;
            Scale(new SizeF(pomer, pomer));

            LayoutSetter.NastavVelkostiElementov(this, pomer);

            if (striedanyHrac != null)
            {
                try
                {
                    fotka1PictureBox.Image = striedanyHrac.FotkaImage;
                }
                catch
                {
                    fotka1PictureBox.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                }

                cisloHraca1Label.Text = striedanyHrac.CisloDresu.ToString();           

                menoHraca1Label.Text = striedanyHrac.Meno + " " + striedanyHrac.Priezvisko.ToUpper();
            }

            if (striedajuciHrac != null)
            {
                try
                {
                    fotka2PictureBox.Image = striedajuciHrac.FotkaImage;
                }
                catch
                {
                    fotka2PictureBox.Image = Image.FromFile(adresar + "\\" + fotkyAdresar + "Default.png");
                }

                cisloHraca2Label.Text = striedajuciHrac.CisloDresu.ToString();

                menoHraca2Label.Text = striedajuciHrac.Meno + " " + striedajuciHrac.Priezvisko.ToUpper();
            }

            nazovLabel.ForeColor = farby.GetNadpisFarba();
            label1.ForeColor = farby.GetNadpisFarba();
            
            nazovLabel.Font = pisma.CreateNazvyPrezentaciaFont();
            label1.Font = pisma.CreatePodnadpisPrezentaciaFont();

            cisloHraca1Label.Font = pisma.CreateStriedaniaFont();
            menoHraca1Label.Font = pisma.CreateStriedaniaFont();
            cisloHraca2Label.Font = pisma.CreateStriedaniaFont();
            menoHraca2Label.Font = pisma.CreateStriedaniaFont();

            prezentacnyPanel.Visible = false;
            uvodnyPanel.Visible = true;
        }

        private void StriedanieForm_Load(object sender, EventArgs e)
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

            SpustiCas();
        }

        private void Casovac_Tick(object sender, EventArgs e)
        {
            if ((striedanyHrac == null) || (striedajuciHrac == null))
            {
                ZastavCas();
                Close();
            }
            else
            {
                if (prezentaciaSkoncila)
                {
                    ZastavCas();
                    Close();
                }
                else
                {
                    uvodnyPanel.Visible = false;
                    prezentacnyPanel.Visible = true;
                    prezentaciaSkoncila = true;
                }
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
