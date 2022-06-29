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
    }
}