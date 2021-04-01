using Hsf.ApplicatonProcess.August2020.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Hsf.ApplicatonProcess.August2020.Data
{
    public class CountriesList
    {
        public List<string> GetNames()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/all");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("https://restcountries.eu/rest/v2/all").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            List<string> ListOfCountries = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Countries>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    
                    ListOfCountries.Add(d.Name.ToString());
                }
                client.Dispose();
                return ListOfCountries;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                client.Dispose();
                return ListOfCountries;
            }

        }
    }
}
