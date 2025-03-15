var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var weatherApi = builder.AddProject<Projects.AspireWithJavaScript_ApiService>("weatherapi");

builder.AddProject<Projects.AspireWithJavaScript_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(weatherApi)
    .WaitFor(weatherApi);

builder.AddNpmApp("angular", "../AspireJavaScript.Angular")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


builder.Build().Run();
