using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class LoginController : ApiController
    {
        private static VlasnikRepo vlasnikRepo = new VlasnikRepo();
        private static PosetilacRepo posetilacRepo = new PosetilacRepo();
        private static TrenerRepo trenerRepo = new TrenerRepo();
        public IHttpActionResult Post([FromBody]Korisnik korisnik)
        {
            if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);
            if(korisnik.Lozinka == null || korisnik.Lozinka.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            foreach (var vlasnik in vlasnikRepo.DobaviVlasnike())
            {
                if(vlasnik.KorisnickoIme == korisnik.KorisnickoIme
                    && vlasnik.Lozinka == korisnik.Lozinka)
                {
                    return Ok(vlasnik);
                }
            }
            foreach (var posetilac in posetilacRepo.DobaviPosetioce())
            {
                if (posetilac.KorisnickoIme == korisnik.KorisnickoIme
                    && posetilac.Lozinka == korisnik.Lozinka)
                {
                    return Ok(posetilac);
                }
            }
            foreach (var trener in trenerRepo.DobaviTrenere())
            {
                if (trener.KorisnickoIme == korisnik.KorisnickoIme
                    && trener.Lozinka == korisnik.Lozinka)
                {
                    if (trener.Blokiran == 1)
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        return Ok(trener);
                    }
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        public IHttpActionResult Get(string korisnickoIme)
        {
            foreach (var vlasnik in vlasnikRepo.DobaviVlasnike())
            {
                if (vlasnik.KorisnickoIme == korisnickoIme)
                {
                    return Ok(vlasnik);
                }
            }
            foreach (var posetilac in posetilacRepo.DobaviPosetioce())
            {
                if (posetilac.KorisnickoIme == korisnickoIme)
                {
                    return Ok(posetilac);
                }
            }
            foreach (var trener in trenerRepo.DobaviTrenere())
            {
                if (trener.KorisnickoIme == korisnickoIme)
                {
                    if(trener.Blokiran == 1)
                    {
                        return Unauthorized();
                    }
                    else {
                        return Ok(trener);
                    }
                }
            }
            return BadRequest();
        }

        public IHttpActionResult Put([FromBody]Korisnik korisnik)
        {
            if (!IsValid(korisnik))
            {
                return BadRequest();
            }
            foreach (var vlasnik in vlasnikRepo.DobaviVlasnike())
            {
                if (vlasnik.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    vlasnik.Ime = korisnik.Ime;
                    vlasnik.Prezime = korisnik.Prezime;
                    vlasnik.Lozinka = korisnik.Lozinka;
                    vlasnik.Email = korisnik.Email;
                    vlasnik.DatumRodjenja = korisnik.DatumRodjenja;
                    vlasnik.Pol = korisnik.Pol;
                    vlasnikRepo.IzmeniVlasnika(vlasnik);
                    
                    return Ok(vlasnik);
                }
            }
            foreach (var posetilac in posetilacRepo.DobaviPosetioce())
            {
                if (posetilac.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    posetilac.Ime = korisnik.Ime;
                    posetilac.Prezime = korisnik.Prezime;
                    posetilac.Lozinka = korisnik.Lozinka;
                    posetilac.Email = korisnik.Email;
                    posetilac.DatumRodjenja = korisnik.DatumRodjenja;
                    posetilac.Pol = korisnik.Pol;
                    posetilacRepo.IzmeniPosetioca(posetilac);
                    return Ok(posetilac);
                }
            }
            foreach (var trener in trenerRepo.DobaviTrenere())
            {
                if (trener.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    trener.Ime = korisnik.Ime;
                    trener.Prezime = korisnik.Prezime;
                    trener.Lozinka = korisnik.Lozinka;
                    trener.Email = korisnik.Email;
                    trener.DatumRodjenja = korisnik.DatumRodjenja;
                    trener.Pol = korisnik.Pol;
                    trenerRepo.IzmeniTrenera(trener);
                    return Ok(trener);
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        private bool IsValid(Korisnik korisnik)
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