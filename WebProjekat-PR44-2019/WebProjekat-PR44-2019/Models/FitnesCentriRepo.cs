using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class FitnesCentriRepo
    {
        private List<FitnesCentar> fitnesCentri;
        public FitnesCentriRepo()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);
            fitnesCentri = new List<FitnesCentar>();

            foreach (var fitnesCentar in dataModel)
            {
                FitnesCentar ft = new FitnesCentar() {
                    Naziv = fitnesCentar.Naziv,
                    Adresa = fitnesCentar.Adresa,
                    CenaJGT = fitnesCentar.CenaJGT,
                    CenaJT = fitnesCentar.CenaJT,
                    CenaMC = fitnesCentar.CenaMC,
                    CenaJTP = fitnesCentar.CenaJTP,
                    GodinaOtvaranja = fitnesCentar.GodinaOtvaranja,
                    Vlasnik = fitnesCentar.Vlasnik
                };
                fitnesCentri.Add(ft);
            }
        }

        public List<FitnesCentar> DobaviFitnesCentre()
        {
            return this.fitnesCentri;
        }
    }
}