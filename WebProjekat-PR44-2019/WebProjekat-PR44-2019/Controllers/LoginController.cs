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
    }
}