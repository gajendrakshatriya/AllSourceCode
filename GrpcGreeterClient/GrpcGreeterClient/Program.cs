// See https://aka.ms/new-console-template for more information
// The port number must match the port of the gRPC server.
using Grpc.Net.Client;
using GrpcGreeter;
using GrpcMath;


//Console.WriteLine("Hello, World!");

MainEntry();

async void SayHello()
{
    // The port number must match the port of the gRPC server.
    using var channel = GrpcChannel.ForAddress("https://localhost:7275");
    var client = new Greeter.GreeterClient(channel);
    var name = GetName();
    var reply = await client.SayHelloAsync( new HelloRequest { Name = name });
    Console.WriteLine("Greeting: " + reply.Message);
}

async void Add()
{
    // The port number must match the port of the gRPC server.
    using var channel = GrpcChannel.ForAddress("https://localhost:7275");
    var client = new Addition.AdditionClient(channel);
    var reply = await client.AddAsync(GetNumbersToAdd());
    Console.WriteLine("Total: " + reply.Result);
}

string GetName()
{
    Console.WriteLine("Enter Name:");
    var name = Console.ReadLine();
    return name ?? "GreeterClient";
}

AddRequest GetNumbersToAdd()
{
    Console.WriteLine("Enter Value 1:");
    var val1 = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter Value 2:");
    var val2 = Convert.ToInt32(Console.ReadLine());

    return new AddRequest { Value1 = val1, Value2 = val2 };
}

async void MainEntry()
{
    Console.WriteLine("Enter A to Add, G To Greet or anoy other key to exit");
    var input = Console.ReadLine().ToUpper();
    switch(input)
    {
        case "A":
            Add();
            break;
        case "G":
            SayHello();
            break;
        default:
            break;
    }
    Console.ReadKey();
}
