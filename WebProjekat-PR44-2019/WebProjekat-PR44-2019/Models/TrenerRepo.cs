using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class TrenerRepo
    {
        private List<Trener> treneri;
        public TrenerRepo()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/treneri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel1 = JsonConvert.DeserializeObject<List<Trener>>(jsonString);
            treneri = new List<Trener>();

            foreach (var item in dataModel1)
            {
                Trener t = new Trener(item.KorisnickoIme,
                                         item.Lozinka, item.Ime,
                                         item.Prezime,
                                         item.Email,
                                         item.DatumRodjenja, Uloga.Trener);
                t.FitnesCentar = item.FitnesCentar;
                t.GrupniTrening = item.GrupniTrening;
                treneri.Add(t);   
            }
        }
        public Trener GetTrener(string korisnickoIme)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/treneri.json");
            var jsonData = File.ReadAllText(path);
            var treneri = JsonConvert.DeserializeObject<List<Trener>>(jsonData);
            foreach (var item in treneri)
            {
                if (item.KorisnickoIme == korisnickoIme)
                    return item;
            }
            return null;
        }
        public List<Trener> DobaviTrenere()
        {
            return this.treneri;
        }
    }
}