﻿using System;
using System.Text;
using System.IO;
using System.Drawing;

namespace LGR_Futbal.Model
{
    public class Hrac : Osoba
    {
        public int IdHrac { get; set; }
        public DateTime DatumNastupu { get; set; }
        public FutbalovyTim FutbalovyTim { get; set; }
        public string CisloDresu { get; set; }
        public DateTime DatumUkoncenia { get; set; }
        public int IdPozicia { get; set; }
        public byte[] FotkaBlob { get; set; }
        public Image FotkaImage { get; set; }
        public string Fotka { get; set; }
        public bool ZltaKarta { get; set; }
        public bool CervenaKarta { get; set; }
        public String Poznamka { get; set; }
        public bool Nahradnik { get; set; }
        public bool Funkcionar { get; set; }
        public bool HraAktualnyZapas { get; set; }
        public string Pozicia { get; set; }
        public DateTime DatumNarodenia { get; set; }

        public Hrac()
        {

        }

        public int getVek()
        {
            var today = DateTime.Today;
            int age = today.Year - DatumNarodenia.Year;
            if (DatumNarodenia.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }

    

}