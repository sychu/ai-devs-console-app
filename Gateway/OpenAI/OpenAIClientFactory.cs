using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;

namespace AIDevs.Gateway.OpenAI;

public class OpenAIClientFactory(IConfiguration _configuration)
{
    public OpenAIClient CreateClient()
    {
        var apiKey = _configuration[MetaData.ConfigKeys.OpenAIApiKey] ?? throw new Exception("OpenAI API key not found");
        return new OpenAIClient(apiKey);
    }
}
