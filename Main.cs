namespace AIDevs;

using AIDevs.Gateway.OpenAI;
using Azure.AI.OpenAI;

public class Main(OpenAIClientFactory openAIClientFactory)
{
    private readonly OpenAIClient openAIClient = openAIClientFactory.CreateClient();

    public async Task RunAsync()
    {
        var chat = new ChatCompletionsOptions()
        {
            DeploymentName = "gpt-4",
            Messages =
            {
                new ChatRequestSystemMessage(@"As a Senior JavaScript Programmer, elaborate complex concepts."),
                new ChatRequestUserMessage(@"Closure")
            }
        };

        string result = "";
        await foreach (StreamingChatCompletionsUpdate chatUpdate in openAIClient.GetChatCompletionsStreaming(chat))
        {
            if (chatUpdate.Role.HasValue)
            {
                Console.Write($"{chatUpdate.Role.Value.ToString().ToUpperInvariant()}: ");
            }
            if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
            {
                result += chatUpdate.ContentUpdate;
                Console.Write(chatUpdate.ContentUpdate);
            }
        }
    }
}
