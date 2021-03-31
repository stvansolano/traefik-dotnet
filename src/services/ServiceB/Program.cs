// Program

using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

class Program
{
    public static void Main(string[] args) =>  WebHost.CreateDefaultBuilder(args)
    .Configure(app =>
    {
        app.UseRouting();
        app.UseEndpoints(e =>
        {
            e.MapGet("/", context =>
                context.Response.WriteAsync("Hello, World! This is Service B")
            );
        });
    }).Build().Run();
}