using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Posetilac : Korisnik
    {
        public Posetilac(string korisnickoIme, string lozinka, string ime, string prezime, string email, string datumRodjenja, Uloga uloga)
            : base(korisnickoIme, lozinka, ime, prezime, email, datumRodjenja, uloga)
        {
        }

        public List<string> GrupniTreninzi { get; set; }
    }
}