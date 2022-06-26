using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class VlasnikRepo
    {
        private List<Vlasnik> vlasnici;
        public VlasnikRepo()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/vlasnici.json");
            string jsonString = File.ReadAllText(path);
            var dataModel1 = JsonConvert.DeserializeObject<List<Vlasnik>>(jsonString);
            vlasnici = new List<Vlasnik>();

            path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            jsonString = File.ReadAllText(path);
            var dataModel2 = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);

            foreach (var item in dataModel1)
            {
                Vlasnik vl = new Vlasnik(item.KorisnickoIme,
                                         item.Lozinka, item.Ime,
                                         item.Prezime,
                                         item.Email,
                                         item.DatumRodjenja, Uloga.Vlasnik,
                                         new List<FitnesCentar>());
             
                foreach (var fc in dataModel2)
                {
                    if(fc.Vlasnik.Equals(vl.KorisnickoIme))
                    {
                        vl.FitnesCentri.Add(new FitnesCentar()
                        { Naziv = fc.Naziv, Adresa = fc.Adresa, GodinaOtvaranja = fc.GodinaOtvaranja, Vlasnik = fc.Vlasnik,
                          CenaJGT = fc.CenaJGT, CenaJT = fc.CenaJT, CenaJTP = fc.CenaJTP, CenaMC = fc.CenaMC});
                    }
                }
                vlasnici.Add(vl);
            }
         
        }

        public Vlasnik GetVlasnik(string korisnickoIme)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/vlasnici.json");
            var jsonData = File.ReadAllText(path);
            var vlasnici = JsonConvert.DeserializeObject<List<Vlasnik>>(jsonData);
            foreach (var item in vlasnici)
            {
                if (item.KorisnickoIme == korisnickoIme)
                    return item;
            }
            return null;
        }

        public List<Vlasnik> DobaviVlasnike()
        {
            return this.vlasnici;
        }
    }
}