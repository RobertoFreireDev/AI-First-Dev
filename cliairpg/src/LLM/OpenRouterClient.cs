using Newtonsoft.Json;

namespace CLIAirpg.LLM;

public class Message
{
    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    public Message(string role, string content)
    {
        Role = role;
        Content = content;
    }
}

public class OpenRouterRequest
{
    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("messages")]
    public List<Message> Messages { get; set; }

    [JsonProperty("max_tokens")]
    public int MaxTokens { get; set; }

    [JsonProperty("temperature")]
    public float Temperature { get; set; }

    public OpenRouterRequest(string model, List<Message> messages)
    {
        Model = model;
        Messages = messages;
        MaxTokens = 500;
        Temperature = 0.7f;
    }
}

public class OpenRouterResponse
{
    [JsonProperty("choices")]
    public List<Choice>? Choices { get; set; }

    [JsonProperty("usage")]
    public Usage? Usage { get; set; }
}

public class Choice
{
    [JsonProperty("message")]
    public Message? Message { get; set; }
}

public class Usage
{
    [JsonProperty("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonProperty("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonProperty("total_tokens")]
    public int TotalTokens { get; set; }
}

public class OpenRouterClient
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://openrouter.ai/api/v1";
    private const string DefaultModel = "gpt-3.5-turbo";

    public OpenRouterClient(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    public async Task<string> GenerateText(string prompt, string model = DefaultModel)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            return "[LLM disabled - API key not set]";
        }

        try
        {
            var messages = new List<Message>
            {
                new Message("user", prompt)
            };

            var request = new OpenRouterRequest(model, messages);
            var jsonContent = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/ai-first-dev/cliairpg");

            var response = await _httpClient.PostAsync($"{BaseUrl}/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"[Error: {response.StatusCode} - {errorContent}]";
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var openRouterResponse = JsonConvert.DeserializeObject<OpenRouterResponse>(responseContent);

            return openRouterResponse?.Choices?.FirstOrDefault()?.Message?.Content ?? "[No response]";
        }
        catch (Exception ex)
        {
            return $"[Error calling OpenRouter API: {ex.Message}]";
        }
    }

    public void SetApiKey(string apiKey)
    {
        // Allow runtime API key setting
    }
}
