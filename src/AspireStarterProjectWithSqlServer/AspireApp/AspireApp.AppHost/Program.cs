var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var sql = builder.AddSqlServer(name: "sql", password, 1443)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount(source: @"D:\DataStores\DataVolume");

var sqldb = sql.AddDatabase("sqldb", "master");

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount(source: @"D:\DataStores\DataVolume\psql");
var postgresdb = postgres.AddDatabase("postgresdb");

var mysql = builder.AddMySql("mysql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataBindMount(source: @"D:\DataStores\DataVolume\mysql");
var mysqldb = mysql.AddDatabase("mysqldb");

var apiService = builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
    .WithReference(sqldb)
    .WaitFor(sqldb)
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WithReference(mysqldb)
    .WaitFor(mysqldb);

builder.AddProject<Projects.AspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
