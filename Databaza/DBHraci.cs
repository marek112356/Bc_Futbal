﻿using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
using BC_Futbal.Model;

namespace BC_Futbal.Databaza
{

    public class DBHraci
    {
        private OracleConnection conn = null;

        public DBHraci(Pripojenie pripojenie)
        {
            conn = pripojenie.GetConnection();
        }

        public int InsertHrac(Hrac h)
        {
            int id = 0;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string cmdQuery1 = "INSERT INTO osoba(meno, priezvisko, datum_narodenia, pohlavie) VALUES(:meno, :priezvisko, :datum_narodenia, :pohlavie)";
                OracleCommand cmd = new OracleCommand(cmdQuery1);
                OracleParameter[] param = new OracleParameter[4];
                param[0] = cmd.Parameters.Add("meno", OracleDbType.Varchar2);
                param[0].Value = h.Meno;
                param[1] = cmd.Parameters.Add("priezvisko", OracleDbType.Varchar2);
                param[1].Value = h.Priezvisko;
                param[2] = cmd.Parameters.Add("datum_narodenia", OracleDbType.Date);
                param[2].Value = h.DatumNarodenia;
                param[3] = cmd.Parameters.Add("pohlavie", OracleDbType.Char);
                if (h.Pohlavie != 'X')
                {

                    param[3].Value = h.Pohlavie;
                }
                else
                {
                    param[3].Value = null;
                }
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                string cmdQuery2 = "SELECT MAX(id_osoba) FROM osoba";
                cmd.CommandText = cmdQuery2;
                int ID = int.Parse(cmd.ExecuteScalar().ToString());

                byte[] blob = null;


                if (h.Fotka != null)
                {
                    FileStream fls;
                    fls = new FileStream(h.Fotka, FileMode.Open, FileAccess.Read);
                    blob = new byte[fls.Length];
                    fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                    fls.Close();
                }


                OracleParameter[] param1 = new OracleParameter[6];

                int? idTimu = null;
                if (h.IdFutbalovyTim != 0)
                    idTimu = h.IdFutbalovyTim;
                string pozicia = null;
                if (!h.Pozicia.Equals(string.Empty))
                    pozicia = h.Pozicia;
                string poznamka = null;
                if (!h.Poznamka.Equals(string.Empty))
                    poznamka = h.Poznamka;
                int? cislo_dresu = null;
                if (!h.CisloDresu.Equals(string.Empty))
                    cislo_dresu = Convert.ToInt32(h.CisloDresu);

                string cmdQuery3 = "INSERT INTO hrac(id_futbalovy_tim, id_osoba, poznamka, cislo_dresu, fotka, post) VALUES(:id_futbalovy_tim, :id_osoba, :poznamka, :cislo_dresu, :fotka, :post)";
                cmd.CommandText = cmdQuery3;
                param1[0] = cmd.Parameters.Add("id_futbalovy_tim", OracleDbType.Int32);
                param1[0].Value = idTimu;
                param1[1] = cmd.Parameters.Add("id_osoba", OracleDbType.Int32);
                param1[1].Value = ID;
                param1[2] = cmd.Parameters.Add("poznamka", OracleDbType.Varchar2);
                param1[2].Value = poznamka;
                param1[3] = cmd.Parameters.Add("cislo_dresu", OracleDbType.Int32);
                param1[3].Value = cislo_dresu;
                param1[4] = cmd.Parameters.Add("fotka", OracleDbType.Blob);
                param1[4].Value = blob;
                param1[5] = cmd.Parameters.Add("post", OracleDbType.Varchar2);
                param1[5].Value = pozicia;
                cmd.ExecuteNonQuery();

                string cmdQuery4 = "SELECT MAX(id_hrac) FROM hrac";
                cmd.CommandText = cmdQuery4;
                id = int.Parse(cmd.ExecuteScalar().ToString());


                conn.Close();
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
            return id;
        }

