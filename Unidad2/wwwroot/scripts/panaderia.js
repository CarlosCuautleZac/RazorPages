//https://integracion.itesrc.net/api/panes

var listaProductos;

async function descargarProductos() {
    let response = await fetch("https://integracion.itesrc.net/api/panes");
    if (response.ok) {
        listaProductos = await response.json();

    }
}

descargarProductos();

