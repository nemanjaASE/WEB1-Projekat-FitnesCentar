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
        private List<Posetilac> posetioci;
        public PosetilacRepo()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/posetioci.json");
            string jsonString = File.ReadAllText(path);
            var dataModel1 = JsonConvert.DeserializeObject<List<Posetilac>>(jsonString);
            posetioci = new List<Posetilac>();

            path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            jsonString = File.ReadAllText(path);
            var dataModel2 = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonString);

            foreach (var item in dataModel1)
            {
                Posetilac p = new Posetilac(item.KorisnickoIme,
                                         item.Lozinka, item.Ime,
                                         item.Prezime,
                                         item.Email,
                                         item.DatumRodjenja, Uloga.Posetilac);
               
                foreach (var gt in dataModel2)
                {
                    if (gt.Posetioci.Contains(p.KorisnickoIme))
                    {
                        p.GrupniTreninzi.Add(new GrupniTrening()
                        {
                            DatumIVreme = gt.DatumIVreme,
                            FitnesCentarr = gt.FitnesCentarr,
                            MaxPosetilaca = gt.MaxPosetilaca,
                            Naziv = gt.Naziv,
                            Posetioci = gt.Posetioci,
                            TipTreninga = gt.TipTreninga,
                            TrajanjeTreninga = gt.TrajanjeTreninga,
                        });
                    }
                }
                posetioci.Add(p);
            }

        }

        public List<Posetilac> DobaviPosetioce()
        {
            return this.posetioci;
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
    }
}