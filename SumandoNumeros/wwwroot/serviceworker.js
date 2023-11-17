const fijos = ["/", "/styles.css", "/manifest.json"];
const nombreCache = "cache1";
const apiUrl = "https://general.itesrc.net/api/pokemon/";


self.addEventListener("install", function (event) {
    event.waitUntil(
        caches.open(nombreCache).then(function (cache) {
            // Almacenar recursos estáticos
            return cache.addAll(fijos).then(function () {
                // Hacer la solicitud a la API y almacenar recursos dinámicos
                return fetch(apiUrl).then(function (response) {
                    return response.json();
                }).then(function (datos) {
                    let iconos = datos.map(x => x.icono);
                    return cache.addAll(iconos);
                });
            });
        })
    );
});





self.addEventListener("activate", function () {

});

self.addEventListener("fetch", async function (event) {
    event.respondWith(caches.open(nombreCache).then(function (cache) {
        return cache.match(event.request).then(function (r) {
            if (r) {
                return r || fetch(event.request);
            }
        })
    }));
});
