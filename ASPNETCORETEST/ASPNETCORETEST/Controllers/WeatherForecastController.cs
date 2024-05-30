using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.Security.KeyVault.Secrets;
using AzureOperationsLib;
using AzureOperationsMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web.Resource;
using System.IO;
using System.Text;

namespace ASPNETCORETEST.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;
        private SecretClient _secretClient;
        private string _env;
        private IServicebusQueue _servicebusQueue;
        private IAzureBlobOP _azureBlobOP;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration configuration,IServicebusQueue servicebusQueue,IAzureBlobOP azureBlobOP, SecretClient secretClient)
        {
            _configuration = configuration;
            _logger = logger;
            var vaultName = configuration["VaultName"];
            _secretClient = secretClient;// new SecretClient(new Uri(vaultName), new ManagedIdentityCredential());
            //_secretClient = new SecretClient(new Uri(vaultName), new DefaultAzureCredential());
            //_secretClient = new SecretClient(new Uri(vaultName), new ManagedIdentityCredential());
            _env = configuration["Env"];
            _servicebusQueue = servicebusQueue;
            _azureBlobOP = azureBlobOP;
            //ProcessDocs($"https://azstoragetest2024.blob.core.windows.net/aidocument/5.pdf");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var secrete = await _secretClient.GetSecretAsync("WeatherAsia");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Secrete = secrete.Value.Value,
                Env = _env,
            }).ToArray();
        }

        /// <summary>
        /// Sends message to service bus
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Send")]
        public async Task SendMessage(string data)
        {
            var connectionString = _configuration["ServiceBusConnectionString"]; //"Endpoint=sb://ng-sbus-namespace.servicebus.windows.net/;SharedAccessKeyName=Queue-1-Policy;SharedAccessKey=6zQNICwvljKtffJ++2etYr7RxcuuUtpyQ+ASbNZbD/Y=;EntityPath=sbus-queue-1";
            var queueName = "sbus-queue-1";
            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender(queueName);
            var msg = new ServiceBusMessage(data);
            await sender.SendMessageAsync(msg);
        }

        [HttpPost("SendMessageUsingLib")]
        public void SendMessageUsingLib(string data)
        {
            _servicebusQueue.SendMessage(data);
        }

        [HttpPost("SendMessageModel")]
        public void SendMessageModel(QueueMessage data)
        {
            _servicebusQueue.SendMessage(data);
        }

        //[SwaggerRequestPart(Name = "file", Required = true, Type = "file", Description = "Select file")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0]; // Assuming only one file is uploaded

                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                //_azureBlobOP.UploadImageAsync()

                //var filePath = Path.GetTempFileName(); // You can change this to your desired file path

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //    await _azureBlobOP.UploadImageAsync(stream, file.Name);
                //}

                using (var stream = file.OpenReadStream())
                {
                    await _azureBlobOP.UploadImageAsync(stream, file.FileName);
                }


                // Optionally, you can return some response indicating success
                return Ok("File uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("ProcessDocs")]
        public async Task<ObjectResult> ProcessDocsAsync(string blobFileUrl)
        {
            var ocrOP = new OCROP();
            ocrOP.ProcessDocAsync(blobFileUrl);
            return Ok("File uploaded successfully");
        }

        [HttpPost("GetProcessedDoc")]
        public async Task<ObjectResult> GetProcessedDocsAsync([FromBody]string blobFileUrl)
        {
            var ocrOP = new OCROP();
            var docFields = await ocrOP.GetProcessedDocAsync(blobFileUrl);
            return Ok(docFields);
        }


    }
}