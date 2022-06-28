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
        public VlasnikRepo()
        {

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
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/vlasnici.json");
            var jsonData = File.ReadAllText(path);
            var vlasnici = JsonConvert.DeserializeObject<List<Vlasnik>>(jsonData);
            return vlasnici;
        }

        public void IzmeniVlasnika(Vlasnik vlasnik)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/vlasnici.json");
            var jsonData = File.ReadAllText(path);
            var vlasniciTemp = JsonConvert.DeserializeObject<List<Vlasnik>>(jsonData);
            foreach (var item in vlasniciTemp)
            {
                if(item.KorisnickoIme == vlasnik.KorisnickoIme)
                {
                    item.Ime = vlasnik.Ime;
                    item.Prezime = vlasnik.Prezime;
                    item.Lozinka = vlasnik.Lozinka;
                    item.Email = vlasnik.Email;
                    item.DatumRodjenja = vlasnik.DatumRodjenja;
                    break;
                }
            }
          
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(vlasniciTemp));
        }
    }
}