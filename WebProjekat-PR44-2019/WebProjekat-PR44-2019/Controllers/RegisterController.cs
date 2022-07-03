using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class RegisterController : ApiController
    {
        private static VlasnikRepo vlasnikRepo = new VlasnikRepo();
        private static PosetilacRepo posetilacRepo = new PosetilacRepo();
        private static TrenerRepo trenerRepo = new TrenerRepo();
        public IHttpActionResult Post(Posetilac posetilac)
        {
            if(!IsValid(posetilac))
                return BadRequest();

            if (posetilacRepo.GetPosetilac(posetilac.KorisnickoIme) == null
                && vlasnikRepo.GetVlasnik(posetilac.KorisnickoIme) == null
                && trenerRepo.GetTrener(posetilac.KorisnickoIme ) == null)
            {
                posetilac.Uloga = Uloga.Posetilac;
                posetilac.GrupniTreninzi = new List<string>();
                posetilacRepo.DodajPosetioca(posetilac);
                return Ok(posetilac);
            }
            else
            {
                return BadRequest();
            }
        }

        private bool IsValid(Posetilac korisnik)
        {
            bool isValid = true;

            if (korisnik == null)
                return isValid = false;
            else if (korisnik.Ime == null || korisnik.Ime.Length == 0)
                isValid = false;
            else if (korisnik.Prezime == null || korisnik.Prezime.Length == 0)
                isValid = false;
            else if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme.Length == 0)
                isValid = false;
            else if (korisnik.Lozinka == null || korisnik.Lozinka.Length == 0)
                isValid = false;
            else if (korisnik.Email == null || korisnik.Email.Length == 0)
                isValid = false;
            else if (korisnik.DatumRodjenja.Length == 0)
                isValid = false;

            if (korisnik.DatumRodjenja == null)
                return false;

            string[] tokens = korisnik.DatumRodjenja.Split('/');
            if (tokens.Length != 3)
                return false;
            int tempGodina;
            if (!int.TryParse(tokens[2], out tempGodina))
            {
                isValid = false;
            }
            else if (tempGodina >= 2004 || tempGodina < 1980)
            {
                isValid = false;
            }

            int tempMesec;
            if (!int.TryParse(tokens[1], out tempMesec))
            {
                isValid = false;
            }
            else if (tempMesec > 12 || tempMesec <= 0)
            {
                isValid = false;
            }

            int tempDan;
            if (!int.TryParse(tokens[0], out tempDan))
            {
                isValid = false;
            }
            else
            {
                if (tempDan > 31)
                {
                    isValid = false;
                }
                else if (DateTime.IsLeapYear(tempGodina) && tempMesec == 2 && tempDan > 29)
                {
                    isValid = false;
                }
                else if (!DateTime.IsLeapYear(tempGodina) && tempMesec == 2 && tempDan > 28)
                {
                    isValid = false;
                }
                else if (tempDan > 30 && (tempMesec == 4 || tempMesec == 6 || tempMesec == 9 || tempMesec == 11))
                {
                    isValid = false;
                }
            }
            if (!EmailIsValid(korisnik.Email))
            {
                isValid = false;
            }
            if (korisnik.Pol == null || korisnik.Pol.Length == 0 ||
                (korisnik.Pol != "Muski" && korisnik.Pol != "Zenski"))
            {
                isValid = false;
            }

            return isValid;
        }
        private bool EmailIsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}