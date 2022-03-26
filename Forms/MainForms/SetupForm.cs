﻿using LGR_Futbal.Properties;
using LGR_Futbal.Setup;
using LGR_Futbal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using LGR_Futbal.Databaza;

namespace LGR_Futbal.Forms
{
    #region Delegates 

    public delegate void PrenesDataHandler(bool zobrazovatPozadie, bool zobrazNastavenia, int sirka, int vyska, int cas, bool prerusenie, bool diakritika, int animacia);
    public delegate void ResetHandler();
    public delegate void ZhasniHandler();
    public delegate void RozsvietHandler();
    public delegate void NazvyLogaConfirmedHandler(string domNazov, Image domaciLogo, string hosNazov, Image hosLogo);
    public delegate void TimySelectedHandler(FutbalovyTim domTim, FutbalovyTim hosTim);
    public delegate void ObnovaFariebHandler();
    public delegate void ColorsLoadedHandler(FarebnaSchema fs);
    public delegate void ObnovaFontovHandler();
    public delegate void LanguageSelectedHandler(int cislo);
    public delegate void AnimacieKarietConfirmedHandler(string s1, string s2);
    public delegate void OnLayoutChangedHandler();

    #endregion

    public partial class SetupForm : Form
    {
        #region Konstanty

        private const string nazovProgramuString = "FutbalApp";
        private const string gifyAdresar = "Files\\Gify\\";
        private const string kartyAdresar = "Files\\Karty\\";
        private const string typyZapasovSubor = "Files\\Typy.xml";

        #endregion

        #region Atributy

        private bool aktivnaZmena = true;

        private FutbalovyTim domaciT = null;
        private FutbalovyTim hostiaT = null;

        private string originalFolder = null;
        private FarebnaSchema nastaveniaFarieb;
        private RozlozenieForm rf = null;
        private FontyForm fontyForm = null;

        private List<ParametreZapasu> zoznamTypovZapasu = null;
        private AnimacnaKonfiguracia konfig;
        private List<string> zoznamSuborov;
        private List<Rozhodca> rozhodcovia = null;

        public RozlozenieTabule rozlozenieTabule { get; set; }
        public FontyTabule Pisma { get; set; }

        private DBTimy dbtimy = null;
        private DBHraci dbhraci = null;
        private DBRozhodcovia dbrozhodcovia = null;
        private DBZapasy dbzapasy = null;

        private string zltaAnimacia;
        private string cervenaAnimacia;
        private int sirkaObr;
        private int vyskaObr;
        private int sirka;
        private int vyska;

        public event PrenesDataHandler OnDataConfirmed;
        public event ResetHandler OnReset;
        public event ZhasniHandler OnZhasnut;
        public event RozsvietHandler OnRozsvietit;
        public event NazvyLogaConfirmedHandler OnNazvyLogaConfirmed;
        public event TimySelectedHandler OnTimySelected;
        public event ObnovaFariebHandler OnObnovaFarieb;
        public event ColorsLoadedHandler OnColorsLoaded;
        public event ObnovaFontovHandler OnObnovaFontov;
        public event LanguageSelectedHandler OnLanguageSelected;
        public event AnimacieKarietConfirmedHandler OnAnimacieKarietConfirmed;
        public event OnLayoutChangedHandler OnLayoutChanged;

        #endregion

        #region Konstruktor a metody

