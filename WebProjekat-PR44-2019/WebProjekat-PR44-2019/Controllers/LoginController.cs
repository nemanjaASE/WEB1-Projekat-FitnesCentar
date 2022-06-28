using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                    return Ok(trener);
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
                    return Ok(trener);
                }
            }
            return BadRequest();
        }

        public IHttpActionResult Put([FromBody]Korisnik korisnik)
        {
            if (korisnik == null)
                return StatusCode(HttpStatusCode.BadRequest);

            if(korisnik.Ime == null || korisnik.Ime.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            if (korisnik.Prezime == null || korisnik.Prezime.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            if (korisnik.Lozinka == null || korisnik.Lozinka.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            if (korisnik.Email == null || korisnik.Email.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            if (korisnik.DatumRodjenja == null || korisnik.DatumRodjenja.Length == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            foreach (var vlasnik in vlasnikRepo.DobaviVlasnike())
            {
                if (vlasnik.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    vlasnik.Ime = korisnik.Ime;
                    vlasnik.Prezime = korisnik.Prezime;
                    vlasnik.Lozinka = korisnik.Lozinka;
                    vlasnik.Email = korisnik.Email;
                    vlasnik.DatumRodjenja = korisnik.DatumRodjenja;
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
                    trenerRepo.IzmeniTrenera(trener);
                    return Ok(trener);
                }
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
    }
}