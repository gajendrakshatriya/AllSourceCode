using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using System.Text;

namespace ASPNETCORETEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class CraftGPTController : ControllerBase
    {
        [HttpPost]
        public async Task<object> Post()
        {

            //organization id : org-5oGCMxB2vQwZIERf12jGT5xk
            //string nonAzureOpenAIApiKey = "your-api-key-from-platform.openai.com";
            string nonAzureOpenAIApiKey = "sk - proj - qEQ0m2Pt74lNTc5lJbfqT3BlbkFJwWt8NmkOYdtbS1fjOHIB";
            //string nonAzureOpenAIApiKey = "sk - proj - qEQ0m2Pt74lNTc5lJbfqT3BlbkFJwWt8NmkOYdtbS1fjOHIB";
            //string nonAzureOpenAIApiKey = "qEQ0m2Pt74lNTc5lJbfqT3BlbkFJwWt8NmkOYdtbS1fjOHIB";
            var client = new OpenAIClient(nonAzureOpenAIApiKey, new OpenAIClientOptions());
            
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-3.5-turbo", // Use DeploymentName for "model" with non-Azure clients
                Messages =
                        {
                            new ChatRequestSystemMessage("You are a helpful assistant. You will talk like a pirate."),
                            new ChatRequestUserMessage("Can you help me?"),
                            new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
                            new ChatRequestUserMessage("What's the best way to train a parrot?"),
                        }
            };

            var str = new StringBuilder();
            await foreach (StreamingChatCompletionsUpdate chatUpdate in client.GetChatCompletionsStreaming(chatCompletionsOptions))
            {
                if (chatUpdate.Role.HasValue)
                {
                    //Console.Write($"{chatUpdate.Role.Value.ToString().ToUpperInvariant()}: ");
                    str.AppendLine($"{chatUpdate.Role.Value.ToString().ToUpperInvariant()}: ");
                }
                if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
                {
                    //Console.Write(chatUpdate.ContentUpdate);
                    str.Append(chatUpdate.ContentUpdate);
                }
            }
            return str.ToString();
        }

        [HttpPost("Dummy")]
        public async Task<ActionResult> Dummy()
        {
            return Ok("Dummy executed. Api is working.");
        }

        [HttpPost("DummyModel")]
        public async Task<ActionResult> Dummy2()
        {
            var dummyModel = new DummyModel { Name = "Dummy executed. Api is working." };
            return Ok(dummyModel);
        }

        public class DummyModel
        {
            public string Name { get; set; }
        }

        [HttpPost("Dummy3")]
        public async Task<string> Dummy3()
        {
            return "Dummy executed. Api is working.";
        }

    }
}
