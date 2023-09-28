//https://integracion.itesrc.net/api/panes

var listaProductos;
const botonera = document.querySelector(".panes div");

async function descargarProductos() {
    let response = await fetch("https://integracion.itesrc.net/api/panes");
    if (response.ok) {
        listaProductos = await response.json();
        crearBotones();
    }
}

function crearBotones() {
    listaProductos.forEach(x => {
        //console.log(x);
        let btn = document.createElement("BUTTON");
        btn.dataset.id = x.id;
        let img = document.createElement("IMG")
        img.src = x.imagen;
        let txt = document.createTextNode("$" + x.precio.toFixed(2));
        btn.append(img, txt);


        botonera.append(btn);
    });
}

botonera.addEventListener("click", function (e) {
    
});

descargarProductos();

