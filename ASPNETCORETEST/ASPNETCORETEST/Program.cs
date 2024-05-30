using AzureOperationsLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Dependecy injection operations
        var configuration = builder.Configuration;
        var azureConfigData = configuration.GetSection("AzureConfigData").Get<AzureConfigData>();
        builder.Services.AddAzureClients(builder =>
        {
            builder.AddSecretClient(configuration.GetSection("KeyVault"));
        });
            
            //.AddSecretClient(new Uri(configuration["VaultName"]));

        builder.Services.AddSingleton(azureConfigData);
        builder.Services.AddTransient<IServicebusQueue,ServicebusQueue>();
        builder.Services.AddTransient<IServicebusTopic,ServicebusTopic>();
        builder.Services.AddTransient<IAzureBlobOP,AzureBlobOP>();
        //builder.Services.AddCors(options => 
        //{ options.AddPolicy(name: "MyAllowSpecificOrigins", policy => { policy.AllowAnyOrigin();policy.AllowAnyMethod();policy.AllowAnyHeader(); }); });
        ////.WithOrigins("http://example.com", "http://www.contoso.com"); }); });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyHeader();
                                  policy.AllowAnyOrigin();
                                  policy.AllowAnyMethod();
                                  //policy.AllowCredentials();
                              });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //remove https
        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseCors("MyAllowSpecificOrigins");
        //app.Use(async (context, next) =>
        //{
        //    if (!context.User.Identity?.IsAuthenticated ?? false)
        //    {
        //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //        await context.Response.WriteAsync("USer is not authenticated.");
        //    }
        //});
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}