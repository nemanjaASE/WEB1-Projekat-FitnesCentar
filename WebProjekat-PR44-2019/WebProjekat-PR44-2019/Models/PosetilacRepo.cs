using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class PosetilacRepo
    {
        public PosetilacRepo()
        {
         
        }

        public List<Posetilac> DobaviPosetioce()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetioci = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            return posetioci;
        }

        public void DodajPosetioca(Posetilac posetilac)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetioci = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            posetioci.Add(posetilac);
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(posetioci));
        }

        public Posetilac GetPosetilac(string korisnickoIme)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetioci = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            foreach (var item in posetioci)
            {
                if (item.KorisnickoIme == korisnickoIme)
                    return item;
            }
            return null;
        }

        public void IzmeniPosetioca(Posetilac posetilac)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetilacTemp = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            foreach (var item in posetilacTemp)
            {
                if (item.KorisnickoIme == posetilac.KorisnickoIme)
                {
                    item.Ime = posetilac.Ime;
                    item.Prezime = posetilac.Prezime;
                    item.Lozinka = posetilac.Lozinka;
                    item.Email = posetilac.Email;
                    item.DatumRodjenja = posetilac.DatumRodjenja;
                    item.Pol = posetilac.Pol;
                    break;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(posetilacTemp));
        }

        public void DodajGrupniTrening(string username, string nazivGT)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetioci = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            foreach (var item in posetioci)
            {
                if(item.KorisnickoIme == username)
                {
                    item.GrupniTreninzi.Add(nazivGT);
                    break;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(posetioci));
        }

        public Posetilac DobaviPosetioca(string username)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            var jsonData = File.ReadAllText(path);
            var posetioci = JsonConvert.DeserializeObject<List<Posetilac>>(jsonData);
            foreach (var item in posetioci)
            {
                if (item.KorisnickoIme == username)
                {
                    return item;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(posetioci));
            return null;
        }
    }
}