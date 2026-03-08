using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AVEquipmentManager.Web;
using AVEquipmentManager.Web.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure API base URL from appsettings
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5184";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<ChatService>();

// MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();