        public SetupForm(int jazyk, bool zobrazitPozadie, bool zobrazitNastaveniaPoSpusteni, int sirka, int vyska, int dlzkaPolcasu, bool preruseniePovolene, bool diakritika,
            string logoDom, string logoHos, string nazovDom, string nazovHos,
            FutbalovyTim domaciTim, FutbalovyTim hostiaTim, string folder, int animacia,
            FontyTabule fonty, FarebnaSchema schema, AnimacnaKonfiguracia konfiguracia,
            string animZlta, string animCervena, List<Rozhodca> rozhodcovia,
            DBTimy dbt, DBHraci dbh, DBRozhodcovia dbr, DBZapasy dbz)
        {
            InitializeComponent();

            zltaAnimacia = animZlta;
            cervenaAnimacia = animCervena;
            this.vyska = vyska;
            this.sirka = sirka;

            if (jazyk == 1)
            {
                this.Text = "LGR Fotbal - nastavení";
                aktivovatButton.Text = aktivovatButton.Text.Replace("Uložiť", "Uložit");
                aktivovatButton.Text = aktivovatButton.Text.Replace("zmeny", "změny");
                zrusitButton.Text = zrusitButton.Text.Replace("Zrušiť", "Zrušit");
                tabulaButton.Text = tabulaButton.Text.Replace("TABUĽA", "TABULE");
                hraButton.Text = "PRŮBĚH\nHRY";
                TeamyButton.Text = "TÝMY\na LOGA";
                animacieButton.Text = "ANIMACE\nBRANEK";
                databazaButton.Text = "DATABÁZE\nHRÁČŮ";
                kartyButton.Text = "ANIMACE\nKARET";

                ovladace.TabPages[0].Text = "Výsledková tabule";
                ovladace.TabPages[1].Text = "Průběh hry";
                ovladace.TabPages[2].Text = "Týmy a loga";
                ovladace.TabPages[3].Text = "Animace branek";
                ovladace.TabPages[4].Text = "Animace karet";

                velkostGroupBox.Text = "Velikost zobrazovací plochy a jazyk";
                sirkaLabel.Text = "Šířka:";
                infoLabel.Text = "Poznámka: Poměr stran je fixován na 16:9!";
                aktLabel.Text = "Aktuální rozlišení obrazovky je:";
                label2.Text = "Animační čas [sec]:";
                jazykGroupBox.Text = "Jazykové nastavení";
                button12.Text = button12.Text.Replace("Zmeniť", "Změnit");
                pozadieCheckBox.Text = "Překrýt obrazovku černou barvou";
                initNastaveniaCheckBox.Text = "Zobrazovat nastavovací okno při spuštění aplikace";
                zhasniButton.Text = "ZHASNOUT";
                rozsvietButton.Text = "ROZSVÍTIT";

                createColorsButton.Text = "Změnit farební        \nnastavení barev       \n(změnit aktuální)       ";
                fontyButton.Text = "Nastavit fonty           \na velikosti písma         ";

                label1.Text = "Délka poločasu [min]:";
                prerusenieCheckBox.Text = "Povolit přerušení hry";
                vybratButton.Text = vybratButton.Text.Replace("Vybrať", "Vybrat");
                odstranitTypZapasuButton.Text = odstranitTypZapasuButton.Text.Replace("Odstrániť", "Odstranit");

                domaciLabel.Text = "DOMÁCÍ";
                hostiaLabel.Text = "HOSTÉ";

                diakritikaCheckBox.Text = "Odstranit diakritiku z názvů";
                zmenaLogaDom.Text = zmenaLogaDom.Text.Replace("Zmeniť", "Změnit");
                zmenaLogaHos.Text = zmenaLogaHos.Text.Replace("Zmeniť", "Změnit");
                zrusitLogoDom.Text = zrusitLogoDom.Text.Replace("Zrušiť", "Zrušit");
                zrusitLogoHos.Text = zrusitLogoHos.Text.Replace("Zrušiť", "Zrušit");
                nacitatDatabazaButton.Text = nacitatDatabazaButton.Text.Replace("Vybrať", "Vybrat");
                nacitatDatabazaButton.Text = nacitatDatabazaButton.Text.Replace("tímy", "týmy");
                nacitatDatabazaButton.Text = nacitatDatabazaButton.Text.Replace("z databázy", "z databáze");
                rozlozenieButton.Text = "Změnit rozložení";
                zrusitDatabazaButton.Text = "Zrušit propojení      \ns databází           ";

                checkBox1.Text = "Domácí - zobrazovat předdefinovanou animaci (góóól)";
                checkBox2.Text = "Hosté - zobrazovat předdefinovanou animaci (góóól)";
                label3.Text = "Animace pro gól domácích";
                label4.Text = "Animace pro gól hosty";
                importAnimacieBtn.Text = "Importovat soubor";

                groupBox1.Text = "Žlutá karta";
                zmenitZltaKartaBtn.Text = "Změnit obrázek (animaci)";
                zmenitCervenaKartaBtn.Text = "Změnit obrázek (animaci)";
                ZrusitZltaKartaBtn.Text = "Zrušit obrázek (animaci)";
                ZrusitCervenaKartaBtn.Text = "Zrušit obrázek (animaci)";
            }

            if (jazyk == 0)
            {
                skRadioButton.Checked = true;
                czRadioButton.Checked = false;
            }
            else
            {
                skRadioButton.Checked = false;
                czRadioButton.Checked = true;
            }

            dbtimy = dbt;
            dbhraci = dbh;
            dbrozhodcovia = dbr;
            dbzapasy = dbz;

            this.rozhodcovia = rozhodcovia;
            domaciT = domaciTim;
            hostiaT = hostiaTim;
            zoznamSuborov = new List<string>();
            konfig = konfiguracia;
            originalFolder = folder;
            InicializujNastaveniaAnimacii();
            nastaveniaFarieb = schema;
            nastavMuzstvoDomacibutton.Enabled = true;
            if (domaciT == null)
            {
                nastavMuzstvoDomacibutton.Enabled = false;
            }
            nastavMuzstvoHostiabutton.Enabled = true;
            if (hostiaT == null)
            {
                nastavMuzstvoHostiabutton.Enabled = false;
            }

            if (zltaAnimacia.Equals(string.Empty))
                pictureBox1.Image = null;
            else
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(originalFolder + "\\" + kartyAdresar + zltaAnimacia);
                }
                catch
                {
                    pictureBox1.Image = null;
                    zltaAnimacia = string.Empty;
                }
            }

