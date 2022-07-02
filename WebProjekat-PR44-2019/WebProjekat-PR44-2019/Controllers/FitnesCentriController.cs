using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class FitnesCentriController : ApiController
    {
        private static FitnesCentriRepo repo = new FitnesCentriRepo();
        private static GrupniTreningRepo grupniRepo = new GrupniTreningRepo();
        private static TrenerRepo trenerRepo = new TrenerRepo();

        public IEnumerable<FitnesCentar> Get()
        {
            List<FitnesCentar> fitnesCentri = repo.DobaviFitnesCentre();

            int n = fitnesCentri.Count();
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (String.Compare(fitnesCentri[j].Naziv, fitnesCentri[min].Naziv) < 0)
                    {
                        min = j;
                    }
                   
                }
                if (min != i)
                {
                    var tmp = fitnesCentri[i];
                    fitnesCentri[i] = fitnesCentri[min];
                    fitnesCentri[min] = tmp;
                }
            }
            return fitnesCentri;
        }

        public FitnesCentar Get(string naziv)
        {
            return repo.DobaviFitnesCentar(naziv);
        }

        public IHttpActionResult Put(FitnesCentar fitnesCentar)
        {
            if(fitnesCentar == null)
                return BadRequest();
            if (fitnesCentar.Naziv == null || fitnesCentar.Naziv.Length == 0)
                return BadRequest();
            if (fitnesCentar.Vlasnik == null || fitnesCentar.Vlasnik.Length == 0)
                return BadRequest();
            if (fitnesCentar.GodinaOtvaranja == null || fitnesCentar.GodinaOtvaranja.Length == 0)
                return BadRequest();
            if(fitnesCentar.CenaGT < 0)
                return BadRequest();
            if (fitnesCentar.CenaJGT < 0)
                return BadRequest();
            if (fitnesCentar.CenaJT < 0)
                return BadRequest();
            if (fitnesCentar.CenaJTP < 0)
                return BadRequest();
            if (fitnesCentar.CenaMC < 0)
                return BadRequest();

            if (repo.IzmeniFitnesCentar(fitnesCentar))
                return Ok(fitnesCentar);
            else
                return BadRequest();
        }

        public IHttpActionResult Delete(string naziv,string vlasnik)
        {
            List<GrupniTrening> grupniTreninzi = grupniRepo.DobaviGrupneTreninge();

            DateTime now = DateTime.Now;
            int dan = now.Day;
            int mesec = now.Month;
            int godina = now.Year;
            int sat = now.Hour;
            int min = now.Minute;
            foreach (var item in grupniTreninzi)
            {
         
                    string[] tokens = item.DatumIVreme.Split(' ');
                    int tempDan = int.Parse(tokens[0].Split('/')[0]);
                    int tempMesec = int.Parse(tokens[0].Split('/')[1]);
                    int tempGodina = int.Parse(tokens[0].Split('/')[2]);

                    int tempSat = int.Parse(tokens[1].Split(':')[0]);
                    int tempMin = int.Parse(tokens[1].Split(':')[1]);

                    if (tempGodina < godina)
                        continue;
                    if (tempMesec < mesec && tempGodina == godina)
                        continue;
                    if (tempDan < dan && tempGodina == godina && tempMesec == mesec)
                        continue;
                    if (tempSat < sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                        continue;
                    if (tempMin < min && tempSat == sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                        continue;
                if (item.IsDeleted == 1)
                    continue;

                if(item.FitnesCentarr.Naziv == naziv && item.FitnesCentarr.Vlasnik == vlasnik)
                    return BadRequest();
            }
            if(repo.DeleteFitnesCentar(naziv, vlasnik))
            {
                foreach (var trener in trenerRepo.DobaviTrenere())
                {
                    if(trener.FitnesCentar == naziv)
                    {
                        trenerRepo.ZabraniPristup(trener.KorisnickoIme);
                    }
                }
                return Ok();
            }
            else{
                return BadRequest();
            }
        }
    }
}
