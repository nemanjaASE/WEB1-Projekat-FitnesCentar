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
        public FitnesCentriRepo()
        {
        }

        public List<FitnesCentar> DobaviFitnesCentre()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);
            List<FitnesCentar> retVal = new List<FitnesCentar>();
            foreach (var item in dataModel)
            {
                if(item.IsDeleted == 0)
                {
                    retVal.Add(item);
                }
            }
            return retVal;
        }

        public FitnesCentar DobaviFitnesCentar(string naziv)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);
            foreach (var item in dataModel)
            {
                if (item.Naziv == naziv && item.IsDeleted == 0)
                    return item;
            }
            return null;
        }

        public bool IzmeniFitnesCentar(FitnesCentar fitnesCentar)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);

            foreach (var fc in dataModel)
            {
                if (fc.Naziv == fitnesCentar.Naziv
                    && fc.Adresa.Ulica == fitnesCentar.Adresa.Ulica
                    && fc.Adresa.Broj == fitnesCentar.Adresa.Broj
                    && fc.Adresa.Grad == fitnesCentar.Adresa.Grad
                    && fc.Vlasnik == fitnesCentar.Vlasnik)
                {
                    fc.CenaJTP = fitnesCentar.CenaJTP;
                    fc.CenaJGT = fitnesCentar.CenaJGT;
                    fc.CenaJT = fitnesCentar.CenaJT;
                    fc.CenaMC = fitnesCentar.CenaMC;
                    fc.CenaGT = fitnesCentar.CenaGT;
                    fc.GodinaOtvaranja = fitnesCentar.GodinaOtvaranja;
                    File.Delete(path);
                    File.WriteAllText(path, JsonConvert.SerializeObject(dataModel));
                    return true;
                }
            }

            return false;
        }

        public bool DeleteFitnesCentar(string naziv, string vlasnik)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/fitnesCentri.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<FitnesCentar>>(jsonString);
            foreach (var item in dataModel)
            {
                if (item.Naziv == naziv && item.Vlasnik == vlasnik &&  item.IsDeleted == 0)
                {
                    item.IsDeleted = 1;
                    File.Delete(path);
                    File.WriteAllText(path, JsonConvert.SerializeObject(dataModel));
                    return true;
                }
            }
            return false;
        }
    }
}