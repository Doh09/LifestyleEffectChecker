using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models.Action;

namespace LifestyleEffectChecker.Repository.ServiceGateway.Action
{
    class ActionPartServiceGateway : IRepository<ActionPart>
    {
        private readonly UriAzure _azure = new UriAzure();
        public async Task<ActionPart> Create(ActionPart obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);
                
                HttpResponseMessage response = client.PostAsJsonAsync("api/actionparts", obj).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<ActionPart>().Result);
                }
            }
            return null;
        }

        public async Task<ActionPart> Read(int id, bool goOnline = false)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with an journal id and waits for a response.
                var response = client.GetAsync($"/api/actionparts/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<ActionPart>().Result);
                }
                return null;
            }
        }

        public async Task<IEnumerable<ActionPart>> ReadAll(bool goOnline = false)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with list of journal and waits for a response.
                var response = client.GetAsync("/api/actionparts").Result;
                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<List<ActionPart>>().Result);
                }
            }
            return new List<ActionPart>();
        }

        public async Task<ActionPart> Update(ActionPart obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a put request with an journal Id and waits for a response.
                var response = client.PutAsJsonAsync($"/api/actionparts/{obj.ID}", obj).Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<ActionPart>().Result);
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
                var response = client.DeleteAsync($"/api/actionparts/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<ActionPart>().Result != null);
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
