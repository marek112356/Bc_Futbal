﻿using System;
using System.Drawing;

namespace LGR_Futbal.Setup
{
    [Serializable]
    public class FarebnaSchema
    {
        public int CasFarba_r { get; set; }
        public int PolcasFarba_r { get; set; }
        //public int PredlzenieFarba_r { get; set; }
        //public int KoniecFarba_r { get; set; }
        public int NadpisDomFarba_r { get; set; }
        public int NadpisHosFarba_r { get; set; }
        public int SkoreFarba_r { get; set; }
        public int CasFarba_g { get; set; }
        public int PolcasFarba_g { get; set; }
        //public int PredlzenieFarba_g { get; set; }
        //public int KoniecFarba_g { get; set; }
        public int NadpisDomFarba_g { get; set; }
        public int NadpisHosFarba_g { get; set; }
        public int SkoreFarba_g { get; set; }
        public int CasFarba_b { get; set; }
        public int PolcasFarba_b { get; set; }
        //public int PredlzenieFarba_b { get; set; }
        //public int KoniecFarba_b { get; set; }
        public int NadpisDomFarba_b { get; set; }
        public int NadpisHosFarba_b { get; set; }
        public int SkoreFarba_b { get; set; }

        public FarebnaSchema()
        {
            setCasFarba(Color.White);
            setPolcasFarba(Color.White);
            //setPredlzenieFarba(Color.White);
            //setKoniecFarba(Color.White);
            setNadpisDomFarba(Color.White);
            setNadpisHosFarba(Color.White);
            setSkoreFarba(Color.White);
        }

        public void setCasFarba(Color f)
        {
            CasFarba_r = f.R;
            CasFarba_g = f.G;
            CasFarba_b = f.B;
        }

        public void setPolcasFarba(Color f)
        {
            PolcasFarba_r = f.R;
            PolcasFarba_g = f.G;
            PolcasFarba_b = f.B;
        }

        //public void setPredlzenieFarba(Color f)
        //{
        //    PredlzenieFarba_r = f.R;
        //    PredlzenieFarba_g = f.G;
        //    PredlzenieFarba_b = f.B;
        //}

        //public void setKoniecFarba(Color f)
        //{
        //    KoniecFarba_r = f.R;
        //    KoniecFarba_g = f.G;
        //    KoniecFarba_b = f.B;
        //}

        public void setNadpisDomFarba(Color f)
        {
            NadpisDomFarba_r = f.R;
            NadpisDomFarba_g = f.G;
            NadpisDomFarba_b = f.B;
        }

        public void setNadpisHosFarba(Color f)
        {
            NadpisHosFarba_r = f.R;
            NadpisHosFarba_g = f.G;
            NadpisHosFarba_b = f.B;
        }

        public void setSkoreFarba(Color f)
        {
            SkoreFarba_r = f.R;
            SkoreFarba_g = f.G;
            SkoreFarba_b = f.B;
        }

        public Color CasFarba()
        {
            return Color.FromArgb(CasFarba_r, CasFarba_g, CasFarba_b);
        }

        public Color PolcasFarba()
        {
            return Color.FromArgb(PolcasFarba_r, PolcasFarba_g, PolcasFarba_b);
        }

        //public Color PredlzenieFarba()
        //{
        //    return Color.FromArgb(PredlzenieFarba_r, PredlzenieFarba_g, PredlzenieFarba_b);
        //}

        //public Color KoniecFarba()
        //{
        //    return Color.FromArgb(KoniecFarba_r, KoniecFarba_g, KoniecFarba_b);
        //}

        public Color NadpisDomFarba()
        {
            return Color.FromArgb(NadpisDomFarba_r, NadpisDomFarba_g, NadpisDomFarba_b);
        }

        public Color NadpisHosFarba()
        {
            return Color.FromArgb(NadpisHosFarba_r, NadpisHosFarba_g, NadpisHosFarba_b);
        }
        
        public Color SkoreFarba()
        {
            return Color.FromArgb(SkoreFarba_r, SkoreFarba_g, SkoreFarba_b);
        }
    }
}
