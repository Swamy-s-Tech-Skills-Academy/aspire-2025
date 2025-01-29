var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var sql = builder.AddSqlServer(name: "sql", password, 1443)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount(source: @"D:\DataStores\DataVolume");

var sqldb = sql.AddDatabase("sqldb", "master");

var apiService = builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
    .WithReference(sqldb);

builder.AddProject<Projects.AspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
