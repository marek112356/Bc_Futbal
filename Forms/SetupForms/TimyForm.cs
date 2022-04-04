﻿using LGR_Futbal.Properties;
using LGR_Futbal.Setup;
using LGR_Futbal.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LGR_Futbal.Databaza;

namespace LGR_Futbal.Forms
{
    public partial class TimyForm : Form
    {
        public event TimyVybraneHandler OnTimyVybrane;

        private DBTimy dbTimy = null;
        private DBHraci dbHraci= null;
        private List<FutbalovyTim> timy = null;
        private FutbalovyTim domaci = null;
        private FutbalovyTim hostia = null;

        public TimyForm(FutbalovyTim domaci, FutbalovyTim hostia, DBTimy dbt, DBHraci dbh)
        {
            InitializeComponent();

            dbTimy = dbt;
            dbHraci = dbh;
            timy = dbTimy.GetTimy();
            if (timy.Count == 0)
                AktivovatBtn.Enabled = false;
            else
            {
                foreach (FutbalovyTim t in timy)
                {
                    domaciLB.Items.Add(t.NazovTimu);
                    hostiaLB.Items.Add(t.NazovTimu);
                }

                domaciLB.SelectedIndex = 0;
                hostiaLB.SelectedIndex = 0;
            }
            this.domaci = domaci;
            this.hostia = hostia;
        }

        private void AktivovatBtn_Click(object sender, EventArgs e)
        {
            if (domaciLB.SelectedIndex == 0 )
            {
                domaci = null;
            }
            else
            {
                domaci = timy[domaciLB.SelectedIndex];
                domaci.ZoznamHracov = dbHraci.GetHraciVTime(timy[domaciLB.SelectedIndex].IdFutbalovyTim);
            }
            if (hostiaLB.SelectedIndex == 0)
            {
                hostia = null;
            }
            else
            {
                hostia = timy[hostiaLB.SelectedIndex - 1];
                hostia.ZoznamHracov = dbHraci.GetHraciVTime(timy[hostiaLB.SelectedIndex].IdFutbalovyTim);
            }
            OnTimyVybrane?.Invoke(domaci, hostia);
            Close();
        }

        private void OdznacenieVsetkeho(object sender, EventArgs e)
        {
            domaciLB.ClearSelected();
            hostiaLB.ClearSelected();
        }
    }
}