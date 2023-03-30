using Newtonsoft.Json;
using System.Text;

namespace DFBlazor.Services {
    public class ChatGPTService {
        //https://www.c-sharpcorner.com/article/building-ai-chatbot-app-with-chatgpt-api-and-blazor-a-step-by-step-guide/
        private readonly HttpClient _httpClient;

        public ChatGPTService(string baseURL, string apiKey) {
            _httpClient = new();
            _httpClient.BaseAddress = new Uri(baseURL);
            _httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {apiKey}");
        }

        public async Task<string> GetResponseDV(string query) {
            var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress) {
                Content = new StringContent("{\"model\": \"text-davinci-003\", \"prompt\": \"" +
                                            query +
                                            "\",\"temperature\": 1,\"max_tokens\": 100}",
                                            Encoding.UTF8,
                                            "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseString = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return responseString!.choices[0].text;
        }

        public async Task<string> GetResponseGPT35(string query) {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions") {
                Content = new StringContent("{\"model\": \"gpt-3.5-turbo\", " + 
                                            "\"messages\": [{\"role\": \"user\", \"content\": \"" + query + "\"}]}",
                                            Encoding.UTF8,
                                            "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseString = JsonConvert.DeserializeAnonymousType(responseContent, new {
                choices = new[] { new { message = new { role = string.Empty, content = string.Empty } } },
                error = new { message = string.Empty }
            });

            return responseString?.choices[0].message.content;
        }
    }
}
