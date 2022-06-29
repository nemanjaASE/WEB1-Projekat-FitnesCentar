using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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
            if(posetilac.KorisnickoIme == null || posetilac.KorisnickoIme.Length == 0)
                return BadRequest();
            if(posetilac.Lozinka == null || posetilac.Lozinka.Length == 0)
                return BadRequest();
            if (posetilac.Ime == null || posetilac.Ime.Length == 0)
                return BadRequest();
            if (posetilac.Prezime == null || posetilac.Prezime.Length == 0)
                return BadRequest();
            if (posetilac.Email == null || posetilac.Email.Length == 0 || !IsValid(posetilac.Email))
                return BadRequest();
            if (posetilac.DatumRodjenja == null || posetilac.DatumRodjenja.Length == 0)
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

        private bool IsValid(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}