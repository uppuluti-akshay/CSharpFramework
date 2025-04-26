using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using SpecflowCSharpFramework.Support;
using SpecflowCSharpFramework.Utilities;
using System;
using TechTalk.SpecFlow;

namespace SpecflowCSharpFramework.StepDefinitions
{
    [Binding]
    public class ApiTestingSteps
    {
        private RestSharpApi apiClient; // Changed from _apiSetUp to apiClient
        private RestResponse response;

        [Given(@"the API base URL is ""(.*)""")]
        public void GivenTheAPIBaseURLIs(string baseUrl)
        {
            apiClient = new RestSharpApi(baseUrl); // Updated to use RestSharpApi
        }

        [When(@"I send a GET request to ""(.*)""")]
        public async Task WhenISendAGETRequestTo(string endpoint)
        {
            response = await apiClient.GetAsync(endpoint); // Updated to use apiClient
        }

        [When(@"I send a POST request to ""(.*)"" with the following data:")]
        public async Task WhenISendAPOSTRequestToWithTheFollowingData(string endpoint, Table table)
        {
            var data = ConvertTableToDictionary(table);
            response = await apiClient.PostAsync(endpoint, data); // Updated to use apiClient
        }

        [When(@"I send a PUT request to ""(.*)"" with the following data:")]
        public async Task WhenISendAPUTRequestToWithTheFollowingData(string endpoint, Table table)
        {
            var data = ConvertTableToDictionary(table);
            response = await apiClient.PutAsync(endpoint, data); // Updated to use apiClient
        }

        [When(@"I send a PATCH request to ""(.*)"" with the following data:")]
        public async Task WhenISendAPATCHRequestToWithTheFollowingData(string endpoint, Table table)
        {
            var data = ConvertTableToDictionary(table);
            response = await apiClient.PatchAsync(endpoint, data); // Updated to use apiClient
        }

        [When(@"I send a DELETE request to ""(.*)""")]
        public async Task WhenISendADELETERequestTo(string endpoint)
        {
            response = await apiClient.DeleteAsync(endpoint); // Updated to use apiClient
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode,
                $"Expected status code {expectedStatusCode}, but got {(int)response.StatusCode}.");
        }

        [Then(@"the response content should not be null")]
        public void ThenTheResponseContentShouldNotBeNull()
        {
            Assert.IsNotNull(response.Content, "Response content is null.");
        }

        private Dictionary<string, string> ConvertTableToDictionary(Table table)
        {
            var data = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                data.Add(row[0], row[1]);
            }
            return data;
        }
    }
}
