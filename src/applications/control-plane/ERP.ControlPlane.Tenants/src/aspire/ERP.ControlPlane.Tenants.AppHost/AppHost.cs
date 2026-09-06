var builder = DistributedApplication.CreateBuilder(args);

var migrationWorkerProject = builder.AddProject<Projects.ERP_ControlPlane_Tenants_MigrationWorker>("erp-controlplane-tenants-migration-worker");

builder.AddProject<Projects.ERP_ControlPlane_Tenants_OutboxWorker>("erp-controlplane-tenants-outbox-worker")
    .WaitForCompletion(migrationWorkerProject);

builder.AddProject<Projects.ERP_ControlPlane_Tenants_WebApi>("erp-controlplane-tenants-webapi")
    .WaitForCompletion(migrationWorkerProject);

builder.Build().Run();