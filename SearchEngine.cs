using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobBoard.Engines
{
    public class CountryValue
    {
        public string CountryName { get; set; }
        public string CountryIso { get; set; }
        public string CountryLan { get; set; }
    }
    public class SearchEngine
    {
        private string _keyword { get; set; }
        private string _city { get; set; }
        private CountryValue _country { get; set; }
        private int _pagesize { get; set; }
        private int _pageno { get; set; }
        private string _sort { get; set; }
        public string IndeedKey { get; set; }
        public string CareerJetKey { get; set; }
        public string IndeedApi { get; set; }
        public string CareerJetApi { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public bool SearchInIndeed { get; set; }
        public bool SearchInCareerJet { get; set; }


        public Models.SearchResults Search(string keyword, CountryValue country, string city, int pagesize, int pageno, string sort)
        {
            _keyword = keyword;
            _country = country;
            _city = city;
            _pageno = pageno;
            _pagesize = pagesize;
            _sort = sort;
            Models.SearchResults results = new Models.SearchResults();
            Indeed.IndeedResults indeed;
            CareerJet.CareerJetResults careerjet;

            if (SearchInIndeed == true)
            {
                indeed = SearchIndeed();
                List<Models.Job> indeedjobs = indeed.results.Select(x => new Models.Job()
                {
                    JobTitle = x.jobtitle,
                    Company = x.company,
                    Description = x.snippet,
                    Date = DateTime.Parse(x.date),
                    Location = x.formattedlocation,
                    Country=country.CountryIso,
                    Url = x.url,
                    Api = "indeed",
                    JobKey=x.jobkey
                }).ToList();
                results.Jobs.AddRange(indeedjobs);
                results.Count += int.Parse(indeed.totalresults);
            }
            if(SearchInCareerJet==true)
            {
                careerjet = SearchCareerJet();
                if(careerjet.jobs!=null)
                {
                    List<Models.Job> careerjetjobs = careerjet.jobs.Select(x => new Models.Job()
                    {
                        JobTitle = x.title,
                        Company = x.company,
                        Description = x.description,
                        Date = DateTime.Parse(x.date),
                        Location = x.locations,
                        Country = country.CountryIso,
                        Url = x.url,
                        Api = "careerjet",
                        JobKey = ""
                    }).ToList();
                    results.Jobs.AddRange(careerjetjobs);
                    results.Count += int.Parse(careerjet.hits);
                }
              
            }
            
            return results;

        }
        private Indeed.IndeedResults SearchIndeed()
        {
            Indeed.IndeedSearchEngine indeed = new Indeed.IndeedSearchEngine(IndeedApi)
            {
                Apikey = IndeedKey,
                Keyword = _keyword,
                Pageno = _pageno,
                Pagesize = _pagesize,
                Country = _country.CountryIso,
                City = _city,
                Sort = _sort,
                Ip = Ip,
                Useragent = UserAgent
            };
            return indeed.Search();
        }
        private CareerJet.CareerJetResults SearchCareerJet()
        {
            CareerJet.CareerJetSearchEngine careerjet = new CareerJet.CareerJetSearchEngine(CareerJetApi)
            {
                Apikey = CareerJetKey,
                Keyword = _keyword,
                Pageno = _pageno,
                Pagesize = _pagesize,
                Country = _country.CountryLan + "_" + _country.CountryIso,
                City = _city,
                Sort = _sort,
                Ip = Ip,
                Useragent = UserAgent
            };
            return careerjet.Search();
        }
    }
}
