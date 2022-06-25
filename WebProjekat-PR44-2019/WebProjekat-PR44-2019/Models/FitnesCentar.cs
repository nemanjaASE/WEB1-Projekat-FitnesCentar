using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class FitnesCentar
    {
        private string naziv;
        private Adresa adresa;
        private string godinaOtvaranja;
        private Vlasnik vlasnik;
        private double cenaMC;
        private double cenaJT;
        private double cenaJGT;
        private double cenaJTP;

        public string Naziv { get => naziv; set => naziv = value; }

        public Adresa Adresa { get => adresa; set => adresa = value; }

        public string GodinaOtvaranja { get => godinaOtvaranja; set => godinaOtvaranja = value; }

        public Vlasnik Vlasnik { get => vlasnik; set => vlasnik = value; }

        public double CenaMC { get => cenaMC; set => cenaMC = value; }

        public double CenaJT { get => cenaJT; set => cenaJT = value; }

        public double CenaJGT { get => cenaJGT; set => cenaJGT = value; }

        public double CenaJTP { get => cenaJTP; set => cenaJTP = value; }
    }
}