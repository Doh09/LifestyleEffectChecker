using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Repository.ServiceGateway.Effect
{
    class EffectServiceGateway : IRepository<Models.Effect.Effect>
    {
        private readonly UriAzure _azure = new UriAzure();
        public async Task<Models.Effect.Effect> Create(Models.Effect.Effect obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                HttpResponseMessage response = client.PostAsJsonAsync("api/effects", obj).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Effect.Effect>().Result);
                }
            }
            return null;
        }

        public async Task<Models.Effect.Effect> Read(int id)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with an journal id and waits for a response.
                var response = client.GetAsync($"/api/effects/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Effect.Effect>().Result);
                }
                return null;
            }
        }

        public async Task<IEnumerable<Models.Effect.Effect>> ReadAll()
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with list of journal and waits for a response.
                var response = client.GetAsync("/api/effects").Result;
                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<List<Models.Effect.Effect>>().Result);
                }
            }
            return new List<Models.Effect.Effect>();
        }

        public async Task<Models.Effect.Effect> Update(Models.Effect.Effect obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a put request with an journal Id and waits for a response.
                var response = client.PutAsJsonAsync($"/api/effects/{obj.ID}", obj).Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Effect.Effect>().Result);
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
                var response = client.DeleteAsync($"/api/effects/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Effect.Effect>().Result != null);
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
}
