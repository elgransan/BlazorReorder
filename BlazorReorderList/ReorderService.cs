using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorReorderList;

public class ReorderService<TItem> : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    public List<TItem>? originItems;
    public int elemIndex = -1;
    public TItem selected = default(TItem);
    public point elemClickPosition = new point(0, 0);
    public bool isDragging = false;

    public ReorderService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorReorderList/ReorderJsInterop.js").AsTask());
    }

    public void Set(List<TItem> list, TItem item, int index, point clickPoint)
    {
        isDragging = true;
        originItems = list;
        selected = item;
        elemIndex = index;
        elemClickPosition = clickPoint;
    }

    public void Reset()
    {
        isDragging = false;
        originItems = default(List<TItem>);
        selected = default(TItem);
        elemClickPosition = new point(0, 0);
    }

    public async ValueTask initEvents(DotNetObjectReference<Reorder<TItem>> dotNetInstance)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("initEvents", dotNetInstance);
    }

    public async ValueTask removeEvents(DotNetObjectReference<Reorder<TItem>> dotNetInstance)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("removeEvents", dotNetInstance);
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
