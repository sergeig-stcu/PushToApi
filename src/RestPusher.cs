using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PushToApi {
    public class RestPusher {

        private static readonly HttpClientHandler httpHandler = new HttpClientHandler();

        static RestPusher() {
            httpHandler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => {
                return true;
            };
        }

        public static async Task<int> SendMessageAsync(Config config, Model model, String payload) {
            int errCode = 0; // success
            var client = GetClient(config, model);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string message;
            HttpResponseMessage response = null;
            String body = null;
            String path = config.PathPrefix + model.EP.PathSuffix;
            try {
                switch (model.EP.Method.ToUpper())
                {
                    case "GET":
                        response = await client.GetAsync(path);
                        break;
                    case "POST": {
                        var stringContent = new StringContent(payload, Encoding.UTF8, model.EP.ContentType);
                        response = await client.PostAsync(path, stringContent);
                        break;
                    }
                    case "PUT": {
                        var stringContent = new StringContent(payload, Encoding.UTF8, model.EP.ContentType);
                        response = await client.PutAsync(path, stringContent);
                        break;
                    }
                    default:
                        throw new PushToApiException($"{model.EP.Method}: Unknown Method");
                }
                if (response != null) {
                    body = response.Content.ReadAsStringAsync().Result;
                }
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync();
                watch.Stop();
                message = String.Format($"Status Code: {response.StatusCode}");
            } catch (HttpRequestException ex) {
                errCode = 1;  // failed
                watch.Stop();
                message = $"ERROR: {path}: {ex.Message}";
                if (ex.InnerException != null) {
                    message = $"{message}; Inner: {ex.InnerException.Message}";
                }
            } finally {
                if (response != null) {
                    response.Dispose();
                }
            }
            Console.WriteLine($"{DateTime.Now}: {message}; Response time {watch.Elapsed}");
            Console.WriteLine($"Response Body:\n{body}");

            return errCode;
        }

        private static HttpClient GetClient(Config config, Model model) {
            // create new and return it
            var client = new HttpClient(httpHandler);
            var byteArray = Encoding.ASCII.GetBytes($"{config.BasicAuth.Id}:{config.BasicAuth.Password}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(model.EP.ContentType));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

    }
}