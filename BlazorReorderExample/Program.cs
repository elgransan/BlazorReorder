using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorReorderExample;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<BlazorReorderList.ReorderService<ListItem>>();
builder.Services.AddScoped<BlazorReorderList.ReorderService<ItemList>>();

await builder.Build().RunAsync();

public record ListItem(string title, string url, string details);

public class ItemList
{
    public string title { get; set; }
    public string details { get; set; }
    public string domClass { get; set; }

    public ItemList(string a, string c)
    {
        title = a;
        domClass = "";
        details = c;
    }
}