
var _w = window,
    _b = document.body,
    _d = document.documentElement;

// get position of mouse/touch in relation to viewport 
export function getPoint(e) {
    var scrollX = Math.max(0, _w.pageXOffset || _d.scrollLeft || _b.scrollLeft || 0) - (_d.clientLeft || 0),
        scrollY = Math.max(0, _w.pageYOffset || _d.scrollTop || _b.scrollTop || 0) - (_d.clientTop || 0),
        pointX = e ? (Math.max(0, e.pageX || e.clientX || 0) - scrollX) : 0,
        pointY = e ? (Math.max(0, e.pageY || e.clientY || 0) - scrollY) : 0;

    return { x: pointX, y: pointY };
}

export function initEvents(dotNetInstance) {
    window.addEventListener("mousemove", (e) => dotNetInstance.invokeMethodAsync("onMove", getPoint(e)), true);
    window.addEventListener("touchmove", (e) => dotNetInstance.invokeMethodAsync("onMove", getPoint(e)), true);
    window.addEventListener("mouseup", (e) => dotNetInstance.invokeMethodAsync("onRelease", e), true);
    window.addEventListener("touchend", (e) => dotNetInstance.invokeMethodAsync("onRelease", e), true);
}

export function getWidth(e) {
    return e.offsetWidth;
}
export function getPosition(e) {
    return { x: e.offsetLeft, y: e.offsetTop };
}
export function getClientRect(e) {
    return e.getBoundingClientRect();
}
