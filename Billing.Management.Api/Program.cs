using Billing.Management.Infra.CrossCutting.Extensions.DbConnectionConfig;
using Billing.Management.Infra.CrossCutting.Extensions.ExceptionHandlers;
using Billing.Management.Infra.CrossCutting.Extensions.IoC;
using Billing.Management.Infra.CrossCutting.Extensions.JwtAuthConfig;
using Billing.Management.Infra.CrossCutting.Extensions.SwaggerConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDependencyInjection();

builder.Services.AddExceptionHandlers();

builder.Services.AddSwagger();

builder.AddJwtAuthConfig();

builder.AddDbConnection();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerConfig();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
