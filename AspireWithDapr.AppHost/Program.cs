using AspireWithDapr.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore(Constants.StateStoreName);

var cartApi = builder.AddProject<Projects.CartApi>(Constants.CartApiName)
    .WithDaprSidecar()
    .WithReference(stateStore);

builder.Build().Run();