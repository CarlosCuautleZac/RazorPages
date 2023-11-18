const fijos = ["/", "/styles.css", "/manifest.json"];
const nombreCache = "cache1";
const apiUrl = "https://general.itesrc.net/api/pokemon/";

const canal = new BroadcastChannel("progreso");

//self.addEventListener("install", function (event) {
//    event.waitUntil(
//        caches.open(nombreCache).then(function (cache) {
//            // Almacenar recursos estáticos
//            return cache.addAll(fijos).then(function () {
//                // Hacer la solicitud a la API y almacenar recursos dinámicos
//                return fetch(apiUrl).then(function (response) {
//                    return response.json();
//                }).then(function (datos) {
//                    let iconos = datos.map(x => x.icono);
//                    return cache.addAll(iconos);
//                });
//            });
//        })
//    );
//});




self.addEventListener("install", function (event) {
    event.waitUntil(precargar());
});

async function precargar() {
    await descargarFijos();
    await precargarPokemon();
}

async function descargarFijos() {
    let cache = await caches.open(nombreCache);
    await cache.addAll(fijos);
}

async function precargarPokemon() {
    let cache = await caches.open(nombreCache);
    let response = await fetch(apiUrl);
    if (response.ok) {
        let datos = await response.json();
        let iconos = datos.map(x => x.icono);

        for (var i = 0; i < iconos.length; i++) {
            await cache.add(iconos[i]);
            canal.postMessage({ total: iconos.length, avance: i + 1 });
        }

        //await cache.addAll(iconos);

        await cache.put(new Request(apiUrl, JSON.stringify(datos)));
    }
}

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
