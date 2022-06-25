using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Trener : Korisnik
    {
        private FitnesCentar fitnesCentar;
        private List<GrupniTrening> grupniTrening;

        public Trener(string korisnickoIme, string lozinka, string ime, string prezime, string email, string datumRodjenja, Uloga uloga)
            : base(korisnickoIme, lozinka, ime, prezime, email, datumRodjenja, uloga)
        {
            this.FitnesCentar = new FitnesCentar();
            this.GrupniTrening = new List<GrupniTrening>();
        }

        public FitnesCentar FitnesCentar { get => fitnesCentar; set => fitnesCentar = value; }
        public List<GrupniTrening> GrupniTrening { get => grupniTrening; set => grupniTrening = value; }
    }
}