using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Korisnik
    {

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Email { get; set; }

        public string DatumRodjenja { get; set; }

        public Uloga Uloga { get; set; }

        public Korisnik(string korisnickoIme, 
                           string lozinka, 
                           string ime, 
                           string prezime, 
                           string email, 
                           string datumRodjenja, 
                           Uloga uloga)
        {
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.Ime = ime;
            this.Prezime = prezime;
            this.Email = email;
            this.DatumRodjenja = datumRodjenja;
            this.Uloga = uloga;
        }
    }
}