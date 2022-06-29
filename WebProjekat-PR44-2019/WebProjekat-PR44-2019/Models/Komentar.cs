using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Posetilac { get; set; }
        public string FitnesCentar { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public StatusKomentara Status {get; set;}
    }
}