using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorReorderList;

public class ReorderJsInterop<TItem> : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ReorderJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorReorderList/ReorderJsInterop.js").AsTask());
    }

    public async ValueTask initEvents(DotNetObjectReference<Reorder<TItem>> dotNetInstance)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("initEvents", dotNetInstance);
    }

    public async ValueTask<int> getWidth(ElementReference el)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<int>("getWidth", el);
    }

    public async ValueTask<point> getPosition(ElementReference el)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<point>("getPosition", el);
    }

    public async ValueTask<point> getPoint(MouseEventArgs ev)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<point>("getPoint", ev);
    }

    public async ValueTask<clientRect> getClientRect(ElementReference el)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<clientRect>("getClientRect", el);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
