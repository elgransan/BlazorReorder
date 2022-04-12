using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorReorderList;

public class ReorderService<TItem> : IReorderService<TItem>
{
    public bool isDragging { get; set; } = false;
    public TItem? selected { get; set; } = default;
    public List<TItem>? originItems { get; set; }
    public int elemIndex { get; set; } = -1;
    public point elemClickPosition { get; set; } = new point(0, 0);

    private Lazy<Task<IJSObjectReference>> moduleTask;
    private bool _copy = false;

    public ReorderService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorReorderList/ReorderJsInterop.js").AsTask());
    }

    public ReorderService() { }

    public ReorderService<TItem> InitService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorReorderList/ReorderJsInterop.js").AsTask());

        return this;
    }

    public void Set(List<TItem> list, TItem item, int index, point clickPoint, bool copy)
    {
        isDragging = true;
        originItems = list;
        selected = item;
        elemIndex = index;
        elemClickPosition = clickPoint;
        _copy = copy;
    }

    // whe copy only the first time
    public bool isCopy()
    {
        var tempcopy = _copy;
        _copy = false;
        return tempcopy;
    }

    public void Reset()
    {
        isDragging = false;
        originItems = default;
        selected = default;
        elemClickPosition = new point(0, 0);
        _copy = false;
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
