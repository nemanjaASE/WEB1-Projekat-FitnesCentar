using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Komentar
    {
        private Posetilac posetilac;
        private FitnesCentar fitnesCentar;
        private string tekst;
        private int ocena;

        public Posetilac Posetilac { get => posetilac; set => posetilac = value; }
        public FitnesCentar FitnesCentar { get => fitnesCentar; set => fitnesCentar = value; }
        public string Tekst { get => tekst; set => tekst = value; }
        public int Ocena { get => ocena; set => ocena = value; }
    }
}