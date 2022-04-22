using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorReorderList
{
    public interface IReorderService<TItem> : IAsyncDisposable
    {
        bool isDragging { get; set; }
        TItem? selected { get; set; }
        List<TItem>? originItems { get; set; }
        int elemIndex { get; set; }
        point elemClickPosition { get; set; }
        ValueTask<clientRect> getClientRect(ElementReference el);
        ValueTask<point> getPoint(double PageX, double PageY, double ClientX, double ClientY);
        ValueTask<point> getPosition(ElementReference el);
        ValueTask<int> getWidth(ElementReference el);
        ValueTask initEvents(DotNetObjectReference<Reorder<TItem>> dotNetInstance);
        ReorderService<TItem> InitService(IJSRuntime jsRuntime);
        bool isCopy();
        ValueTask removeEvents(DotNetObjectReference<Reorder<TItem>> dotNetInstance);
        void Reset();
        void Set(List<TItem> list, TItem item, int index, point clickPoint, bool copy);
    }
}