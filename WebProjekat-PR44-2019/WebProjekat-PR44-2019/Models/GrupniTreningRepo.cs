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
            
        }

        public List<GrupniTrening> DobaviGrupneTreninge()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonString);

            return dataModel; 
        }

        public void IzmeniGrupniTrening(GrupniTrening gt)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonString);

            foreach (var item in dataModel)
            {
                if (item.Id == gt.Id)
                {
                    item.DatumIVreme = gt.DatumIVreme;
                    item.MaxPosetilaca = gt.MaxPosetilaca;
                    item.Naziv = gt.Naziv;
                    item.TipTreninga = gt.TipTreninga;
                    item.TrajanjeTreninga = gt.TrajanjeTreninga;
                    break;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(dataModel));
        }
        public bool DodajPosetiocaNaTrening(GrupniTrening grupniTrening,string posetilac)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            var jsonData = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonData);
            foreach (var item in data)
            {
                if(item.Naziv == grupniTrening.Naziv &&
                    item.DatumIVreme == grupniTrening.DatumIVreme)
                {
                    if (item.Posetioci.Count() + 1 > item.MaxPosetilaca)
                        return false;
                    if (item.Posetioci.Contains(posetilac))
                        return false;
                    item.Posetioci.Add(posetilac);
                    break;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        public bool DeleteById(long id)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/grupniTreninzi.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<GrupniTrening>>(jsonString);
            foreach (var item in dataModel)
            {
                if (item.Id == id)
                {
                    if (item.Posetioci.Count() != 0)
                        return false;
                    else item.IsDeleted = 1;
                        break;
                }
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(dataModel));
            return true;
        }

        private static int GenerateId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}