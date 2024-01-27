using FluentValidation;
using MarkupProcessor;
using MarkupProcessor.Data;
using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using MarkupProcessor.Data.Repositories;
using MarkupProcessor.Initialization;
using MarkupProcessor.Validators;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddConfiguration();
builder.Services.AddLogging();
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddDataStore("MarkupProcessor", builder.Configuration);

builder.Services.AddScoped<IFlowDiagramInformationRepository, FlowDiagramInformationRepository>();
builder.Services.AddScoped<IMarkupRepository, MarkupRepository>();
builder.Services.AddScoped<IValidator<FlowDiagram>, FlowDiagramValidator>();
builder.Services.AddSingleton<IApplicationInitializer, MarkupProcessorInitializer>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.MapHealthChecks("/health");

app.UseCors("AllowAngularOrigins");

await app.Initialize();
app.Run();
