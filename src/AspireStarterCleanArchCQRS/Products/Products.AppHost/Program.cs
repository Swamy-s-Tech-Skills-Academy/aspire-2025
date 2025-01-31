var builder = DistributedApplication.CreateBuilder(args);

var sqldb = builder.AddConnectionString("sqlDb");

var apiService = builder.AddProject<Projects.Products_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(sqldb);

builder.AddProject<Projects.Products_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
