
var _w = window,
    _b = document.body,
    _d = document.documentElement;
var dotNetInstance = [];

// get position of mouse/touch in relation to viewport 
export function getPoint(e) {
    var scrollX = Math.max(0, _w.pageXOffset || _d.scrollLeft || _b.scrollLeft || 0) - (_d.clientLeft || 0),
        scrollY = Math.max(0, _w.pageYOffset || _d.scrollTop || _b.scrollTop || 0) - (_d.clientTop || 0),
        pointX = e ? (Math.max(0, e.pageX || e.clientX || 0) - scrollX) : 0,
        pointY = e ? (Math.max(0, e.pageY || e.clientY || 0) - scrollY) : 0;

    return { x: pointX, y: pointY };
}

// init events for each list (if the event exists it's ignored)
export function initEvents(dotNet) {
    dotNetInstance.push(dotNet);
    window.addEventListener("mousemove", onMove);
    window.addEventListener("touchmove", onMove);
    window.addEventListener("mouseup", onRelease);
    window.addEventListener("touchend", onRelease);
}

// only remove from the collection
export function removeEvents(dotNet) {
    dotNetInstance = dotNetInstance.filter(x => x._id !== dotNet._id);
}

// only invoke events form the collection
function onMove(e) {
    var point = getPoint(e);
    for (var i = 0; i < dotNetInstance.length; i++) {
        dotNetInstance[i].invokeMethodAsync("onMove", point);
    }
}

// only invoke events form the collection
export function onRelease(e) {
    for (var i = 0; i < dotNetInstance.length; i++) {
        dotNetInstance[i].invokeMethodAsync("onRelease", e);
    }
}

export function getWidth(e) {
    return e.offsetWidth;
}
export function getPosition(e) {
    return { x: e.offsetLeft, y: e.offsetTop };
}
export function getClientRect(e) {
    // blazor reset elements id in the middle of the query, so this invalidate the query and prevents errors
    if (e === null || e === undefined) return { width: -1 };
    return e.getBoundingClientRect();
}
