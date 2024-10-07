using HelloWorld;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Runtime;

var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
    })
    .Build();

await host.StartAsync();


var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
var test1Grain1 = grainFactory.GetGrain<ISimpleGrain>("1");
await test1Grain1.Tell();

await host.StopAsync();
