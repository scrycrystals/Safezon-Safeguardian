using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace app2.Services
{
    public class ChatbotService
    {
        private readonly string apiKey = "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";
        private readonly string apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public async Task<string> GetResponseAsync(string question)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    string admin = "user lives in pakistan. we made security app. and reply as a chatbot. keep your answer limited to 100 words. try to give short and simple answer";

                    var requestData = new
                    {
                        model = "llama-3.3-70b-versatile",
                        messages = new[]
                        {
                            new { role = "system", content = admin },
                            new { role = "user", content = question }
                        }
                    };

                    string json = JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(responseString);
                    return jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString() ?? "No response received";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
