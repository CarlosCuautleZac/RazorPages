var cacheName = "estrategiasV1";

self.addEventListener("fetch", function (event) {
    event.respondWith(networkFirst(event));
});

async function networkFirst(event) {
    //Recuperar desde internet
    //Si me responde lo guardo en cache
    //Regreso la peticion

    let cache = await caches.open(cacheName);
    let response = await fetch(event.request.url);

    if (response.ok) {
        cache.put(event.request.url, response.clone());
        return response;
    }

    return cache.match(event.request.url);
}
