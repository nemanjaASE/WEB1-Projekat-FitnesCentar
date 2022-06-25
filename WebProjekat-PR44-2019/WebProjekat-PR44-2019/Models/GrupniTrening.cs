using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat_PR44_2019.Models
{
    public class GrupniTrening
    {
        private string naziv;
        private TipTreninga tipTreninga;
        private FitnesCentar fitnesCentar;
        private int trajanjeTreninga;
        private string datumIVreme;
        private int maxPosetilaca;
        private List<Posetilac> posetioci;

        public string Naziv { get => naziv; set => naziv = value; }

        public TipTreninga TipTreninga { get => tipTreninga; set => tipTreninga = value; }

        public FitnesCentar FitnesCentar { get => fitnesCentar; set => fitnesCentar = value; }

        public int TrajanjeTreninga { get => trajanjeTreninga; set => trajanjeTreninga = value; }

        public string DatumIVreme { get => datumIVreme; set => datumIVreme = value; }

        public int MaxPosetilaca { get => maxPosetilaca; set => maxPosetilaca = value; }

        public List<Posetilac> Posetioci { get => posetioci; set => posetioci = value; }
    }
}