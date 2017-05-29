using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Repository.ServiceGateway.Action
{
    class ActionServiceGateway : IRepository<Models.Action.Action>
    {

        private readonly UriAzure _azure = new UriAzure();
        public async Task<Models.Action.Action> Create(Models.Action.Action obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                HttpResponseMessage response = client.PostAsJsonAsync("api/actions", obj).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Action.Action>().Result);
                }
            }
            return null;
        }

        public async Task<Models.Action.Action> Read(int id, bool goOnline = false)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with an journal id and waits for a response.
                var response = client.GetAsync($"/api/actions/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Action.Action>().Result);
                }
                return null;
            }
        }

        public async Task<IEnumerable<Models.Action.Action>> ReadAll(bool goOnline = false)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a get request with list of journal and waits for a response.
                var response = client.GetAsync("/api/actions").Result;
                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<List<Models.Action.Action>>().Result);
                }
            }
            return new List<Models.Action.Action>();
        }

        public async Task<Models.Action.Action> Update(Models.Action.Action obj)
        {
            using (var client = new HttpClient())
            {
                ClientSetup(client);

                //Sends a put request with an journal Id and waits for a response.
                var response = client.PutAsJsonAsync($"/api/actions/{obj.ID}", obj).Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Action.Action>().Result);
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
                var response = client.DeleteAsync($"/api/actions/{id}").Result;

                //If successful reads an journal.
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(response.Content.ReadAsAsync<Models.Action.Action>().Result != null);
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
