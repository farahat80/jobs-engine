using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

using System.Data;
using Newtonsoft.Json;

namespace JobBoard.Engines.Indeed
{
    public class IndeedSearchEngine
    {
        private string _apilink;
        private string _apikey;
        private string _keyword;
        private string _city;
        private string _country;
        private int _start;
        private int _pagesize;
        private int _pageno;
        private string _sort;
        private string _ip;
        private string _useragent;
        private NameValueCollection _searchparams;
        public string Apikey
        {
            get { return _apikey; }
            set { _apikey = value; }
        }
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public int Pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        public int Pageno
        {
            get { return _pageno; }
            set {
                _pageno = value;
                _start = (value*_pagesize) -_pagesize ; 
            }
        }
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public string Useragent
        {
            get { return _useragent; }
            set { _useragent = value; }
        }
        public string Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        public IndeedSearchEngine(string apiLink)
        {
            _apilink = apiLink;
            _searchparams=new NameValueCollection();

        }
        public void AddParam(string key,string value)
        {
            _searchparams.Add(key, value);
        }
        private void BuildParams()
        {
            AddParam("publisher", _apikey);
            AddParam("q", _keyword);
            AddParam("l", _city);
            AddParam("sort", _sort);
            AddParam("radius", "");
            AddParam("st", "");
            AddParam("jt", "");
            AddParam("start", _start.ToString());
            AddParam("limit", _pagesize.ToString());
            AddParam("fromage", "7");
            AddParam("format", "json");
            AddParam("filter", "");
            AddParam("latlong", "1");
            AddParam("co", _country);
            AddParam("chnl", "");
            AddParam("userip", _ip);
            AddParam("useragent", _useragent);
            AddParam("v", "2");
        }
        public IndeedResults Search()
        {
            WebClient webclient;
            BuildParams();
            string url = Helpers.Url.BuildURL(_searchparams,_apilink);
            string json=string.Empty;
            using(webclient= new WebClient())
            {
                webclient.Encoding = System.Text.Encoding.UTF8;
                json = webclient.DownloadString(url);
            }
            IndeedResults jObj = JsonConvert.DeserializeObject<IndeedResults>(json);
            return jObj;
        }
        public Models.Job GetJob(string jobkey)
        {
            WebClient webclient;
            string url = _apilink + "?publisher=" + _apikey + "&jobkeys=" + jobkey + "&v=2&format=json";
            string json = string.Empty;
            using (webclient = new WebClient())
            {
                webclient.Encoding = System.Text.Encoding.UTF8;
                json = webclient.DownloadString(url);
            }
            IndeedResults jObj = JsonConvert.DeserializeObject<IndeedResults>(json);
            return new Models.Job()
            {
                JobTitle = jObj.results[0].jobtitle,
                Company = jObj.results[0].company,
                Description = jObj.results[0].snippet,
                Date =DateTime.Parse( jObj.results[0].date),
                Url = jObj.results[0].url,
                Location = jObj.results[0].formattedlocation,
                


            };

        }



    }
}
