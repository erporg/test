using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ProjectResource> migrationWorkerProject =
    builder.AddProject<ERP_ControlPlane_Tenants_MigrationWorker>("erp-controlplane-tenants-migration-worker");

builder.AddProject<ERP_ControlPlane_Tenants_OutboxWorker>("erp-controlplane-tenants-outbox-worker")
    .WaitForCompletion(migrationWorkerProject);

builder.AddProject<ERP_ControlPlane_Tenants_WebApi>("erp-controlplane-tenants-webapi")
    .WaitForCompletion(migrationWorkerProject);

builder.Build().Run();
