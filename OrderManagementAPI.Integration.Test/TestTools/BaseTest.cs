using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace OrderManagementAPI.Integration.Test
{
    public class BaseTest
    {
        protected HttpClient httpClient;

        protected WebApplicationFactory<Startup> AppFactory { get; private set; }

        #region Mocks

        // protected Mock<ITestItem> mock { get; private set; }

        #endregion

        // https://gunnarpeipman.com/aspnet-core-integration-test-startup/
        public BaseTest()
        {
            CreateAppFactoryWithMocks();

            httpClient = AppFactory!.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Tools

        protected HttpContent SerializeToHttpContent<T>(T obj) where T : class
        {
            var userString = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(userString, Encoding.UTF8, "application/json");
            return stringContent;

        }

        protected async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var mappedResult = JsonConvert.DeserializeObject<T>(json);
            return mappedResult!;
        }

        #endregion

        #region private 

        private void CreateAppFactoryWithMocks()
        {
            AppFactory = new WebApplicationFactory<Startup>();
        }

        #endregion
    }
}
