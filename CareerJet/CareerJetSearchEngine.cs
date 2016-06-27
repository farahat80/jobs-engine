using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

using System.Data;
using Newtonsoft.Json;
namespace JobBoard.Engines.CareerJet
{
    public class CareerJetSearchEngine
    {
        private string _apilink;
        private string _apikey;
        private string _keyword;
        private string _city;
        private string _country;
        private int _pagesize;
        private int _pageno;
        private string _ip;
        private string _useragent;
        private string _sort;
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
            set { _country = value ; }
        }
        public int Pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        public int Pageno
        {
            get { return _pageno; }
            set { _pageno = value;}
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
        public CareerJetSearchEngine(string apiLink)
        {
            _apilink = apiLink;
            _searchparams = new NameValueCollection();

        }
        public void AddParam(string key, string value)
        {
            _searchparams.Add(key, value);
        }
        private void BuildParams()
        {

           AddParam("locale_code", _country);
           AddParam("pagesize", _pagesize.ToString());
           AddParam("sort", _sort);
           AddParam("keywords",_keyword);
           AddParam("Page", _pageno.ToString());
           AddParam("location", _city);
           AddParam("location", _city);
           AddParam("affid", _apikey);
           AddParam("user_ip", _ip);
           AddParam("user_agent", _useragent);

        }
        public CareerJetResults Search()
        {
            WebClient webclient;
            BuildParams();
            string url = Helpers.Url.BuildURL(_searchparams, _apilink);
            string results=string.Empty;
            using( webclient = new WebClient())
            {
                webclient.Encoding = System.Text.Encoding.UTF8;
                results= webclient.DownloadString(url);
            }
            CareerJetResults jObj = JsonConvert.DeserializeObject<CareerJetResults>(results);
            return jObj;

        }
      



    }
}
