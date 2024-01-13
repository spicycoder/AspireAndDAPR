using AspireWithDapr.ServiceDefaults;

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore(Constants.StateStoreName)
    .WithDaprSidecar();

var pubSub = builder.AddDaprPubSub(Constants.PubSubName)
    .WithDaprSidecar();

var cartApi = builder.AddProject<Projects.CartApi>(Constants.CartApiName)
    .WithDaprSidecar()
    .WithReference(pubSub)
    .WithReference(stateStore);

var ordersApi = builder.AddProject<Projects.OrdersApi>(Constants.OrdersApiName)
    .WithDaprSidecar()
    .WithReference(pubSub);

var productsApi = builder.AddProject<Projects.ProductsApi>(Constants.ProductsApiName)
    .WithDaprSidecar();

builder.Build().Run();