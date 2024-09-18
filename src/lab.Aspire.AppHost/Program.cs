var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.lab_dotnet8_webapi>("dotnet-8-sample")
       .WithEnvironment("TRIGGER_BY_ASPIRE", "TRUE");
// builder.AddProject<Projects.lab_dotnet6_webapi>("dotnet-6-sample")
//        .WithEnvironment("TRIGGER_BY_ASPIRE", "TRUE");
builder.AddProject<Projects.lab_outbound_webapi>("dotnet-outbound-sample")
       .WithEnvironment("TRIGGER_BY_ASPIRE", "TRUE");

builder.Build().Run();