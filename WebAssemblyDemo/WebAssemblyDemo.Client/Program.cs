using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("");

await builder.Build().RunAsync();
