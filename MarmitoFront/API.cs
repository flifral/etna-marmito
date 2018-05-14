using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MarmitoFront
{
    public class API
    {
        private string m_address = "http://127.0.0.1:6000";

        public HttpClient getClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(m_address);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}