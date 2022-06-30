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
        private static TrenerRepo trenerRepo = new TrenerRepo();

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
                if (gt.IsDeleted == 1)
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
            List<GrupniTrening> temp = new List<GrupniTrening>();
            foreach (var item in repo.DobaviGrupneTreninge())
            {
                if(item.IsDeleted == 0)
                {
                    temp.Add(item);
                }
            }
            return temp;
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
                if (item.IsDeleted == 1)
                    continue;
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

        public IHttpActionResult Put(GrupniTrening grupniTrening)
        {
            if (grupniTrening == null)
                return BadRequest();
            if (grupniTrening.Naziv == null || grupniTrening.Naziv.Length == 0)
                return BadRequest();
            if (grupniTrening.TipTreninga.ToString() == null || grupniTrening.TipTreninga.ToString().Length == 0)
                return BadRequest();
            if (grupniTrening.FitnesCentarr == null)
                return BadRequest();
            if (grupniTrening.DatumIVreme == null || grupniTrening.DatumIVreme.Length == 0)
                return BadRequest();

            int dan = DateTime.Now.Day;
            int godina = DateTime.Now.Year;
            int mesec = DateTime.Now.Month;
            int sat = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;

            List<string> invalidValueDate = new List<string>();
            int m = mesec;
            int d = dan;
            int g = godina;
            for (int i = 0; i < 3; i++)
            {
                if (d == 31 && (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12))
                {
                    m++;
                    d = 1;
                    invalidValueDate.Add("1" + (m).ToString() + godina.ToString());
                }
                else if (d == 30 && (m == 4 || m == 6 || m == 9 || m == 11))
                { 
                    m++;
                    d = 1;
                    invalidValueDate.Add("1" + (m).ToString() + g.ToString());
                }
                else if (d == 28 && m == 2 && DateTime.IsLeapYear(g))
                {
                    d = 1;
                    m = 3;
                    invalidValueDate.Add("1" + (m).ToString() + g.ToString());
                }
                else if (d == 29 && m == 2)
                {
                    d = 1;
                    m = 3;
                    invalidValueDate.Add("1" + (m).ToString() + g.ToString());
                }
                else if (d == 31 && m == 12)
                {
                    d = 1;
                    m = 1;
                    g++;
                    invalidValueDate.Add("1" + (m).ToString() + g.ToString());
                }
                else
                {
                    d++;
                    invalidValueDate.Add(d.ToString() + m.ToString() + g.ToString());
                }
            }


            string[] tokens = grupniTrening.DatumIVreme.Split(' ');
            int tempDan;
            if(!int.TryParse(tokens[0].Split('/')[0], out tempDan))
            {
                return BadRequest();
            }
            int tempMesec;
            if(!int.TryParse(tokens[0].Split('/')[1],out tempMesec))
            {
                return BadRequest();
            }
            int tempGodina;
            if (!int.TryParse(tokens[0].Split('/')[2], out tempGodina))
            {
                return BadRequest();
            }

            if (tokens[1].Length < 5)
                return BadRequest();

            int tempSat;
            if(!int.TryParse(tokens[1].Split(':')[0], out tempSat))
            {
                return BadRequest();
            }
            int tempMin;
            if (!int.TryParse(tokens[1].Split(':')[1], out tempMin))
            {
                return BadRequest();
            }

            if (tempGodina < godina)
                return BadRequest();
            if (tempMesec < mesec && tempGodina == godina)
                return BadRequest();
            if (tempDan <= dan && tempGodina == godina && tempMesec == mesec)
                return BadRequest();
            if (tempSat <= sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                return BadRequest();
            if (tempMin <= min && tempSat == sat && tempDan == dan && tempGodina == godina && tempMesec == mesec)
                return BadRequest();


            if(invalidValueDate.Contains(tempDan.ToString() + tempMesec.ToString() + tempGodina.ToString()))
                return BadRequest();

            repo.IzmeniGrupniTrening(grupniTrening);
            return StatusCode(HttpStatusCode.OK);
        }

        public IHttpActionResult Delete(long id)
        {
            if (repo.DeleteById(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
