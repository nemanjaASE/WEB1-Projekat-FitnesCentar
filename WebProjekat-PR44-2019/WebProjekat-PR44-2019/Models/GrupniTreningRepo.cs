﻿using Newtonsoft.Json;
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
    }
}