            if (cervenaAnimacia.Equals(string.Empty))
                pictureBox2.Image = null;
            else
            {
                try
                {
                    pictureBox2.Image = Image.FromFile(originalFolder + "\\" + kartyAdresar + cervenaAnimacia);
                }
                catch
                {
                    pictureBox2.Image = null;
                    cervenaAnimacia = string.Empty;
                }
            }

            var primaryDisplay = Screen.AllScreens.ElementAtOrDefault(0);
            var screen = Screen.AllScreens.FirstOrDefault(s => s != primaryDisplay) ?? primaryDisplay;
            sirkaObr = screen.Bounds.Width;
            vyskaObr = screen.Bounds.Height;
            rozlisenieLabel.Text = sirkaObr.ToString() + " x " + vyskaObr.ToString();

            sirkaNumUpDown.Value = sirka;
            vyskaNumUpDown.Value = vyska;
            dlzkaPolcasuNumUpDown.Value = dlzkaPolcasu;
            prerusenieCheckBox.Checked = preruseniePovolene;
            diakritikaCheckBox.Checked = diakritika;

            pozadieCheckBox.Checked = zobrazitPozadie;
            initNastaveniaCheckBox.Checked = zobrazitNastaveniaPoSpusteni;

            ovladace.SelectedIndex = 2; 

            if ((domaciT != null) && (hostiaT != null))
            {
                ZobrazLoga(domaciT.LogoImage, hostiaT.LogoImage);
                domNazov.Text = domaciT.NazovTimu;
                hosNazov.Text = hostiaT.NazovTimu;
                zrusitDatabazaButton.Enabled = true;
            }
            else
            {
                domNazov.Text = nazovDom;
                hosNazov.Text = nazovHos;
                zrusitDatabazaButton.Enabled = false;
            }

            animaciaNumUpDown.Value = animacia;

