using Grpc.Core;
using System.Text.Json;
using GrpcMath;

namespace GrpcGreeter.Services
{
    public class MathService : Addition.AdditionBase
    {
        private readonly ILogger<MathService> _logger;
        public MathService(ILogger<MathService> logger)
        {
            _logger = logger;
        }

        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            Console.WriteLine(JsonSerializer.Serialize(request));
            return Task.FromResult(new AddResponse
            {
                Result = request.Value1 + request.Value2
            });
        }
    }
}
