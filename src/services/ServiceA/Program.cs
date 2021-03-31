// Program

using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

class Program
{
    public static void Main(string[] args) =>  WebHost.CreateDefaultBuilder(args)
    .ConfigureServices((IServiceCollection services) => {
        services.AddTransient(sp => new HttpClient { 
            BaseAddress = new System.Uri(
                System.Environment.GetEnvironmentVariable("service-b-url") ??
                "http://localhost:6000") 
        });
    })
    .Configure(app =>
    {
        app.UseRouting();
        app.UseEndpoints(e =>
        {
            e.MapGet("/", async context =>
                await context.Response.WriteAsJsonAsync(new object[]
                {
                    new { Message = "Hello, World! This is Service A"},
                    // Call ServiceB 
                    new 
                    {
                        Message = await context.RequestServices.GetService<HttpClient>()
                                               .GetStringAsync("/")
                                               .ConfigureAwait(false)
                    }
                })
            );
        });
    }).Build().Run();
}