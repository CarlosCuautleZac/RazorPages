const fijos = ["/", "/styles.css"];
const nombreCache = "cache1"


self.addEventListener("install", async function () {
    //abrir el catch
    let cache = await caches.open(nombreCache);
    //guardar las peticiones
    await cache.addAll(fijos);
});


self.addEventListener("activate", function () {

});

self.addEventListener("fetch", async function (event) {
    //abrir la cache
    let cache = await caches.open(nombreCache);
    //verificar si la peticion esta en cache
    let peticion = await cache.match(event);
    //si esta en cache registro en la cache

    if (peticion) {
        event.responseWith(peticion);
        return;
    }
    //si no esta en cache regreso la petición de internet
    return await fetch(event);
});
