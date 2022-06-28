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

            return dataModel;
        }
    }
}