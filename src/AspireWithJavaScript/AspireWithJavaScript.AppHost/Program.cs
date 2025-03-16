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

builder.AddNpmApp("react", "../AspireJavaScript.React")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.AddNpmApp("vue", "../AspireJavaScript.Vue")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