        public async Task<List<Hrac>> GetVsetciHraciAsync()
        {
            List<Hrac> hraci = new List<Hrac>();
            Hrac hrac;
            string cmdQuery = "SELECT * FROM hrac WHERE datum_ukoncenia IS NULL";
            try
            {
                await Task.Run(() =>
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    OracleCommand cmd = new OracleCommand(cmdQuery);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hrac = new Hrac();
                            hrac.IdHrac = reader.GetInt32(0);
                            hrac.IdOsoba = reader.GetInt32(1);
                            if (!reader.IsDBNull(2))
                                hrac.IdFutbalovyTim = reader.GetInt32(2);
                            if (!reader.IsDBNull(3))
                                hrac.Poznamka = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                hrac.CisloDresu = reader.GetString(4);
                            if (!reader.IsDBNull(6))
                            {
                                hrac.FotkaBlob = reader.GetOracleBlob(6).Value;
                                hrac.FotkaImage = BytesToImage(hrac.FotkaBlob);
                            }
                            if (!reader.IsDBNull(7))
                                hrac.Pozicia = reader.GetString(7);
                            NastavOsobneUdaje(hrac);
                            hraci.Add(hrac);
                        }
                    }
                    conn.Close();
                }).ConfigureAwait(false);
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
            return hraci;
        }


        public async Task<List<Hrac>> GetHraciVTime(int idTim)
        {
            List<Hrac> hraci = new List<Hrac>();
            Hrac hrac;
            string cmdQuery = "SELECT * FROM hrac WHERE datum_ukoncenia IS NULL AND id_futbalovy_tim = :id_timu";
            try
            {
                await Task.Run(() =>
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    OracleCommand cmd = new OracleCommand(cmdQuery);
                    OracleParameter param = new OracleParameter("id_timu", OracleDbType.Int32);
                    param.Value = idTim;
                    cmd.Parameters.Add(param);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hrac = new Hrac();
                            if (!reader.IsDBNull(0))
                                hrac.IdHrac = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                hrac.IdOsoba = reader.GetInt32(1);
                            if (!reader.IsDBNull(2))
                                hrac.IdFutbalovyTim = reader.GetInt32(2);
                            if (!reader.IsDBNull(3))
                                hrac.Poznamka = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                hrac.CisloDresu = reader.GetString(4);
                            if (!reader.IsDBNull(6))
                            {
                                hrac.FotkaBlob = reader.GetOracleBlob(6).Value;
                                hrac.FotkaImage = BytesToImage(hrac.FotkaBlob);
                            }
                            if (!reader.IsDBNull(7))
                                hrac.Pozicia = reader.GetString(7);
                            NastavOsobneUdaje(hrac);
                            hraci.Add(hrac);
                        }
                    }

                    conn.Close();
                }).ConfigureAwait(false);
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
            return hraci;
        }

        public async Task<List<Hrac>> GetNezaradeniHraci()
        {
            List<Hrac> hraci = new List<Hrac>();
            Hrac hrac;
            string cmdQuery = "SELECT * FROM hrac WHERE datum_ukoncenia IS NULL AND id_futbalovy_tim IS NULL";
            try
            {
                await Task.Run(() =>
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    OracleCommand cmd = new OracleCommand(cmdQuery);
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hrac = new Hrac();
                            if (!reader.IsDBNull(0))
                                hrac.IdHrac = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                hrac.IdOsoba = reader.GetInt32(1);
                            if (!reader.IsDBNull(2))
                                hrac.IdFutbalovyTim = reader.GetInt32(3);
                            if (!reader.IsDBNull(3))
                                hrac.Poznamka = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                hrac.CisloDresu = reader.GetString(4);
                            if (!reader.IsDBNull(6))
                            {
                                hrac.FotkaBlob = reader.GetOracleBlob(6).Value;
                                hrac.FotkaImage = BytesToImage(hrac.FotkaBlob);
                            }
                            if (!reader.IsDBNull(7))
                                hrac.Pozicia = reader.GetString(7);
                            NastavOsobneUdaje(hrac);
                            hraci.Add(hrac);
                        }
                    }
                    conn.Close();
                }).ConfigureAwait(false);
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
            return hraci;
        }

        public void UpdateHrac(Hrac h)
        {
            string cmdQuery1 = "UPDATE osoba SET meno = :meno, priezvisko = :priezvisko, datum_narodenia = :datum_narodenia, pohlavie = :pohlavie WHERE id_osoba = :id_osoba";
            string cmdQuery2 = "UPDATE hrac SET id_futbalovy_tim = :id_futbalovy_tim, poznamka = :poznamka, cislo_dresu = :cislo_dresu, fotka = :fotka, post = :post WHERE id_hrac = :id_hrac";
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleCommand cmd = new OracleCommand(cmdQuery1);
                OracleParameter[] param = new OracleParameter[5];

                param[0] = cmd.Parameters.Add("meno", OracleDbType.Varchar2);
                param[0].Value = h.Meno;
                param[1] = cmd.Parameters.Add("priezvisko", OracleDbType.Varchar2);
                param[1].Value = h.Priezvisko;
                param[2] = cmd.Parameters.Add("datum_narodenia", OracleDbType.Date);
                param[2].Value = h.DatumNarodenia;
                param[3] = cmd.Parameters.Add("pohlavie", OracleDbType.Char);
                if (h.Pohlavie != 'X')
                {
                    param[3].Value = h.Pohlavie;
                }
                else
                {
                    param[3].Value = null;
                }
                param[4] = cmd.Parameters.Add("id_osoba", OracleDbType.Int32);
                param[4].Value = h.IdOsoba;


                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                byte[] blob = null;

                if (h.Fotka != null)
                {
                    if (h.FotkaBlob != null)
                    {
                        blob = h.FotkaBlob;
                    }
                    else
                    {
                        FileStream fls;
                        fls = new FileStream(h.Fotka, FileMode.Open, FileAccess.Read);
                        blob = new byte[fls.Length];
                        fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                        fls.Close();
                    }
                }

                int? idTimu = null;
                if (h.IdFutbalovyTim != 0)
                    idTimu = h.IdFutbalovyTim;
                string pozicia = null;
                if (!h.Pozicia.Equals(string.Empty))
                    pozicia = h.Pozicia;
                string poznamka = null;
                if (!h.Poznamka.Equals(string.Empty))
                    poznamka = h.Poznamka;
                int? cislo_dresu = null;
                if (!h.CisloDresu.Equals(string.Empty))
                    cislo_dresu = Convert.ToInt32(h.CisloDresu);

                OracleCommand cmd2 = new OracleCommand(cmdQuery2);
                OracleParameter[] param2 = new OracleParameter[6];

                param2[0] = cmd2.Parameters.Add("id_futbalovy_tim", OracleDbType.Int32);
                param2[0].Value = idTimu;
                param2[1] = cmd2.Parameters.Add("poznamka", OracleDbType.Varchar2);
                param2[1].Value = poznamka;
                param2[2] = cmd2.Parameters.Add("cislo_dresu", OracleDbType.Int32);
                param2[2].Value = cislo_dresu;
                param2[3] = cmd2.Parameters.Add("fotka", OracleDbType.Blob);
                param2[3].Value = blob;
                param2[4] = cmd2.Parameters.Add("post", OracleDbType.Varchar2);
                param2[4].Value = pozicia;
                param2[5] = cmd2.Parameters.Add("id_hrac", OracleDbType.Int32);
                param2[5].Value = h.IdHrac;

                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();

                conn.Close();
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
        }

        public void OdstranHrac(Hrac h)
        {
            string cmdQuery = "UPDATE hrac SET datum_ukoncenia = SYSDATE WHERE id_hrac = :id_hrac";
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleCommand cmd = new OracleCommand(cmdQuery);
                OracleParameter param = new OracleParameter("id_hrac", OracleDbType.Int32);
                param.Value = h.IdHrac;
                cmd.Parameters.Add(param);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
        }

        public Hrac GetHrac(int idHrac)
        {
            Hrac hrac = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleCommand cmd = new OracleCommand("SELECT * FROM hrac WHERE id_hrac = :id_hrac");
                OracleParameter param = new OracleParameter("id_hrac", OracleDbType.Int32);
                param.Value = idHrac;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(param);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hrac = new Hrac();
                        if (!reader.IsDBNull(0))
                            hrac.IdHrac = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            hrac.IdOsoba = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            hrac.IdFutbalovyTim = reader.GetInt32(2);
                        if (!reader.IsDBNull(3))
                            hrac.Poznamka = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            hrac.CisloDresu = reader.GetString(4);
                        if (!reader.IsDBNull(6))
                        {
                            hrac.FotkaBlob = reader.GetOracleBlob(6).Value;
                            hrac.FotkaImage = BytesToImage(hrac.FotkaBlob);
                        }
                        if (!reader.IsDBNull(7))
                            hrac.Pozicia = reader.GetString(7);
                        NastavOsobneUdaje(hrac);
                    }
                }
                conn.Close();
            }
            catch
            {
                throw new Exception("Chyba pri práci s Databázou");
            }
            return hrac;
        }

        public void NastavOsobneUdaje(Osoba o)
        {
            string cmdQuery = "SELECT * FROM osoba WHERE id_osoba = :id_osoba";
            OracleParameter param = new OracleParameter("id_osoba", OracleDbType.Varchar2);
            OracleCommand cmd = new OracleCommand(cmdQuery);
            param.Value = o.IdOsoba;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(param);
            if (conn.State != ConnectionState.Open)
                conn.Open();

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    o.Meno = reader.GetString(1);
                    o.Priezvisko = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        o.DatumNarodenia = reader.GetDateTime(3);
                    if (!reader.IsDBNull(4))
                        o.Pohlavie = reader.GetString(4)[0];
                }
            }
        }

        public Image BytesToImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    return Image.FromStream(memoryStream);
                }
            }
            catch
            {
                throw new Exception("Problem pri konvertovaní z pola bytov na image");
            }
        }
    }
}
