var builder = DistributedApplication.CreateBuilder(args);

// add projects and cloud-native backing services here
builder.AddProject<Projects.catalog>("catalog");

builder.Build().Run();
