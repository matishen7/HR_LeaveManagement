using HR_LeaveManagement.BlazorUI;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Services;
using HR_LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7114"));

builder.Services.AddScoped<ILeaveTypeServices, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationServices, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestServices, LeaveRequestService>();

await builder.Build().RunAsync();
