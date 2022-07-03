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
    public class TrenerController : ApiController
    {
        private static TrenerRepo trenerRepo = new TrenerRepo();
        private static PosetilacRepo posetilacRepo = new PosetilacRepo();
        private static VlasnikRepo vlasnikRepo = new VlasnikRepo();
        private static FitnesCentriRepo fitnesCentriRepo = new FitnesCentriRepo();

        public IHttpActionResult Post(Trener trener)
        {
            if (posetilacRepo.GetPosetilac(trener.KorisnickoIme) == null
                            && vlasnikRepo.GetVlasnik(trener.KorisnickoIme) == null
                            && trenerRepo.GetTrener(trener.KorisnickoIme) == null)
            {
                if(!IsValid(trener))
                    return BadRequest();
                else
                {
                    trenerRepo.DodajTrenera(trener);
                    return Ok(trener);
                }

            }
            else
            {
                return BadRequest();
            }
        }
        public List<Trener> Get(string vlasnik)
        {
            List<Trener> treneri = trenerRepo.DobaviTrenere();
            List<Trener> retVal = new List<Trener>();
            foreach (var item in treneri)
            {
                FitnesCentar fitnesCentar = fitnesCentriRepo.DobaviFitnesCentar(item.FitnesCentar);
                if(item.Blokiran == 0 && fitnesCentar.Vlasnik == vlasnik)
                {
                    retVal.Add(item);
                }
            }
            return retVal;
        }
        public IHttpActionResult Put(string trener)
        {
            trenerRepo.ZabraniPristup(trener);
            return Ok(trener);
        }


        private bool IsValid(Trener trener)
        {
            bool isValid = true;
            if (trener == null)
            {
                return false;
            }else if (trener.KorisnickoIme == null || trener.KorisnickoIme.Length == 0)
            {
                isValid = false;
            }
            else if (trener.FitnesCentar == null || trener.FitnesCentar.Length == 0)
            {
                isValid = false;
            }
            else if (trener.Lozinka == null || trener.Lozinka.Length == 0)
            {
                isValid = false;
            }else if (trener.Ime == null || trener.Ime.Length == 0)
            {
                isValid = false;
            }else if (trener.Prezime == null || trener.Prezime.Length == 0)
            {
                isValid = false;
            }
            try
            {
                MailAddress address = new MailAddress(trener.Email);
            }
            catch (Exception)
            {
                isValid = false;
            }

            if (trener.DatumRodjenja == null)
                return false;

            string[] tokens = trener.DatumRodjenja.Split('/');
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
            }else if(tempMesec > 12 || tempMesec <= 0)
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
                if(tempDan > 31)
                {
                    isValid = false;
                }
                else if(DateTime.IsLeapYear(tempGodina) && tempMesec == 2 && tempDan > 29)
                {
                    isValid = false;
                }
                else if(!DateTime.IsLeapYear(tempGodina) && tempMesec == 2 && tempDan > 28)
                {
                    isValid = false;
                }
                else if(tempDan > 30 && (tempMesec == 4 || tempMesec == 6 || tempMesec == 9 || tempMesec == 11))
                {
                    isValid = false;
                }
            }

            if (trener.Pol == null || trener.Pol.Length == 0 || 
                (trener.Pol != "Muski" && trener.Pol != "Zenski"))
            {
                isValid = false;
            }
            return isValid;
        }
    }

}
