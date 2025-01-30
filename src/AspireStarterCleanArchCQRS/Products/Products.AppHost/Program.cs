var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Products_ApiService>("apiservice");

builder.AddProject<Projects.Products_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
