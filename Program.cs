using System;
using System.Net.Http;
using System.Web;

namespace predict_with_rest
{
    class Program
    {
        static void Main(string[] args)
        {
            // YOUR-KEY: for example, the starter key
            var key = "3d6d559b9973499d886866ae7cf4d876";

            // YOUR-ENDPOINT: example is westus2.api.cognitive.microsoft.com
            var endpoint = "westus.api.cognitive.microsoft.com";
       
            // //public sample app
            var appId = "8966f2ea-0e34-4af8-8468-8e715053548d";

            var utterance = "turn on all lights";

            MakeRequest(key, endpoint, appId, utterance);

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest(string key, string endpoint, string appId, string utterance)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // The request header contains your subscription key
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            // The "q" parameter contains the utterance to send to LUIS
            queryString["query"] = utterance;

            // These optional request parameters are set to their default values
            queryString["verbose"] = "true";
            queryString["show-all-intents"] = "true";
            queryString["staging"] = "false";
            queryString["timezoneOffset"] = "0";

            var endpointUri = String.Format("https://{0}/luis/prediction/v3.0/apps/{1}/slots/production/predict?query={2}", endpoint, appId, queryString);

            var response = await client.GetAsync(endpointUri);

            var strResponseContent = await response.Content.ReadAsStringAsync();

            // Display the JSON result from LUIS
            Console.WriteLine(strResponseContent.ToString());
        }
    }
}
