using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Vlasnik : Korisnik
    {
        private List<FitnesCentar> fitnesCentri;

        public Vlasnik(string korisnickoIme, string lozinka, string ime, string prezime, string email, string datumRodjenja, Uloga uloga, List<FitnesCentar>fitnesCentri)
            : base(korisnickoIme, lozinka, ime, prezime, email, datumRodjenja, uloga)
        {
            this.fitnesCentri = new List<FitnesCentar>();
        }

        public List<FitnesCentar> FitnesCentri { get => fitnesCentri; set => fitnesCentri = value; }
    }
}