//https://integracion.itesrc.net/api/panes

var listaProductos;
const botonera = document.querySelector(".panes div");
const tabla = document.querySelector(".tabla tbody")

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
    const item = e.target.closest("button");
    if (item) {
        const id = item.dataset.id;

        var pan = listaProductos.find(x => x.id == id);
        if (pan) {

            //para encontrar la row con el dataset que contiene el id hay de dos maneras
            //manera 1
            //let row = tabla.childNodes.find(x => x.dataset.id == id);
            //manera 2
            let row = tabla.querySelector(`[data-id="${id}"]`)

            if (row) {//si lo encontro

                let cantidad = parseInt(row.cells[1].textContent) + 1;
                row.cells[1].textContent = cantidad;
                let subtotal = cantidad * pan.precio;
                row.cells[3].textContent = "$" + subtotal.toFixed(2);
            }
            else {//si no lo encuntre
                row = tabla.insertRow();
                row.dataset.id = pan.id;
                row.insertCell().textContent = pan.descripcion;
                row.insertCell().textContent = 1;
                row.insertCell().textContent = pan.precio.toFixed(2);
                row.insertCell().textContent = pan.precio.toFixed(2);
            }
        }
    }  
},true);

descargarProductos();

