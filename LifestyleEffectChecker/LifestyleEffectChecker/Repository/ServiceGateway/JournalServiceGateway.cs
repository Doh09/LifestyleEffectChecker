using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;

namespace LifestyleEffectChecker.Repository.ServiceGateway
{
    class JournalServiceGateway : IRepository<Journal>
    {
        private readonly UriAzure _azure = new UriAzure();
        public async Task<Journal> Create(Journal item)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a post request with an journal and waits for a response.
                HttpResponseMessage response = client.PostAsJsonAsync("api/journals", item).Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Journal>().Result);
                }
            }
            return null;
        }

        public async Task<Journal> Read(int id)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with an journal id and waits for a response.
                var response = client.GetAsync($"/api/journals/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Journal>().Result);
                }
                return null;
            }
        }

        public async Task<IEnumerable<Journal>> ReadAll()
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with list of journal and waits for a response.
                var response = client.GetAsync("/api/journals").Result;
                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<List<Journal>>().Result);
                }
            }
            return new List<Journal>();
        }

        public async Task<Journal> Update(Journal obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a put request with an journal Id and waits for a response.
                var response = client.PutAsJsonAsync($"/api/journals/{obj.ID}", obj).Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Journal>().Result);
                }
                return obj;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a delete request with an journal Id and waits for a response.
                var response = client.DeleteAsync($"/api/journals/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Journal>().Result != null);
                }
                return false;
            }
        }

        private void ClientSetup(HttpClient client)
        {
            //Sets which api to get.
            client.BaseAddress = new Uri(_azure.DataBaseUri);
            //Removes all default headers.
            client.DefaultRequestHeaders.Accept.Clear();
            //Makes Json header.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
