using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class KomentarRepo
    {
        public KomentarRepo()
        {
           
        }

        public List<Komentar> DobaviKomentareUObradi()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/komentari.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<Komentar>>(jsonString);
            List<Komentar> uObradi = new List<Komentar>();
            foreach (var item in dataModel)
            {
                if (item.Status == StatusKomentara.OBRADA)
                    uObradi.Add(item);
            }
            return uObradi;
        }

        public List<Komentar> DobaviKomentareOdbijene()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/komentari.json");
            string jsonString = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<List<Komentar>>(jsonString);
            List<Komentar> odobreni = new List<Komentar>();
            foreach (var item in dataModel)
            {
                if (item.Status == StatusKomentara.ODBACEN)
                    odobreni.Add(item);
            }
            return odobreni;
        }

        public void DodajKomentarNaObradu(Komentar komentar)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/komentari.json");
            var jsonData = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<List<Komentar>>(jsonData);
            komentar.Id = GenerateId();
            data.Add(komentar);
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }

        public void IzmeniKomentar(Komentar komentar)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/komentari.json");
            var jsonData = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<List<Komentar>>(jsonData);
            foreach (var item in data)
            {
                if (item.Id == komentar.Id)
                    item.Status = komentar.Status;
            }
            File.Delete(path);
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }

        private static int GenerateId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}