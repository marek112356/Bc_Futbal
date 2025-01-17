﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BC_Futbal.Model;

namespace BC_Futbal.Forms
{
    public delegate void HracCervenaKartaSelectedHandler(Hrac hrac);
    public partial class CervenaKartaSettingsForm : Form
    {
        public event HracCervenaKartaSelectedHandler OnHracCervenaKartaSelected;
        public event UdalostPridanaHandler OnUdalostPridana;

        private bool domaci = false;
        private bool uspech = false;
        private List<Hrac> zoznamHracov = null;
        private FutbalovyTim futbalovyTim = null;
        private Zapas zapas = null;
        private Karta karta = null;

        public CervenaKartaSettingsForm(FutbalovyTim tim, Zapas zapas, bool domaci, Karta karta)
        {
            InitializeComponent();

            this.domaci = domaci;
            futbalovyTim = tim;
            zoznamHracov = new List<Hrac>();
            this.zapas = zapas;
            this.karta = karta;

            if (tim != null)
            {
                foreach (Hrac h in tim.ZoznamHracov)
                {
                    if ((h.HraAktualnyZapas) && (!h.Nahradnik) && (!h.CervenaKarta))
                    {
                        zoznamHracov.Add(h);
                        if (!h.CisloDresu.Equals(string.Empty))
                            HraciLB.Items.Add(h.CisloDresu + ". " + h.Meno + " " + h.Priezvisko.ToUpper());
                        else
                            HraciLB.Items.Add(h.Meno + " " + h.Priezvisko.ToUpper());
                    }
                }
            }

        }

        private void PotvrdKartu()
        {
            if (OnHracCervenaKartaSelected != null)
            {
                karta.NazovTimu = domaci ? zapas.NazovDomaci : zapas.NazovHostia;
                karta.IdFutbalovyTim = futbalovyTim != null ? futbalovyTim.IdFutbalovyTim : 0;
                karta.TypKarty = 'C';
                if (futbalovyTim == null || HraciLB.SelectedIndex == -1)
                {
                    zapas.Udalosti.Add(karta);
                    uspech = true;
                    OnHracCervenaKartaSelected(null);
                }
                else
                {
                    karta.Hrac = zoznamHracov[HraciLB.SelectedIndex];
                    zapas.Udalosti.Add(karta);
                    uspech = true;
                    OnHracCervenaKartaSelected(zoznamHracov[HraciLB.SelectedIndex]);
                }
            }
            Close();
        }

        private void PotvrditBtn_Click(object sender, EventArgs e)
        {
            PotvrdKartu();
        }

        private void SpatBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HraciLB_DoubleClick(object sender, EventArgs e)
        {
            if (HraciLB.SelectedIndex >= 0)
                PotvrdKartu();
        }

        private void CervenaKartaSettingsForm_Click(object sender, EventArgs e)
        {
            HraciLB.ClearSelected();
        }

        private void CervenaKartaSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (uspech && OnUdalostPridana != null && Screen.AllScreens.Length != 1)
                OnUdalostPridana("ČERVENÁ KARTA ÚSPEŠNE PRIDANÁ");
        }
    }
}
