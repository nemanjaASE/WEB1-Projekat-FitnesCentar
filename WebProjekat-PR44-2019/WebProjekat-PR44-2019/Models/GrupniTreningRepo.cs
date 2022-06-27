using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class GrupniTreningRepo
    {
        private List<GrupniTrening> grupniTreninzi;
        public GrupniTreningRepo()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonString);
            grupniTreninzi = new List<GrupniTrening>();

            List<FitnesCentar> fitnesCentri = new List<FitnesCentar>();

            foreach (var grupniTrening in dataModel)
            {
                GrupniTrening gt = new GrupniTrening()
                {
                    DatumIVreme = grupniTrening.DatumIVreme,
                    MaxPosetilaca = grupniTrening.MaxPosetilaca,
                    Naziv = grupniTrening.Naziv,
                    Posetioci = grupniTrening.Posetioci,
                    TrajanjeTreninga = grupniTrening.TrajanjeTreninga,
                    TipTreninga = grupniTrening.TipTreninga,
                    FitnesCentarr = grupniTrening.FitnesCentarr
                };
                
                grupniTreninzi.Add(gt);
            }
        }

        public List<GrupniTrening> DobaviGrupneTreninge()
        {
            return this.grupniTreninzi;
        }
    }
}