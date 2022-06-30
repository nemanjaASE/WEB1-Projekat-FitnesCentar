using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class GrupniTrening
    {
        
        public long Id { get; set; }

        public string Naziv { get; set; }

        public TipTreninga TipTreninga { get; set; }

        public FitnesCentar FitnesCentarr { get; set; }

        public int TrajanjeTreninga { get; set; }

        public string DatumIVreme { get; set; }

        public int MaxPosetilaca { get; set; }

        public List<string> Posetioci { get; set; }

        public int IsDeleted { get; set; }
    }
}