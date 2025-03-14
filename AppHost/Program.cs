var builder = DistributedApplication.CreateBuilder(args);

// Backing Services
var postgres = builder
        .AddPostgres("postgres")
        .WithPgAdmin()
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

// Projects
var catalog = builder
        .AddProject<Projects.catalog>("catalog")
        .WithReference(catalogDb)
        .WaitFor(catalogDb);

builder.AddProject<Projects.Basket>("basket");

builder.Build().Run();
