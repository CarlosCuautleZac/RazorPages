const fijos = ["/", "/styles.css","/manifest.json"];
const nombreCache = "cache1"


self.addEventListener("install", function (event) {
    event.waitUntil(caches.open(nombreCache)
        .then(cache => {
        return cache.addAll(fijos);
    }));
});


self.addEventListener("activate", function () {

});

self.addEventListener("fetch", async function (event) {
    ////abrir la cache
    //let cache = await caches.open(nombreCache);
    ////verificar si la peticion esta en cache
    //let peticion = await cache.match(event);
    ////si esta en cache registro en la cache

    event.respondWith(caches.open(nombreCache).then(function (cache) {
        return cache.match(event.request).then(function (r) {
            if (r) {
                return r || fetch(event.request);
            }
        })
    }));
});
