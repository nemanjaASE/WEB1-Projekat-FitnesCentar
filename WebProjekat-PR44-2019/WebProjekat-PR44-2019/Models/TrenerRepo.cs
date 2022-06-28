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
        public TrenerRepo()
        {
            
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
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/treneri.json");
            var jsonData = File.ReadAllText(path);
            var treneri = JsonConvert.DeserializeObject<List<Trener>>(jsonData);
            return treneri;
        }

        public void IzmeniTrenera(Trener trener)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/treneri.json");
            var jsonData = File.ReadAllText(path);
            var treneriTemp = JsonConvert.DeserializeObject<List<Trener>>(jsonData);
            foreach (var item in treneriTemp)
            {
                if (item.KorisnickoIme == trener.KorisnickoIme)
                {
                    item.Ime = trener.Ime;
                    item.Prezime = trener.Prezime;
                    item.Lozinka = trener.Lozinka;
                    item.Email = trener.Email;
                    item.DatumRodjenja = trener.DatumRodjenja;
                    break;
                }
            }
           
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(treneriTemp));
        }
    }
}