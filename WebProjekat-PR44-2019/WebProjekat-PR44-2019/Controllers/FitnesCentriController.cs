using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat_PR44_2019.Models;

namespace WebProjekat_PR44_2019.Controllers
{
    public class FitnesCentriController : ApiController
    {
        private static FitnesCentriRepo repo = new FitnesCentriRepo();
        public IEnumerable<FitnesCentar> Get()
        {
            List<FitnesCentar> fitnesCentri = repo.DobaviFitnesCentre();

            int n = fitnesCentri.Count();
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (String.Compare(fitnesCentri[j].Naziv, fitnesCentri[min].Naziv) < 0)
                    {
                        min = j;
                    }
                   
                }
                if (min != i)
                {
                    var tmp = fitnesCentri[i];
                    fitnesCentri[i] = fitnesCentri[min];
                    fitnesCentri[min] = tmp;
                }
            }
            return repo.DobaviFitnesCentre();
        }
    }
}
