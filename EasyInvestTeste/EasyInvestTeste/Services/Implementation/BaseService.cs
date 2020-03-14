using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyInvestTeste.Services.Implementation
{
    public class BaseService : IDisposable
    {
        #region Variables

        private string uri;
        
        public BaseService()
        {

        }

        #endregion

        #region Constructor

        public BaseService(string _uri)
        {
            this.uri = _uri;
        }

        #endregion

        #region Methods

        private void ConfigurarHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(this.uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T Get<T>(string request)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                this.ConfigurarHttpClient(client);
                response = client.GetAsync(request).Result;
            }

            string retorno = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                retorno = response.Content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public T Post<T>(string request, object parametro)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                this.ConfigurarHttpClient(client);
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(parametro), Encoding.UTF8, "application/json");
                response = client.PostAsync(request, conteudo).Result;
            }

            string retorno = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                retorno = response.Content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        #endregion

        #region Disposable

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}
