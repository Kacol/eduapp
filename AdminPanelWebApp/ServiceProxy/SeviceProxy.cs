using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProxy
{
    public class ServiceProxy : IDisposable //tu singletonu w celu występowania tylko jednej instancji tej klasy, http client będzie kożystał tylko z jednej instancji
    {
        private readonly string address = "http://localhost:57179/";
        private readonly string prefix = "api/external/";

        private static ServiceProxy instance;
        public static ServiceProxy Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServiceProxy();
                return instance;
            }
        }

        private HttpClient client;

        private ServiceProxy()
        {          
          
          
        }

        public void Init(string adress)// metoda do inicjowania adresu, możemy wywołać ją tylko raz
        {
            if (adress == null)            
                throw new ArgumentNullException(nameof(adress));            
            if (client!= null)
                throw new InvalidOperationException("Cannot change adress");
            
            client = new HttpClient(); //komunikacja opiera się na http client
            client.BaseAddress = new Uri(address); //tu następuje inicjalizacja adresem naszej strony
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<int> GetTimeoutSetting()// metoda do łączenia się z serwerem i pobierania ustawień
        {
            var response = await client.GetAsync(prefix + "GetTimeoutSetting");//wykonuje się asynchronicznie, najpierw wywołuje się pobieranie danych i nie blokuje aplikacji
            //następnie po pobraniu wykonuje się reszta metody 
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }

            var result = await response.Content.ReadAsAsync<int>();//cztanie danych
            return result;
        }
        public async Task<int> GetWrongAnswerTimeoutSetting()
        {
            var response = await client.GetAsync(prefix + "GetWrongAnswerTimeoutSetting");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }

            var result = await response.Content.ReadAsAsync<int>();
            return result;
        }
        public async Task<GetNextQuestionResult> GetNextQuestion()
        {
            var response = await client.GetAsync(prefix + "GetNextQuestion");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }

            var result = await response.Content.ReadAsAsync<GetNextQuestionResult>();
            return result;
        }
        public async Task<bool> IsAnswerCorrect(string userAnswer,int questionId)
        {
            var response = await client.GetAsync(prefix + "IsAnswerCorrect/" + userAnswer +"/" + questionId);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }

            var result = await response.Content.ReadAsAsync<bool>();
            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ServiceProxy() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    

}
