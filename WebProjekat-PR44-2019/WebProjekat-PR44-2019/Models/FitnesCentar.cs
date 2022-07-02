using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class FitnesCentar
    {
 

        public string Naziv { get; set; }

        public Adresa Adresa { get; set; }

        public string GodinaOtvaranja { get; set; }

        public string Vlasnik { get; set; }

        public double CenaMC { get; set; }

        public double CenaGT { get; set; }

        public double CenaJT { get; set; }

        public double CenaJGT { get; set; }

        public double CenaJTP { get; set; }

        public int IsDeleted { get; set; }
    }
}