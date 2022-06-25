using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public abstract class Korisnik
    {
        private string korisnickoIme;
        private string lozinka;
        private string ime;
        private string prezime;
        private string email;
        private string datumRodjenja;
        private Uloga uloga;

        protected string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }

        protected string Lozinka { get => lozinka; set => lozinka = value; }

        protected string Ime { get => ime; set => ime = value; }

        protected string Prezime { get => prezime; set => prezime = value; }

        protected string Email { get => email; set => email = value; }

        protected string DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }

        protected Uloga Uloga { get => uloga; set => uloga = value; }

        protected Korisnik(string korisnickoIme, 
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