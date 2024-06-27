using Billing.Management.Infra.CrossCutting.Extensions.DbConnectionConfig;
using Billing.Management.Infra.CrossCutting.Extensions.ExceptionFilter;
using Billing.Management.Infra.CrossCutting.Extensions.IoC;
using Billing.Management.Infra.CrossCutting.Extensions.SwaggerConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDependencyInjection();

builder.Services.AddSwagger();

builder.AddDbConnection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
