using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class GrupniTreningController : ApiController
    {
        private static GrupniTreningRepo repo = new GrupniTreningRepo();
        private static PosetilacRepo repoPosetilac = new PosetilacRepo();
        public IEnumerable<GrupniTrening> Get(string naziv, string ulica, string broj, string grad)
        {
            List<GrupniTrening> grupniTreninzi = repo.DobaviGrupneTreninge();
            List<GrupniTrening> temp = new List<GrupniTrening>();
            DateTime now = DateTime.Now;
            int dan = now.Day;
            int mesec = now.Month;
            int godina = now.Year;
            int sat = now.Hour;
            int min = now.Minute;

            foreach (var gt in grupniTreninzi)
            {
                string[] tokens = gt.DatumIVreme.Split(' ');
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

                if (gt.FitnesCentarr.Naziv == naziv 
                    && gt.FitnesCentarr.Adresa.Ulica == ulica
                    && gt.FitnesCentarr.Adresa.Broj.ToString() == broj
                    && gt.FitnesCentarr.Adresa.Grad == grad)
                {
                    temp.Add(gt);
                }
            }
            return temp;
        }

        public IEnumerable<GrupniTrening> Get()
        {
            return repo.DobaviGrupneTreninge();
        }

        public IEnumerable<GrupniTrening> Get(string username)
        {
            List<GrupniTrening> gt = repo.DobaviGrupneTreninge();
            List<GrupniTrening> retVal = new List<GrupniTrening>();

            DateTime now = DateTime.Now;
            int dan = now.Day;
            int mesec = now.Month;
            int godina = now.Year;
            int sat = now.Hour;
            int min = now.Minute;

            foreach (var item in gt)
            {
                if (item.Posetioci.Contains(username))
                {
                    string[] tokens = item.DatumIVreme.Split(' ');
                    int tempDan = int.Parse(tokens[0].Split('/')[0]);
                    int tempMesec = int.Parse(tokens[0].Split('/')[1]);
                    int tempGodina = int.Parse(tokens[0].Split('/')[2]);

                    int tempSat = int.Parse(tokens[1].Split(':')[0]);
                    int tempMin = int.Parse(tokens[1].Split(':')[1]);

                    if (tempGodina > godina)
                        continue;
                    if (tempMesec > mesec && tempGodina == godina)
                        continue;
                    if (tempDan > dan && tempGodina == godina && tempMesec == mesec)
                        continue;
                    if (tempSat > sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                        continue;
                    if (tempMin > min && tempSat == sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                        continue;

                    retVal.Add(item);
                }
            }
            return retVal;
        }

        public IHttpActionResult Put(string username, GrupniTrening grupniTrening)
        {   if (repoPosetilac.DobaviPosetioca(username) == null)
                return BadRequest();
            if(!repo.DodajPosetiocaNaTrening(grupniTrening, username))
                return BadRequest();
            repoPosetilac.DodajGrupniTrening(username, grupniTrening.Naziv);
            return StatusCode(HttpStatusCode.OK);

        }
    }
}
