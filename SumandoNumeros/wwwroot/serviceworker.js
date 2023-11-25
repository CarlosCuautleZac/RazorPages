var cacheName = "estrategiasV2";

self.addEventListener("fetch", function (event) {
    if (event.request.url.includes("api/categorias")) {
        event.respondWith(staleWhileRevalidate(event));
    }
    else {
        event.respondWith(cacheFirst(event));
    }
});


//Tecnicas de cache
async function networkFirst(event) {
    //Recuperar desde internet
    //Si me responde lo guardo en cache
    //Regreso la peticion

    let cache = await caches.open(cacheName);
    try {
        let response = await fetch(event.request.url);

        if (response.ok) {
            cache.put(event.request, response.clone());
            return response;
        }
    }
    catch (error) {

        let resCache = await cache.match(event.request.url);
        if (resCache) {
            return resCache;
        }
        else {
            return new Response("No hay conexi�n a internet");
        }
    }
}

async function cacheFirst(event) {
    let cache = await caches.open(cacheName);
    let respuestaCache = await cache.match(event.request);
    if (respuestaCache) {
        return respuestaCache;
    }
    else {
        try {
            let response = await fetch(event.request);
            cache.put(event.request, response.clone());
            return response;
        }
        catch (error) {
            return new Response("No hay conexi�n a internet");
        }
    }
}


async function cacheOnly(event) {
    let cache = await caches.open(cacheName);
    let respuestaCache = cache.match(event.request);
    if (respuestaCache) {
        return respuestaCache;
    }
    return new Response("Necesita reinstalar la aplicaci�n");
}

self.addEventListener("install", function (event) {
    event.waitUntil(descargarDatos());
})

async function descargarDatos() {
    let datos = ["/", "/conectar", "styles.css", "manifest.json"];
    let cache = await caches.open(cacheName);
    cache.addAll(datos);
}


//Stale while revalidate
async function staleWhileRevalidate(event) {
    try {
        let cache = await caches.open(cacheName);
        let respuestaCache = cache.match(event.request);

        event.waitUntil(revalidate(event.request.url, cache));

        return respuestaCache || fetch(event.request);
    }
    catch(error) {
        return new Response("No hay conexio a internet");
    }
}

async function revalidate(url,cache) {
    try {
        //Revaludar: descargar y guardar en cache
        let response = await fetch(url);
        if (response.ok) {
            cache.put(url, response);
        }
    }
    catch (error) {
        //No hubo respuesta, no es necesario hacer nada
    }
}