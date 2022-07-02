using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Trener : Korisnik
    {
        private string fitnesCentar;
        private List<string> grupniTrening;
        public int Blokiran { get; set; }

        public Trener(string korisnickoIme, string lozinka, string ime, string prezime, string email, string datumRodjenja, Uloga uloga)
            : base(korisnickoIme, lozinka, ime, prezime, email, datumRodjenja, uloga)
        {
        }

        public string FitnesCentar { get => fitnesCentar; set => fitnesCentar = value; }
        public List<string> GrupniTrening { get => grupniTrening; set => grupniTrening = value; }
    }
}