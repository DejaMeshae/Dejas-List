using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DejasList.Controllers
{
    public class GeocodeController
    {
        string _lat;
        string _lng;

        public string latitude { get { return _lat; } }
        public string longitude { get { return _lng; } }

        //Geocode
        public void SendRequest(string Address)
        {
            string google = "https://maps.googleapis.com/maps/api/geocode/json?address=" + Address + "&key=" + APIKeys.GoogleAPIKey;
            RunAsync(google).GetAwaiter().GetResult();

        }

        public class JSONObj
        {
            public Results[] results { get; set; }
        }

        public class Results
        {
            public Geometry geometry { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
        }

        public class Location
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }



        static HttpClient client = new HttpClient();

        async Task<JSONObj> GetResults(string url)
        {
            JSONObj result = null;
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<JSONObj>();
            }
            return result;

        }

        async Task RunAsync(string url)
        {

            client.BaseAddress = new Uri(url);
            try
            {
                JSONObj result = await GetResults(url).ConfigureAwait(false);
                _lat = result.results[0].geometry.location.lat;
                _lng = result.results[0].geometry.location.lng;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}