            zoznamTypovZapasu = new List<ParametreZapasu>();
            typyZapasovListBox.Items.Clear();
            loadList();
        }

        private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveList();
        }

        private void ZobrazLoga(Image domaci, Image hostia)
        {
            try
            {
                logoDomaci.Image = domaci;
            }
            catch
            {
                logoDomaci.Image = null;
            }

            try
            {
                logoHostia.Image = hostia;
            }
            catch
            {
                logoHostia.Image = null;
            }
        }

        private void SirkaNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (aktivnaZmena)
            {
                // Prepocet vysky vzhladom na novu sirku v pomere 16:9
                int aktualnaHodnota = (int)sirkaNumUpDown.Value;
                aktivnaZmena = false;
                vyskaNumUpDown.Value = (9 * aktualnaHodnota) / 16;
                aktivnaZmena = true;
            }
        }

        private void VyskaNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (aktivnaZmena)
            {
                // Prepocet sirky vzhladom na novu vysku v pomere 16:9
                int aktualnaHodnota = (int)vyskaNumUpDown.Value;
                aktivnaZmena = false;
                sirkaNumUpDown.Value = (aktualnaHodnota * 16) / 9;
                aktivnaZmena = true;
            }
        }

        private void TabulaButton_Click(object sender, EventArgs e)
        {
            ovladace.SelectedIndex = 0;
        }

        private void HraButton_Click(object sender, EventArgs e)
        {
            ovladace.SelectedIndex = 1;
        }

        private void TeamyButton_Click(object sender, EventArgs e)
        {
            ovladace.SelectedIndex = 2;
        }

        private void animacieButton_Click(object sender, EventArgs e)
        {
            ovladace.SelectedIndex = 3;
        }

        private void kartyButton_Click(object sender, EventArgs e)
        {
            ovladace.SelectedIndex = 4;
        }

        private void AktivovatButton_Click(object sender, EventArgs e)
        {
            UlozNastaveniaAnimacii();

            if (OnAnimacieKarietConfirmed != null)
                OnAnimacieKarietConfirmed(zltaAnimacia, cervenaAnimacia);

            if (OnDataConfirmed != null)
            {
                int s = (int)sirkaNumUpDown.Value;
                int v = (int)vyskaNumUpDown.Value;
                bool poz = pozadieCheckBox.Checked;
                bool initSet = initNastaveniaCheckBox.Checked;
                int d = (int)dlzkaPolcasuNumUpDown.Value;
                bool p = prerusenieCheckBox.Checked;
                bool diak = diakritikaCheckBox.Checked;
                int animCas = (int)animaciaNumUpDown.Value;
                OnDataConfirmed(poz, initSet, s, v, d, p, diak, animCas);
            }

            if (OnNazvyLogaConfirmed != null)
            {
                string dn = domNazov.Text;
                string hn = hosNazov.Text;

                if (diakritikaCheckBox.Checked)
                {
                    dn = OdstranDiakritiku(dn);
                    hn = OdstranDiakritiku(hn);
                }
                if(domaciT != null && hostiaT != null)
                {
                    OnNazvyLogaConfirmed(dn, domaciT.LogoImage, hn, hostiaT.LogoImage);
                } 
                else
                {
                    OnNazvyLogaConfirmed(dn, logoDomaci.Image, hn, logoHostia.Image);
                }
                
            }

            if (OnTimySelected != null)
                OnTimySelected(domaciT, hostiaT);

            this.Close();
        }

        private void ZrusitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 1;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 2;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 3;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 4;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 5;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 6;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 7;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 8;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10) + 9;
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = (hodnota * 10);
            if (hodnota <= dlzkaPolcasuNumUpDown.Maximum)
                dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            int hodnota = (int)dlzkaPolcasuNumUpDown.Value;
            hodnota = hodnota / 10;
            dlzkaPolcasuNumUpDown.Value = hodnota;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Translate(1), nazovProgramuString, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (OnReset != null)
                {
                    OnReset();
                    this.Close();
                }
            }
        }

        private void ZhasniButton_Click(object sender, EventArgs e)
        {
            if (OnZhasnut != null)
            {
                OnZhasnut();
                this.Close();
            }
        }

        private void RozsvietButton_Click(object sender, EventArgs e)
        {
            if (OnRozsvietit != null)
            {
                OnRozsvietit();
                this.Close();
            }
        }

        private void ZmenaLogaDom_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "jpeg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    logoDomaci.Image = Image.FromFile(ofd.FileName);
                    if (domaciT != null)
                    {
                        domaciT.LogoImage = Image.FromFile(ofd.FileName);
                    }   
                }
            }
            catch
            {
                logoDomaci.Image = null;
            }
        }

        private void ZmenaLogaHos_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "jpeg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    logoHostia.Image = Image.FromFile(ofd.FileName);
                    if(hostiaT != null)
                    {
                        hostiaT.LogoImage = Image.FromFile(ofd.FileName);
                    } 
                }
            }
            catch
            {
                logoHostia.Image = null;
            }
        }

        private void ZrusitLogoDom_Click(object sender, EventArgs e)
        {
            logoDomaci.Image = null;
            domaciT.LogoImage = null;
        }

        private void ZrusitLogoHos_Click(object sender, EventArgs e)
        {
            logoHostia.Image = null;
            hostiaT.LogoImage = null;
        }

        private string OdstranDiakritiku(string vstup)
        {
            // Pomocna metoda na odstranenie diakritiky z retazca
            vstup = vstup.Normalize(NormalizationForm.FormD);

            StringBuilder stb = new StringBuilder();
            for (int i = 0; i < vstup.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(vstup[i]) != UnicodeCategory.NonSpacingMark)
                    stb.Append(vstup[i]);
            }

            return stb.ToString();
        }

        private void ZrusitDatabazaButton_Click(object sender, EventArgs e)
        {
            domaciT = null;
            hostiaT = null;
            zrusitDatabazaButton.Enabled = false;
            nastavMuzstvoDomacibutton.Enabled = false;
            nastavMuzstvoHostiabutton.Enabled = false;
        }

        private void NacitatDatabazaButton_Click(object sender, EventArgs e)
        {
            SelectForm selectform = new SelectForm(domaciT, hostiaT, dbtimy, dbhraci);
            selectform.OnTeamsSelected += Selectform_OnTeamsSelected;
            selectform.ShowDialog();
        }

        private void Selectform_OnTeamsSelected(FutbalovyTim t1, FutbalovyTim t2)
        {
            domaciT = t1;
            hostiaT = t2;
            if (domaciT != null && hostiaT != null)
            {
                ZobrazLoga(domaciT.LogoImage, hostiaT.LogoImage);
                domNazov.Text = domaciT.NazovTimu;
                hosNazov.Text = hostiaT.NazovTimu;
                nastavMuzstvoDomacibutton.Enabled = true;
                nastavMuzstvoHostiabutton.Enabled = true;
            } 
            else if (domaciT != null && hostiaT == null)
            {
                ZobrazLoga(domaciT.LogoImage, null);
                domNazov.Text = domaciT.NazovTimu;
                hosNazov.Text = "Hostia";
                nastavMuzstvoDomacibutton.Enabled = true;
                nastavMuzstvoHostiabutton.Enabled = false;
            }
            else if (domaciT == null && hostiaT != null)
            {
                ZobrazLoga(null, hostiaT.LogoImage);
                domNazov.Text = "Domáci";
                hosNazov.Text = hostiaT.NazovTimu;
                nastavMuzstvoDomacibutton.Enabled = false;
                nastavMuzstvoHostiabutton.Enabled = true;
            }
            //ZobrazLoga(domaciT.LogoImage, hostiaT.LogoImage);
                       
            zrusitDatabazaButton.Enabled = true;
        }

        private void DatabazaButton_Click(object sender, EventArgs e)
        {

            DatabazaForm df = new DatabazaForm(originalFolder, dbtimy, dbhraci, dbrozhodcovia, dbzapasy);
            df.Show();
        }

        private void loadList()
        {
            TextReader textReader = null;

            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<ParametreZapasu>));
                textReader = new StreamReader(originalFolder + "\\" + typyZapasovSubor);
                zoznamTypovZapasu = (List<ParametreZapasu>)deserializer.Deserialize(textReader);

                if (zoznamTypovZapasu.Count > 0)
                {
                    foreach (ParametreZapasu pz in zoznamTypovZapasu)
                        typyZapasovListBox.Items.Add(pz.toString());

                    typyZapasovListBox.SelectedIndex = 0;
                    odstranitTypZapasuButton.Enabled = true;
                    vybratButton.Enabled = true;
                }
            }
            catch
            {
                typyZapasovListBox.Items.Clear();
                zoznamTypovZapasu.Clear();

                odstranitTypZapasuButton.Enabled = false;
                vybratButton.Enabled = false;
            }
            finally
            {
                if (textReader != null)
                    textReader.Close();
            }
        }

        private void saveList()
        {
            TextWriter textWriter = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ParametreZapasu>));
                textWriter = new StreamWriter(originalFolder + "\\" + typyZapasovSubor);
                serializer.Serialize(textWriter, zoznamTypovZapasu);
            }
            catch
            {

            }
            finally
            {
                if (textWriter != null)
                    textWriter.Close();
            }
        }

        private void pridatTypZapasuButton_Click(object sender, EventArgs e)
        {
            TypZapasuForm tzf = new TypZapasuForm();
            tzf.onNovyTypZapasu += Tzf_onNovyTypZapasu;
            tzf.Show();
        }

        private void Tzf_onNovyTypZapasu(ParametreZapasu parZap)
        {
            zoznamTypovZapasu.Add(parZap);
            typyZapasovListBox.Items.Add(parZap.toString());
            typyZapasovListBox.SelectedIndex = 0;

            odstranitTypZapasuButton.Enabled = true;
            vybratButton.Enabled = true;
        }

        private void vybratButton_Click(object sender, EventArgs e)
        {
            int index = typyZapasovListBox.SelectedIndex;
            if (index >= 0)
            {
                ParametreZapasu pz = zoznamTypovZapasu[index];
                dlzkaPolcasuNumUpDown.Value = pz.Minuty;
                prerusenieCheckBox.Checked = pz.Prerusenie;
            }
        }

        private void odstranitTypZapasuButton_Click(object sender, EventArgs e)
        {
            int index = typyZapasovListBox.SelectedIndex;
            if (index >= 0)
            {
                zoznamTypovZapasu.RemoveAt(index);
                typyZapasovListBox.Items.RemoveAt(index);

                if (typyZapasovListBox.Items.Count > 0)
                    typyZapasovListBox.SelectedIndex = 0;
                else
                {
                    odstranitTypZapasuButton.Enabled = false;
                    vybratButton.Enabled = false;
                }
            }
        }

        private void createColorsButton_Click(object sender, EventArgs e)
        {
            FarbyForm ff = new FarbyForm(originalFolder + "\\Files\\FarebneNastavenia", nastaveniaFarieb);
            ff.OnColorsLoadedFF += Ff_OnColorsLoaded;
            ff.OnObnovaFariebFF += Ff_OnObnovaFarieb;
            ff.Show();
        }

        private void Ff_OnColorsLoaded(FarebnaSchema s)
        {
            if (OnColorsLoaded != null)
            {
                OnColorsLoaded(s);
            }
        }

        private void Ff_OnObnovaFarieb()
        {
            if (OnObnovaFarieb != null)
            {
                OnObnovaFarieb();
            }
        }

        private void fontyButton_Click(object sender, EventArgs e)
        {
            fontyForm = new FontyForm(originalFolder + "\\Files\\FontyNastavenia", Pisma);
            fontyForm.OnFontsConfirmed += Ff_OnFontsConfirmed;
            fontyForm.Show();
        }

        private void Ff_OnFontsConfirmed()
        {
            if (OnObnovaFontov != null)
            {
                this.Pisma = fontyForm.pisma;
                OnObnovaFontov();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (OnLanguageSelected != null)
            {
                int j = -1;
                if (skRadioButton.Checked)
                    j = 0;
                else if (czRadioButton.Checked)
                    j = 1;

                OnLanguageSelected(j);
            }
            this.Close();
        }

        private void SetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private string Translate(int cisloVety)
        {
            if (Settings.Default.Jazyk == 0)
            {
                switch (cisloVety)
                {
                    case 1: return "Naozaj chcete resetovať výsledkovú tabuľu?";
                    case 2: return "Naozaj chcete obnoviť výrobné nastavenia farieb?";
                    case 3: return "V databáze sa už nachádza súbor s rovnakým názvom!";
                    case 4: return "Naozaj chcete zrušiť obrázok (animáciu)?";
                }
            }
            else if (Settings.Default.Jazyk == 1)
            {
                switch (cisloVety)
                {
                    case 1: return "Opravdu chcete resetovat výsledkovou tabuli?";
                    case 2: return "Opravdu chcete obnovit výrobní nastavení barev?";
                    case 3: return "V databázi se již nachází soubor se stejným názvem!";
                    case 4: return "Opravdu chcete zrušit obrázek (animaci)?";
                }
            }

            return string.Empty;
        }

        private void InicializujNastaveniaAnimacii()
        {
            checkBox1.Checked = konfig.ZobrazitPreddefinovanuAnimaciuDomaci;
            checkBox2.Checked = konfig.ZobrazitPreddefinovanuAnimaciuHostia;

            FileInfo fi;
            string nazov;
            string[] pole = Directory.GetFiles(originalFolder + "\\" + gifyAdresar);
            for (int i = 0; i < pole.Length; i++)
            {
                if (pole[i].EndsWith(".gif"))
                {
                    fi = new FileInfo(pole[i]);
                    nazov = fi.Name;
                    zoznamSuborov.Add(pole[i]);

                    if (konfig.AnimacieDomaci.Contains(nazov))
                        animDomBox.Items.Add(nazov, true);
                    else
                        animDomBox.Items.Add(nazov, false);

                    if (konfig.AnimacieHostia.Contains(nazov))
                        animHosBox.Items.Add(nazov, true);
                    else
                        animHosBox.Items.Add(nazov, false);
                }
            }
        }

        private void UlozNastaveniaAnimacii()
        {
            konfig.ZobrazitPreddefinovanuAnimaciuDomaci = checkBox1.Checked;
            konfig.ZobrazitPreddefinovanuAnimaciuHostia = checkBox2.Checked;

            konfig.AnimacieDomaci.Clear();
            konfig.AnimacieHostia.Clear();

            FileInfo fi;
            string nazov;
            for (int i = 0; i < animDomBox.Items.Count; i++)
            {
                if (animDomBox.GetItemChecked(i))
                {
                    fi = new FileInfo(zoznamSuborov[i]);
                    nazov = fi.Name;
                    konfig.AnimacieDomaci.Add(nazov);
                }

                if (animHosBox.GetItemChecked(i))
                {
                    fi = new FileInfo(zoznamSuborov[i]);
                    nazov = fi.Name;
                    konfig.AnimacieHostia.Add(nazov);
                }
            }
        }

        private void importAnimacieBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.InitialDirectory = originalFolder;
            ofd.Filter = "gif files (*.gif)|*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);
                string nazov = fi.Name;
                string novyNazov = originalFolder + "\\" + gifyAdresar + "\\" + nazov;
                if (File.Exists(novyNazov))
                    MessageBox.Show(Translate(3), nazovProgramuString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    File.Copy(ofd.FileName, novyNazov);
                    zoznamSuborov.Add(novyNazov);
                    animDomBox.Items.Add(nazov, false);
                    animHosBox.Items.Add(nazov, false);
                }
            }
        }

        private void zmenitZltaKartaBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = originalFolder + "\\" + kartyAdresar;
            ofd.Multiselect = false;
            ofd.Filter = "jpeg files (*.jpg)|*.jpg|gif files (*.gif)|*.gif|png files (*.png)|*.png|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string povodnySubor = ofd.FileName;
                FileInfo fi = new FileInfo(povodnySubor);
                string nazov = fi.Name;
                string novyNazov = originalFolder + "\\" + kartyAdresar + nazov;
                if (!File.Exists(novyNazov))
                    File.Copy(povodnySubor, novyNazov);

                try
                {
                    pictureBox1.Image = Image.FromFile(novyNazov);
                    zltaAnimacia = nazov;
                }
                catch
                {
                    pictureBox1.Image = null;
                    zltaAnimacia = string.Empty;
                }
            }
        }

        private void zmenitCervenaKartaBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = originalFolder + "\\" + kartyAdresar;
            ofd.Multiselect = false;
            ofd.Filter = "jpeg files (*.jpg)|*.jpg|gif files (*.gif)|*.gif|png files (*.png)|*.png|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string povodnySubor = ofd.FileName;
                FileInfo fi = new FileInfo(povodnySubor);
                string nazov = fi.Name;
                string novyNazov = originalFolder + "\\" + kartyAdresar + nazov;
                if (!File.Exists(novyNazov))
                    File.Copy(povodnySubor, novyNazov);

                try
                {
                    pictureBox2.Image = Image.FromFile(novyNazov);
                    cervenaAnimacia = nazov;
                }
                catch
                {
                    pictureBox2.Image = null;
                    cervenaAnimacia = string.Empty;
                }
            }
        }

        private void ZrusitZltaKartaBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Translate(4), nazovProgramuString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pictureBox1.Image = null;
                zltaAnimacia = string.Empty;
            }
        }

        private void ZrusitCervenaKartaBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Translate(4), nazovProgramuString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pictureBox2.Image = null;
                cervenaAnimacia = string.Empty;
            }
        }

        private void rozlozenieButton_Click(object sender, EventArgs e)
        {
            rf = new RozlozenieForm(originalFolder + "\\Files\\RozlozenieNastavenia", rozlozenieTabule, sirka, vyska);
            this.rozlozenieTabule = rf.RozlozenieTabule;
            rf.OnLayoutConfirmed += On_LayoutConfirmed;
            rf.Show();
        }

        private void On_LayoutConfirmed()
        {
            if (rf != null)
            {
                this.rozlozenieTabule = rf.RozlozenieTabule;
                OnLayoutChanged?.Invoke();
            }

        }

        #endregion

        private void nastavMuzstvoDomacibutton_Click(object sender, EventArgs e)
        {
            HraciZapasForm hraciZapasForm = new HraciZapasForm(domaciT);
            hraciZapasForm.ShowDialog();
        }

        private void nastavMuzstvoHostiabutton_Click(object sender, EventArgs e)
        {
            HraciZapasForm hraciZapasForm = new HraciZapasForm(hostiaT);
            hraciZapasForm.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            RozhodcoviaForm rf = new RozhodcoviaForm(rozhodcovia, dbrozhodcovia);
            rf.Show();
        }
    }
}