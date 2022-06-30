using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class KomentarController : ApiController
    {
        private KomentarRepo repo = new KomentarRepo();
        private FitnesCentriRepo fitnesRepo = new FitnesCentriRepo();

        public IHttpActionResult Post(Komentar komentar)
        {
            if (komentar == null)
                return BadRequest();
            if(komentar.FitnesCentar == null || komentar.FitnesCentar.Length == 0)
                return BadRequest();
            if(komentar.Posetilac == null || komentar.Posetilac.Length == 0)
                return BadRequest();
            if (komentar.Tekst == null || komentar.Tekst.Length == 0)
                return BadRequest();
            if (komentar.Posetilac.Length == 0)
                return BadRequest();

            komentar.Status = StatusKomentara.OBRADA;

            repo.DodajKomentarNaObradu(komentar);

            return Ok(komentar);
        }

        public IEnumerable<Komentar> Get(int option,string username)
        {
            List<Komentar> retVal = new List<Komentar>();
            if (option == 1)
            {
                List<Komentar> komentari = repo.DobaviKomentareUObradi();
                foreach (var item in komentari)
                {
                    FitnesCentar fc = fitnesRepo.DobaviFitnesCentar(item.FitnesCentar);
                    if (fc.Vlasnik == username)
                        retVal.Add(item);
                }
                return retVal;
            }
            else if (option == 2)
            {
                List<Komentar> komentari = repo.DobaviKomentareOdbijene();
                foreach (var item in komentari)
                {
                    FitnesCentar fc = fitnesRepo.DobaviFitnesCentar(item.FitnesCentar);
                    if (fc.Vlasnik == username)
                        retVal.Add(item);
                }
                return retVal;
            }
            else
                return null;
        }

        public IEnumerable<Komentar> Get(string naziv)
        {
            List<Komentar> komentari =  repo.DobaviKomentareObradjene();
            List<Komentar> temp = new List<Komentar>();
            foreach (var komentar in komentari)
            {
                if (komentar.FitnesCentar == naziv)
                    temp.Add(komentar);
            }
            return temp;
        }

        public IHttpActionResult Put(Komentar komentar)
        {
            repo.IzmeniKomentar(komentar);
            return Ok(komentar);
        }

    }
}
